using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectSARS
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
            Login1.FailureText = "Error en la autenticación, porfavor intente nuevamente.";
        }
    }
    
}