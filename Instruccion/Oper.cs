using P1.Arbol;
using P1.Generacion;
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
                case "<>": return tipOper.DIF;
                case "and": return tipOper.AND;
                case "or": return tipOper.OR;
                case "not": return tipOper.NOT;
                default: return tipOper.OTRO;
            }

        }
        public Simb.Tipo getTipo(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {//retorna el tipo de dato que se esta utilizando
            object val = this.getValImp(gen,en, arbol,inter);
            if (val is bool) return Tipo.BOOL;
            else if (val is int) return Tipo.INT;
            else if (val is Double) return Tipo.REAL;
            else if (val is String) return Tipo.STRING;
            return Tipo.OBJ;
        }

        public String getEtiq(LinkedList<Instruc> inter, String cond)
        {
            String etiqueta = "";
            foreach (Etiq ins in inter)
            {
                if (ins.cond == cond) etiqueta = etiqueta + "L" + ins.numero + ":\n";
            }
            return etiqueta;
        }


        public object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
          //  try
            //{
                if (operUna == null)
                {
                    //la variable tendria que retornar el temporal donde se asigna el valor de la variable en el stack
                    object op1 = operIzq.getValImp(gen,en, arbol, inter);
                    object op2 = operDer.getValImp(gen,en, arbol, inter);
                    #region Sum
                    if (operador == tipOper.SUMA)
                    {
                        Instruc temp = new Temp(inter);
                        String tempo = (String)temp.ejecutar(gen,en,arbol,inter);
                        inter.AddLast(temp);//agrego el nuevo temporal

                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "+", tempo, "", ""));
                        return tempo;
                        
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
                        Instruc temp = new Temp(inter);
                        String tempo = (String)temp.ejecutar(gen,en, arbol, inter);
                        inter.AddLast(temp);//agrego el nuevo temporal

                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "-", tempo, "", ""));
                        return tempo;
                        
                    }
                    #endregion
                    #region Multi
                    else if (operador == tipOper.POR)
                    {
                        Instruc temp = new Temp(inter);
                        String tempo = (String)temp.ejecutar(gen,en, arbol, inter);
                        inter.AddLast(temp);//agrego el nuevo temporal

                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "*", tempo, "", ""));
                        return tempo;

                    }
                    #endregion
                    #region Division
                    else if (operador == tipOper.DIVISION)
                    {
                       if(op2 is int)
                        {
                            if((int)op2 ==0 )
                            {
                                Form1.salir.AppendText("Error, No se puede dividir por 0, infinito");
                                return null;
                            }
                        }

                        Instruc temp = new Temp(inter);
                        String tempo = (String)temp.ejecutar(gen,en, arbol, inter);
                        inter.AddLast(temp);//agrego el nuevo temporal

                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "/", tempo, "", ""));
                        return tempo;
                        /*
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
                         }*/
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
                        LinkedList <Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1),Convert.ToString(op2),">","REL",Convert.ToString(eti.ejecutar(gen,en,arbol,inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        
                        return etiquetas;
                        
                    }
                    #endregion
                    #region MayorIgualQue
                    if (operador == tipOper.MAYIOQ)
                    {
                        LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), ">=", "REL", Convert.ToString(eti.ejecutar(gen,en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        return etiquetas;
                        /*if (op1 is int && op2 is int)
                            return (int)op1 >= (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) >= Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \">\" datos No numericos");
                            return null;
                        }*/
                    }
                    #endregion
                    #region MenorQue
                    if (operador == tipOper.MENORQ)
                    {
                        LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "<", "REL", Convert.ToString(eti.ejecutar(gen,en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        return etiquetas;
                        /*if (op1 is int && op2 is int)
                            return (int)op1 < (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) < Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \"<\" datos No numericos");
                            return null;
                        }*/
                    }
                    #endregion
                    #region MenorIgualQue
                    if (operador == tipOper.MENIQ)
                    {
                        LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "<=", "REL", Convert.ToString(eti.ejecutar(gen,en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        return etiquetas;
                        /*if (op1 is int && op2 is int)
                            return (int)op1 <= (int)op2;

                        else if ((op1 is int || op1 is double || op1 is Decimal) && (op2 is int || op2 is double || op2 is Decimal))
                        {
                            return Convert.ToDecimal(op1) <= Convert.ToDecimal(op2);
                        }
                        else
                        {
                            Form1.error.AppendText("Error Sintactico, No se pueden Comparar \">\" datos No numericos");
                            return null;
                        }*/
                    }
                    #endregion
                    #region igual
                    if (operador == tipOper.IGUAL)
                    {
                        LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "==", "REL", Convert.ToString(eti.ejecutar(gen,en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        return etiquetas;
                        //return (op1.Equals(op2)) ;
                    }
                    #endregion
                    #region diferente
                    if (operador == tipOper.DIF)
                    {
                        LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
                        Etiq eti = new Etiq(inter, "true");
                        inter.AddLast(eti);
                        etiquetas.AddLast(eti);
                        Etiq etiF = new Etiq(inter, "false");
                        inter.AddLast(etiF);
                        etiquetas.AddLast(etiF);
                        inter.AddLast(new GenCod(Convert.ToString(op1), Convert.ToString(op2), "!=", "REL", Convert.ToString(eti.ejecutar(gen,en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen,en, arbol, inter))));
                        return etiquetas;
                        //return !(op1.Equals(op2));
                    }
                    #endregion
                    #region and
                    else if (operador == tipOper.AND)
                    {
                       // LinkedList<Instruc> etider = (LinkedList<Instruc>)op1;
                     //   LinkedList<Instruc> etiizq = (LinkedList<Instruc>)op2;


                        LinkedList<Instruc> etider = new LinkedList<Instruc>();
                    if (op1 is LinkedList<Instruc>) etider = (LinkedList<Instruc>)op1;
                    else
                    {
                        if (op1 != null)
                        {
                            if (op1 is bool)
                            {
                                if ((bool)op1 == true) op1 = 1;
                                else op1 = 0;
                            }
                            Etiq eti = new Etiq(inter, "true");
                            inter.AddLast(eti);
                            etider.AddLast(eti);
                            Etiq etiF = new Etiq(inter, "false");
                            inter.AddLast(etiF);
                            etider.AddLast(etiF);
                            inter.AddLast(new GenCod("", Convert.ToString(op1), "", "REL", Convert.ToString(eti.ejecutar(gen, en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen, en, arbol, inter))));
                            //etider;
                        }else return null;
                    }
                    LinkedList<Instruc> etiizq = new LinkedList<Instruc>();
                    if (!(op1 is LinkedList<Instruc>) && op2 is LinkedList<Instruc>)
                    {
                        etiizq = (LinkedList<Instruc>)op2;
                        inter.AddBefore(inter.Last, new GenCod("", "", "", "LOG", getEtiq(etiizq, "true"), ""));

                        foreach (Etiq eti in etiizq)
                        {
                            if (eti.cond == "false")
                            {
                                etider.AddLast(eti);
                            }
                        }
                        //retorno las etiquetas falsas y verdaderas del lado izquierdo y la verdadera del lado derecho
                        return etider;
                    }
                    else
                        inter.AddBefore(inter.Last, new GenCod("", "", "", "LOG", getEtiq(etider, "true"), ""));

                    
                    if (op2 is LinkedList<Instruc>)
                    {
                        etiizq = (LinkedList<Instruc>)op2;

                    }

                    else
                    {
                        if (op2 != null)
                        {
                            if (op2 is bool)
                            {
                                if ((bool)op2 == true) op2 = 1;
                                else op2 = 0;
                            }
                            Etiq eti = new Etiq(inter, "true");
                            inter.AddLast(eti);
                            etiizq.AddLast(eti);
                            Etiq etiF = new Etiq(inter, "false");
                            inter.AddLast(etiF);
                            etiizq.AddLast(etiF);
                            inter.AddLast(new GenCod("", Convert.ToString(op2), "", "REL", Convert.ToString(eti.ejecutar(gen, en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen, en, arbol, inter))));

                        }else  return null;


                    }

                    /*if (etider == null)
                    {
                        etider = new LinkedList<Instruc>();
                        if (op1 is int || op1 is decimal)
                        {
                            if (Convert.ToDecimal(op1) == 0)
                            {
                                etider.AddLast(new Etiq(inter, "false"));
                                inter.AddLast(new Etiq(inter, "false"));
                            }
                            etider.AddLast(new Etiq(inter, "true"));
                            inter.AddLast(new Etiq(inter, "true"));
                        }
                    }*/


                    /*if (etiizq == null)
                        {
                            etiizq = new LinkedList<Instruc>();
                            if (op2 is int || op2 is decimal)
                            {
                                if (Convert.ToDecimal(op2) == 0)
                                {
                                    etiizq.AddLast(new Etiq(inter, "false"));
                                    inter.AddLast(new Etiq(inter, "false"));
                                }
                                etiizq.AddLast(new Etiq(inter, "true"));
                                inter.AddLast(new Etiq(inter, "true"));
                            }
                        }*/

                        
                        //obtengo las etiquetas falsas derecho para asi agregagarla a a lista de salida de etiquetas
                        foreach (Etiq eti in etider)
                        {
                            if(eti.cond == "false")
                            {
                                etiizq.AddLast(eti);
                            }
                        }
                        return etiizq;

/*
                        if (op1 is bool && op2 is bool)
                        {
                            return (bool)op1 && (bool)op2;
                        }
                        else
                        {
                            Form1.error.AppendText("Error, no se permiten datos que no sean boolean");
                            return null;
                        }*/
                    }
                    #endregion
                    #region or
                    else if (operador == tipOper.OR)
                    {
                    /* LinkedList<Instruc> etider = (LinkedList<Instruc>)op1;
                     LinkedList<Instruc> etiizq = (LinkedList<Instruc>)op2;

                     if (etider == null)
                     {
                         etider = new LinkedList<Instruc>();
                         if (op1 is int || op1 is decimal)
                         {
                             if (Convert.ToDecimal(op1) == 0)
                             {
                                 etider.AddLast(new Etiq(inter, "false"));
                                 inter.AddLast(new Etiq(inter, "false"));
                             }
                             etider.AddLast(new Etiq(inter, "true"));
                             inter.AddLast(new Etiq(inter, "true"));
                         }
                     }

                     if (etiizq == null)
                     {
                         etiizq = new LinkedList<Instruc>();
                         if (op2 is int || op2 is decimal)
                         {
                             if (Convert.ToDecimal(op2) == 0)
                             {
                                 etiizq.AddLast(new Etiq(inter, "false"));
                                 inter.AddLast(new Etiq(inter, "false"));
                             }
                             etiizq.AddLast(new Etiq(inter, "true"));
                             inter.AddLast(new Etiq(inter, "true"));
                         }
                     }*/

                    LinkedList<Instruc> etider = new LinkedList<Instruc>();
                    LinkedList<Instruc> etiizq = new LinkedList<Instruc>();
                    if (op1 is LinkedList<Instruc>)
                    {
                        etider = (LinkedList<Instruc>)op1;
                        
                    }
                    else
                    {
                        if (op1 != null)
                        {
                            
                            if (op1 is bool)
                            {
                                if ((bool)op1 == true) op1 = 1;
                                else op1 = 0;
                            }
                            Etiq eti = new Etiq(inter, "true");
                            inter.AddLast(eti);
                            etider.AddLast(eti);
                            Etiq etiF = new Etiq(inter, "false");
                            inter.AddLast(etiF);
                            etider.AddLast(etiF);
                            inter.AddLast(new GenCod("", Convert.ToString(op1), "", "REL", Convert.ToString(eti.ejecutar(gen, en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen, en, arbol, inter))));
                           // if(op2 is LinkedList<Instruc>)//quiere decir que trae etiquetas
                            //inter.AddBefore(inter.Last, new GenCod("", "", "", "LOG", getEtiq(etider, "false"), ""));
                            //etider;
                        }
                        else return null;
                    }

                    if(!(op1 is LinkedList<Instruc>) && op2 is LinkedList<Instruc>)
                    {
                        etiizq = (LinkedList<Instruc>)op2;
                        inter.AddBefore(inter.Last, new GenCod("", "", "", "LOG", getEtiq(etiizq, "false"), ""));

                        foreach (Etiq eti in etiizq)
                        {
                            if (eti.cond == "true")
                            {
                                etider.AddLast(eti);
                            }
                        }
                        //retorno las etiquetas falsas y verdaderas del lado izquierdo y la verdadera del lado derecho
                        return etider;
                    }
                    else
                        inter.AddBefore(inter.Last, new GenCod("", "", "", "LOG", getEtiq(etider, "false"), ""));
                

                    

                    
                    if (op2 is LinkedList<Instruc>)
                    {
                        etiizq = (LinkedList<Instruc>)op2;

                    }

                    else
                    {
                        if (op2 != null)
                        {
                            if (op2 is bool)
                            {
                                if ((bool)op2 == true) op2 = 1;
                                else op2 = 0;
                            }
                            Etiq eti = new Etiq(inter, "true");
                            inter.AddLast(eti);
                            etiizq.AddLast(eti);
                            Etiq etiF = new Etiq(inter, "false");
                            inter.AddLast(etiF);
                            etiizq.AddLast(etiF);
                            inter.AddLast(new GenCod("", Convert.ToString(op2), "", "REL", Convert.ToString(eti.ejecutar(gen, en, arbol, inter)), Convert.ToString(etiF.ejecutar(gen, en, arbol, inter))));
                            

                        }
                        else return null;


                    }


                        //obtengo las etiquetas verdaderaas derecho para asi agregagarla a a lista de salida de etiquetas
                        foreach (Etiq eti in etider)
                        {
                            if (eti.cond == "true")
                            {
                                etiizq.AddLast(eti);
                            }
                        }
                        //retorno las etiquetas falsas y verdaderas del lado izquierdo y la verdadera del lado derecho
                        return etiizq;
                        /*if (op1 is bool && op2 is bool)
                        {
                            return (bool)op1 || (bool)op2;
                        }
                        else
                        {
                            Form1.error.AppendText("Error, no se permiten datos que no sean boolean");
                            return null;
                        }*/
                    }
                    #endregion

                }
                else
                {
                    #region unario
                    object operUn = operUna.getValImp(gen,en, arbol, inter);
                    if (this.operador == tipOper.MENOSU)
                    {
                        Instruc temp = new Temp(inter);
                        String tempo = (String)temp.ejecutar(gen, en, arbol, inter);
                        inter.AddLast(temp);//agrego el nuevo temporal

                        inter.AddLast(new GenCod("", Convert.ToString(operUn), "-", tempo, "", ""));
                        return tempo;
                    }
                    else if (operador == tipOper.NOT)
                    {//lo unico que se hace es un intercambio de etiquetas las true pasan a ser false y las false a true
                    LinkedList<Instruc> etiq = new LinkedList<Instruc>();
                         if (operUn is LinkedList<Instruc>) etiq = (LinkedList<Instruc>)operUn;

                        else  
                        {
                             etiq = new LinkedList<Instruc>();
                        if (operUn != null)
                        {
                            if (operUn is bool)
                            {
                                if ((bool)operUn == true) operUn = 1;
                                else operUn = 0;
                            }
                            Etiq eti = new Etiq(inter, "true");
                            inter.AddLast(eti);
                            etiq.AddLast(eti);
                            Etiq etiF = new Etiq(inter, "false");
                            inter.AddLast(etiF);
                            etiq.AddLast(etiF);
                            inter.AddLast(new GenCod("", Convert.ToString(operUn), "", "REL", Convert.ToString(etiF.ejecutar(gen, en, arbol, inter)), Convert.ToString(eti.ejecutar(gen, en, arbol, inter))));
                            return etiq;
                        }
                        else return val;

                        
                        }
                    foreach(Etiq eti in etiq)
                    {
                            if (eti.cond == "true") eti.cond = "false";
                            else if (eti.cond == "false") eti.cond = "true";
                    }

                        return etiq;
                        /*
                        if (operUn is bool) return !(bool)operUn;
                        
                        else
                        {
                            Form1.error.AppendText("Error, no se puede negar este tipo de datos");
                            return null;
                        }*/
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
