//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectSARS
{
    using System;
    using System.Collections.Generic;
    
    public partial class RESERVA
    {
        public int ID_RESERVA { get; set; }
        public Nullable<int> IDSALA { get; set; }
        public string ID_USUARIO { get; set; }
        public System.DateTime FECHA { get; set; }
        public System.DateTime HORA_INICIO { get; set; }
        public System.DateTime HORA_TERMINO { get; set; }
        public int IDESTADO { get; set; }
        public bool ACTIVO { get; set; }
        public string MOTIVO { get; set; }
    
        public virtual ESTADO ESTADO { get; set; }
        public virtual SALA SALA { get; set; }
    }
}
