using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SISTEMA_VENTA_CSHARP;
using SISTEMA_VENTA_CSHARP.Presentacion;
using SISTEMA_VENTA_CSHARP.CONEXION;
using System.Management;
using System.Threading;
using SISTEMA_VENTA_CSHARP.Logica;
using SISTEMA_VENTA_CSHARP.Datos;

namespace SISTEMA_VENTA_CSHARP.Presentacion.PRODUCTOS
{
    public partial class productosOK : Form
    {
        int txtcontador;
        public static int idusuario;
        public static int idcaja;
        //int lblSerialPc;
         
        public productosOK()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        internal void LIMPIAR() 
        {
            txtidproducto.Text = "";
            txtdescripcion.Text = "";
            txtcompra.Text = "0";
            TXTPRECIODEVENTA.Text = "0";
            txtlote.Text = "";
            txtLAB.Text = "";
            
            txtcontiene.Text = "";
            afraccion.Checked = false;
            txtstockminimo.Text = "0";
            txtstock.Text = "0";
            lblestadocodigo.Text = "NUEVO";

        }

        private void productosOK_Load(object sender, EventArgs e)
        {
            Bases.Cambiar_idioma_regional();
            
            datalistado.Visible = true;
            PANELREGISTRO.Visible = false;
            txtbusca.Text = "Buscar...";
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
            buscar();

            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
           
        }

        internal void sumar_costo_de_inventario_CONTAR_PRODUCTOS()
        {

            //string resultado;
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = CONEXION.CONEXIONMAESTRA.conexion;
            //SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", con);
            //da.CommandType = CommandType.StoredProcedure;
            //da.Parameters.AddWithValue("@correo", txtcorreo.Text);

            //con.Open();
            //lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());
            //con.Close();

            string resultado;
            string queryMoneda;
            queryMoneda = "SELECT Moneda  FROM EMPRESA";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToString(comMoneda.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                resultado = "";
            }

            string importe;
            string query;
            query = "SELECT      CONVERT(NUMERIC(18,2),sum(Producto.Precio_de_compra * Stock )) as suma FROM  Producto where  Usa_inventario ='SI'";

            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                importe = Convert.ToString(com.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                lblcosto_inventario.Text = resultado + " " + importe;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                lblcosto_inventario.Text = resultado + " " + 0;
            }

            string conteoresultado;
            string querycontar;
            querycontar = "select count(Id_Producto ) from Producto ";
            SqlCommand comcontar = new SqlCommand(querycontar, con);
            try
            {
                con.Open();
                conteoresultado = Convert.ToString(comcontar.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                lblcantidad_productos.Text = conteoresultado;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                conteoresultado = "";
                lblcantidad_productos.Text = "0";
            }

        }

        private void btnGuardarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("InsertarLaboratorios", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", txtLAB.Text);
                cmd.Parameters.AddWithValue("@Por_defecto","NO");
                cmd.ExecuteNonQuery();
                con.Close();
                MostrarGrupos();

                lblIdLaboratorio.Text = datalistadoLaboratorios.SelectedCells[2].Value.ToString();
                txtLAB.Text = datalistadoLaboratorios.SelectedCells[3].Value.ToString();

                PanelGRUPOSSELECT.Visible = false;
                btnGuardarLab.Visible = false;
                btnGuardarCambios.Visible = false;
                btnCancelar.Visible = false;
                btnAgregarLab.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MostrarGrupos() 
        {
            PanelGRUPOSSELECT.Visible = true;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("MostrarLaboratorios", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar",txtLAB.Text);
                da.Fill(dt);
                datalistadoLaboratorios.DataSource = dt;
                con.Close();

                datalistadoLaboratorios.DataSource = dt;
                datalistadoLaboratorios.Columns[2].Visible = false;
                datalistadoLaboratorios.Columns[3].Width = 500;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Bases.MultiLinea(ref datalistadoLaboratorios);
            //SE HIZO CAMBIOS
        }

        private void btnAgregarLab_Click(object sender, EventArgs e)
        {
            txtLAB.Text = "ESCRIBE EL NUEVO LABORATORIO";
            txtLAB.SelectAll();
            txtLAB.Focus();

            PanelGRUPOSSELECT.Visible = false;
            btnGuardarLab.Visible = true;
            btnGuardarCambios.Visible = false;
            btnCancelar.Visible = true;
            btnAgregarLab.Visible = false;
        }

        private void txtLAB_TextChanged(object sender, EventArgs e)
        {
            MostrarGrupos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PanelGRUPOSSELECT.Visible = false;
            btnGuardarLab.Visible = false;
            btnGuardarCambios.Visible = false;
            btnCancelar.Visible = false;
            btnAgregarLab.Visible = true;
            txtLAB.Clear(); 
            MostrarGrupos();
        }

        private void TGUARDAR_Click(object sender, EventArgs e)
        {
            INSERTARPRODUCTOS();
            buscar();
            
        }

        private void INSERTARPRODUCTOS() 
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("InsertarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
                cmd.Parameters.AddWithValue("@Id_Laboratorio", lblIdLaboratorio.Text);
                cmd.Parameters.AddWithValue("@Precio_de_compra", txtcompra.Text);
                cmd.Parameters.AddWithValue("@Precio_de_venta", TXTPRECIODEVENTA.Text);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                
                cmd.Parameters.AddWithValue("@Impuesto", txtPorcentajeGanancia.Text);
                cmd.Parameters.AddWithValue("@Lote",txtlote.Text);
                cmd.Parameters.AddWithValue("@Contiene", txtcontiene.Text);

                if (porunidad.Checked == true) txtse_vende_a.Text = "Unidad";
                if (afraccion.Checked == true) txtse_vende_a.Text = "Fraccion";
                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                if (PANELINVENTARIO.Visible==true)
                {
                    cmd.Parameters.AddWithValue("@Usa_Inventario", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo",txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock.Text);
                    if (No_aplica_fecha.Checked==true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }
                    if (No_aplica_fecha.Checked==false)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                    }
                }
                if (PANELINVENTARIO.Visible==false)
                {
                    cmd.Parameters.AddWithValue("@Usa_Inventario", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Stock", "ILIMITADO");
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                }
                cmd.Parameters.AddWithValue("@fecha", DateTime.Today);
                cmd.Parameters.AddWithValue("@motivo", "REGISTRO INICIAL DE PRODUCTO");
                cmd.Parameters.AddWithValue("@cantidad", txtstock.Text);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@tipo", "ENTRADA");
                cmd.Parameters.AddWithValue("@estado", "CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
           
                cmd.ExecuteNonQuery();

                con.Close();
               
                PANELREGISTRO.Visible = false;
                txtbusca.Text = txtdescripcion.Text;
                buscar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EDITARPRODUCTOS()
        {           
            try
            {

  
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXIONMAESTRA.Conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("EditarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto", TXTIDPRODUCTOOk.Text);
                cmd.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
                cmd.Parameters.AddWithValue("@Id_Laboratorio", lblIdLaboratorio.Text);
                cmd.Parameters.AddWithValue("@Lote", txtlote.Text);
                cmd.Parameters.AddWithValue("@Precio_de_compra", txtcompra.Text);
                cmd.Parameters.AddWithValue("@Precio_de_venta", TXTPRECIODEVENTA.Text);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                cmd.Parameters.AddWithValue("@Impuesto", txtPorcentajeGanancia.Text);
                cmd.Parameters.AddWithValue("@Contiene", txtcontiene.Text);
                
                if (porunidad.Checked == true) txtse_vende_a.Text = "Unidad";
                if (afraccion.Checked == true) txtse_vende_a.Text = "Fraccion";

                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                if (PANELINVENTARIO.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@Usa_Inventario", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo", txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock.Text);

                    if (No_aplica_fecha.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }

                    if (No_aplica_fecha.Checked == false)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                    }


                }
                if (PANELINVENTARIO.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventario", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Stock", "Ilimitado");

                }

                cmd.ExecuteNonQuery();


                con.Close();
                PANELREGISTRO.Visible = false;
                txtbusca.Text = txtdescripcion.Text;
                buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            EDITARPRODUCTOS();
        }

        private void datalistadoLaboratorios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datalistadoLaboratorios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistadoLaboratorios.Columns["EliminarG"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Laboratorio?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in datalistadoLaboratorios.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["Id_Laboratorio"].Value);

                            try
                            {

                                try
                                {

                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
                                    con.Open();
                                    cmd = new SqlCommand("EliminarLaboratorio", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@idLaboratorio", onekey);
                                    cmd.ExecuteNonQuery();

                                    con.Close();

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

                        }
                        txtLAB.Text = "GENERAL";
                        MostrarGrupos();
                        lblIdLaboratorio.Text = datalistadoLaboratorios.SelectedCells[2].Value.ToString();
                        PanelGRUPOSSELECT.Visible = true;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            if (e.ColumnIndex == this.datalistadoLaboratorios.Columns["EditarG"].Index)

            {
                lblIdLaboratorio.Text = datalistadoLaboratorios.SelectedCells[2].Value.ToString();
                txtLAB.Text = datalistadoLaboratorios.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnGuardarLab.Visible = false;
                btnGuardarCambios.Visible = true;
                btnCancelar.Visible = true;
                btnagregar.Visible = false;
            }

            if (e.ColumnIndex == this.datalistadoLaboratorios.Columns["Descripcion"].Index)
            {
                lblIdLaboratorio.Text = datalistadoLaboratorios.SelectedCells[2].Value.ToString();
                txtLAB.Text = datalistadoLaboratorios.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnGuardarLab.Visible = false;
                btnGuardarCambios.Visible = false;
                btnCancelar.Visible = false;
                btnagregar.Visible = true;
                if (lblestadocodigo.Text == "NUEVO")
                {
                    GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();
                }

            }

            lblIdLaboratorio.Text = datalistadoLaboratorios.SelectedCells[2].Value.ToString();
            txtLAB.Text = datalistadoLaboratorios.SelectedCells[3].Value.ToString();
            PanelGRUPOSSELECT.Visible = false;
            btnGuardarLab.Visible = false;
            btnGuardarCambios.Visible = false;
            btnCancelar.Visible = false;
            btnAgregarLab.Visible = true;
        }
        private void GENERAR_CODIGO_DE_BARRAS_AUTOMATICO()
        {
            Double resultado;
            string queryMoneda;
            queryMoneda = "SELECT max(Id_Producto)  FROM Producto";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToDouble(comMoneda.ExecuteScalar()) + 1;
                con.Close();
            }
            catch (Exception ex)
            {
                resultado = 1;
            }

            string Cadena = txtLAB.Text;
            string[] Palabra;
            String espacio = " ";
            Palabra = Cadena.Split(Convert.ToChar(espacio));
            try
            {

                txtcodigodebarras.Text = resultado + Palabra[0].Substring(0, 2) + 369;
            }
            catch (Exception ex)
            {
            }
        }

        private void CheckInventarios_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckInventarios.Checked == true)
            {
                PANELINVENTARIO.Visible = true;
            }
            else 
            {
                PANELINVENTARIO.Visible = false;
            }
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {
            mostrar_descripcion_produco_sin_repetir();
            contar();
            if (txtcontador==0)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
            if (txtcontador>0)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = true;
            }
            if (TGUARDAR.Visible == false)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
        }

        private void mostrar_descripcion_produco_sin_repetir()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_descripcion_produco_sin_repetir", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtdescripcion.Text);
                da.Fill(dt);
                DATALISTADO_PRODUCTOS_OKA.DataSource = dt;
                con.Close();

                datalistado.Columns[1].Width = 500;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void contar()
        {
            int x;

            x = DATALISTADO_PRODUCTOS_OKA.Rows.Count;
            txtcontador = (x);

        }

        private void DATALISTADO_PRODUCTOS_OKA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtdescripcion.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[1].Value.ToString();
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        private void DATALISTADO_PRODUCTOS_OKA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();
        }

        private void txtPorcentajeGanancia_TextChanged(object sender, EventArgs e)
        {

            TimerCalucular_porcentaje_ganancia.Stop();

            TimerCalcular_precio_venta.Start();
            TimerCalucular_porcentaje_ganancia.Stop();
        }

        private void TimerCalucular_porcentaje_ganancia_Tick(object sender, EventArgs e)
        {
            TimerCalucular_porcentaje_ganancia.Stop();
            try
            {


                double TotalVentaVariabledouble;
                double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA.Text);
                double txtcostov = Convert.ToDouble(txtcompra.Text);

                TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                if (TotalVentaVariabledouble > 0)
                {
                    this.txtPorcentajeGanancia.Text = Convert.ToString(TotalVentaVariabledouble);
                }
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void TimerCalcular_precio_venta_Tick(object sender, EventArgs e)
        {
            TimerCalcular_precio_venta.Stop();

            try
            {
                double TotalVentaVariabledouble;
                double txtcostov = Convert.ToDouble(txtcompra.Text);
                double txtPorcentajeGananciav = Convert.ToDouble(txtPorcentajeGanancia.Text);

                TotalVentaVariabledouble = txtcostov + ((txtcostov * txtPorcentajeGananciav) / 100);

                if (TotalVentaVariabledouble > 0 & txtPorcentajeGanancia.Focused == true)
                {
                    this.TXTPRECIODEVENTA.Text = Convert.ToString(TotalVentaVariabledouble);
                }
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }
            }
            catch (Exception ex)
            {

            }
        }  

        private void buscar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_producto_por_descripcion", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();

                datalistado.Columns[2].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[10].Visible = false;
                datalistado.Columns[14].Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            Bases.MultiLinea(ref datalistado);
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }
        internal void proceso_para_obtener_datos_de_productos()
        {
            try
            {

                Panel25.Enabled = true;
                DATALISTADO_PRODUCTOS_OKA.Visible = false;

                Panel6.Visible = false;
                TGUARDAR.Visible = false;
                TGUARDARCAMBIOS.Visible = true;
                PANELREGISTRO.Visible = true;


                btnAgregarLab.Visible = true;
                TXTIDPRODUCTOOk.Text = datalistado.SelectedCells[2].Value.ToString();
                lblestadocodigo.Text = "EDITAR";
                PanelGRUPOSSELECT.Visible = false;
                btnGuardarCambios.Visible = false;
                btnGuardarLab.Visible = false;
                btnCancelar.Visible = false;
                btnAgregarLab.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                txtidproducto.Text = datalistado.SelectedCells[2].Value.ToString();
                txtcodigodebarras.Text = datalistado.SelectedCells[3].Value.ToString();
                txtLAB.Text = datalistado.SelectedCells[4].Value.ToString();

                txtdescripcion.Text = datalistado.SelectedCells[5].Value.ToString();
                txtnumeroigv.Text = datalistado.SelectedCells[6].Value.ToString();
                
                LBL_ESSERVICIO.Text = datalistado.SelectedCells[7].Value.ToString();
                txtcompra.Text = datalistado.SelectedCells[8].Value.ToString();
                TXTPRECIODEVENTA.Text = datalistado.SelectedCells[9].Value.ToString();

                LBLSEVENDEPOR.Text = datalistado.SelectedCells[10].Value.ToString();
                if (LBLSEVENDEPOR.Text == "Unidad")
                {
                    porunidad.Checked = true;

                }
                if (LBLSEVENDEPOR.Text == "Fraccion")
                {
                    afraccion.Checked = true;
                }
                txtstockminimo.Text = datalistado.SelectedCells[11].Value.ToString();
                txtstock.Text = datalistado.SelectedCells[12].Value.ToString();
                lblfechasvenci.Text = datalistado.SelectedCells[13].Value.ToString();
                if (lblfechasvenci.Text == "NO APLICA")
                {
                    No_aplica_fecha.Checked = true;
                }
                if (lblfechasvenci.Text != "NO APLICA")
                {
                    No_aplica_fecha.Checked = false;
                }
                lblIdLaboratorio.Text = datalistado.SelectedCells[14].Value.ToString();
                txtcontiene.Text = datalistado.SelectedCells[15].Value.ToString();
                txtlote.Text = datalistado.SelectedCells[16].Value.ToString();
                try
                {

                    double TotalVentaVariabledouble;
                    double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA.Text);
                    double txtcostov = Convert.ToDouble(txtcompra.Text);

                    TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                    if (TotalVentaVariabledouble > 0)
                    {
                        this.txtPorcentajeGanancia.Text = Convert.ToString(TotalVentaVariabledouble);
                    }
                    else
                    {
                        //Me.txtPorcentajeGanancia.Text = 0
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                if (LBL_ESSERVICIO.Text == "SI")
                {

                    PANELINVENTARIO.Visible = true;
                    PANELINVENTARIO.Visible = true;
                    txtstock.ReadOnly = true;
                    CheckInventarios.Checked = true;

                }
                if (LBL_ESSERVICIO.Text == "NO")
                {
                    CheckInventarios.Checked = false;

                    PANELINVENTARIO.Visible = false;
                    PANELINVENTARIO.Visible = false;
                    txtstock.ReadOnly = true;
                    txtstock.Text = "0";
                    txtstockminimo.Text = "0";
                    No_aplica_fecha.Checked = true;
                    txtstock.ReadOnly = false;
                }



                PanelGRUPOSSELECT.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TGUARDARCAMBIOS_Click(object sender, EventArgs e)
        {
            EDITARPRODUCTOS();
            buscar();
        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void datalistado_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datalistado_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistado.Columns["Eliminar"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Producto?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in datalistado.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["Id_Producto"].Value);

                            try
                            {

                                try
                                {

                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = SISTEMA_VENTA_CSHARP.CONEXION.CONEXIONMAESTRA.Conexion;
                                    con.Open();
                                    cmd = new SqlCommand("EliminarProducto", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@id", onekey);
                                    cmd.ExecuteNonQuery();

                                    con.Close();

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

                        }
                        buscar();
                    }

                    catch (Exception ex)
                    {

                    }



                }
                if (e.ColumnIndex == this.datalistado.Columns["Editar"].Index)
                {
                    proceso_para_obtener_datos_de_productos();
                }

            }



        }

        

        private void datalistado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            proceso_para_obtener_datos_de_productos();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PANELREGISTRO.Visible = false;
            buscar();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DATALISTADO_PRODUCTOS_OKA.Visible=false;
        }

        private void txtstock_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (TXTIDPRODUCTOOk.Text != "0")
                {
                    Tmensajes.SetToolTip(txtstock, "Para modificar el Stock Hazlo desde el Modulo de Inventarios");
                    Tmensajes.ToolTipTitle = "Accion denegada";
                    Tmensajes.ToolTipIcon = ToolTipIcon.Info;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtcompra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar != '.') || (e.KeyChar != ','))
            //{


            //    string CultureName = Thread.CurrentThread.CurrentCulture.Name;
            //    CultureInfo ci = new CultureInfo(CultureName);


            //    ci.NumberFormat.NumberDecimalSeparator = ".";
            //    Thread.CurrentThread.CurrentCulture = ci;
            //}
            //Separador_de_Numeros(txtcompra, e);
        }

        //public static void Separador_de_Numeros(System.Windows.Forms.TextBox CajaTexto, System.Windows.Forms.KeyPressEventArgs e) 
        //{
        //    if (Char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }
        //    else if (Char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }

        //    else if (!(e.KeyChar == CajaTexto.Text.IndexOf('.')))
        //    {
        //        e.Handled = true;
        //    }


        //    else if (e.KeyChar == '.')
        //    {
        //        e.Handled = false;
        //    }
        //    else if (e.KeyChar == ',')
        //    {
        //        e.Handled = false;

        //    }
        //    else
        //    {
        //        e.Handled = true;

        //    }
        //}

        private void pNuevo_Click(object sender, EventArgs e)
        {
            PANELREGISTRO.Visible = true;
            CheckInventarios.Checked = true;
            PANELINVENTARIO.Visible = true;
            PanelGRUPOSSELECT.Visible = true;
            btnGuardarLab.Visible = false;
            btnGuardarCambios.Visible = false;
            btnCancelar.Visible = false;
            btnAgregarLab.Visible = true;
            MostrarGrupos();
            txtLAB.Clear();

            lblestadocodigo.Text = "NUEVO";
            PanelGRUPOSSELECT.Visible = true;
            btnGuardarLab.Visible = false;
            btnGuardarCambios.Visible = false;
            btnCancelar.Visible = false;
            btnAgregarLab.Visible = true;
            MostrarGrupos();


            txtstock.ReadOnly = false;
            Panel25.Enabled = true;
            Panel21.Visible = false;
            Panel22.Visible = false;
            Panel18.Visible = false;
            TXTIDPRODUCTOOk.Text = "0";

            txtdescripcion.AutoCompleteCustomSource = SISTEMA_VENTA_CSHARP.CONEXION.DataHelper.LoadAutoComplete();
            txtdescripcion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtdescripcion.AutoCompleteSource = AutoCompleteSource.CustomSource;

            PANELREGISTRO.Visible = true;
            porunidad.Checked = true;
            No_aplica_fecha.Checked = false;
            Panel6.Visible = false;

            LIMPIAR();
            btnagregaryguardar.Visible = true;
            btnagregar.Visible = false;


            txtdescripcion.Text = "";
            PANELINVENTARIO.Visible = true;


            TGUARDAR.Visible = true;
            TGUARDARCAMBIOS.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Asistente_de_importacionExcel frm = new Asistente_de_importacionExcel();
            frm.ShowDialog();
        }

        private void productosOK_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            CONFIGURACION.PANEL_CONFIGURACIONES frm = new CONFIGURACION.PANEL_CONFIGURACIONES();
            frm.ShowDialog();
        }
    }
}
