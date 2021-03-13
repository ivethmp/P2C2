using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class IdentVal : Expr
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        private String ide;

        public IdentVal(String ide, int lin, int col)
        {
            this.ide = ide;
            this.lin = lin;
            this.col = col;
        }

        public Simb.Tipo getTipo(Entor en, TabS arbol)
        {
            if (en.buscar(ide))
            {
                Simb simbol = en.getSimb(ide);
                return simbol.tip;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("no encontro el id el tipo saber cual es");
                return Simb.Tipo.VOID;
            }
        }

        public object getValImp(Entor en, TabS arbol)
        {
            if (en.buscar(ide))
            {
                Simb simbol = en.getSimb(ide);
                return simbol.val;
            }
            else
            {
                Form1.error.AppendText("Error, No existe el id " + ide + " lin:" + lin + " col:" + col);
                return null;
            }
        }
    }
}
