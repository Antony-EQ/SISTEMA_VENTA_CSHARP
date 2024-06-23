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
using SISTEMA_VENTA_CSHARP.Logica;
using SISTEMA_VENTA_CSHARP.Datos;
using SISTEMA_VENTA_CSHARP.CONEXION;

namespace SISTEMA_VENTA_CSHARP.Presentacion.VENTAS_MENU_PRINCIPAL
{
    public partial class Ventas_en_espera : Form
    {
        public Ventas_en_espera()
        {
            InitializeComponent();
        }
        int idcaja;
        int idventa;
        
        

        private void Ventas_en_espera_Load(object sender, EventArgs e)
        {

            mostrar_ventas_en_espera_con_fecha_y_monto();
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
        }

        private void mostrar_ventas_en_espera_con_fecha_y_monto()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.mostrar_ventas_en_espera_con_fecha_y_monto(ref dt);
                datalistado_ventas_en_espera.DataSource = dt;
                datalistado_ventas_en_espera.Columns[1].Visible = false;
                datalistado_ventas_en_espera.Columns[4].Visible = false;
                Bases.MultiLinea(ref datalistado_ventas_en_espera);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void datalistado_ventas_en_espera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idventa = Convert.ToInt32(datalistado_ventas_en_espera.SelectedCells[1].Value);
                mostrar_detalle_venta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void mostrar_detalle_venta()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_productos_agregados_a_ventas_en_espera(ref dt, idventa);
            datalistadodetalledeventasarestaurar.DataSource = dt;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            Eliminar_datos.eliminar_venta(idventa);
            idventa = 0;
            mostrar_ventas_en_espera_con_fecha_y_monto();
            mostrar_detalle_venta();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            Presentacion.VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK.idVenta = idventa;
            Editar_datos.cambio_de_Caja(idcaja, idventa);
            Dispose();
        }
    }
}
