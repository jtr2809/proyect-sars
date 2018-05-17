using ProyectSARS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectSARS.Admin
{
    public partial class Ubicaciones : System.Web.UI.Page
    {
        //isntancia de objeto de clase UbicacionBLL para acceder a metodos.
        private UbicacionBLL ubll = new UbicacionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            //obtiene el nombre de usuario conectado y lo escribe en un control label lbUsuario
            lbUsuario.Text = HttpContext.Current.User.Identity.Name;
        }

        //metodo para evento Click de btnAgregar
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //obtiene valor de confirmBox desplegada en página.
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                //crea la ubicacion, despliega mensaje de confirmacion y se vuelven a enlazar las ubicaciones a la tabla GridView1 con los datos actualizados.
                ubll.CrearUbicacion(txtDescripcion.Text);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ubicación creada exitosamente')", true);
                GridView1.DataBind();
            }
            else
            {
                //despliega mensaje de alerta si ha hecho click sobre opcion "No"
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No sea han guardado los cambios')", true);
            }
            
        }

        //metodo para evento Click de boton btnBorrar
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            //obtiene valor de confirmBox desplegada en página.
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                //borra la ubicacion, despliega mensaje de confirmacion y vuelve a enlazar las ubicaciones a la tabla GridView1 con los datos actualizados
                ubll.EliminarUbicacion(Convert.ToInt32(GridView1.SelectedValue.ToString()));
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Se ha borrado la ubicación')", true);
                GridView1.DataBind();
            }
            else
            {
                //despliega mensaje de alerta si ha hecho click sobre opcion "No"
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Operación cancelada')", true);
            }
            
        }

        //metodo para cerrar sesion y redirigir  pagina de inicio
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