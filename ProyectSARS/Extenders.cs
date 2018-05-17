using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProyectSARS
{
    //el objetivo de esta clase es obtener una coleccion de objetos en uno de tipo DataTable para enlazar en un gridview y poder implementar paginación y usar clases anónimas (LINQ)
    //al momento de consultar por los datos de la base de datos
    public static class Extenders
    {
        //constructor para clase DataTable
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        //constructor para clase DataTable
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();

            //crea las columnas con los campos de las tablas de la base de datos
            foreach (PropertyInfo pi in pia)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }

            //llena la coleccion de la tabla
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();

                foreach (PropertyInfo pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();

                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
