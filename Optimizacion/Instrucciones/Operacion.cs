using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Operacion:Expr2
    {
        public int lin { get; set; }
        public int col { get; set; }
      
        public enum tipOper
        {
            SUMA,
            CONCAT,
            RESTA,
            POR,
            DIVISION,
            MOD,
            MENOSU,
            MAYORQ,
            MENORQ,
            MENIQ,
            MAYIOQ,
            IGUAL,
            NOT,
            DIF,
            AND,
            OR,
            OTRO
        }

        private tipOper operador;
        public object val;
        public Expr2 operDer { get; set; }
        public Expr2 operIzq { get; set; }
        public Expr2 operUna { get; set; }

        public static tipOper getOperador(string oper)
        {//retorna el tipo de operacion que se realiza
            oper = oper.ToLower();
            switch (oper)
            {
                case "+": return tipOper.SUMA;
                case ",": return tipOper.CONCAT;
                case "-": return tipOper.RESTA;
                case "*": return tipOper.POR;
                case "/": return tipOper.DIVISION;
                case "<": return tipOper.MENORQ;
                case ">": return tipOper.MAYORQ;
                case ">=": return tipOper.MAYIOQ;
                case "<=": return tipOper.MENIQ;
                case "=": return tipOper.IGUAL;
                case "!=": return tipOper.DIF;
                default: return tipOper.OTRO;
            }

        }

        public Operacion(Expr2 operIzq, Expr2 operDer, tipOper operador)
        {
            this.operador = operador;
            this.operIzq = operIzq;
            this.operDer = operDer;
        }
        public Operacion(Expr2 operUna, tipOper operador)
        {
            this.operUna = operUna;
            this.operador = operador;
        }


        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo,LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return null;
        }

    }
}
