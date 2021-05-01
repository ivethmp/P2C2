using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{
    class Etiq : Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }

        String temp;
        int cont = 0;
        public int numero;
        public String cond;

        public Etiq(LinkedList<Instruc> inter,String cond)
        {
            temp = "L";
            this.cond = cond;
            this.numero = cuentaEtiq(inter);
            cont++;
            System.Diagnostics.Debug.WriteLine("veamos si cuenta" + cont + " el num es " + numero);
        }
       

        int cuentaEtiq(LinkedList<Instruc> inter)
        {
            int cuenta = 0;
            foreach (Instruc ins in inter)
            {
                if (ins is Etiq) cuenta++;
            }
            return cuenta;
        }

       

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return temp + numero;
        }
    }
}
