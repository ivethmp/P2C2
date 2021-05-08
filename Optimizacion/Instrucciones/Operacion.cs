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
            IIGUAL,
            NOT,
            DIF,
            AND,
            OR,
            OTRO
        }

        public tipOper operador;

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
                case "==": return tipOper.IIGUAL;
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
               
            if (operUna == null)
            {

                //la variable tendria que retornar el temporal donde se asigna el valor de la variable en el stack

                object op1 = operIzq.getOptimizar(arbol, nuevo, reporte, temp);
                

                object op2 = operDer.getOptimizar(arbol, nuevo, reporte, temp);

               
                #region Sum
                if (operador == tipOper.SUMA)
                {
                    if (lin == -10) return op1.ToString() + "+" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else
                    {
                        String[] valores = new String[2];
                        if (op1 is int || op1 is Decimal)
                        {
                            if (Convert.ToDecimal(op1)==0)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "+" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = "" + Convert.ToDecimal(op1);
                    }

                    if (op2 is String) salida = salida + "+" + op2.ToString();
                    else
                    {

                        String[] valores = new String[2];
                        if (op2 is int || op2 is Decimal)
                        {
                            if (Convert.ToDecimal(op2) == 0)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "+" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = salida + "+" + Convert.ToDecimal(op2);
                    }

                    return salida;

                }
                #endregion
                
                #region Resta
                else if (operador == tipOper.RESTA)
                {

                    if (lin == -10) return op1.ToString() + "-" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else
                    {
                        /*String[] valores = new String[2];
                        if (op1 is int || op1 is Decimal)
                        {
                            if (Convert.ToDecimal(op1) == 0)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "-" + op2.ToString();
                                return valores;
                            }
                        }*/
                        salida = "" + Convert.ToDecimal(op1);
                    }

                    if (op2 is String) salida = salida + "-" + op2.ToString();
                    else
                    {

                        String[] valores = new String[2];
                        if (op2 is int || op2 is Decimal)
                        {
                            if (Convert.ToDecimal(op2) == 0)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "-" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = salida + "-" + Convert.ToDecimal(op2);
                    }

                    return salida;

                }
                #endregion
                #region Multi
                else if (operador == tipOper.POR)
                {
                    if (lin == -10) return op1.ToString() + "*" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else
                    {
                        String[] valores = new String[2];
                        if (op1 is int || op1 is Decimal)
                        {
                            if (Convert.ToDecimal(op1) == 1)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "*" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = "" + Convert.ToDecimal(op1);
                    }

                    if (op2 is String) salida = salida + "*" + op2.ToString();
                    else
                    {

                        String[] valores = new String[2];
                        if (op2 is int || op2 is Decimal)
                        {
                            if (Convert.ToDecimal(op2) == 1)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "*" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = salida + "*" + Convert.ToDecimal(op2);
                    }

                    return salida;

                }
                #endregion
                #region Division
                else if (operador == tipOper.DIVISION)
                {
                    if (lin == -10) return op1.ToString() + "/" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else
                    {
                        salida = "" + Convert.ToDecimal(op1);
                    }

                    if (op2 is String) salida = salida + "/" + op2.ToString();
                    else
                    {

                        String[] valores = new String[2];
                        if (op2 is int || op2 is Decimal)
                        {
                            if (Convert.ToDecimal(op2) == 1)
                            {
                                valores[0] = "false";
                                valores[1] = op1.ToString() + "/" + op2.ToString();
                                return valores;
                            }
                        }
                        salida = salida + "/" + Convert.ToDecimal(op2);
                    }

                    return salida;
                }
                #endregion
                



                //Aqui tendrian que ir el resto de operaciones RELACIONALES
                #region MayorQue
                if (operador == tipOper.MAYORQ)
                {
                    if (lin == -10) return op1.ToString() + ">" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else salida = "" + Convert.ToDecimal(op1);
                    
                    if (op2 is String) salida = salida + "==" + op2.ToString();
                    else salida = salida + ">" + Convert.ToDecimal(op2);
                
                    return salida;
                }
                #endregion
                #region MayorIgualQue
                if (operador == tipOper.MAYIOQ)
                {
                    if (lin == -10) return op1.ToString() + ">=" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else salida = "" + Convert.ToDecimal(op1);

                    if (op2 is String) salida = salida + ">=" + op2.ToString();
                    else salida = salida + "==" + Convert.ToDecimal(op2);

                    return salida;

                }
                #endregion
                #region MenorQue
                if (operador == tipOper.MENORQ)
                {
                    if (lin == -10) return op1.ToString() + "<" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else salida = "" + Convert.ToDecimal(op1);

                    if (op2 is String) salida = salida + "<=" + op2.ToString();
                    else salida = salida + "==" + Convert.ToDecimal(op2);

                    return salida;

                }
                #endregion
                #region MenorIgualQue
                if (operador == tipOper.MENIQ)
                {
                    if (lin == -10) return op1.ToString() + "<=" + op2.ToString();
                    String salida = "";
                    if (op1 is String) salida = op1.ToString();
                    else salida = "" + Convert.ToDecimal(op1);

                    if (op2 is String) salida = salida + "<=" + op2.ToString();
                    else salida = salida + "==" + Convert.ToDecimal(op2);

                    return salida;

                }
                #endregion
                #region igual
                if (operador == tipOper.IIGUAL)
                {
                    String salida = "";
                    if(op1 is String)//significa que una variable x
                    {
                        salida = op1.ToString();
                    }else//es un numero
                    {
                        if(op2 is Decimal || op2 is int)
                        {
                            String[] sal = new string[2];
                            sal[0] = op1.ToString() + "==" + op2.ToString();
                            if (Convert.ToDecimal(op1) == Convert.ToDecimal(op2)) sal[1] = "true";
                            else sal[1] = "false";
                            return sal;
                        }

                        salida = ""+Convert.ToDecimal(op1);
                    }
                    if (op2 is String) salida = salida +"!="+ op2.ToString();
                    else
                    {
                        salida = salida + "!=" + Convert.ToDecimal(op2);
                    }

                    return salida;
                    //return (op1.Equals(op2)) ;
                }
                #endregion
                #region diferente
                if (operador == tipOper.DIF)
                {
                    if (lin == -10) return op1.ToString() + "!=" + op2.ToString();
                    String salida = "";
                    if (op1 is String)//significa que una variable x
                    {
                        salida = op1.ToString();
                    }
                    else//es un numero
                    {
                        salida = "" + Convert.ToDecimal(op1);
                    }
                    if (op2 is String) salida = salida + "==" + op2.ToString();
                    else
                    {
                        salida = salida + "==" + Convert.ToDecimal(op2);
                    }

                    return salida;
                    //return !(op1.Equals(op2));
                }
                #endregion
              
             

            }
            else
            {
                #region unario
                object operUn = operUna.getOptimizar(arbol, nuevo, reporte, temp);
                if (this.operador == tipOper.MENOSU)
                {
                   
                }
                
                #endregion
            }


            /*   }
              catch
               {
                   Form1.error.AppendText("Error, imposible realizar operacion ");
               }*/
            return val;
        }

    }
}
