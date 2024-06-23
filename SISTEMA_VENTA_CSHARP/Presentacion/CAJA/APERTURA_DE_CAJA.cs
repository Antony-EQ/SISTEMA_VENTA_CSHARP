using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Management;
using System.Xml;
using Guna.UI2.WinForms;
using SISTEMA_VENTA_CSHARP.Logica;
using SISTEMA_VENTA_CSHARP.Datos;

namespace SISTEMA_VENTA_CSHARP.Presentacion.CAJA
{
    public partial class APERTURA_DE_CAJA : Form
    {
        public APERTURA_DE_CAJA()
        {
            InitializeComponent();
        }

        int txtidcaja;

        private void btnIniciarCaja_Click(object sender, EventArgs e)
        {
            bool estado = Editar_datos.editar_dinero_caja_inicial(txtidcaja, Convert.ToDouble(txtMonto.Text));
            if (estado==true)
            {
                pasar_a_ventas();
            }
            
        }

        private void pasar_a_ventas() 
        {
            Dispose();
            VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK frm = new VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK();
            frm.ShowDialog();
            
        }

        private void APERTURA_DE_CAJA_Load(object sender, EventArgs e)
        {           
            Bases.Cambiar_idioma_regional();
            Obtener_datos.Obtener_id_caja_PorSerial(ref txtidcaja);            
        }

        private void btnOmitir_Click(object sender, EventArgs e)
        {
            pasar_a_ventas();
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            CONEXION.Numeros_separadores.Separador_de_Numeros(txtMonto, e);
        }

        
        private static void OnlyNumber(bool isdecimal, KeyPressEventArgs e)
        {
            String aceptados;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
            }
            else
                aceptados = "0123456789," + Convert.ToChar(8);

            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


    }
}
    

