using SISTEMA_VENTA_CSHARP.Datos;
using SISTEMA_VENTA_CSHARP.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA_VENTA_CSHARP.Presentacion.CAJA
{
    public partial class CierreTurno : Form
    {
        public CierreTurno()
        {
            InitializeComponent();
        }

        double dinerocalculado;
        double resultado;
        int idusuario;
        int idcaja;
        string usuario;

        private void CierreTurno_Load(object sender, EventArgs e)
        {
            lblDeberiaHaber.Text = CIERRE_DE_CAJA.dineroencaja.ToString();
            dinerocalculado = Convert.ToDouble(lblDeberiaHaber.Text);
        }

        private void txthay_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }

        private void validacionesCalculo()
        {
            if (resultado == 0)
            {
                lblanuncio.Text = "Genial, Todo esta perfecto";
                lblanuncio.ForeColor = Color.FromArgb(0, 166, 63);
                lbldiferencia.ForeColor = Color.FromArgb(0, 166, 63);
                lblanuncio.Visible = true;

            }
            if (resultado < dinerocalculado & resultado != 0)
            {
                lblanuncio.Text = "La diferencia sera Registrada en su Turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;

            }
            if (resultado > dinerocalculado)
            {
                lblanuncio.Text = "La diferencia sera Registrada en su Turno y se enviara a Gerencia";
                lblanuncio.ForeColor = Color.FromArgb(231, 63, 67);
                lbldiferencia.ForeColor = Color.FromArgb(231, 63, 67);
                lblanuncio.Visible = true;
            }
        }

        private void calcular()
        {
            try
            {


                double hay;
                hay = Convert.ToDouble(txthay.Text);
                if (string.IsNullOrEmpty(txthay.Text))
                {
                    hay = 0;
                }
                resultado = hay - dinerocalculado;
                lbldiferencia.Text = resultado.ToString();
                validacionesCalculo();
            }
            catch (Exception)
            {


            }

        }

        private void cerrarCaja()
        {

            Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);

            Lmcaja parametros = new Lmcaja();
            Editar_datos funcion = new Editar_datos();
            parametros.fechafin = DateTime.Now;
            parametros.fechacierre = DateTime.Now;
            parametros.ingresos = CIERRE_DE_CAJA.Ingresos;
            parametros.egresos = CIERRE_DE_CAJA.Egresos;
            parametros.Saldo_queda_en_caja = 0;
            parametros.Id_usuario = idusuario;
            parametros.Total_calculado = dinerocalculado;
            parametros.Total_real = Convert.ToDouble(txthay.Text);
            parametros.Estado = "CAJA CERRADA";
            parametros.Diferencia = resultado;
            parametros.Id_caja = idcaja;
            if (funcion.cerrarCaja(parametros) == true)
            {
                DialogResult dlgRes = MessageBox.Show("CAJA CERRADA EXITOSAMENTE", "CERRANDO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dlgRes == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            cerrarCaja();
        }
    }
}
