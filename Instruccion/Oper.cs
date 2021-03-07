using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;
using static P1.TS.Simb;

namespace P1.Instruccion
{
    class Oper : Expr
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }


        public enum tipOper
        {
            SUMA,
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
            OTRO
        }


        private tipOper operador;
        public object val;
        public Expr operDer { get; set; }
        public Expr operIzq { get; set; }
        public Expr operUna { get; set; }

        public Oper(Expr operIzq, Expr operDer, tipOper operador)
        {
            this.operador = operador;
            this.operIzq = operIzq;
            this.operDer = operDer;
        }

        public static tipOper getOperador(string oper)
        {//retorna el tipo de operacion que se realiza
            switch(oper)
            {
                case "+": return tipOper.SUMA;
                case "-": return tipOper.RESTA;
                case "*": return tipOper.POR;
                case "/": return tipOper.DIVISION;
                case "<": return tipOper.MENORQ;
                case ">": return tipOper.MAYORQ;
                case ">=": return tipOper.MAYIOQ;
                case "<=": return tipOper.MENIQ;
                case "=": return tipOper.IGUAL;
                default: return tipOper.OTRO;
            }
          
        }
        public Simb.Tipo getTipo(Entor en, TabS tabS)
        {//retorna el tipo de dato que se esta utilizando
            object val = this.getValImp(en, tabS);
            if (val is bool) return Tipo.BOOL;
            else if (val is int) return Tipo.INT;
            else if (val is Double) return Tipo.REAL;
            else if (val is String) return Tipo.STRING;
            return Tipo.OBJ;
        }

        public object getValImp(Entor en, TabS tabS)
        {
            try
            {
                if(operUna == null)
                {
                    object op1 = operIzq.getValImp(en, tabS);
                    object op2 = operDer.getValImp(en, tabS);
                    #region Sum
                    if(operador == tipOper.SUMA)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 + (int)op2;
                        
                    }
                }
            }
            return val;
        }
    }
}
