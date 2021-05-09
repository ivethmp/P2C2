using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Printf : Instr2
    {
        public int lin { get; set ; }
        public int col { get ; set; }
        String cadena;
        Expr2 expr;
        Simbolo.Tipo tipo;
        public Printf(String cadena, Expr2 expr, Simbolo.Tipo tipo, int lin, int col)
        {
            this.cadena = cadena;
            this.expr = expr;
            this.tipo = tipo;
            this.lin = lin;
            this.col = col;
        }
        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            String cad = "printf(" + cadena + ",("+tipo.ToString().ToLower()+")"+expr.getOptimizar(arbol,nuevo,reporte,temp)+");";
            nuevo.AddLast(new NewCod(cad));
            return null;
        }
    }
}
