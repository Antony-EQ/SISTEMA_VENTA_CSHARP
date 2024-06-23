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

namespace SISTEMA_VENTA_CSHARP.Presentacion.REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS
{
    public partial class FormInventariosTodos : Form
    {
        public FormInventariosTodos()
        {
            InitializeComponent();
        }
        ReportInventarios_Todos rptFREPORT2 = new ReportInventarios_Todos();
       
        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("imprimir_inventarios_todos", con);
                da.Fill(dt);
                con.Close();
                rptFREPORT2 = new ReportInventarios_Todos();
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
        private void FormInventariosTodos_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
    }
}
