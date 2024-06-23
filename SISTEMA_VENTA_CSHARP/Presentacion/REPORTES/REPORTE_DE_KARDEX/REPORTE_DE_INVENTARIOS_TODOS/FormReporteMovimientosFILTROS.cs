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
    public partial class FormReporteMovimientosFILTROS : Form
    {
        public FormReporteMovimientosFILTROS()
        {
            InitializeComponent();
        }

        private void FormReporteMovimientosFILTROS_Load(object sender, EventArgs e)
        {
            Mostrar();
        }

        Reporte_Movimientos_con_filtros rptFREPORT2 = new Reporte_Movimientos_con_filtros();
        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX_filtros", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", Presentacion.INVENTARIOS_KARDEX.INVENTARIO_MENU.fecha);
                da.SelectCommand.Parameters.AddWithValue("@tipo", Presentacion.INVENTARIOS_KARDEX.INVENTARIO_MENU.Tipo_de_movimiento);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", Presentacion.INVENTARIOS_KARDEX.INVENTARIO_MENU.id_ususario);
                da.Fill(dt);
                con.Close();
                rptFREPORT2 = new Reporte_Movimientos_con_filtros();
                rptFREPORT2.DataSource = dt;
                rptFREPORT2.table1.DataSource = dt;
                reportViewer1.Report = rptFREPORT2;
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
