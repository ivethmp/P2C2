using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace P1.Analizador
{
    class Sintactico
    {
        public void Analizar(String cad)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;

            instrucs(raiz.ChildNodes.ElementAt(0));

        }

        public void instrucs(ParseTreeNode NodoA)
        {
            if (NodoA.ChildNodes.Count == 2)
            {
                instruc(NodoA.ChildNodes.ElementAt(0));
                instrucs(NodoA.ChildNodes.ElementAt(1));
            }
            else
            {
                instruc(NodoA.ChildNodes.ElementAt(0));
            }
        }

        public void instruc(ParseTreeNode NodoA)
        {
            System.Diagnostics.Debug.WriteLine("El valor de la expresion es: " + Exp(NodoA.ChildNodes.ElementAt(2)));
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
