using P1.Arbol;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Interfaz
{
    interface Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }

        Object ejecutar(Entor en, AST arbol);
    }
}
