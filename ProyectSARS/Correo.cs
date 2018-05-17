using System.Net.Mail;

namespace ProyectSARS
{
    public class Correo
    {

        public void EnviarConfirmacionUsuario(string usuario, string sala, string fecha, string horaInicio, string horaTermino)
        {

            MailMessage mail = new MailMessage("sars@dominio.com", usuario + "@dominio.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "**.***.***.**";
            mail.Subject = "Nueva Reserva de Sala";
            mail.Body = "Estimado Usuario: Su reserva ha sido creada:\nSala: " + sala + "\nFecha: " + fecha + "\nHora Inicio: " +  horaInicio + "\nHora Término: " +  horaTermino + "\nPorfavor, espere confirmación del coordinador de sala.\nSaludos Cordiales.\n\n";
            client.Send(mail);
        }


        public void EnviarConfirmacionCoordinador(string coordinador, string usuario, string sala, string fecha, string horaInicio, string horaTermino)
        {

            fecha = string.Format("{0:d}", fecha);
            horaInicio = string.Format("{0:t}", horaInicio);
            horaTermino = string.Format("{0:t}", horaTermino);

            MailMessage mail = new MailMessage("sars@dominio.com", coordinador + "@dominio.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "**.**.**.***";
            mail.Subject = "Nueva Reserva de Sala";
            mail.Body = "Se ha reservado la siguiente sala:\n\nSala: " + sala +"\n\nUsuario: "+usuario +"\n\nFecha: " + fecha + "\n\nHora Inicio: " + horaInicio + "\n\nHora Término: " + horaTermino + "\n\nPorfavor conteste la solicitud\n\n\nSaludos Cordiales.\n\n";
            client.Send(mail);
        }


        public void EnviarSolicitudAprobada(string usuario, string sala, string fecha, string horaInicio, string horaTermino)
        {

            MailMessage mail = new MailMessage("sars@dominio.com", usuario + "@dominio.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "**.**.**.***";
            mail.Subject = "Solicitud de Reserva Aprobada";
            mail.Body = "Estimado Usuario: Su reserva ha sido Aprobada:\n\nSala: " + sala + "\n\nFecha: " + fecha + "\n\nHora Inicio: " + horaInicio + "\n\nHora Término: " + horaTermino + "\n\nSaludos Cordiales.\n\n";
            client.Send(mail);
        }


        public void EnviarSolicitudRechazada(string usuario, string motivo)
        {
            MailMessage mail = new MailMessage("sars@dominio.com", usuario + "@dominio.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "**.**.**.**";
            mail.Subject = "Solicitud de Reserva de Sala rechazada";
            mail.Body = "Estimado usuario:\n\nSu solicitud fue rechazada.\n\nMotivo: " + motivo+"\n\n\nSaludos Cordiales\n\n";
            client.Send(mail);
        }

        public void EnviarAvisoEdicion(string coordinador, string usuario, string sala, string fecha, string horaInicio, string horaTermino, string fechaOLD, string horaInicioOLD, string horaTerminoOLD)
        {

            MailMessage mail = new MailMessage("sars@dominio.com", coordinador + "@dominio.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "**.**.**.**";
            mail.Subject = "Solicitud de reserva de Sala cambiada";
            mail.Body = "Se ha reservado la siguiente sala:\n\nSala: " + sala + "\n\nUsuario: " + usuario + "\n\nFecha Antigua: " + fechaOLD + "\n\nFecha Nueva: "+fecha+"\n\nHora Inicio Antigua: " + horaInicioOLD + "\n\nHora Inicio Nueva: "+horaInicio+"\n\nHora Término Antigua: " + horaTerminoOLD + "\n\nHora Término Nueva: "+horaTermino+"\n\nPorfavor conteste la solicitud\n\n\nSaludos Cordiales.\n\n";
            client.Send(mail);
        }
    }
}