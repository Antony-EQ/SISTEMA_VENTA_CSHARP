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

namespace SISTEMA_VENTA_CSHARP.Presentacion.ASISTENTE_DE_INSTALACION_servidor
{
    public partial class Eleccion_Servidor_o_remoto : Form
    {
        public Eleccion_Servidor_o_remoto()
        {
            InitializeComponent();
        }

        private void Eleccion_Servidor_o_remoto_Load(object sender, EventArgs e)
        {
            Panel4.Location = new Point((Width - Panel4.Width) / 2, (Height - Panel4.Height) / 2);
            Listar();
            if (Estado_de_conexion == "CONECTADO")
            {
                Dispose();
                Presentacion.ASISTENTE_DE_INSTALACION_servidor.REGISTRO_DE_EMPRESA frm = new Presentacion.ASISTENTE_DE_INSTALACION_servidor.REGISTRO_DE_EMPRESA();
                frm.ShowDialog();
            }

        }

        string Estado_de_conexion;
        private void Listar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("select * from USUARIO", con);
                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();
                Estado_de_conexion = "CONECTADO";
            }
            catch (Exception ex)
            {
                Estado_de_conexion = "-";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
            Presentacion.ASISTENTE_DE_INSTALACION_servidor.Instalacion_del_servidorSQL frm = new Presentacion.ASISTENTE_DE_INSTALACION_servidor.Instalacion_del_servidorSQL();
            frm.ShowDialog();
        }
    }
}
