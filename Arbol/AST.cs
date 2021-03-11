using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Arbol
{
    class AST
    {
        private LinkedList<Object> objs;
        private LinkedList<Instruc> instrucs;

        public AST(LinkedList<Instruc> instrucs)
        {
            objs = new LinkedList<Object>();
            this.instrucs = instrucs;
        }

        public void addObject(Object a)
        {
            objs.AddLast(a);
        }
    }
}
