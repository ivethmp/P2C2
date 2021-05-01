using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{//esta devio ser un tipo expresion pero no quiero hacerlo, creo que las otras tambien debieron ser expresion y no instruccion 
    //o debi crear una nueva interfaz que atienda a la generacion de codigo intermedio 
    //los metodos obligatorio del get y set debieron estar para recuperar etiquetas o temporales 
    //tambien en el stack y el heap para recorrerlo
    class Stack : Instruc
    {
        public int lin { get ; set; }
        public int col { get; set ; }
        public int val;
        public Stack (int val)
        {
            this.val = val;
        }

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            int posicion = 0;
            foreach(Instruc pila in inter)
            {
                if(pila is Stack) posicion++;
            }
            return posicion;
        }
    }
}
