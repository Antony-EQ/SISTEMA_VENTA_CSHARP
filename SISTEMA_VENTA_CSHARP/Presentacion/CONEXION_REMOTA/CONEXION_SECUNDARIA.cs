using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_VENTA_CSHARP.Presentacion.CONEXION_REMOTA
{
    public partial class CONEXION_SECUNDARIA : Form
    {
        public CONEXION_SECUNDARIA()
        {
            InitializeComponent();
        }

        private void btnconectar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIp.Text))
            {

            }
            else
            {
                MessageBox.Show("Ingrese la IP", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void conectar_manualmente()
        {
            //string IP = txtIp.Text;
            //cadena_de_conexion = "Data Source =" + IP + ";Initial Catalog=BASEADACURSO;Integrated Security=False;User Id=pruebas2020;Password=pruebas123";
            //comprobar_conexion();
            //if (indicador_de_conexion == "HAY CONEXION")
            //{
            //    SavetoXML(aes.Encrypt(cadena_de_conexion, Desencryptacion.appPwdUnique, int.Parse("256")));
            //    Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            //    if (idcaja > 0)
            //    {
            //        MessageBox.Show("Conexion Correcta. Vuelve a Abrir el Sistema", "Conexion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Dispose();
            //    }
            //    else
            //    {
            //        Caja_secundaria.lblconexion = cadena_de_conexion;
            //        Dispose();
            //        Caja_secundaria frm = new Caja_secundaria();
            //        frm.ShowDialog();
            //    }
            //}

        }

        private void txtIp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
