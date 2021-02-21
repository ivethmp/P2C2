using P1.Arbol;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Interfaz
{
    interface Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }

        object ejecutar(Entor en, AST arbol);
    }
}
