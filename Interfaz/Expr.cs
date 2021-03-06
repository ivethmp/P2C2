using P1.Arbol;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;
using static P1.TS.Simb;

namespace P1.Interfaz
{
    interface Expr
    {
        //interfaz expresion se utiliza para valores enteros y primitivos 
        //operaciones que conllevan relacion directa con valores primitivos
        public int lin { get; set; }
        public int col { get; set; }

        Tipo getTipo(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter);

        object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter);

    }
}
