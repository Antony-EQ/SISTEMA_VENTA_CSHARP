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
using System.Text.RegularExpressions;
using SISTEMA_VENTA_CSHARP.Logica;

namespace SISTEMA_VENTA_CSHARP
{
    public partial class USUARIOS : Form
    {
        public USUARIOS()
        {
            InitializeComponent();
        }

        private void CargarEstadoIconos() 
        {
            try
            {
                foreach (DataGridViewRow row in dgvListado.Rows)
                {
                    try
                    {
                        string Icono = Convert.ToString(row.Cells["Nombre de Icono"].Value);

                        if (Icono == "1")
                        {
                            pictureBox3.Visible = false;
                        }
                        else if (Icono == "2")
                        {
                            pictureBox4.Visible = false;
                        }
                        else if (Icono == "3")
                        {
                            pictureBox5.Visible = false;
                        }
                        else if (Icono == "4")
                        {
                            pictureBox6.Visible = false;
                        }
                        else if (Icono == "5")
                        {
                            pictureBox8.Visible = false;
                        }
                        else if (Icono == "6")
                        {
                            pictureBox9.Visible = false;
                        }
                        else if (Icono == "7")
                        {
                            pictureBox10.Visible = false;
                        }
                        else if (Icono == "8")
                        {
                            pictureBox10.Visible = false;
                        }
                        
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public bool ValidarCorreo(string sMail) 
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-])*(\.[a-z]{2,4})$");

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ValidarCorreo(txtCorreo.Text) == false)
            {
                MessageBox.Show("Dirección de correo electronico no válida, el correo debe tener el formato: nombre@dominio.com, " + " por favor ingrese un correro valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCorreo.Focus();
                txtCorreo.SelectAll();
            }
            else 
            {
                if (txtNombre.Text != "")
                {
                    if (txtUsuario.Text != "")
                    {
                        if (txtContraseña.Text != "")
                        {
                            if (cbxRol.Text != "")
                            {
                                if (lblAnuncioIcono.Visible == false)
                                {
                                    try
                                    {
                                        SqlConnection con = new SqlConnection();
                                        con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand();
                                        cmd = new SqlCommand("InsertarUsuario", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                                        cmd.Parameters.AddWithValue("@login", txtUsuario.Text);
                                        cmd.Parameters.AddWithValue("@password", Bases.Encriptar(txtContraseña.Text));

                                        cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                                        cmd.Parameters.AddWithValue("@rol", cbxRol.Text);
                                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                        Icono.Image.Save(ms, Icono.Image.RawFormat);
                                        cmd.Parameters.AddWithValue("@icono", ms.GetBuffer());
                                        cmd.Parameters.AddWithValue("@nombre_de_icono", lblNumeroIcono.Text);
                                        cmd.Parameters.AddWithValue("@estado", "ACTIVO");
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        Mostrar();
                                        panel4.Visible = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Elija un Icono", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Elija un Rol", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Digite una Contraseña para poder continuar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Escriba un Nombre de Usuario", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    


                }
                else 
                {
                    MessageBox.Show("Asegurese de llenar todos los campos para poder continuar","Registro",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }

            }
            
        }
        private void Mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("MostrarUsuarios", con);
                da.Fill(dt);
                dgvListado.DataSource = dt;
                con.Close();
                dgvListado.Columns[1].Visible = false;
                dgvListado.Columns[5].Visible = false;
                dgvListado.Columns[6].Visible = false;
                dgvListado.Columns[7].Visible = false;
                dgvListado.Columns[8].Visible = false;
                //dgvListado.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Bases.MultiLinea(ref dgvListado);


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox3.Image;
            lblNumeroIcono.Text = "1";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;

        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            CargarEstadoIconos();
            panelIcono.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox4.Image;
            lblNumeroIcono.Text = "2";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox5.Image;
            lblNumeroIcono.Text = "3";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox6.Image;
            lblNumeroIcono.Text = "4";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            lblAnuncioIcono.Visible = true;
            txtNombre.Text = "";
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            txtCorreo.Text = "";
            btnGuardarCambios.Visible = false;
        }

        private void USUARIOS_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panelIcono.Visible = false;
            Mostrar();
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIdUsuario.Text = dgvListado.SelectedCells[1].Value.ToString();
            txtNombre.Text = dgvListado.SelectedCells[2].Value.ToString();
            txtUsuario.Text = dgvListado.SelectedCells[3].Value.ToString();
            txtContraseña.Text = Bases.Desencriptar(dgvListado.SelectedCells[4].Value.ToString());


            Icono.BackgroundImage = null;
            byte[] b = (byte[])dgvListado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            Icono.SizeMode = PictureBoxSizeMode.Zoom;
            lblAnuncioIcono.Visible = false;

            lblNumeroIcono.Text = dgvListado.SelectedCells[6].Value.ToString();
            txtCorreo.Text = dgvListado.SelectedCells[7].Value.ToString();
            cbxRol.Text = dgvListado.SelectedCells[8].Value.ToString();
            panel4.Visible = true;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("EditarUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", lblIdUsuario.Text);
                    cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@password",  Bases.Encriptar(txtContraseña.Text));

                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@rol", cbxRol.Text);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    Icono.Image.Save(ms, Icono.Image.RawFormat);
                    cmd.Parameters.AddWithValue("@icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@nombre_de_icono", lblNumeroIcono.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Mostrar();
                    panel4.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            CargarEstadoIconos();
            panelIcono.Visible = true;
        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==this.dgvListado.Columns["Eli"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este usuario?","Emininando Registros",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach  (DataGridViewRow row in dgvListado.SelectedRows)
                        {
                            int onekey = Convert.ToInt32(row.Cells["idUsuario"].Value);
                            string usuario = Convert.ToString(row.Cells["Login"].Value);
                            try
                            {
                                try
                                {
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                                    con.Open();
                                    cmd = new SqlCommand("EliminarUsuario", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@idUsuario", onekey);
                                    cmd.Parameters.AddWithValue("@login", usuario);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                                catch ( Exception ex)
                                {
                                    MessageBox.Show(ex.Message);       
                                }
                                                  
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        
                    }
                    
                }
                Mostrar();
            }
            
        } 
       

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes";
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                Icono.BackgroundImage = null;
                Icono.Image = new Bitmap(dlg.FileName);
                Icono.SizeMode = PictureBoxSizeMode.Zoom;
                lblNumeroIcono.Text = Path.GetFileName(dlg.FileName);
                lblAnuncioIcono.Visible = false;
                panelIcono.Visible = false;
            }
        }
        private void Buscar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("BuscarUsuarios", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscar.Text);
                da.Fill(dt);
                dgvListado.DataSource = dt;
                con.Close();
                

                dgvListado.Columns[1].Visible = false;
                dgvListado.Columns[5].Visible = false;
                dgvListado.Columns[6].Visible = false;
                dgvListado.Columns[7].Visible = false;
                dgvListado.Columns[8].Visible = false;
                //dgvListado.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Bases.MultiLinea(ref dgvListado);


        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox7.Image;
            lblNumeroIcono.Text = "5";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox8.Image;
            lblNumeroIcono.Text = "6";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox9.Image;
            lblNumeroIcono.Text = "7";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox10.Image;
            lblNumeroIcono.Text = "8";
            lblAnuncioIcono.Visible = false;
            panelIcono.Visible = false;
        }

        public void Numeros(System.Windows.Forms.TextBox CajaTexto, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numeros(txtBuscar, e);
        }
    }
}
