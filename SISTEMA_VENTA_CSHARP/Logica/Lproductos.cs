using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_VENTA_CSHARP.Logica
{
    public class Lproductos
    {
        public int Id_Producto { get; set; }
        public string Descripcion { get; set; }
       
        public int Id_Laboratorio { get; set; }
        public string Usa_inventario { get; set; }
        public string Stock { get; set; }
        public double Precio_de_compra { get; set; }
        public string Fecha_de_vencimiento { get; set; }
        public double Precio_de_venta { get; set; }
        public string Codigo { get; set; }
        public string Se_vende_a { get; set; }
        public string Impuesto { get; set; }
        public double Stock_minimo { get; set; }
        
        public double Sub_total_pv { get; set; }
        
    }
}
