using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Reporte
{
    class CodigoC
    {
        String tipoOp;
        int regla;
        String CodDelete;
        String CodAdd;
        public CodigoC(String tipoOp, int regla, String CodDelete, String CodAdd)
        {
            this.tipoOp = tipoOp;
            this.regla = regla;
            this.CodDelete = CodDelete;
            this.CodAdd = CodAdd;
        }
    }
}
