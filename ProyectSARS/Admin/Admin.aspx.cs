using ProyectSARS.BLL;
using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace ProyectSARS.Admin
{
    public partial class Admin : System.Web.UI.Page
    {
        //instancia de objeto de clase SalaBLL para metodo de crear sala
        private SalaBLL sbll = new SalaBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = HttpContext.Current.User.Identity;
            lbUsuario.Text = currentUser.Name;
        }

        //metodo para evento click de boton "Agregar Sala"
        protected void txtAgregarSala_Click(object sender, EventArgs e)
        {
            //obtiene valor de confirmBox desde la página web
            string confirmValue = Request.Form["confirm_value"];

            //si el valor es "Si", dispara metodo para guardar sala
            if (confirmValue == "Si")
            {
                sbll.CrearSala(
                txtCoordinador.Text,
                Convert.ToInt32(ddlUbicaciones.SelectedValue),
                txtNombreSala.Text, Convert.ToInt32(txtCapacidad.Text),
                Convert.ToInt32(txtPiso.Text),
                txtEquipamiento.Text,
                chkNotePC.Checked,
                chkMonitorPantalla.Checked,
                chkVC.Checked);

                //si el usuario ingresado no está en la tabla "aspnet_UsersInRoles", lo añade
                if (Roles.IsUserInRole(txtCoordinador.Text, "Coordinador") == false)
                {
                    Roles.AddUserToRole(txtCoordinador.Text, "Coordinador");
                }

                txtCoordinador.Text = "";
                txtNombreSala.Text = "";
                txtCapacidad.Text = "";
                txtPiso.Text = "";
                txtEquipamiento.Text = "";

                //dispara evento de confirmbox confirmando sala creada
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sala creada correctamente')", true);
            }
            else
            {
                //dispara evento de confirmbox señalando que ningun cambio se ha guardado
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se ha guardado ningún cambio')", true);
            }

        }

        //metodo para evento "Click" de boton "Editar Sala"
        protected void txtEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/EditarSala.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // limpiar cookie de autenticación
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

        //metodo para evento cambio de texto
        protected void txtCoordinador_TextChanged(object sender, EventArgs e)
        {

            //si hay un punto al principio al inicio o al final de la cadena string en textbox, aparece una X a la derecha, sino, coloca un check
            if (txtCoordinador.Text.IndexOf(".") == 0 || txtCoordinador.Text.IndexOf(".")>=txtCoordinador.Text.Length-1)
            {
                lbError.Text = "X";
                lbError.CssClass = "text-danger";
            }
            else
            {
                lbError.Text = "\u221A";
                lbError.CssClass = "text-danger";
            }
        }

    }
}