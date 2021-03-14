using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;
using static P1.TS.Simb;

namespace P1.Instruccion
{
    class Prim : Expr
    {
        public int lin { get; set; }
        public int col { get; set; }
        
        private object val;
       // se obtiene el valor del simbolo
        public Tipo getTipo(Entor en, AST arbol)
        {
            object valor = this.getValImp(en, arbol);
            if (valor is int)
                return Tipo.INT;
            else if (valor is Double || valor is Decimal)
                return Tipo.REAL;
            else if (valor is String)
                return Tipo.STRING;
            else if (valor is bool)
                return Tipo.BOOL;
            
            
            return Tipo.OBJ;
        }

        public object getValImp(Entor en, AST arbol)
        { 
            return val;
        }

        //necesua del valor, col y lin debido a que solo los simbolos contienen una ubicacion
        //especifica, las instrucciones no pues no van en la tabla de simbolos
        public Prim(object val, int col, int lin)
        {
            this.val = val;
            this.col = col;
            this.lin = lin;
        }
    }
}
