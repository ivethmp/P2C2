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
        public Expr operDer { get; set; }
        public Expr operIzq { get; set; }
        public Expr operUna { get; set; }

        public Oper(Expr operIzq, Expr operDer, tipOper operador)
        {
            this.operador = operador;
            this.operIzq = operIzq;
            this.operDer = operDer;
        }
        public Oper(Expr operUna, tipOper operador)
        {
            this.operUna = operUna;
            this.operador = operador;
        }

        public static tipOper getOperador(string oper)
        {//retorna el tipo de operacion que se realiza
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
                case "<>": return tipOper.DIF;
                case "and": return tipOper.AND;
                case "or": return tipOper.OR;
                case "not": return tipOper.NOT;
                default: return tipOper.OTRO;
            }

        }
        public Simb.Tipo getTipo(Entor en, AST arbol)
        {//retorna el tipo de dato que se esta utilizando
            object val = this.getValImp(en, arbol);
            if (val is bool) return Tipo.BOOL;
            else if (val is int) return Tipo.INT;
            else if (val is Double) return Tipo.REAL;
            else if (val is String) return Tipo.STRING;
            return Tipo.OBJ;
        }

        public object getValImp(Entor en, AST arbol)
        {
            try
            {
                if (operUna == null)
                {
                    object op1 = operIzq.getValImp(en, arbol);
                    object op2 = operDer.getValImp(en, arbol);
                    #region Sum
                    if (operador == tipOper.SUMA)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 + (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) + Convert.ToDecimal(op2);
                        }
                        
                        else
                        {
                            Form1.error.AppendText("Error, datos incorrectos para realizar Suma");
                            return null;
                        }
                    }
                    #endregion
                    #region Concatenar
                    else if (operador == tipOper.CONCAT)
                    {
                        return op1.ToString() + op2.ToString();      
                    }
                    #endregion
                    #region Resta
                    else if (operador == tipOper.RESTA)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 - (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) - Convert.ToDecimal(op2);
                        }
                        else if (op1 is String || op2 is String)
                        {
                            Form1.error.AppendText("Error, datos incorrectos para realizar Resta");
                            return null;
                        }
                    }
                    #endregion
                    #region Multi
                    else if (operador == tipOper.POR)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 * (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) * Convert.ToDecimal(op2);
                        }
                        else if (op1 is String || op2 is String)
                        {
                            Form1.error.AppendText("Error, datos incorrectos para realizar Multiplicacion");
                            return null;
                        }
                    }
                    #endregion
                    #region Division
                    else if (operador == tipOper.DIVISION)
                    {
                        if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            if((int)op2 == 0)
                            {
                                Form1.salir.AppendText("Error, No se puede dividir por 0, infinito");
                                return null;
                            }
                            if (op1 is int && op2 is int)
                            {
                                return (int)op1 / (int)op2;
                            }
                                return Convert.ToDecimal(op1) / Convert.ToDecimal(op2);
                        }
                        else 
                        {
                            Form1.error.AppendText("Error, datos incorrectos para realizar Division");
                            return null;
                        }
                    }
                    #endregion
                    #region MOD
                    else if (operador == tipOper.MOD)
                    {
                        if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            if ((int)op2 == 0)
                            {
                                Form1.salir.AppendText("Error, No se puede utilizar MOD por 0, infinito");
                                return null;
                            }
                            if (op1 is int && op2 is int)
                            {
                                return (int)op1 % (int)op2;
                            }
                            return Convert.ToDecimal(op1) % Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error, datos incorrectos para realizar MOD");
                            return null;
                        }
                    }
                    #endregion
                    //Aqui tendrian que ir el resto de operaciones RELACIONALES
                    #region MayorQue
                    if (operador == tipOper.MAYORQ)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 > (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) > Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \">\" datos No numericos");
                            return null;
                        }
                    }
                    #endregion
                    #region MayorIgualQue
                    if (operador == tipOper.MAYIOQ)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 >= (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) >= Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \">\" datos No numericos");
                            return null;
                        }
                    }
                    #endregion
                    #region MenorQue
                    if (operador == tipOper.MENORQ)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 < (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) < Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \"<\" datos No numericos");
                            return null;
                        }
                    }
                    #endregion
                    #region MenorIgualQue
                    if (operador == tipOper.MENIQ)
                    {
                        if (op1 is int && op2 is int)
                            return (int)op1 <= (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) <= Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \">\" datos No numericos");
                            return null;
                        }
                    }
                    #endregion
                    #region igual
                    if (operador == tipOper.IGUAL)
                    {
                        return (op1.Equals(op2)) ;
                    }
                    #endregion
                    #region diferente
                    if (operador == tipOper.IGUAL)
                    {
                        return !(op1.Equals(op2));
                    }
                    #endregion
                    #region and
                    else if (operador == tipOper.AND)
                    {
                        if (op1 is bool && op2 is bool)
                        {
                            return (bool)op1 && (bool)op2;
                        }
                        else
                        {
                            Form1.error.AppendText("Error, no se permiten datos que no sean boolean");
                            return null;
                        }
                    }
                    #endregion
                    #region or
                    else if (operador == tipOper.OR)
                    {
                        if (op1 is bool && op2 is bool)
                        {
                            return (bool)op1 || (bool)op2;
                        }
                        else
                        {
                            Form1.error.AppendText("Error, no se permiten datos que no sean boolean");
                            return null;
                        }
                    }
                    #endregion

                }
                else
                {
                    #region unario
                    object operUn = operUna.getValImp(en, arbol);
                    if (this.operador == tipOper.MENOSU)
                    {
                        if (operUn is Decimal || operUn is double) return -1 * (Decimal)operUn;
                        
                        else if (operUn is int) return -1 * (int)operUn;
                        
                        else
                        {
                            Form1.error.AppendText("Error,No se puede operar menos unario con este tipo de datos");
                            return null;
                        }
                    }
                    else if (operador == tipOper.NOT)
                    {
                        if (operUn is bool) return !(bool)operUn;
                        
                        else
                        {
                            Form1.error.AppendText("Error, no se puede negar este tipo de datos");
                            return null;
                        }
                    }
                    #endregion
                }


            }
            catch
            {
                Form1.error.AppendText("Error, imposible realizar operacion ");
            }
            return val;
        }
    }
}
