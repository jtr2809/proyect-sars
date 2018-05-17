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

namespace ProyectSARS.Coord
{
    public partial class Coordinador : System.Web.UI.Page
    {
        //instancia de objeto de clase RESERVA
        private RESERVA reserva;

        //metodo que se ejecuta cuando la pagina se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            //comprobacion: el usuario que inicia sesion tiene salas asignadas?
            if (new SalaBLL().ObtenerSalasDeUsuario(HttpContext.Current.User.Identity.Name).Any() == false)
            {
                //si usuario no tiene salas asignadas, se le quita el rol de Coordinador y se le redirige a la pagina de inicio
                Roles.RemoveUserFromRole(HttpContext.Current.User.Identity.Name, "Coordinador");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error. No tiene salas asignadas. Póngase en contacto con el administrador')", true);
                Response.Redirect("~/default.aspx");
            }

            //obtiene el nombre de usuario y lo escribe en un control label lbUsuario
            lbUsuario.Text = HttpContext.Current.User.Identity.Name;
            
            if (!this.IsPostBack)
            {
                BindData();
            }
        }

        //metodo para enlazar datos a GridView gvSolicitudes 
        private void BindData()
        {
            gvSolicitudes.DataSource = new ReservaBLL().ListaReservasCoordinador(lbUsuario.Text);
            gvSolicitudes.DataBind();
        }

        //metodo para cerrar sesion y redirigir a pagina de inicio
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // limpiar cookie dea autenticacion
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

        //metodo para evento Click del boton btnEditar
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //obtiene datos de reserva seleccionada en gridview gvSolicitudes
            reserva = new ReservaBLL().ObtenerDatosReserva(Convert.ToInt32(gvSolicitudes.SelectedValue));

            //asignar formato a fecha y horarios para correo de confirmacion o rechazo
            string fecha = string.Format("{0:d}", reserva.FECHA);
            string horaInicio = string.Format("{0:t}", reserva.HORA_INICIO);
            string horaTermino = string.Format("{0:t}", reserva.HORA_TERMINO);

            //comprobacion: si no se ha seleccionado una sala, despliega un mensaje de alerta con la leyenda descrita
            if (gvSolicitudes.SelectedValue == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe seleccionar una sala')", true);
            }
            else //caso contrario, ejecuta el siguiente bloque de código
            {
                //obtiene el valor de la opcion seleccionada del confirmBox desplegado
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Si")
                {
                    //se ejecuta el siguiente codigo si la opcion seleccionada del comboBox ddlEstado es 3 "APROBADA"
                    if (Convert.ToInt32(ddlEstado.SelectedValue) == 3)
                    {
                        //cambia el estado de la reserva seleccionada a "APROBADA"
                        new ReservaBLL().AprobarReserva(reserva.ID_RESERVA, Convert.ToInt32(ddlEstado.SelectedValue));

                        //envia un correo de confirmacion al usuario solicitante
                        new Correo().EnviarSolicitudAprobada(reserva.ID_USUARIO, txtNombreSala.Text, fecha, horaInicio, horaTermino);

                        //reinicia el formulario de la página a sus valores por defecto
                        txtUsuario.Text = "";
                        txtNombreSala.Text = "";
                        txtMotivoRechazo.Text = "";
                        lbFecha.Text = "Fecha: ";
                        lbHoraInicio.Text = "Hora Inicio: ";
                        lbHoraTermino.Text = "Hora Término: ";
                        gvSolicitudes.SelectedIndex = -1;
                        ddlEstado.SelectedIndex = -1;

                    }

                    //se ejecuta el siguiente codigo si la opcion seleccionada del comboBox ddlEstado es 2 "RRECHAZADA"
                    if (Convert.ToInt32(ddlEstado.SelectedValue) == 2)
                    {
                        //cambia el estado de la reserva seleccionada a "RECHAZADA"
                        new ReservaBLL().RechazarReserva(reserva.ID_RESERVA, Convert.ToInt32(ddlEstado.SelectedValue), txtMotivoRechazo.Text);

                        //envia correo a usuario detallando motivo de rechazo
                        new Correo().EnviarSolicitudRechazada(reserva.ID_USUARIO, txtMotivoRechazo.Text);

                        //reinicia el formulario de la página a sus valores por defecto
                        txtUsuario.Text = "";
                        txtNombreSala.Text = "";
                        txtMotivoRechazo.Text = "";
                        lbFecha.Text = "Fecha: ";
                        lbHoraInicio.Text = "Hora Inicio: ";
                        lbHoraTermino.Text = "Hora Término: ";
                        gvSolicitudes.SelectedIndex = -1;
                        ddlEstado.SelectedIndex = -1;
                    }

                    //despliega mensaje de alerta con el mensaje descrito
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Datos guardados exitosamente')", true);
                }
                else
                {
                    //despliega mensaje de alerta con el mensaje descrito
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se han guardado los cambios')", true);
                }

                //invoca metodo para re-enlazar datos de reserva desde base de datos
                BindData();
            }
        }

        //metodo para evento Cambio de Indice seleccionado del control gridview gvSolicitudes
        protected void GvSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //obtiene datos de reserva seleccionada
            reserva = new ReservaBLL().ObtenerDatosReserva(Convert.ToInt32(gvSolicitudes.SelectedValue));

            //escribe en el formulario los datos de reserva seleccionada
            txtUsuario.Text = reserva.ID_USUARIO;
            txtNombreSala.Text = reserva.SALA.NOMBRESALA;
            lbFecha.Text = "Fecha: "+ string.Format("{0:dd-MM-yyyy}", reserva.FECHA);
            lbHoraInicio.Text = "Hora Inicio: " + string.Format("{0:HH:mm}", reserva.HORA_INICIO);
            lbHoraTermino.Text = "Hora Término: " + string.Format("{0:HH:mm}", reserva.HORA_TERMINO);
            ddlEstado.SelectedValue = reserva.IDESTADO.ToString();
            txtMotivoRechazo.Text = reserva.MOTIVO;
            
        }

        //metodo para evento Cambio de Número de Paginacion de control gridview gvSolicitudes
        protected void GvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //establece el nuevo número de página
            gvSolicitudes.PageIndex = e.NewPageIndex;

            //establece el origen de los datos del control gridview y establece enlace
            gvSolicitudes.DataSource = new ReservaBLL().ListaReservasCoordinador(lbUsuario.Text);
            gvSolicitudes.DataBind();
        }

        //metodo para evento de cambio del valor seleccionado de comboBox ddlEstado en página Coordinador
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlEstado.SelectedValue)
            {
                //si el valor es 1 "Pendiente" 
                case "1":

                    //invisibiliza los controles textbox txtMotivoRechazo y label lbMotivo y deshabilita validador de campo RequiredFieldValidator1  
                    txtMotivoRechazo.Visible = false;
                    lbMotivo.Visible = false;
                    RequiredFieldValidator1.Enabled = false;
                    break;

                //Si el valor es 2 "Rechazada"
                case "2":

                    //visibiliza los controles textbox txtMotivoRechazo y label lbMotivo y habilita validador de campo RequiredFieldValidator1  
                    txtMotivoRechazo.Visible = true;
                    lbMotivo.Visible = true;
                    RequiredFieldValidator1.Enabled = true;
                    break;

                //Si el valor es 3 "Aprobada"
                case "3":

                    //invisibiliza los controles textbox txtMotivoRechazo y label lbMotivo y deshabilita validador de campo RequiredFieldValidator1  
                    txtMotivoRechazo.Visible = false;
                    lbMotivo.Visible = false;
                    RequiredFieldValidator1.Enabled = false;
                    break;
            }

        }

        //metodo que se ejecuta cuando se enlaza un origen de datos a control gridview gvSolicitudes
        protected void gvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //obtiene la fecha del dia presente
            DateTime fechaHoy = DateTime.Now;

            //comprobacion: es el tipo de fila obtenido una fila enlazada a datos?
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                //obtiene una vista del item de datos de la fila
                var dr = e.Row.DataItem as DataRowView;

                //si el campo "horaInicio" que contiene fecha y hora de inicio de la reserva es menor a la fecha presente ejecuta el siguiente codigo
                if (DateTime.Parse(dr["horaInicio"].ToString()) < fechaHoy)
                {
                    //obtiene el contro tipo LinkButton de la fila y cambia el texto a "Expirada" y lo deshabilita
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Text = "Expirada";
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Enabled = false;
                }
            }
        }
    }
}                               