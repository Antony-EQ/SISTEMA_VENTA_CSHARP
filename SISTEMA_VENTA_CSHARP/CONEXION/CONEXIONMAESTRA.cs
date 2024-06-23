using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SISTEMA_VENTA_CSHARP.CONEXION
{
    class CONEXIONMAESTRA
    {
        public static string Conexion = Convert.ToString(CONEXION.Desencryptacion.checkServer());
        //@"Data source = DESKTOP-HFURIK4\SQLEXPRESS; Initial Catalog=BASESISTEMAS; integrated security =  true ";
        public static SqlConnection conectar = new SqlConnection(Conexion);
        public static void abrir ()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void cerrar() 
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
