using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectSARS.BLL
{
    [DataObject]
    public class SalaBLL
    {
        //establece el conjunto de entidades de la base de datos
        private Entidades1 entidades = new Entidades1();

        //Metodo para traer datos de sala segun su ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public SALA GetSALAs(int idsala)
        {
            SALA sala = (from e in entidades.SALA where e.DISPONIBILIDAD == true && e.IDSALA == idsala select e).First();
            return sala;
        }

        //Metodo para crear nueva sala
        public void CrearSala(string id_coord, int id_ubicacion, string nombre, int capacidad, int piso, string equip, bool note, bool monitor, bool vc)
        {
            entidades.SALA.Add(new SALA() { IDCOORD = id_coord, ID_UBICACION = id_ubicacion, NOMBRESALA = nombre, CAPACIDAD = capacidad, PISO = piso, EQUIPAMIENTO = equip, POSEE_NOTE = note, POSEE_MONITOR = monitor, POSEE_VC = vc, DISPONIBILIDAD = true });
            entidades.SaveChanges();
        }

        //Metodo para deshabilitar salas segun id ingresada como parametro
        public void DeshabilitarSala(int id_sala)
        {
            SALA sala = (from t in entidades.SALA where id_sala == t.IDSALA select t).First();

            //Marca sala con disponibilidad en 'false'
            sala.DISPONIBILIDAD = false;
            entidades.SaveChanges();
        }

        //Metodo para editar salas
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void EditarSala(int idSala, string nombreSala, string equipamiento, string idCoord, int capacidad, int piso, bool monitor, bool PC_Note, bool VC, bool disponibilidad)
        {
            SALA sala = (from e in entidades.SALA where e.IDSALA == idSala select e).First();
            sala.IDCOORD = idCoord;
            sala.NOMBRESALA = nombreSala;
            sala.EQUIPAMIENTO = equipamiento;
            sala.PISO = piso;
            sala.POSEE_MONITOR = monitor;
            sala.POSEE_NOTE = PC_Note;
            sala.POSEE_VC = VC;
            sala.DISPONIBILIDAD = disponibilidad;
            sala.CAPACIDAD = capacidad;
            entidades.SaveChanges();
        }

        //obtiene un conjunto de salas en un objeto DataTable para enlazar a control gridview 
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable ObtenerSalasPorUbicacion(int idUbicacion)
        {

            return (from e in entidades.SALA
                    where e.ID_UBICACION == idUbicacion && e.DISPONIBILIDAD == true
                    select new
                    {
                        idsala = e.IDSALA,
                        nombreSala = e.NOMBRESALA,
                        idCoord = e.IDCOORD,
                        equipamiento = e.EQUIPAMIENTO,
                        capacidad = e.CAPACIDAD,
                        piso = e.PISO,
                        pcNote = e.POSEE_NOTE,
                        vc = e.POSEE_VC,
                        monitor = e.POSEE_MONITOR
                    }
                    ).ToDataTable();

        }

        //devuelve true o false si existe o no la sala
        public bool BuscarSalaExistente(int idSala)
        {
            bool result = (from e in entidades.SALA where e.IDSALA == idSala select e).Any();
            return result;
        }

        //trae un conjunto de salas cuyo coordinador coincida con el valor ingresado en idCoordinador
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SALA> ObtenerSalasDeUsuario(string idCoordinador)
        {
            return (from e in entidades.SALA where e.IDCOORD == idCoordinador && e.DISPONIBILIDAD == true select e).ToList();
        }

        //método para traer un arreglo de tipo List para usar en la clase UbicacionBLL - trae un conjunto de salas que tengan por ID_ubicacion el valor ingresado
        public List<SALA> EliminarSalasPorUbicacion(int idUbicacion)
        {
            return (from e in entidades.SALA where e.ID_UBICACION == idUbicacion select e).ToList();
        }
    }
}