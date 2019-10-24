using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToExcel.Actas
{
    public class Acta
    {
        public int RowId { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Municipio { get; set; }
        public string Circunscripcion { get; set; }
        public string Localidad { get; set; }
        public string Recinto { get; set; }

        public int NumeroMesa { get; set; }

        public string CodigoMesa { get; set; }
        public string Eleccion { get; set; }

        public double Inscritos { get; set; }
        public double CC { get; set; }
        public double FPV { get; set; }
        public double MTS { get; set; }
        public double UCS { get; set; }
        public double MAS { get; set; }
        public double F21 { get; set; }
        public double PDC { get; set; }
        public double MNR { get; set; }
        public double PAN { get; set; }
        public double Validos { get; set; }
        public double Blancos { get; set; }
        public double Nulos { get; set; }

    }
}
