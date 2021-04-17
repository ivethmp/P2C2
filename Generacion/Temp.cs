using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{
    class Temp : Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }

        String temp;
        int cont = 0;
        int numero;

        public Temp( LinkedList<Instruc> inter)
        {
            temp = "t";
            
            this.numero = cuentaTemp(inter);
            cont++;
            System.Diagnostics.Debug.WriteLine("veamos si cuenta" + cont + " el num es " + numero);
        }

        int cuentaTemp(LinkedList<Instruc> inter)
        {
            int cuenta = 0;
            foreach (Instruc ins in inter)
            {
                if (ins is Temp) cuenta++;
            }
            return cuenta;
        }

        public object ejecutar(Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return temp + numero;
        }
    }
}
