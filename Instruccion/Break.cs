using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Break : Instruc
    {
        public int lin { get ; set; }
        public int col { get ; set ; }

        public Break(int lin, int col)
        {
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor gen, Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return null;
        }
    }
}
