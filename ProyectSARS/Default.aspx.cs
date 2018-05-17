using ProyectSARS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectSARS
{
    public partial class _default : System.Web.UI.Page
    {
        //instancia de las clases ReservaBLL y SalaBLL para acceder a metodos
        private ReservaBLL rbll = new ReservaBLL();
        private SalaBLL sbll = new SalaBLL();

        //metodo que se ejecuta al momento de cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            //obtiene el nombre de usuario y lo coloca en un control label
            lbUsuario.Text = HttpContext.Current.User.Identity.Name.ToString();

            //coloca el texto en el control label lbGvError
            lbGvError.Text = "* Seleccione una Sala";

            //obtiene el dia de la maquina host y ocupa el valor para colocar el dia de inicio del control calendario
            DayPilotCalendar1.StartDate = DateTime.Today;

            //bloque if que se ejecuta si el usuario conectado tiene salas a su 
            if (sbll.ObtenerSalasDeUsuario(lbUsuario.Text).Any() == true && Roles.IsUserInRole(lbUsuario.Text,"Coordinador") == false)
            {
                Roles.AddUserToRole(lbUsuario.Text, "Coordinador");
            }
        }

        //método para evento databound del control DropDownList1 para insertar un elemento de valor -1 y texto "- Seleccione -" en el el conjunto de objetos del control
        protected void MyListDataBound(object sender, EventArgs e)
        {
            DropDownList1.Items.Insert(0, new ListItem("- Seleccione -", "-1"));
        }

        //método para evento Click del boton btnVerSolicitudes para redirigir al usuario a la página VerSolicitudes.aspx
        protected void btnVerSolicitudes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario/VerSolicitudes.aspx");
        }

        //método para evento Click del boton btnCrear
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            //variable bool para chequeo de formulario
            bool ok = true;

            //variable string para mensajes de error
            string msg = "";

            //convierte los string de los campos fecha, hora inicio y hora término en tipos DateTime
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            DateTime horaInicio = fecha.Add(Convert.ToDateTime(txtHoraInicio.Text).TimeOfDay);
            DateTime horaTermino = fecha.Add(Convert.ToDateTime(txtHoraTermino.Text).TimeOfDay);

            //Buscar conflicto - hora de inicio es mayor a hora de termino
            if (TimeSpan.Compare(horaInicio.TimeOfDay, horaTermino.TimeOfDay) >= 0)
            {
                ok = false;
                msg = msg + " * La hora de inicio no puede ser mayor a la hora de término";
            }

            //Buscar conflicto - sala pedida con anticipación

            try
            {
                if (rbll.BuscarConflictoHorarios(Convert.ToInt32(GridView1.SelectedValue.ToString()), fecha, horaInicio, horaTermino) == true)
                {
                    ok = false;
                    msg = msg + " * Horario no disponible. Porfavor seleccione otro";
                }
            }
            catch (Exception)
            {

            }


            //Buscar conflicto - fecha de solicitud anterior a dia presente
            if (DateTime.Compare(fecha.Date, DateTime.Now.Date) < 0)
            {
                ok = false;
                msg = msg + " * La fecha de reserva no puede ser anterior a hoy";
            }

            //Buscar conflicto - hora de solicitud anterior a hora presente
            if (DateTime.Now.Date == fecha.Date && TimeSpan.Compare(horaInicio.TimeOfDay, DateTime.Now.TimeOfDay) <= 0)
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

            //Crear Reserva
            string confirmValue = Request.Form["confirm_value"];
            if (ok == true)
            {
                switch (confirmValue)
                {
                    case "Si":
                        //ejecuta método para crear reserva
                        rbll.CrearReserva(lbUsuario.Text,
                                           Convert.ToInt32(GridView1.SelectedValue.ToString()),
                                           fecha,
                                           horaInicio,
                                           horaTermino);

                        //mensaje confirmacion en cliente
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('¡Reserva creada con éxito!')", true);

                        string fechaTxt = string.Format("{0:d}", fecha);
                        string horaInicioTxt = string.Format("{0:t}", horaInicio);
                        string horaTerminoTxt = string.Format("{0:t}", horaTermino);

                        //Enviar Confirmacion a usuario y coonfirmacion con datos de reserva
                        new Correo().EnviarConfirmacionUsuario(lbUsuario.Text, new SalaBLL().GetSALAs(Convert.ToInt32(GridView1.SelectedValue.ToString())).NOMBRESALA, fechaTxt, horaInicioTxt, horaTerminoTxt);
                        new Correo().EnviarConfirmacionCoordinador(new SalaBLL().GetSALAs(Convert.ToInt32(GridView1.SelectedValue.ToString())).IDCOORD, lbUsuario.Text, new SalaBLL().GetSALAs(Convert.ToInt32(GridView1.SelectedValue.ToString())).NOMBRESALA + " - " + new SalaBLL().GetSALAs(Convert.ToInt32(GridView1.SelectedValue.ToString())).UBICACION.DESCRIPCION, fechaTxt, horaInicioTxt, horaTerminoTxt);

                        //Reiniciar el formulario
                        GridView1.SelectedIndex = -1;
                        txtFecha.Text = "";
                        txtHoraInicio.Text = "";
                        txtHoraTermino.Text = "";
                        lbGvError.Text = "";
                        break;

                    case "No":
                        //despliega mensaje de alerta con mensaje descrito
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Operación cancelada')", true);
                        break;
                }
            }

            //ejecuta este codigo si la variable de confirmacion es false
            if (ok == false)
            {
                switch (confirmValue)
                {
                    case "Si":
                        //despliega mensaje de alerta con mensaje descrito
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error en la solicitud. Porfavor revise el formulario')" , true);
                        //escribe mensajes de errores obtenidos en el formulario en contro label lbGvError
                        lbGvError.Text = msg;
                        break;

                    case "No":
                        //despliega mensaje de alerta con mensaje descrito
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Operación cancelada')", true);
                        //escribe mensajes de errores obtenidos en el formulario en contro label lbGvError
                        lbGvError.Text = msg;
                        break;
                }
            }
        }

        //método para cerrar sesión
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // limpiar cookie de autenticación
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // limpiar cookie de sesión
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }

        //método para evento de cambio de indice seleccionado de control gridview GridView1
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reinicia el texto de control label lbGvError
            lbGvError.Text = "";
        }


    }
}