using System;
using System.Collections.Generic;
using System.Linq;
using ToExcel.Actas;

namespace ActasExcelValidacion
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string filePath = @"c:\actas\acta.2019.10.24.17.22.43.xlsx";
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            var actas = Mapper.ExcelToList(filePath);

            var actasPresidenciales = actas.Where(a => a.Eleccion.Contains("Presidente")).ToList();
            Console.WriteLine($"Total presidenciales: {actasPresidenciales.Count}");
            var masPorcentajeAltos = new List<Acta>();

            Console.WriteLine("calculando totales...");

            double totalCalculado = 0;
            double totalValidos = 0;
            double CC = 0;
            double FPV = 0;
            double MTS = 0;
            double UCS = 0;
            double MAS = 0;
            double F21 = 0;
            double PDC = 0;
            double MNR = 0;
            double PAN = 0;
            double blancos = 0;
            double nulos = 0;

            var actasErroneas = new List<Acta>();
            double highPercentage = 0.6;
            foreach (var a in actasPresidenciales)
            {
                // errores
                double validos = a.CC + a.FPV + a.MTS + a.UCS + a.MAS + a.F21 + a.PDC + a.MNR + a.PAN;
                if (validos != a.Validos)
                {
                    actasErroneas.Add(a);
                }

                // mas, porcentajes altos
                if (a.Validos > 0 && validos > 0 && a.MAS / validos >= highPercentage)
                {
                    var mpercent = a.MAS / validos;
                    masPorcentajeAltos.Add(a);
                }

                // calculo totales
                totalCalculado += validos;

                CC += a.CC;
                FPV += a.FPV;
                MTS += a.MTS;
                UCS += a.UCS;
                MAS += a.MAS;
                F21 += a.F21;
                PDC += a.PDC;
                MNR += a.MNR;
                PAN += a.PAN;

                blancos += a.Blancos;
                nulos += a.Nulos;
                totalValidos += a.Validos;
            }
            Console.WriteLine($"Total con errores: {actasErroneas.Count}");
            System.Console.WriteLine("Fila, Departamento, Numero Mesa, Codigo Mesa, Calculado/Registrado");
            foreach (var a in actasErroneas)
            {
                var calculado = a.CC + a.FPV + a.MTS + a.UCS + a.MAS + a.F21 + a.PDC + a.MNR + a.PAN;
                Console.WriteLine($"{a.RowId}, {a.Departamento}, {a.NumeroMesa}, {a.CodigoMesa} :{calculado}/{a.Validos}");
            }

            Console.WriteLine($"MAS > {100 * highPercentage}%: {masPorcentajeAltos.Count} actas");
            //foreach (var a in masPorcentajeAltos)
            //{
            //    Console.WriteLine($"{a.RowId}, {a.Departamento}, {a.Municipio}, {a.NumeroMesa}, : {(100.00 * ((double)a.MAS / (double)a.Validos)):0.##}%");
            //}

            var ccPercent = (CC / totalCalculado) * 100;
            var masPercent = (MAS / totalCalculado) * 100;
            Console.WriteLine($"Total de validos: {totalValidos}");
            Console.WriteLine($"Total validos (calculado): {totalCalculado}");
            Console.WriteLine($"CC: {CC}, \t\t { ccPercent }%");
            Console.WriteLine($"MAS: {MAS}, \t\t { masPercent }%");

            Console.WriteLine($"Diferencia: {masPercent - ccPercent} %");
        }
    }
}
