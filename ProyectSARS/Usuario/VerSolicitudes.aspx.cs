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
    public partial class VerSolicitudes : System.Web.UI.Page
    {
        //instancia de objeto de clase ReservaBLL
        private ReservaBLL rbb = new ReservaBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            lbUsuario.Text = HttpContext.Current.User.Identity.Name;

            if (!IsPostBack)
            {
                BindData();
            }
        }
        //metodo para enlazar datos de reserva en tabla de datos
        private void BindData()
        {
            GridView1.DataSource = new ReservaBLL().ListaReservasUsuarioActivas(lbUsuario.Text);
            GridView1.DataBind();
        }

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

        //metodo para paginacion de tabla de datos
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            BindData();

        }

        //metodo para evento seleccion de fila
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //instancia de objeto de clase RESERVA
            //obtiene datos de reserva segun su n° (columna IdReserva en la tabla)
            RESERVA res = rbb.ObtenerDatosReserva(Convert.ToInt32(GridView1.SelectedValue));
            
            //escribe los datos fecha, horaInicio y horatermino en los campos del formulario de la página
            txtNombreSala.Text = res.SALA.NOMBRESALA;
            txtFecha.Text = res.FECHA.ToString("yyyy-MM-dd");
            txtHoraInicio.Text = res.HORA_INICIO.TimeOfDay.ToString();
            txtHoraTermino.Text = res.HORA_TERMINO.TimeOfDay.ToString();

        }

        //metodo para evento Click para boton btnEditar
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //variable bool para chequeo de datos de formulario
            bool ok = true;

            //variable string para mensajes de error
            string msg = "";


            //instancia de objeto de clase RESERVA, obtenida segun id seleccionada en tabla
            RESERVA res = rbb.ObtenerDatosReserva(Convert.ToInt32(GridView1.SelectedValue));
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            DateTime horaInicio = fecha.Add(Convert.ToDateTime(txtHoraInicio.Text).TimeOfDay);
            DateTime horaTermino = fecha.Add(Convert.ToDateTime(txtHoraTermino.Text).TimeOfDay);

            //obtiene id de sala de la reserva
            int idSalaReserva = res.IDSALA.Value;

            //conversion de datos fecha, horaInicio y horaTermino, obtenidos de la reserva antes de editar, de tipo DateTime a tipo string para envio de correo a coordinador
            string fechaOLD = String.Format("{0:d}", res.FECHA);
            string horaInicioOLD = String.Format("{0:t}", res.HORA_INICIO);
            string horaTerminoOLD = String.Format("{0:t}", res.HORA_TERMINO);

            //conversion de datos fecha, horaInicio y horaTermino, obtenidos de la reserva despues de editar, de tipo DateTime a tipo string para envio de correo a coordinador
            string fechaTxt = string.Format("{0:d}", fecha);
            string horaInicioTxt = string.Format("{0:t}", horaInicio);
            string horaTerminoTxt = string.Format("{0:t}", horaTermino);

            //comprobacion: hora de inicio mayor a hora de término? ej hora inicio 1pm hora termino 12am del mismo dia
            if (TimeSpan.Compare(horaInicio.TimeOfDay, horaTermino.TimeOfDay) >= 0)
            {
                //if true=
                ok = false;
                msg = msg + " * La hora de inicio no puede ser mayor a la hora de término";
            }

            //comprobacion: esta intentando pedir en una hora que ya pasó? 
            if (fecha.Date == DateTime.Now.Date && TimeSpan.Compare(horaInicio.TimeOfDay, DateTime.Now.TimeOfDay) <= 0)
            {
                ok = false;
                msg = msg + " * No se permite reservar en horas pasadas";
            }

            //Buscar conflicto - sala no seleccionada
            if (GridView1.SelectedValue == null)
            {
                ok = false;
                msg = msg + " * Seleccione una sala";
            }

            //Buscar conflicto: esta intentando pedir reserva para dias pasados?
            if (fecha.Date < DateTime.Now.Date)
            {
                ok = false;
                msg = msg + " * No se puede pedir para dias anteriores";
            }

            //intentar ejecutar el siguiente codigo esperando un error
            try
            {
                //si el nuevo horario y fecha tienen un conflicto con otra solicitud hecha, ejecuta el siguiente codigo
                if (rbb.BuscarConflictoHorarios(idSalaReserva, fecha, horaInicio, horaTermino) == true)
                {
                    //cambia variable de chequeo a false y despliega un mensaje de error
                    ok = false;
                    msg = msg + " * Horario no disponible. Porfavor seleccione otro";
                }
            }
            catch (Exception)
            {

            }

            //si la variable de confirmacion es true, ejecuta el siguiente código
            if (ok == true)
            {
                //obtiene el valor de la opcion seleccionada del confirmBox desplegado en la página VerSolicitudes
                string confirm_value = Request.Form["confirm_value"];
                switch (confirm_value)
                {
                    //si el valor de la opcion seleccionada del confirmBox es si:
                    case "Si":

                        //intentar ejecutar el siguiente código:
                        try
                        {
                            //ejecuta el método para editar reserva con los siguientes parámetros: id de la reserva, id de la sala, fecha de reserva, hora de inicio y hora de termino de la reserva 
                            rbb.EditarReserva(res.ID_RESERVA, res.ID_USUARIO, res.IDSALA.Value, fecha, horaInicio, horaTermino);

                            //despliega mensaje de confirmacion a usuario en la página
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Reserva editada correctamente')", true);

                            //reinicia el formulario de la página a sus valores por defecto
                            txtFecha.Text = "";
                            txtHoraInicio.Text = "";
                            txtHoraTermino.Text = "";
                            txtNombreSala.Text = "";
                            GridView1.SelectedIndex = -1;

                            //envia al coordinador un correo de aviso de modificacion de reserva
                            new Correo().EnviarAvisoEdicion(res.SALA.IDCOORD, res.ID_USUARIO, txtNombreSala.Text, fechaTxt, horaInicioTxt, horaTerminoTxt,fechaOLD,horaInicioOLD,horaTerminoOLD);

                            //vuelve a enlazar los datos de las reservas con la informacion actualizada
                            BindData();
                        }
                        catch (Exception)
                        {
                            //envia mensaje de error con la leyenda descrita
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: no se pudo guardar los cambios')", true);
                        }
                        break;

                    //si el valor de la opcion seleccionada del confirmBox es si:
                    case "No":

                        //despliega la alerta con el mensaje descrito
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se guardaron cambios')", true);
                        break;
                }
            }
            //si hay error, despliega una alerta con el error especificado en variable msg
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+msg+"')", true);
            }
        }

        //método para evento de enlace de datos en gridview GridView1
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //obtiene fecha de hoy
            DateTime fechaHoy = DateTime.Now;

            //bloque if si comprueba que fila que disparó evento es de tipo que puede tener datos enlazados
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dr = e.Row.DataItem as DataRowView;

                //convierte texto de fecha en tipo DateTime y lo compara con la fecha de hoy || examina la cadena de strings de columna "activo"
                //si se da una de las condiciones (fecha de reserva anterior al dia presente o estado de solicitud = "APROBADA"), deshabilita el boton "Elegir" correspondiente a la fila
                if (DateTime.Parse(dr["fecha"].ToString()) < fechaHoy || dr["estado"].ToString() == "APROBADA")
                {
                    //busca el control LinkButton1 en la fila y lo deshabilita y le cambia el texto a "No Disponible"
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Text = "No Disponible";
                    ((LinkButton)e.Row.FindControl("LinkButton1")).Enabled = false;
                }
            }
        }
    }
}