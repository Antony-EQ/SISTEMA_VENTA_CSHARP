using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_VENTA_CSHARP.Presentacion.CONFIGURACION
{
    public partial class PANEL_CONFIGURACIONES : Form
    {
        public PANEL_CONFIGURACIONES()
        {
            InitializeComponent();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Dispose();
            PRODUCTOS.productosOK frm = new PRODUCTOS.productosOK();
            frm.ShowDialog();


        }

       

        private void Logo_empresa_Click(object sender, EventArgs e)
        {
            Configurar_empresa();
        }

        private void Configurar_empresa()
        {

            EMPRESA_CONFIGURACION.EMPRESA_CONFIG frm = new EMPRESA_CONFIGURACION.EMPRESA_CONFIG();
            frm.ShowDialog();
        }

        private void Label47_Click(object sender, EventArgs e)
        {
            Configurar_empresa();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Usuarios();
        }
        private void Usuarios()
        {
            USUARIOS frm = new USUARIOS();
            frm.ShowDialog();

        }

        private void Label26_Click(object sender, EventArgs e)
        {
            Usuarios();
        }

        private void PANEL_CONFIGURACIONES_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Admin_nivel_dios.DASHBOARD_PRINCIPAL();
            frm.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            COPIASBD.CREAR_COPIAS_BD frm = new COPIASBD.CREAR_COPIAS_BD();
            frm.ShowDialog();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            COPIASBD.CREAR_COPIAS_BD frm = new COPIASBD.CREAR_COPIAS_BD();
            frm.ShowDialog(); 
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            CLIENTES_PROVEEDORES.ClientesOK frm = new CLIENTES_PROVEEDORES.ClientesOK();
            frm.ShowDialog();
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            CLIENTES_PROVEEDORES.ClientesOK frm = new CLIENTES_PROVEEDORES.ClientesOK();
            frm.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DISEÑADOR_DE_COMPROBANTES.Ticked frm = new DISEÑADOR_DE_COMPROBANTES.Ticked();
            frm.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            CAJA.CAJAS_FORM frm = new CAJA.CAJAS_FORM();
            frm.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SERIALIZACION_DE_COMPROBANTES.SERIALIZACION frm = new SERIALIZACION_DE_COMPROBANTES.SERIALIZACION();
            frm.ShowDialog();
        }

        private void Label27_Click(object sender, EventArgs e)
        {
            CAJA.CAJAS_FORM frm = new CAJA.CAJAS_FORM();
            frm.ShowDialog();
        }

        private void Label29_Click(object sender, EventArgs e)
        {
            SERIALIZACION_DE_COMPROBANTES.SERIALIZACION frm = new SERIALIZACION_DE_COMPROBANTES.SERIALIZACION();
            frm.ShowDialog();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            CLIENTES_PROVEEDORES.Proveedores frm = new CLIENTES_PROVEEDORES.Proveedores();
            frm.ShowDialog();
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            CLIENTES_PROVEEDORES.Proveedores frm = new CLIENTES_PROVEEDORES.Proveedores();
            frm.ShowDialog();
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            PRODUCTOS.productosOK frm = new PRODUCTOS.productosOK();
            frm.ShowDialog();
        }

        private void Label31_Click(object sender, EventArgs e)
        {
            DISEÑADOR_DE_COMPROBANTES.Ticked frm = new DISEÑADOR_DE_COMPROBANTES.Ticked();
            frm.ShowDialog();
        }
    }
}
