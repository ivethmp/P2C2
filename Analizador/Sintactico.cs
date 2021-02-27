using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;

namespace P1.Analizador
{
    class Sintactico
    {
        public void Analizar(TextBox salida, String cad)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;

            if (raiz==null)
            {
                System.Diagnostics.Debug.WriteLine("no tiene nada el arbol");
                return;
            }

            instrucs(salida, raiz.ChildNodes.ElementAt(0));

        }

        public void instrucs(TextBox salida, ParseTreeNode NodoA)
        {
            if (NodoA.ChildNodes.Count == 2)
            {
                instruc(salida,NodoA.ChildNodes.ElementAt(0));
                instrucs(salida, NodoA.ChildNodes.ElementAt(1));
            }
            else
            {
                instruc(salida, NodoA.ChildNodes.ElementAt(0));
            }
        }

        public void instruc(TextBox salida,ParseTreeNode NodoA)
        {
            System.Diagnostics.Debug.WriteLine("El valor de la expresion es: " + Exp(NodoA.ChildNodes.ElementAt(2)));

            salida.Text+= ("El valor de la expresion es: " + Exp(NodoA.ChildNodes.ElementAt(2))+"\n")+"\r\n";
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
