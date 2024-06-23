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
using SISTEMA_VENTA_CSHARP.Logica;

namespace SISTEMA_VENTA_CSHARP.Presentacion.INVENTARIOS_KARDEX
{
    public partial class INVENTARIO_MENU : Form
    {
        public INVENTARIO_MENU()
        {
            InitializeComponent();
        }
        REPORTES.REPORTE_DE_KARDEX.Reporte_de_Kardex_diseño.ReportKARDEX_Movimientos_ok rptFREPORT2 = new REPORTES.REPORTE_DE_KARDEX.Reporte_de_Kardex_diseño.ReportKARDEX_Movimientos_ok();

        private void mostrar_kardex_movimientos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();
                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idProducto", DATALISTADO_PRODUCTOS_Kardex.SelectedCells[1].Value.ToString());
                da.Fill(dt);
                con.Close();
                rptFREPORT2 = new REPORTES.REPORTE_DE_KARDEX.Reporte_de_Kardex_diseño.ReportKARDEX_Movimientos_ok();
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
        private void INVENTARIO_MENU_Load(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.None;
            PanelREPORTEInventario.Dock = DockStyle.None;
            PaneliNVENTARIObajo.Dock = DockStyle.None;
            PanelVencimientos.Dock = DockStyle.None;
            
            PanelKardex.Dock = DockStyle.Fill;
            panelMOVIMIENTOS.Visible = false;
            PanelREPORTEInventario.Visible = false;
            PaneliNVENTARIObajo.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = true;
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(255,255,255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TVencimientos.CustomBorderColor = Color.FromArgb(255, 255, 255);



        }

        private void txtbuscarMovimiento_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscarMovimiento.Text == "Buscar producto" | txtbuscarMovimiento.Text == "")
            {
                DATALISTADO_PRODUCTOS_Movimientos.Visible = false;

            }
            else
            {
                DATALISTADO_PRODUCTOS_Movimientos.Visible = true;
                buscar_productos_movimientos();
            }
        }

        private void buscar_productos_movimientos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", txtbuscarMovimiento.Text);
                da.Fill(dt);
                DATALISTADO_PRODUCTOS_Movimientos.DataSource = dt;
                con.Close();


                DATALISTADO_PRODUCTOS_Movimientos.Columns[1].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[3].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[4].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[5].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[6].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[7].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[8].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[9].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[10].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[11].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[12].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[13].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[14].Visible = false;
                DATALISTADO_PRODUCTOS_Movimientos.Columns[15].Visible = false;
               

                Bases.MultiLinea(ref DATALISTADO_PRODUCTOS_Movimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static int  idProducto;

        private void DATALISTADO_PRODUCTOS_Movimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtbuscarMovimiento.Text = DATALISTADO_PRODUCTOS_Movimientos.SelectedCells[2].Value.ToString();
            DATALISTADO_PRODUCTOS_Movimientos.Visible = false;
            buscar_MOVIMIENTOS_DE_KARDEX();

            try
            {
                idProducto = Convert.ToInt32(DATALISTADO_PRODUCTOS_Movimientos.SelectedCells[1].Value.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void buscar_MOVIMIENTOS_DE_KARDEX()
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
                da.SelectCommand.Parameters.AddWithValue("@idProducto", DATALISTADO_PRODUCTOS_Movimientos.SelectedCells[1].Value.ToString());
                da.Fill(dt);
                DatalistadoMovimientos.DataSource = dt;
                con.Close();


                DatalistadoMovimientos.Columns[0].Visible = false;
                DatalistadoMovimientos.Columns[10].Visible = false;
                DatalistadoMovimientos.Columns[11].Visible = false;
                Bases.MultiLinea(ref DatalistadoMovimientos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            DATALISTADO_PRODUCTOS_Movimientos.Visible = false;
            txtTipoMovi.Text = "-Todos-";
            buscar_usuario();/*AGREGADO MOMENTANEAMENTE*/
            buscar_MOVIMIENTOS_FILTROS();
            buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            panel4.Visible = true;
            btnFiltroAvanzado.Visible = false;
            btnImprimirSinFiltro.Visible = false;

        }

        private void buscar_MOVIMIENTOS_FILTROS()
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
                da.SelectCommand.Parameters.AddWithValue("@fecha", txtfechaM.Value);
                da.SelectCommand.Parameters.AddWithValue("@tipo", txtTipoMovi.Text);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", txtIdusuario.Text);


                da.Fill(dt);
                DatalistadoMovimientos.DataSource = dt;
                con.Close();


                DatalistadoMovimientos.Columns[0].Visible = false;
                DatalistadoMovimientos.Columns[10].Visible = false;
                DatalistadoMovimientos.Columns[11].Visible = false;

                DatalistadoMovimientos.Columns[9].Visible = false;
                DatalistadoMovimientos.Columns[13].Visible = false;
                DatalistadoMovimientos.Columns[14].Visible = false;
                DatalistadoMovimientos.Columns[12].Visible = false;
                Bases.MultiLinea(ref DatalistadoMovimientos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void btnOcultarFiltro_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            groupBox1.Visible = false;
            buscar_MOVIMIENTOS_DE_KARDEX();
            txtTipoMovi.Text = "-Todos-";
            txtbuscarMovimiento.Text = "Buscar producto";
            btnFiltroAvanzado.Visible = true;
            btnImprimirSinFiltro.Visible = true;
        }

        internal void Buscar_id_USUARIOS()
        {

            string resultado;
            string queryMoneda;
            queryMoneda = "Buscar_id_USUARIOS";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;

            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            comMoneda.CommandType = CommandType.StoredProcedure;
            comMoneda.Parameters.AddWithValue("@Nombre_y_Apellidos", txtUSUARIOS.Text);
            try
            {
                con.Open();
                resultado = Convert.ToString(comMoneda.ExecuteScalar()); //asignamos el valor del importe
                txtIdusuario.Text = resultado;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resultado = "";
            }
        }

        private void buscar_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("select*from USUARIO", con);

                da.Fill(dt);
                txtUSUARIOS.DisplayMember = "Nombres_y_Apellidos";
                txtUSUARIOS.ValueMember = "idUsuario";

                txtUSUARIOS.DataSource = dt;
                txtIdusuario.Text = txtUSUARIOS.ValueMember;

                con.Close();
                Buscar_id_USUARIOS();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }


        }

        private void txtUSUARIOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(groupBox1.Visible == true)
            {
               

                Buscar_id_USUARIOS();
                buscar_usuario();/*AGREGADO MOMENTANEAMENTE*/
                buscar_MOVIMIENTOS_FILTROS();
                buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            }
        }

        private void buscar_MOVIMIENTOS_FILTROS_ACUMULADO()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_MOVIMIENTOS_DE_KARDEX_filtros_acumulado", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", txtfechaM.Value);
                da.SelectCommand.Parameters.AddWithValue("@tipo", txtTipoMovi.Text);
                da.SelectCommand.Parameters.AddWithValue("@Id_usuario", txtIdusuario.Text);


                da.Fill(dt);
                DatalistadoMovimientosACUMULADO_PRODUCTO.DataSource = dt;
                con.Close();


                DatalistadoMovimientosACUMULADO_PRODUCTO.Columns[4].Visible = false;
                DatalistadoMovimientosACUMULADO_PRODUCTO.Columns[5].Visible = false;
                DatalistadoMovimientosACUMULADO_PRODUCTO.Columns[6].Visible = false;

                Bases.MultiLinea(ref DatalistadoMovimientosACUMULADO_PRODUCTO);
                DataGridViewCellStyle styCabeceras = new DataGridViewCellStyle();
                styCabeceras.BackColor = System.Drawing.Color.FromArgb(26, 115, 232);
                styCabeceras.ForeColor = System.Drawing.Color.White;
                styCabeceras.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                DatalistadoMovimientosACUMULADO_PRODUCTO.ColumnHeadersDefaultCellStyle = styCabeceras;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtfechaM_ValueChanged(object sender, EventArgs e)
        {
            if (groupBox1.Visible == true)
            {
                buscar_MOVIMIENTOS_FILTROS();
                buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            }
        }

        private void txtTipoMovi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupBox1.Visible == true)
            {

                buscar_MOVIMIENTOS_FILTROS();
                buscar_MOVIMIENTOS_FILTROS_ACUMULADO();
            }
        }

        private void MOSTRAR_Inventarios_bajo_minimo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("MOSTRAR_Inventarios_bajo_minimo", con);

                da.Fill(dt);
                datalistadoInventarioBAJO.DataSource = dt;
                con.Close();


                datalistadoInventarioBAJO.Columns[0].Visible = false;
                datalistadoInventarioBAJO.Columns[4].Visible = false;
                datalistadoInventarioBAJO.Columns[7].Visible = false;
                datalistadoInventarioBAJO.Columns[8].Visible = false;
                datalistadoInventarioBAJO.Columns[9].Visible = false;


                Bases.MultiLinea(ref datalistadoInventarioBAJO);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TInventarios_bajos_Click(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.None;
            PanelREPORTEInventario.Dock = DockStyle.None;
            PaneliNVENTARIObajo.Dock = DockStyle.Fill;
            PanelVencimientos.Dock = DockStyle.None;

            PanelREPORTEInventario.Visible = false;
            PaneliNVENTARIObajo.Visible = true;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            PanelKardex.Dock = DockStyle.None;
            panelMOVIMIENTOS.Visible = false;
            TKardex.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 204, 1);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TVencimientos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(255, 255, 255);
            MOSTRAR_Inventarios_bajo_minimo();
        }

        private void txtbuscar_inventarios_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar_inventarios.Text != "Buscar...") 
            {
                mostrar_inventarios_todos();
            }
        }

        private void mostrar_inventarios_todos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_inventarios_todos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbuscar_inventarios.Text);

                da.Fill(dt);
                datalistadoInventariosReport.DataSource = dt;
                con.Close();


                datalistadoInventariosReport.Columns[0].Visible = false;
                datalistadoInventariosReport.Columns[9].Visible = false;
                datalistadoInventariosReport.Columns[10].Visible = false;

                Bases.MultiLinea(ref datalistadoInventariosReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void sumar_costo_de_inventario_CONTAR_PRODUCTOS()
        {

            string resultado;
            string queryMoneda;
            queryMoneda = "SELECT Moneda  FROM EMPRESA";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
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
                lblcostoInventario.Text = resultado + " " + importe;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                lblcostoInventario.Text = resultado + " " + 0;
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
                lblcantidaddeProductosEnInventario.Text = conteoresultado;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                conteoresultado = "";
                lblcantidaddeProductosEnInventario.Text = "0";
            }

        }

        private void TReporteInventarios_Click(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.None;
            PanelREPORTEInventario.Dock = DockStyle.Fill;
            PaneliNVENTARIObajo.Dock = DockStyle.None;
            PanelVencimientos.Dock = DockStyle.None;

            PanelREPORTEInventario.Visible =true;
            PaneliNVENTARIObajo.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            PanelKardex.Dock = DockStyle.None;
            panelMOVIMIENTOS.Visible = false;
            TKardex.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(0, 166, 63);
            TVencimientos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(255, 255, 255);
            mostrar_inventarios_todos();
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }

        private void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            txtbuscar_inventarios.Clear();
            mostrar_inventarios_todos();
        }

        private void txtBuscarVencimientos_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarVencimientos.Text != "Buscar producto/Codigo")
            {
                buscar_productos_vencidos();
                CheckPorVenceren30Dias.Checked = false;
                CheckProductosVencidos.Checked = false;
            }
        }

        private void buscar_productos_vencidos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_productos_vencidos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscarVencimientos.Text);

                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();


                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;
                datalistadoVencimientos.Columns[6].Visible = false;
                datalistadoVencimientos.Columns[7].Visible = false;
                Bases.MultiLinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBuscarVencimientos_Click(object sender, EventArgs e)
        {
            txtBuscarVencimientos.SelectAll();
        }

        private void CheckPorVenceren30Dias_CheckedChanged(object sender, EventArgs e)
        {
            mostrar_productos_vencidos_en_menos_de_30_dias();
            txtBuscarVencimientos.Text = "Buscar producto/Codigo";
        }
        private void mostrar_productos_vencidos_en_menos_de_30_dias()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_productos_vencidos_en_menos_de_30_dias", con);


                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();


                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;

                Bases.MultiLinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckProductosVencidos_CheckedChanged(object sender, EventArgs e)
        {
            mostrar_productos_vencidos();
            txtBuscarVencimientos.Text = "Buscar producto/Codigo";
        }

        private void mostrar_productos_vencidos()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_productos_vencidos", con);


                da.Fill(dt);
                datalistadoVencimientos.DataSource = dt;
                con.Close();


                datalistadoVencimientos.Columns[0].Visible = false;
                datalistadoVencimientos.Columns[1].Visible = false;

                Bases.MultiLinea(ref datalistadoVencimientos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TKardex_Click(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.None;
            PanelREPORTEInventario.Dock = DockStyle.None;
            PaneliNVENTARIObajo.Dock = DockStyle.None;
            PanelVencimientos.Dock = DockStyle.None;

            PanelKardex.Dock = DockStyle.Fill;
            panelMOVIMIENTOS.Visible = false;
            PanelREPORTEInventario.Visible = false;
            PaneliNVENTARIObajo.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = true;
            TKardex.CustomBorderColor = Color.FromArgb(231, 63, 67);
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TVencimientos.CustomBorderColor = Color.FromArgb(255, 255, 255);
        }

        private void TMOVIMIENTOS_Click(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.Fill;
            PanelREPORTEInventario.Dock = DockStyle.None;
            PaneliNVENTARIObajo.Dock = DockStyle.None;
            PanelVencimientos.Dock = DockStyle.None;
            
            PanelREPORTEInventario.Visible = false;
            PaneliNVENTARIObajo.Visible = false;
            PanelVencimientos.Visible = false;
            PanelKardex.Visible = false;
            PanelKardex.Dock = DockStyle.None;
            panelMOVIMIENTOS.Visible = true;
            TKardex.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TVencimientos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(26, 115, 232);
        }

        private void TVencimientos_Click(object sender, EventArgs e)
        {
            panelMOVIMIENTOS.Dock = DockStyle.None;
            PanelREPORTEInventario.Dock = DockStyle.None;
            PaneliNVENTARIObajo.Dock = DockStyle.None;
            PanelVencimientos.Dock = DockStyle.Fill;

            PanelREPORTEInventario.Visible = false;
            PaneliNVENTARIObajo.Visible = false;
            PanelVencimientos.Visible = true;
            PanelKardex.Visible = false;
            PanelKardex.Dock = DockStyle.None;
            panelMOVIMIENTOS.Visible = false;
            TKardex.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TInventarios_bajos.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TReporteInventarios.CustomBorderColor = Color.FromArgb(255, 255, 255);
            TVencimientos.CustomBorderColor = Color.FromArgb(64, 64, 64);
            TMOVIMIENTOS.CustomBorderColor = Color.FromArgb(255, 255, 255);
        }

        private void txtbuscarKardex_movimientos_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscarKardex_movimientos.Text == "Buscar producto" | txtbuscarKardex_movimientos.Text == "")
            {
                DATALISTADO_PRODUCTOS_Kardex.Visible = false;
            }
            else 
            {
                buscar_productos_kardex();
            }
        }
        private void buscar_productos_kardex()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.CONEXIONMAESTRA.Conexion;
                con.Open();

                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", txtbuscarKardex_movimientos.Text);
                da.Fill(dt);
                DATALISTADO_PRODUCTOS_Kardex.DataSource = dt;
                con.Close();


                DATALISTADO_PRODUCTOS_Kardex.Columns[1].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[3].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[4].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[5].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[6].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[7].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[8].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[9].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[10].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[11].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[12].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[13].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[14].Visible = false;
                DATALISTADO_PRODUCTOS_Kardex.Columns[15].Visible = false;
                
                DATALISTADO_PRODUCTOS_Kardex.Visible = true;
                Bases.MultiLinea(ref DATALISTADO_PRODUCTOS_Kardex);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DATALISTADO_PRODUCTOS_Kardex_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DATALISTADO_PRODUCTOS_Kardex_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtbuscarKardex_movimientos.Text = DATALISTADO_PRODUCTOS_Kardex.SelectedCells[2].Value.ToString();
            DATALISTADO_PRODUCTOS_Kardex.Visible = false;
            mostrar_kardex_movimientos();
        }

        private void DATALISTADO_PRODUCTOS_Movimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnImprimirSinFiltro_Click(object sender, EventArgs e)
        {
            Presentacion.REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS.FormMovimientosBuscar frm = new REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS.FormMovimientosBuscar();
            frm.ShowDialog();
        }

        public static string Tipo_de_movimiento;
        public static DateTime fecha;
        public static int id_ususario;

        private void btnImprimirconFiltroAvanzado_Click(object sender, EventArgs e)
        {
            Tipo_de_movimiento = txtTipoMovi.Text;
            fecha = txtfechaM.Value;
            id_ususario = Convert.ToInt32(txtIdusuario.Text);
            Presentacion.REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS.FormReporteMovimientosFILTROS frm = new REPORTES.REPORTE_DE_KARDEX.REPORTE_DE_INVENTARIOS_TODOS.FormReporteMovimientosFILTROS();
            frm.ShowDialog();
            
        }

        private void lblEntrada_Click(object sender, EventArgs e)
        {
            KardexEntrada frm = new KardexEntrada();
            frm.ShowDialog();
        }

        private void lblSalida_Click(object sender, EventArgs e)
        {
            KardexSalida frm = new KardexSalida();
            frm.ShowDialog();
        }
    }
}
