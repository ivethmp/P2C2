using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace P1.TS
{
    //Dentro de una tabla simbolo estan las funciones para construir la misma
    public class TabS : LinkedList<Simb>
    {
        public TabS():base()
        {

        }
        //obtener los valores dentros de la tabla de simbolos
        public Object getVal(String id)
        {
            foreach (Simb s in this)
            {
                if (s.getId().Equals(id))
                {
                    //referencia al metodo en la clase Simb
                    return s.getVal();
                }
            }
            Console.WriteLine("La variable " + id + " no existe en este ámbito.");
            return "Desconocido";
        }

        public void setVal(String id, Object val)
        {
            foreach (Simb s in this)
            {
                if (s.getId().Equals(id))
                {
                    s.setVal(val);
                    return;
                }
            }
            Console.WriteLine("La variable " + id + " no existe en este ámbito, por lo "
                    + "que no puede asignársele un valor.");
        }

    }
}
