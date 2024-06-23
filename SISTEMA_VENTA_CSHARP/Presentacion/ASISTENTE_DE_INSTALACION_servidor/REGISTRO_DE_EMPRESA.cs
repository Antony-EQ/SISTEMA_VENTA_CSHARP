using SISTEMA_VENTA_CSHARP.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_VENTA_CSHARP.Presentacion.ASISTENTE_DE_INSTALACION_servidor
{
    public partial class REGISTRO_DE_EMPRESA : Form
    {
        public REGISTRO_DE_EMPRESA()
        {
            InitializeComponent();
        }

        string lblSerialPc;
        public static string correo;
        public bool validar_Mail(string sMail)
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");

        }
        private void TSIGUIENTE_Y_GUARDAR__Click(object sender, EventArgs e)
        {
            if (validar_Mail(txtcorreo.Text) == false)
            {
                MessageBox.Show("Dirección de correo electronico no valida, el correo debe tener el formato: nombre@dominio.com, " + " por favor seleccione un correo valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcorreo.Focus();
                txtcorreo.SelectAll();
            }
            else
            {

                if (txtempresa.Text != "")
                {
                    if (txtRuta.Text != "")
                    {
                        if (no.Checked == true)
                        {
                            TXTTRABAJASCONIMPUESTOS.Text = "NO";
                        }
                        if (si.Checked == true)
                        {
                            TXTTRABAJASCONIMPUESTOS.Text = "SI";
                        }
                        Ingresar_empresa();
                        Ingresar_caja();
                        insertar_3_COMPROBANTES_POR_DEFECTO();
                        correo = txtcorreo.Text;
                        Dispose();
                        USUARIOS_AUTORIZADOS_AL_SISTEMA frm = new USUARIOS_AUTORIZADOS_AL_SISTEMA();
                        frm.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Seleccione una Ruta para Guardar las Copias de Seguridad", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }



                }

                else
                {
                    MessageBox.Show("Ingrese un Nombre de Empresa", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Ingresar_empresa()
        {
            try
            {


                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("InsertarEmpresa", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre_Empresa", txtempresa.Text);
                cmd.Parameters.AddWithValue("@Impuesto", txtimpuesto.Text);
                cmd.Parameters.AddWithValue("@Porcentaje_impuestos", txtporcentaje.Text);
                cmd.Parameters.AddWithValue("@Moneda", txtmoneda.Text);
                cmd.Parameters.AddWithValue("@Trabajas_con_impuestos", TXTTRABAJASCONIMPUESTOS.Text);

                cmd.Parameters.AddWithValue("@Carpeta_copias_de_seguridad", txtRuta.Text);
                cmd.Parameters.AddWithValue("@Correo_para_envio_de_reportes", txtcorreo.Text);
                cmd.Parameters.AddWithValue("@Ultima_fecha_de_copia_de_seguridad", "Ninguna");
                cmd.Parameters.AddWithValue("@Ultima_fecha_de_copia_date", txtfecha.Value);
                cmd.Parameters.AddWithValue("@Frecuencia_de_copias", 1);
                cmd.Parameters.AddWithValue("@Estado", "PENDIENTE");
                cmd.Parameters.AddWithValue("@Tipo_de_empresa", "FARMACIA");
                
 
                if (TXTCON_LECTORA.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Modo_de_busqueda", "LECTORA");
                }


                if (txtteclado.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@Modo_de_busqueda", "TECLADO");
                }


                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                ImagenEmpresa.Image.Save(ms, ImagenEmpresa.Image.RawFormat);


                cmd.Parameters.AddWithValue("@logo", ms.GetBuffer());
                cmd.Parameters.AddWithValue("@Pais", TXTPAIS.Text);
                cmd.Parameters.AddWithValue("@Redondeo_total", "NO");

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Ingresar_caja()
        {
            try
            {


                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("InsertarCaja", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Descripcion", txtcaja.Text);
                cmd.Parameters.AddWithValue("@Tema", "Redentor");
                cmd.Parameters.AddWithValue("@Serial_PC", lblSerialPc);
                cmd.Parameters.AddWithValue("@Impresora_Ticked", "Ninguna");
                cmd.Parameters.AddWithValue("@Impresora_A4", "Ninguna");
                cmd.Parameters.AddWithValue("@Tipo", "PRINCIPAL");
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TXTCON_LECTORA_CheckedChanged(object sender, EventArgs e)
        {
            if (TXTCON_LECTORA.Checked == true)
            {
                txtteclado.Checked = false;
            }
            if (TXTCON_LECTORA.Checked == false)
            {
                txtteclado.Checked = true;
            }
        }

        private void txtteclado_CheckedChanged(object sender, EventArgs e)
        {
            if (txtteclado.Checked == true)
            {
                TXTCON_LECTORA.Checked = false;
            }
            if (txtteclado.Checked == false)
            {
                TXTCON_LECTORA.Checked = true;
            }
        }

        private void TXTPAIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmoneda.SelectedIndex = TXTPAIS.SelectedIndex;
        }

        private void lbleditarLogo_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImagenEmpresa.BackgroundImage = null;
                ImagenEmpresa.Image = new Bitmap(dlg.FileName);
                ImagenEmpresa.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un Disco Diferente al Disco C:", "Ruta Invalida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRuta.Text = "";
                }
                else
                {
                    txtRuta.Text = folderBrowserDialog1.SelectedPath;
                }


            }
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = txtRuta.Text;
                if (ruta.Contains(@"C:\"))
                {
                    MessageBox.Show("Selecciona un Disco Diferente al Disco C:", "Ruta Invalida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRuta.Text = "";
                }
                else
                {
                    txtRuta.Text = folderBrowserDialog1.SelectedPath;
                }


            }
        }

        private void REGISTRO_DE_EMPRESA_Load(object sender, EventArgs e)
        {
            Panel16.Location = new Point((Width - Panel16.Width) / 2, (Height - Panel16.Height) / 2);

            Bases.Obtener_serialPC(ref lblSerialPc);
            Panel16.Location = new Point((Width - Panel16.Width) / 2, (Height - Panel16.Height) / 2);
            TXTCON_LECTORA.Checked = true;
            txtteclado.Checked = false;
            no.Checked = true;
            Panel11.Visible = false;
            Panel9.Visible = false;


        }
        
        private void insertar_3_COMPROBANTES_POR_DEFECTO()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("InsertarSerializacion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Serie", "T");
                cmd.Parameters.AddWithValue("@numeroinicio", 6);
                cmd.Parameters.AddWithValue("@numerofin", 0);
                cmd.Parameters.AddWithValue("@Tipodoc", "TICKET");
                cmd.Parameters.AddWithValue("@Destino", "VENTAS");
                cmd.Parameters.AddWithValue("@Por_defecto", "SI");
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                cmd = new SqlCommand("InsertarSerializacion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Serie", "B");
                cmd.Parameters.AddWithValue("@numeroinicio", 6);
                cmd.Parameters.AddWithValue("@numerofin", 0);
                cmd.Parameters.AddWithValue("@Tipodoc", "BOLETA");
                cmd.Parameters.AddWithValue("@Destino", "VENTAS");
                cmd.Parameters.AddWithValue("@Por_defecto", "-");
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                cmd = new SqlCommand("InsertarSerializacion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Serie", "F");
                cmd.Parameters.AddWithValue("@numeroinicio", 6);
                cmd.Parameters.AddWithValue("@numerofin", 0);
                cmd.Parameters.AddWithValue("@Tipodoc", "FACTURA");
                cmd.Parameters.AddWithValue("@Destino", "VENTAS");
                cmd.Parameters.AddWithValue("@Por_defecto", "-");
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                cmd = new SqlCommand("InsertarSerializacion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Serie", "I");
                cmd.Parameters.AddWithValue("@numeroinicio", 6);
                cmd.Parameters.AddWithValue("@numerofin", 0);
                cmd.Parameters.AddWithValue("@Tipodoc", "INGRESO");
                cmd.Parameters.AddWithValue("@Destino", "INGRESO DE COBROS");
                cmd.Parameters.AddWithValue("@Por_defecto", "-");
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                cmd = new SqlCommand("InsertarSerializacion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Serie", "E");
                cmd.Parameters.AddWithValue("@numeroinicio", 6);
                cmd.Parameters.AddWithValue("@numerofin", 0);
                cmd.Parameters.AddWithValue("@Tipodoc", "EGRESO");
                cmd.Parameters.AddWithValue("@Destino", "EGRESO DE PAGOS");
                cmd.Parameters.AddWithValue("@Por_defecto", "-");
                cmd.ExecuteNonQuery();
                con.Close();

                

                con.Open();
                cmd = new SqlCommand("Insertar_FORMATO_TICKET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Identificador_fiscal", "RUC Identificador Fiscal de la Empresa");
                cmd.Parameters.AddWithValue("@Direccion", "Calle, Nro, avenida");
                cmd.Parameters.AddWithValue("@Provincia_Departamento_Pais", "Provincia - Departamento - Pais");
                cmd.Parameters.AddWithValue("@Nombre_de_Moneda", "Nombre de Moneda");
                cmd.Parameters.AddWithValue("@Agradecimiento", "Agradecimiento");
                cmd.Parameters.AddWithValue("@pagina_Web_Facebook", "pagina Web ó Facebook");
                cmd.Parameters.AddWithValue("@Anuncio", "Anuncio");
                cmd.Parameters.AddWithValue("@Datos_fiscales_de_autorizacion", "Datos Fiscales - Numero de Autorizacion, Resolucion...");
                cmd.Parameters.AddWithValue("@Por_defecto", "Ticket No Fiscal");
                cmd.ExecuteNonQuery();
                con.Close();
                /*
                con.Open();
                cmd = new SqlCommand("insertarCorreoBase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string correo;
                string pass;
                string estado;
                correo = Bases.Encriptar("-");
                pass = Bases.Encriptar("-");
                estado = "Sin confirmar";
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Password", pass);
                cmd.Parameters.AddWithValue("@Estado_De_envio", estado);
                cmd.ExecuteNonQuery();
                con.Close();
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void si_CheckedChanged(object sender, EventArgs e)
        {
            Panel11.Visible = true;
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            Panel11.Visible = false;
        }
    }
    
}
