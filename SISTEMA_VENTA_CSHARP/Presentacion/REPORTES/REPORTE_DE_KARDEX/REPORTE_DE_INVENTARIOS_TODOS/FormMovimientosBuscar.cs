using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_VENTA_CSHARP.Presentacion.REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS
{
    public partial class FormMovimientosBuscar : Form
    {
        public FormMovimientosBuscar()
        {
            InitializeComponent();
        }
       ReportMovimientosBuscar rptFREPORT2 = new ReportMovimientosBuscar();
        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idProducto", Presentacion.INVENTARIOS_KARDEX.INVENTARIO_MENU.idProducto);
                da.Fill(dt);
                con.Close();
                rptFREPORT2 = new ReportMovimientosBuscar();
                rptFREPORT2.DataSource = dt; 
                reportViewer1.Report = rptFREPORT2;
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormMovimientosBuscar_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
    }
}
