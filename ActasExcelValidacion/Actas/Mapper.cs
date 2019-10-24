using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToExcel.Actas
{
    public class Mapper
    {
        public static List<Acta> ExcelToList(string path)
        {
            var actas = new List<Acta>();

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var rowId = 2;
                    reader.Read();
                    while (reader.Read())
                    {
                        actas.Add(new Acta
                        {
                            RowId = rowId++,
                            Pais = reader.GetString(0),
                            Departamento = reader.GetString(2),
                            Provincia = reader.GetString(3),
                            Municipio = reader.GetString(5),
                            Circunscripcion = reader.GetString(6),
                            Localidad = reader.GetString(7),
                            Recinto = reader.GetString(8),
                            NumeroMesa = (int)reader.GetDouble(9),
                            CodigoMesa = reader.GetString(10),
                            Eleccion = reader.GetString(11),
                            Inscritos = reader.GetDouble(12),
                            CC = reader.GetDouble(13),
                            FPV = reader.GetDouble(14),
                            MTS = reader.GetDouble(15),
                            UCS = reader.GetDouble(16),
                            MAS = reader.GetDouble(17),
                            F21 = reader.GetDouble(18),
                            PDC = reader.GetDouble(19),
                            MNR = reader.GetDouble(20),
                            PAN = reader.GetDouble(21),
                            Validos = reader.GetDouble(22),
                            Blancos = reader.GetDouble(23),
                            Nulos = reader.GetDouble(24)
                        });
                    }

                }
            }

            return actas;
        }


    }
}
