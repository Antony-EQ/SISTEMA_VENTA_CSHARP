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
using SISTEMA_VENTA_CSHARP.Logica;
using SISTEMA_VENTA_CSHARP.CONEXION;

namespace SISTEMA_VENTA_CSHARP.Presentacion
{
    public partial class LOGIN : Form
    {
        int Contador;
        int txtcontador_USUARIOS;
        int ContadorCajas;
        int ContadorMovimientosCaja;
        public static int idusuariovariable;
        public static int idcajavariable;
        int idusuarioVerificador;
        string lblSerialPc;
        string lblSerialPcLocal;
        string vendedor= "Vendedor";
        string cajero = "Cajero";
        string administrador = "Administrador";
        string lblRol;
        string txtlogin;
        string lblApertura_de_caja;

        public LOGIN()
        {
            InitializeComponent();
        }
        public void DibujarUsuarios()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("Select * from USUARIO WHERE Estado = 'ACTIVO'", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();

                    //CONFIGURANDO LABEL

                    b.Text = rdr["Login"].ToString();
                    b.Name = rdr["idUsuario"].ToString();
                    b.Size = new System.Drawing.Size(172, 25);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    //b.FlatStyle = FlatStyle.Flat;
                    b.BackColor = Color.FromArgb(20, 20, 20);
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Bottom;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    //CONFIGURANDO PANEL

                    p1.Size = new System.Drawing.Size(155, 167);
                    p1.BorderStyle = BorderStyle.None;
                    //p1.Dock = DockStyle.Bottom;
                    p1.BackColor = Color.FromArgb(20, 20, 20);

                    //CONFIGURANDO PICTUREBOX

                    I1.Size = new System.Drawing.Size(175, 132);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    byte[] bi = (Byte[])rdr["Icono"];
                    MemoryStream ms = new MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Tag = rdr["Login"].ToString();
                    I1.Cursor = Cursors.Hand;

                    //ADDS
                    p1.Controls.Add(b);
                    p1.Controls.Add(I1);
                    b.BringToFront();
                    flowLayoutPanel1.Controls.Add(p1);

                    //

                    b.Click += new EventHandler(EventoLabel);
                    I1.Click += new EventHandler(EventoImagen);

                }
                con.Close();
            }
            catch (Exception ex)
            {

               
            }
            
        }

        private void EventoLabel(System.Object sender, EventArgs e)
        {
           txtlogin=((Label)sender).Text;
            panelContraseñas.Visible = true;
            panelUsuarios.Visible = false;
        }
            
        private void EventoImagen(System.Object sender, EventArgs e)
        {
            txtlogin = Convert.ToString(((PictureBox)sender).Tag);
            panelContraseñas.Visible = true;
            panelUsuarios.Visible = false;
            
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
  
            validar_conexion();
            escalar_paneles();
           
        }

        private void escalar_paneles() 
        {
            panelContraseñas.Visible = false;
            panelRecuperarContraseña.Visible = false;
            PicLoading.Location = new Point((Width - PicLoading.Width) / 2, (Height - PicLoading.Height) / 2);
            panelUsuarios.Location = new Point((Width - panelUsuarios.Width) / 2, (Height - panelUsuarios.Height) / 2);
            panelRecuperarContraseña.Location = new Point((Width - panelRecuperarContraseña.Width) / 2, (Height - panelRecuperarContraseña.Height) / 2);
            panelContraseñas.Location = new Point((Width - panelContraseñas.Width) / 2, (Height - panelContraseñas.Height) / 2);
        }

        private void Listar_cierre_de_caja()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc);
                da.Fill(dt);
                dataListado_detalle_cierre_caja.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtContra_TextChanged(object sender, EventArgs e)
        {
            IniciarSesionCorrecto();
            if (txtContra.Text != "")
            {
                btnVer.Visible = true;
            }

        }

        private void Contar_cierre_de_caja()
        {
            int x;
            x = dataListado_detalle_cierre_caja.Rows.Count;
            ContadorCajas = (x);
        }

        private void Aperturar_Detalle_de_Cierre_de_Caja()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("INSERTAR_DETALLE_CIERRE_DE_CAJA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechafin", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechacierre", DateTime.Now);
                cmd.Parameters.AddWithValue("@ingresos", "0.00");
                cmd.Parameters.AddWithValue("@egresos", "0.00");
                cmd.Parameters.AddWithValue("@saldo", "0.00");
                cmd.Parameters.AddWithValue("@idusuario", idusuariovariable);
                cmd.Parameters.AddWithValue("@totalcalculado", "0.00");
                cmd.Parameters.AddWithValue("@totalreal", "0.00");
                cmd.Parameters.AddWithValue("@estado", "CAJA APERTURADA");
                cmd.Parameters.AddWithValue("@diferencia", "0.00");
                cmd.Parameters.AddWithValue("@idcaja", idcajavariable);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_Y_USUARIO()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_Y_USUARIO", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idusuario", idusuariovariable);
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc);
                da.Fill(dt);
                datalistado_movimientos_validar.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CONTAR_MOVIMIENTOS_DE_CAJA_POR__USUARIO()
        {
            int x;
            x = datalistado_movimientos_validar.Rows.Count;
            ContadorMovimientosCaja = (x);
        }

        private void obtener_idusuario() 
        {
            try
            {
                idusuariovariable = Convert.ToInt32(dgvListado.SelectedCells[1].Value);
            }
            catch
            {
                
            }
        }

        private void IniciarSesionCorrecto()
        {
            CargarUsuarios();
            Contar();
            

            if (Contador > 0)
            {
                obtener_idusuario();
                mostrar_roles();
                if (lblRol != cajero)
                {
                    timerValidarRol.Start();
                }
                else if(lblRol == cajero)
                {
                    validar_aperturas_de_caja();
                }
            }     
        }

        private void usuario_que_inicio_caja() 
        {
            try
            {
                lblUsuario_que_inicio_caja.Text = dataListado_detalle_cierre_caja.SelectedCells[1].Value.ToString();
                lblNombreCajero.Text = dataListado_detalle_cierre_caja.SelectedCells[2].Value.ToString();
            }
            catch
            {
                
            }
        }

        private void validar_aperturas_de_caja() 
        {
            Listar_cierre_de_caja();
            Contar_cierre_de_caja();
            if (ContadorCajas == 0)
            {
                Aperturar_Detalle_de_Cierre_de_Caja();
                lblApertura_de_caja = "Nuevo*****";
                timerValidarRol.Start();
            }
            else 
            {
                MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_Y_USUARIO();
                CONTAR_MOVIMIENTOS_DE_CAJA_POR__USUARIO();
                usuario_que_inicio_caja();
                if (ContadorMovimientosCaja == 0)
                {
                    usuario_que_inicio_caja();
                    MessageBox.Show("Para poder continuar con el turno de *" + lblNombreCajero.Text + " * Inicia Sesion con el usuario " + lblUsuario_que_inicio_caja.Text + " -ó- el Usuario *admin*", "CAJA INICIADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblApertura_de_caja = "Aperturado";
                    timerValidarRol.Start();
                } 
            }
        }

        private void mostrar_roles()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;

            SqlCommand com = new SqlCommand("mostrar_permisos_por_usuario_ROL_UNICO", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idusuario", idusuariovariable);
            string importe;

            try
            {
                con.Open();
                importe = Convert.ToString(com.ExecuteScalar());
                con.Close();
                lblRol = importe;
            } 
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void MOSTRAR_licencia_temporal()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("select * from Marcan", con);
                da.Fill(dt);
                datalistado_licencia_temporal.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {

            }
        }

        private void Contar()
        {
            int x;
            x = dgvListado.Rows.Count;
            Contador = (x);
        }
        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("ValidarUsuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", Bases.Encriptar(txtContra.Text));
                da.SelectCommand.Parameters.AddWithValue("@login", txtlogin);
                da.Fill(dt);
                dgvListado.DataSource = dt;
                con.Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void MostrarCorreos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("Select Correo from USUARIO WHERE Estado = 'ACTIVO'", con);

                da.Fill(dt);
                txtCorreo.DisplayMember = "Correo";
                txtCorreo.ValueMember = "Correo";
                txtCorreo.DataSource = dt;
                con.Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnOlvideContra_Click(object sender, EventArgs e)
        {
            panelRecuperarContraseña.Visible = true;
            panelUsuarios.Visible = false;
            MostrarCorreos();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            panelRecuperarContraseña.Visible = false;
            panelUsuarios.Visible = true;
        }

        private void MostrarUsuariosCorreo()
        {
            try
            {
                string resultado;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                SqlCommand da = new SqlCommand("BuscarUsuario_Correo", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", txtCorreo.Text);

                con.Open();
                lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());
                con.Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        internal void EnviarCorreo(string emisor, string password, string mensaje, string asunto, string destinatario, string ruta)
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add(destinatario);
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, password);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);
                lblEstadoLicenciado.Text = "Enviado";
                MessageBox.Show("Contraseña enviada; revisa tu correo electronico", "");
                panelRecuperarContraseña.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            MostrarUsuariosCorreo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", lblResultadoContraseña.Text);
            EnviarCorreo("soporte.kymfarma@gmail.com", "KyMSoporte", richTextBox1.Text, "Solicitud de Contraseña", txtCorreo.Text, "");
        }

        private void MOSTRAR_CAJA_POR_SERIAL()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("Mostrar_Caja_Por_Serial_de_DiscoDuro", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", lblSerialPc);
                da.Fill(dt);
                dgvListadoCaja.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ingresar_por_licencia_Temporal()
        {
            lblEstadoLicencia.Text = "Licencia de Prueba Activada hasta el: " + txtfecha_final_licencia_temporal.Text;

        }
        private void Ingresar_por_licencia_de_paga()
        {
            lblEstadoLicencia.Text = "Licencia PROFESIONAL Activada hasta el: " + txtfecha_final_licencia_temporal.Text;
        }

        private void validar_conexion() 
        {
            mostrar_usuarios_registrados();


            if (Indicador == "CORRECTO")
            {

                if (idusuarioVerificador == 0)
                {
                    this.Dispose();
                    ASISTENTE_DE_INSTALACION_servidor.REGISTRO_DE_EMPRESA frm = new ASISTENTE_DE_INSTALACION_servidor.REGISTRO_DE_EMPRESA();
                    frm.ShowDialog();
                    
                }
                else 
                {
                    DibujarUsuarios();
                }


            }
            if (Indicador == "INCORRECTO")
            {
                Dispose();
                ASISTENTE_DE_INSTALACION_servidor.Eleccion_Servidor_o_remoto frm = new ASISTENTE_DE_INSTALACION_servidor.Eleccion_Servidor_o_remoto();
                frm.ShowDialog();
                
            }
           
            try
            {
                Bases.Obtener_serialPC(ref lblSerialPc);
                MOSTRAR_CAJA_POR_SERIAL();
                try
                {
                    idcajavariable = Convert.ToInt32(dgvListadoCaja.SelectedCells[1].Value);
                    txtdescripcioncaja.Text = dgvListadoCaja.SelectedCells[2].Value.ToString();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MOSTRAR_licencia_temporal();

            try
            {
                txtfecha_final_licencia_temporal.Value = Convert.ToDateTime(Bases.Desencriptar(datalistado_licencia_temporal.SelectedCells[3].Value.ToString()));
                lblSerialPcLocal = (Bases.Desencriptar(datalistado_licencia_temporal.SelectedCells[2].Value.ToString()));
                LBLESTADOLicenciaLocal.Text = Bases.Desencriptar(datalistado_licencia_temporal.SelectedCells[4].Value.ToString());
                txtfecha_inicio_licencia.Value = Convert.ToDateTime(Bases.Desencriptar(datalistado_licencia_temporal.SelectedCells[5].Value.ToString()));

            }
            catch (Exception ex)
            {

            }
            if (LBLESTADOLicenciaLocal.Text != "VENCIDO")

            {
                string fechaHoy = Convert.ToString(DateTime.Now);
                DateTime fecha_ddmmyyyy = Convert.ToDateTime(fechaHoy.Split(' ')[0]);

                if (txtfecha_final_licencia_temporal.Value >= fecha_ddmmyyyy)
                {
                    if (txtfecha_inicio_licencia.Value <= fecha_ddmmyyyy)
                    {
                        if (LBLESTADOLicenciaLocal.Text == "?ACTIVO?")
                        {
                            Ingresar_por_licencia_Temporal();
                        }


                    }
                    else
                    {

                    }

                }


            }
        }

        public static string Mid(string param, int starIndex, int length)
        {
            string result = param.Substring(starIndex, length);
            return result;
        }

        public static string Mid(string param, int starIndex)
        {
            string result = param.Substring(starIndex);
            return result;
        }   

        private void btnVer_Click(object sender, EventArgs e)
        {
            txtContra.PasswordChar = '\0';

            btnOcultar.Visible = true;
            btnVer.Visible = false;
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            txtContra.PasswordChar = '*';
            btnVer.Visible = true;
            btnOcultar.Visible = false;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario o Contraseña Incorrectos", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void timerValidarRol_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                BackColor = Color.FromArgb(20, 20, 20);
                progressBar1.Value = progressBar1.Value + 10;
                PicLoading.Visible = true;
                panelContraseñas.Visible = false;
            }
            else
            {
                progressBar1.Value = 0;
                timerValidarRol.Stop();
                if (lblRol == administrador)
                {
                    editar_inicio_De_sesion();
                    Dispose();
                    Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Admin_nivel_dios.DASHBOARD_PRINCIPAL();
                    frm.ShowDialog();
                   
                }
                else 
                {
                    if (lblApertura_de_caja == "Nuevo*****" & lblRol== cajero)
                    {
                        editar_inicio_De_sesion();
                        Dispose();
                        CAJA.APERTURA_DE_CAJA form = new CAJA.APERTURA_DE_CAJA();
                        form.ShowDialog();
                        
                    }
                    else if (lblApertura_de_caja == "Aperturado" & lblRol == cajero)
                    {
                        editar_inicio_De_sesion();
                        
                        Dispose();
                        VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK frm = new VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK();
                        
                        frm.ShowDialog();
                        
                    }
                    else if(lblRol == vendedor)
                    {
                        editar_inicio_De_sesion();
                        Dispose();
                        VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK frm = new VENTAS_MENU_PRINCIPAL.VENTAS_MENU_PRINCIPALOK();
                        frm.ShowDialog();
                        

                    }
                }

                
            }

        }

        private void editar_inicio_De_sesion()
        {
            try
            {



                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand("editar_inicio_De_sesion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_serial_Pc", lblSerialPc);
                cmd.Parameters.AddWithValue("@id_usuario", idusuariovariable);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        string Indicador;
        private void mostrar_usuarios_registrados() 
        {
            try
            {
                
                CONEXIONMAESTRA.abrir();
                SqlCommand da = new SqlCommand("select idUsuario from USUARIO", CONEXIONMAESTRA.conectar);
                idusuarioVerificador = Convert.ToInt32(da.ExecuteScalar());
                CONEXIONMAESTRA.cerrar();
                Indicador = "CORRECTO";
            }
            catch (Exception )
            {
                Indicador = "INCORRECTO";
                idusuarioVerificador = 0;
            }
        }

       

        private void button0_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "0";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "1";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "2";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "3";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "4";

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "5";

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "6";
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "7";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "8";
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            txtContra.Text = txtContra.Text + "9";
        }

        private void btnBorrarTodo_Click_1(object sender, EventArgs e)
        {
            txtContra.Clear();
        }

        private void btnBorrarDerecha_Click_1(object sender, EventArgs e)
        {
            try
            {
                int largo;
                if (txtContra.Text != "")
                {
                    largo =txtContra.Text.Length;
                    label4.Text = Convert.ToString(largo);
                    txtContra.Text = Mid(txtContra.Text, 0, largo - 1);
                }
            }
            catch
            {

            }
        }

        private void btnCambiarUser_Click(object sender, EventArgs e)
        {
            panelUsuarios.Visible = true;
            panelContraseñas.Visible = false;
            txtContra.Clear();
        }
    }
}
