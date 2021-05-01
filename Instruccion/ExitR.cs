using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class ExitR : Instruc, Expr
    {
        public int lin { get; set; }
        public int col { get ; set ; }

        Expr retorno;

        public ExitR(Expr retorno, int lin, int col)
        {
            this.lin = lin;
            this.col = col;
            this.retorno = retorno;
        }

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return retorno.getValImp(gen,en, arbol, inter);
        }

        public Simb.Tipo getTipo(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            
            throw new NotImplementedException();
        }

        public object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            return retorno.getValImp(gen,en, arbol, inter);
        }
    }
}
