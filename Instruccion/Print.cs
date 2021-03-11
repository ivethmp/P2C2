﻿using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Print : Instruc
    {
        private Expr imprimir;
        private bool bandera;

        public Print(Expr imprimir, int lin, int col, bool bandera)
        {
            this.imprimir = imprimir;
            this.lin = lin;
            this.col = col;
            this.bandera = bandera;
        }

        public int lin { get ; set ; }
        public int col { get ; set ; }

        public object ejecutar(Entor en, TabS arbol)
        {
            object val = imprimir.getValImp(en, arbol);
            if (val != null)
            {
                if (bandera == true)//significa que es con salto de lines writeln
                {
                    Form1.salida.AppendText(val.ToString() + "\n");
                    return true;
                }
                Form1.salida.AppendText(val.ToString());
                return true;

            }

            Form1.errores.AppendText("Error en write(ln), lin:" + lin + " col:" + col);
            return false;
        }
    }
}