using P1.Arbol;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Interfaz
{
    interface Expr
    {
        //interfaz expresion se utiliza para valores enteros y primitivos 
        //operaciones que conllevan relacion directa con valores primitivos
        public int lin { get; set; }
        public int col { get; set; }

        Tipo GetTipo(Entor en, AST arbol);

        object getValImp(Entor en, AST arbol);

    }
}
