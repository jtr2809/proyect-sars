using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProyectSARS.BLL
{
    [DataObject]
    public class UbicacionBLL
    {
        //establece el conjunto de entidades de la base de datos
        private Entidades1 entidades = new Entidades1();

        //metodo para traer una lista de objetos de clase UBICACION
        [DataObjectMethod(DataObjectMethodType.Select)] //representa el tipo de operacion de dato, para configurar controles enlazados a datos, ej. un control gridview
        public List<UBICACION> ObtenerUbicacion()
        {
            List<UBICACION> lista = new List<UBICACION>(from elemento in entidades.UBICACION where elemento.ACTIVO == true select elemento).ToList();
            return lista;
        }

        //metodo para deshabilitar una ubicacion y no mostrarla más a los usuarios
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void EliminarUbicacion(int idUbicacion)
        {
            //instancia de objeto de clase SalaBLL para borrar las salas de la ubicacion
            SalaBLL sbll = new SalaBLL();
            //obtiene la ubicacion de acuerdo a la id entregada en el parametro idUbicacion
            UBICACION u = ((from e in entidades.UBICACION where e.ID_UBICACION == idUbicacion select e).First());

            //recorre el conjunto de salas obtenidas en el metodo EliminarSalasPorUbicacion y las deshabilita
            foreach (SALA sala in sbll.EliminarSalasPorUbicacion(idUbicacion))
            {
                sbll.DeshabilitarSala(sala.IDSALA);
            }

            //finalmente deshabilita la ubicacion y se guardan los cambios
            u.ACTIVO = false;
            entidades.SaveChanges();
        }

        //añade una nueva ubicacion a la base de datos, con la descripcion ingresada en el parametro descripcion
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void CrearUbicacion(string descripcion)
        {
            entidades.UBICACION.Add(new UBICACION() { DESCRIPCION = descripcion, ACTIVO = true });
            entidades.SaveChanges();
        }
    }
}