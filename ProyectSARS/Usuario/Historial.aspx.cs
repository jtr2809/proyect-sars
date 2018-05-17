using ProyectSARS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectSARS.Usuario
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //obtiene el nombre del usuario conectado y lo coloca en un label
            lbUsuario.Text = HttpContext.Current.User.Identity.Name;

            if (!IsPostBack)
            {
                BindData();
            }       
        }

        //método para enlazar datos de historial de reserva de usuario conectado a gridview
        private void BindData()
        {
            GridView1.DataSource = new ReservaBLL().ListaReservasUsuario(lbUsuario.Text);
            GridView1.DataBind();
        }

        //evento para cerrar sesión
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // limpiar cookie de la autenticacion
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // limpiar coockie de la sesion 
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }

        //método para la paginación del gridview
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //establece el numero de pág de la tabla 
            GridView1.PageIndex = e.NewPageIndex;
            BindData();

        }

        //método para el evento borrar de botón "Borrar"
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                //obtiene valor de confirmBox desde pág web
                string confirmValue = Request.Form["confirm_value"];

                //examina valor de confirmBox obtenido previamente
                switch (confirmValue)
                {
                    //si el valor es "Si", ejecuta método para deshabilitar sala, envía alerta confirmando guardado y vuelve a cargar la tabla con método para enlazar datos a tabla.
                    case "Si":

                        new ReservaBLL().DeshabilitarReserva(Convert.ToInt32(GridView1.SelectedValue));
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Reserva borrada correctamente')", true);
                        BindData();
                        break;

                    //Si el valor es "No", envia mensaje de alerta "Sin cambios"
                    case "No":
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sin cambios')", true);
                        break;
                }

            }
            catch (Exception)
            {
                //en caso de error porque no existe la sala, envia mensaje de alerta 
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: La reserva ya está borrada.')", true);
            }
        }

        //método para evento de enlace de datos a tabla 
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //obtiene fecha de hoy
            DateTime fechaHoy = DateTime.Now;

            //bloque if si comprueba que fila que disparó evento es de tipo que puede tener datos enlazados
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dr = e.Row.DataItem as DataRowView;

                //convierte texto de fecha en tipo DateTime y lo compara con la fecha de hoy || examina la cadena de columna "activo"
                //si se da una de las condiciones, deshabilita el boton "Elegir" correspondiente a la fila
                if (DateTime.Parse(dr["horaInicio"].ToString()) < fechaHoy || dr["estado"].ToString() == "APROBADA" || dr["activo"].ToString() == "False")
                {
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Text = "No Disponible";
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Enabled = false;
                }
            }
        }
    }
}