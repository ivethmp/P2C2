using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using P1.Interfaz;
using P1.TS;
using P1.Arbol;
using P1.Instruccion;
using System.Runtime.Serialization;
using Irony;
using P1.Reportes;

namespace P1.Analizador
{
    class Sintactico
    {
        
        public void Analizar( String cad)
        {
            System.Diagnostics.Debug.WriteLine("enter a analizar0");
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            //System.Diagnostics.Debug.WriteLine("enter a analizar" + cad);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;
            bool error = arbol.HasErrors();
            
            if (arbol.HasErrors())
            {
                Form1.errores.Text = (BuildParsingErrorMessage(arbol.ParserMessages));
                return;
                //throw new InvalidOperationException(BuildParsingErrorMessage(arbol.ParserMessages));
            }
            if (raiz == null)
            {
                System.Diagnostics.Debug.WriteLine("no tiene nada el arbol");
                return;
            }

            LinkedList<Instruc> AST = instrucs( raiz.ChildNodes.ElementAt(0));

            TabS global = new TabS();
            Entor entorno = new Entor(null);
            foreach (Instruc inst in AST)
            {
                    inst.ejecutar(entorno, global);
            }
            RepTabS reporte = new RepTabS();
            reporte.GenHTML(entorno);


        }
        private static string BuildParsingErrorMessage(LogMessageList messages)
        {//este metodo obtiene los mensajes de error sintactico desde la gramatica
            var sb = new StringBuilder();
            sb.AppendLine("Errores de sintaxis:");
            messages.ForEach(msg => sb.AppendLine($"\t{msg.Message + " lin:" + msg.Location.Line + " col:"+ msg.Location.Column}"));
            return sb.ToString();
        }

        public LinkedList<Instruc> instrucs( ParseTreeNode nodoA)
        {
            if (nodoA.ChildNodes.Count == 2)
            {
                LinkedList<Instruc> lista = instrucs (nodoA.ChildNodes.ElementAt(0));
              
               // lista.AddLast(instruc ( nodoA.ChildNodes.ElementAt(1)));
                return bloques(nodoA.ChildNodes.ElementAt(1), lista);
            }
            else
            {
                LinkedList<Instruc> lista = new LinkedList<Instruc>();
                //lista.AddLast(instruc( nodoA.ChildNodes.ElementAt(3)));
                return bloques(nodoA.ChildNodes.ElementAt(3),lista);
            }
        }

        private LinkedList<Instruc> bloques(ParseTreeNode bloque, LinkedList<Instruc> lista)
        {
            if (bloque.ChildNodes[1].Term.Name == "BLOQ_VAR")
            {
                
                foreach (ParseTreeNode declarar in bloque.ChildNodes[1].ChildNodes)
                {
                    System.Diagnostics.Debug.WriteLine("entre al for");
                    lista.AddLast(instruc(declarar));
                }
                return lista;
            }else if (bloque.ChildNodes[1].Term.Name == "BLOQ_BEGIN")
            {
                foreach (ParseTreeNode instbegin in bloque.ChildNodes[1].ChildNodes)
                {
                    lista.AddLast(instruc(instbegin.ChildNodes[0]));
                }
                return lista;
            }
            return null;
        }
        private Simb.Tipo tipoVar(ParseTreeNode nodoB)
        {
            String tipo = nodoB.Token.Text;
            switch (tipo.ToLower())
            {
                case "integer": return Simb.Tipo.INT;
                case "string": return Simb.Tipo.STRING;
                case "real": return Simb.Tipo.REAL;
                case "boolean": return Simb.Tipo.BOOL;
                default: return Simb.Tipo.INVAL;
            }
        }
        private Instruc instruc( ParseTreeNode nodoA)
        {
            //(1) ES EL BLOQUE VAR/BEGIN (0) YA ES LA INSTRUCCION EN SI
            //string tokenOp = nodoA.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            string tokenOp = nodoA.ToString().Split(' ')[0];
            //ParseTreeNode actual = nodoA.ChildNodes.ElementAt(0);
            //ParseTreeNode actual2 = nodoA.ChildNodes.ElementAt(1).ChildNodes.ElementAt[0];

            
            System.Diagnostics.Debug.WriteLine("el token es " +tokenOp);
            //System.Diagnostics.Debug.WriteLine("el token es 2 " + actual2);
            switch (tokenOp.ToLower())
             {
                case "declara":
                    {
                        LinkedList<Simb> ides = new LinkedList<Simb>();
                        Simb.Tipo tipoA;
                        if (nodoA.ChildNodes[0].Term.Name == "LISTDECLARA")//lista de ides de declaraciones
                        {
                            tipoA = tipoVar(nodoA.ChildNodes[2].ChildNodes[0]);
                            System.Diagnostics.Debug.WriteLine("el valor en lista es");
                            foreach (ParseTreeNode son in nodoA.ChildNodes[0].ChildNodes)
                            {
                                System.Diagnostics.Debug.WriteLine("el valor es" + son.Token.Text);
                                Simb simbol = new Simb(son.Token.Text, tipoA, son.Token.Location.Line, son.Token.Location.Column);
                                ides.AddLast(simbol);
                            }

                            return (new Declara(ides, tipoA, null, 0, 0));
                        }
                        else // solo es una variable declarada
                        {
                            System.Diagnostics.Debug.WriteLine("entre a declara 1");
                            tipoA = tipoVar(nodoA.ChildNodes[2].ChildNodes[0]);
                            Simb simbol = new Simb(nodoA.ChildNodes[0].Token.Text, tipoA, nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                            ides.AddLast(simbol);
                            if (nodoA.ChildNodes.Count == 3)//significa que no tiene valor
                                return new Declara(ides, tipoA, null, 0, 0);
                            else//quiere decir que tiene valor inicializado
                            {
                                return new Declara(ides, tipoA, expresion(nodoA.ChildNodes[4]), 0, 0);
                            }

                        }
                        
                    }

                
                
                case "asigna":
                    {
                        return new Asig(nodoA.ChildNodes[0].Token.Text,expresion(nodoA.ChildNodes[2]),0,0);
                    }
                case "writel":
                    {//El primer if es de writeln
                        if (nodoA.ChildNodes[0].ChildNodes[0].Term.Name == "writeln")
                        {//significa que es un salto de linea 
                            return new Print(expresion(nodoA.ChildNodes[2]),nodoA.ChildNodes[1].Token.Location.Line,nodoA.ChildNodes[1].Token.Location.Column,true);
                        }//significa que es un write sin salto de linea por lo que la bandera es false
                        return new Print((Expr)expresion(nodoA.ChildNodes[2]), nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column, false);
                        //(Expr)analizr(nodoA, linea,columna)
                    }
                    // return new Print(expresion_cadena(actual.ChildNodes.ElementAt(2)));
                 case "numero":
                     string tokenValor = nodoA.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    return null;
                     //return new Declaracion(tokenValor, Simbolo.Tipo.NUMERO);

                 default:
                     if (nodoA.ChildNodes.Count == 3)
                     {
                         tokenValor = nodoA.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                         //return new Asignacion(tokenValor, expresion_numerica(actual));
                     }
                     else
                     {
                         tokenValor = nodoA.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
                         //return new Asignacion(tokenValor, expresion_numerica(actual.ChildNodes.ElementAt(2)));
                     }
                    return null;
             }
            
        }

        private Expr expresion(ParseTreeNode nodoA)
        {
          /*  case "exprs":
                    {
                return instruc(nodoA.ChildNodes[0]);
            }
                case "expr_arit":
                    {
                if (nodoA.ChildNodes.Count == 3)
                {
                    Oper.tipOper salid = Oper.getOperador(nodoA.ChildNodes[1].Token.Text);
                    return new Oper((Expr)nodoA.ChildNodes[0], (Expr)nodoA.ChildNodes[2], salid);
                }
            }*/
          switch (nodoA.ChildNodes[0].Term.Name)
            {
                case "EXPR_ARIT":
                    {
                        if(nodoA.ChildNodes[0].ChildNodes.Count == 3)
                        {
                            //return new Oper((Expr)nodoA.ChildNodes[0].ChildNodes[0], (Expr)nodoA.ChildNodes[0].ChildNodes[2], Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                            return new Oper((Expr)expresion(nodoA.ChildNodes[0].ChildNodes[0]), (Expr)expresion(nodoA.ChildNodes[0].ChildNodes[2]), Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                        }//else if (nodoA.ChildNodes.Count == 2)
                        
                        return new Oper((Expr)nodoA.ChildNodes[1], Oper.tipOper.MENOSU);  
                    }
                case "EXPR_REL":
                    {
                        return new Oper((Expr)nodoA.ChildNodes[0], (Expr)nodoA.ChildNodes[2], Oper.getOperador(nodoA.ChildNodes[1].Token.Text));
                    }
                case "EXPR_LOG":
                    {
                        if(nodoA.ChildNodes.Count == 3)
                        {
                            return new Oper((Expr)nodoA.ChildNodes[0], (Expr)nodoA.ChildNodes[2], Oper.getOperador(nodoA.ChildNodes[1].Token.Text));
                        }else
                            return new Oper((Expr)nodoA.ChildNodes[1], Oper.tipOper.NOT);

                    }
                case "PRIMITIVOS":
                    {
                        object val = nodoA.ChildNodes[0].ChildNodes[0].Token.Text;
                        ParseTreeNode aux = nodoA.ChildNodes[0].ChildNodes[0];
                        if (aux.Term.Name == "entero")
                        {
                            try
                            {
                                int res = Convert.ToInt32(val.ToString());
                                return new Prim(res, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                            catch
                            {
                                Decimal res = 0;
                                if (Decimal.TryParse(val.ToString(), out res))//comprobando si es decimal
                                    return new Prim(res, aux.Token.Location.Line, aux.Token.Location.Column);
                                return new Prim(val, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                        } else if (aux.Term.Name == "cadena")
                        {
                            return new Prim(val.ToString().Replace("'", ""), aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name== "true")
                        {
                            return new Prim(true,aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name == "false")
                        {
                            return new Prim(false, aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name == "id")
                        {
                            return new IdentVal(val.ToString(), aux.Token.Location.Line, aux.Token.Location.Column);
                        } else
                            return null;
                    } 
                default:
                    {
                        return expresion(nodoA.ChildNodes[1]);
                    }
            }
            
        }
        
    
    }
}
