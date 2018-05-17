using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Itenso.TimePeriod;
using System.Data;

namespace ProyectSARS.BLL
{
    [DataObject]
    public class ReservaBLL
    {
        //establece el conjunto de entidades de la base de datos
        private Entidades1 entidades = new Entidades1();

        //Obtener lista de reservas por usuario
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable ListaReservasUsuario(string id_usuario)
        {

            return (from e in entidades.RESERVA
                    where e.ID_USUARIO == id_usuario
                    select new
                    {
                        idReserva = e.ID_RESERVA,
                        sala = e.SALA.NOMBRESALA,
                        fecha = e.FECHA,
                        horaInicio = e.HORA_INICIO,
                        horaTermino = e.HORA_TERMINO,
                        estado = e.ESTADO.NombreEstado,
                        activo = e.ACTIVO

                    }).ToDataTable();
        }

        //Obtener lista de reservas activas(no borradas) por usuario
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable ListaReservasUsuarioActivas(string id_usuario)
        {

            return (from e in entidades.RESERVA
                    where e.ID_USUARIO == id_usuario && e.ACTIVO == true && e.HORA_INICIO>DateTime.Now
                    select new
                    {
                        idReserva = e.ID_RESERVA,
                        sala = e.SALA.NOMBRESALA,
                        fecha = e.FECHA,
                        horaInicio = e.HORA_INICIO,
                        horaTermino = e.HORA_TERMINO,
                        estado = e.ESTADO.NombreEstado
                    }).ToDataTable();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable ListaReservasPorSala(int idSala)
        {
            return (from e in entidades.RESERVA
                    where e.SALA.IDSALA == idSala && e.IDESTADO == 1 && e.ACTIVO==true|| e.SALA.IDSALA==idSala && e.IDESTADO == 3 && e.ACTIVO == true
                    select new
                    {
                        idReserva = e.ID_RESERVA,
                        usuario=e.ID_USUARIO,
                        horaInicio = e.HORA_INICIO,
                        horaTermino = e.HORA_TERMINO,
                    }).ToDataTable();
        }

[DataObjectMethod(DataObjectMethodType.Insert)]
        public void CrearReserva(string idUsuario, int idSala, DateTime fecha, DateTime hInicio, DateTime hTermino)
        {
            entidades.RESERVA.Add(new RESERVA()
            {
                IDSALA = idSala,
                IDESTADO = 1,
                ID_USUARIO = idUsuario,
                FECHA = fecha,
                HORA_INICIO = hInicio,
                HORA_TERMINO = hTermino,
                ACTIVO = true,
                MOTIVO = ""
            });
            entidades.SaveChanges();
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void EditarReserva(int idReserva, string idUsuario, int idSala, DateTime fecha, DateTime hInicio, DateTime hTermino)
        {
            RESERVA reserva = (from e in entidades.RESERVA where e.ID_RESERVA == idReserva select e).First();
            reserva.ID_USUARIO = idUsuario;
            reserva.IDSALA = idSala;
            reserva.FECHA = fecha;
            reserva.HORA_INICIO = hInicio;
            reserva.HORA_TERMINO = hTermino;
            reserva.IDESTADO = 1;

            entidades.SaveChanges();
        }


        public void DeshabilitarReserva(int idReserva)
        {
            RESERVA reserva = (from e in entidades.RESERVA where e.ID_RESERVA == idReserva && e.ACTIVO == true select e).First();
            reserva.ACTIVO = false;

            entidades.SaveChanges();
        }

        public void AprobarReserva(int idReserva, int idEstado)
        {
            RESERVA reserva = (from e in entidades.RESERVA where e.ID_RESERVA == idReserva select e).First();

            reserva.IDESTADO = idEstado;
            reserva.MOTIVO = "";
            entidades.SaveChanges();
        }
        public void RechazarReserva(int idReserva, int idEstado, string motivo)
        {
            RESERVA reserva = (from e in entidades.RESERVA where e.ID_RESERVA == idReserva select e).First();

            reserva.IDESTADO = idEstado;
            reserva.MOTIVO = motivo;
            entidades.SaveChanges();
        }

        //lista reserva coordinador
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable ListaReservasCoordinador(string id_usuario)
        {

            return (from e in entidades.RESERVA
                    where e.SALA.IDCOORD == id_usuario && e.ACTIVO == true
                    select new
                    {
                        IdReserva = e.ID_RESERVA,
                        idUsuario = e.ID_USUARIO,
                        sala = e.SALA.NOMBRESALA,
                        fecha = e.FECHA,
                        horaInicio = e.HORA_INICIO,
                        horaTermino = e.HORA_TERMINO,
                        estado = e.ESTADO.NombreEstado
                    }).ToDataTable();
        }

        public bool BuscarConflictoHorarios(int idSala, DateTime fecha, DateTime horaInicio, DateTime horaTermino)
        {
            bool conflicto = false;
            TimeRange rangoReservaNueva = new TimeRange(horaInicio, horaTermino);

            List<RESERVA> res = (from s in entidades.RESERVA where s.IDSALA == idSala && s.FECHA == fecha && s.IDESTADO == 1 && s.ACTIVO == true || s.IDSALA == idSala && s.FECHA == fecha && s.IDESTADO == 3 && s.ACTIVO == true orderby s.HORA_TERMINO descending select s).ToList();
            TimeRange rangoReservaHecha;
            foreach (RESERVA item in res)
            {
                rangoReservaHecha = new TimeRange(item.HORA_INICIO, item.HORA_TERMINO);
                if (rangoReservaNueva.IntersectsWith(rangoReservaHecha) == true)
                {
                    conflicto = true;
                }

            }
            return conflicto;

        }

        public RESERVA ObtenerDatosReserva(int idReserva)
        {
            RESERVA reserva = (from e in entidades.RESERVA where e.ID_RESERVA == idReserva select e).First();

            return reserva;

        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ESTADO> ObtenerEstados()
        {
            return (from e in entidades.ESTADO select e).ToList();
        }
    }
}
