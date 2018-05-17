using ProyectSARS.BLL;
using System;
using System.DirectoryServices.AccountManagement;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace ProyectSARS.Admin
{
    public partial class EditarSala : System.Web.UI.Page
    {
        //instancias de clases SALA y SalaBLL para usar más adelante
        private SALA sala;
        SalaBLL sbll = new SalaBLL();
        //private PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "cultura.gob.cl");

        protected void Page_Load(object sender, EventArgs e)
        {
            //obtiene nombre de usuario conectado y lo escribe en control lbUsuario en la página
            lbUsuario.Text = HttpContext.Current.User.Identity.Name;
        }

        //metodo para evento editar del boton btnGuardar
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //bloque para comprobar que sala ha sido seleccionada
            try
            {
                //obtiene id de la sala seleccionada
                int id = Convert.ToInt32(GridView1.SelectedValue.ToString());

                //obtiene valor de confirmBox desplegado en pág de inicio
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Si")
                {
                    //comprueba que la sala existe
                    if (sbll.BuscarSalaExistente(id) == true)
                    {
                        //variable true - false para establecer disponibilidad
                        bool disponible;

                        //comprobacion: valor seleccionado en combobox ddlEstado es 0 o 1?
                        if (ddlEstado.SelectedValue == "1")
                        {
                            disponible = true;
                        }
                        else
                        {
                            disponible = false;
                        }

                        //invoca metodo para guardar los cambios de datos de sala
                        sbll.EditarSala(
                            id,
                            txtNombreSala.Text,
                            txtEquipamiento.Text,
                            txtCoordinador.Text,
                            Convert.ToInt32(txtCapacidad.Text),
                            Convert.ToInt32(txtPiso.Text),
                            chkMNTRPantalla.Checked,
                            chkNote.Checked,
                            chkVC.Checked,
                            disponible);

                        //metodo para volver a enlazar los nuevos datos de salas en tablas
                        GridView1.DataBind();

                        //comprueba si usuario designado (escrito en el textbox txtCoordinador) tiene el rol de coordinador
                        if (Roles.IsUserInRole(txtCoordinador.Text, "Coordinador") == false)
                        {
                            Roles.AddUserToRole(txtCoordinador.Text, "Coordinador");
                        }

                        //reinicio de los campos del formulario a sus valores por defecto
                        GridView1.SelectedIndex = -1;
                        txtNombreSala.Text = "";
                        txtEquipamiento.Text = "";
                        txtCoordinador.Text = "";
                        txtPiso.Text = "";
                        txtCapacidad.Text = "";
                        ddlUbicaciones.SelectedIndex = -1;
                        ddlEstado.SelectedIndex = 0;


                        //despliega mensaje de confirmacion
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Datos guardados correctamente')", true);

                    }
                    else
                    {
                        //despliega mensaje de alerta cuando no encuentra la sala
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('¡Ha ocurrido un problema!')", true);
                    }
                }
                else
                {
                    //despliega mensaje de alerta cuando el usuario le a dado a la opcion "No" del confirmBox
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Los cambios no se han guardado')", true);
                }
            }
            catch (Exception)
            {
                //despliega mensaje de alerta cuando no hay sala seleccionada
                Response.Write("<script>alert('Error: Debe escoger una sala de la tabla');</script>");

            }

        }

        //metodo para evento de textbox txtCoordinador - cambia el texto ingresado
        protected void txtCoordinador_TextChanged(object sender, EventArgs e)
        {
            //comprobación: si en la cadena de texto ingresada hay un punto al inicio o al fin de la cadena, arroja true
            if (txtCoordinador.Text.IndexOf(".") == 0 || txtCoordinador.Text.IndexOf(".") >= txtCoordinador.Text.Length - 1)
            {
                lbOk.Text = "X";
                lbOk.CssClass = "text-danger";
            }
            else
            {
                lbOk.Text = "\u221A";
                lbOk.CssClass = "text-danger";
            }
        }

    
        //metodo para evento de tabla GridView1 - Cambio de indice seleccionado 
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //instancia de objeto de clase SALA, obtienendo datos de sala segun id de valor seleccionado en tabla (propiedad DataKeyNames del gridview - atributo IdSala)
            sala = new SalaBLL().GetSALAs(Convert.ToInt32(GridView1.SelectedValue.ToString()));

            //rellena los campos del formulario con los datos de la sala obtenida anteriormente 
            txtNombreSala.Text = sala.NOMBRESALA;
            txtEquipamiento.Text = sala.EQUIPAMIENTO;
            txtCoordinador.Text = sala.IDCOORD;
            txtPiso.Text = sala.PISO.ToString();
            txtCapacidad.Text = sala.CAPACIDAD.ToString();
            chkMNTRPantalla.Checked = sala.POSEE_MONITOR;
            chkNote.Checked = sala.POSEE_NOTE;
            chkVC.Checked = sala.POSEE_VC;

            //establece el valor seleccionado del combobox de acuerdo al dato DISPONIBILIDAD del objeto sala
            if (sala.DISPONIBILIDAD == true)
            {
                ddlEstado.SelectedIndex = 0;
            }
            else
            {
                ddlEstado.SelectedIndex = 1;
            }
        }
        
        //metodo para cerrar sesion y volver a la pagina de login
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // limpiar cookie de autenticacion
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // limpiar cookie de sesion
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }
    }
    
}