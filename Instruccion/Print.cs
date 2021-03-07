using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Print : Instruc
    {
        private Instruc imprimir;

        public Print(Instruc imprimir)
        {
            this.imprimir = imprimir;
        }

        public int lin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int col { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object ejecutar(Entor en, TabS arbol)
        {
            //String impresion = imprimir.ejecutar(en,arbol).ToString();
            
            System.Diagnostics.Debug.WriteLine("impresion");
            return "empresion";
        }
    }
}
