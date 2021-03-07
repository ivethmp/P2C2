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

namespace P1.Analizador
{
    class Sintactico
    {
        public void Analizar(TextBox salida, String cad)
        {
            System.Diagnostics.Debug.WriteLine("enter a analizar0");
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            System.Diagnostics.Debug.WriteLine("enter a analizar" + cad);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;

            LinkedList<Instruc> AST = instrucs( raiz.ChildNodes.ElementAt(0));

            TabS global = new TabS();
            Entor entorno = new Entor();
            System.Diagnostics.Debug.WriteLine("antes del for");
            foreach (Instruc inst in AST)
            {
                if(inst is Print)
                {
                    System.Diagnostics.Debug.WriteLine("entre al Print");
                    salida.Text += "entre al Print"+inst.ejecutar(entorno,global);
                }
                else
                {
                    inst.ejecutar(entorno, global);
                }
                
            }
            
                        if (raiz==null)
                        {
                            System.Diagnostics.Debug.WriteLine("no tiene nada el arbol");
                            return;
                        }

                        instrucs( raiz.ChildNodes.ElementAt(0));

        }

        public LinkedList<Instruc> instrucs( ParseTreeNode nodoA)
        {
            if (nodoA.ChildNodes.Count == 2)
            {
                LinkedList<Instruc> lista = instrucs (nodoA.ChildNodes.ElementAt(0));
                lista.AddLast(instruc ( nodoA.ChildNodes.ElementAt(1)));
                return lista;

            }
            else
            {
                LinkedList<Instruc> lista = new LinkedList<Instruc>();
                lista.AddLast(instruc( nodoA.ChildNodes.ElementAt(3)));
                return lista;
            }
        }
        private Simb.Tipo tipoVar(ParseTreeNode nodoB)
        {
            String tipo = nodoB.ToString().Split(' ')[0].ToString();
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
            string tokenOp = nodoA.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            ParseTreeNode actual = nodoA.ChildNodes.ElementAt(0);
            System.Diagnostics.Debug.WriteLine("el token es " +tokenOp);
             switch (tokenOp.ToLower())
             {
                case "declara":
                    {
                        LinkedList<Simb> ides = null;


                        return new Declara(ides, tipoVar(actual.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0)));
                    }
                case "asigna":
                    {
                        return null;
                    }
                case "writel":
                    return null;
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
            return null;
        }

        public double Exp(ParseTreeNode NodoA)
        {
            if (NodoA.ChildNodes.Count == 3)
            {
                string token = NodoA.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (token)
                {
                    case "+":
                        return Exp(NodoA.ChildNodes.ElementAt(0)) + Exp(NodoA.ChildNodes.ElementAt(2));
                    case "-":
                        return Exp(NodoA.ChildNodes.ElementAt(0)) - Exp(NodoA.ChildNodes.ElementAt(2));
                    case "*":
                        return Exp(NodoA.ChildNodes.ElementAt(0)) * Exp(NodoA.ChildNodes.ElementAt(2));
                    case "/":
                        return Exp(NodoA.ChildNodes.ElementAt(0)) / Exp(NodoA.ChildNodes.ElementAt(2));
                    default:
                        return Exp(NodoA.ChildNodes.ElementAt(1));
                }

            }
            else if (NodoA.ChildNodes.Count == 2)
            {
                return -1 * Exp(NodoA.ChildNodes.ElementAt(1));
            }
            else
            {
                return Double.Parse(NodoA.ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
            }
        }
    
    }
}
