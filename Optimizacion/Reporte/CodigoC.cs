using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Reporte
{
    class CodigoC
    {
        public String tipoOp { get; set; }
        public int regla { get; set; }
        public String CodDelete { get; set; }
        public String CodAdd { get; set; }
        public int fila { get; set; }
        public CodigoC(String tipoOp, int regla, String CodDelete, String CodAdd, int fila)
        {
            this.tipoOp = tipoOp;
            this.regla = regla;
            this.CodDelete = CodDelete;
            this.CodAdd = CodAdd;
            this.fila = fila;

        }
    }
}
