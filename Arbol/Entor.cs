using P1.TS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace P1.Arbol
{
    class Entor : ICloneable
    {
        private Hashtable tabS;
        private Entor anterior;

        public Entor(Entor anterior)
        {
            tabS = new Hashtable();
            this.anterior = anterior;
        }

        public void Agregar(String id, Simb sim) 
        {
            try
            {
                id = id.ToLower();
                sim.id = sim.id.ToLower();
                tabS.Add(id, sim);
            }
            catch
            {

            }
            
        }

        public Entor eliminar(String id)
        {
            id = id.ToLower();
            for (Entor e = this; e != null; e = e.anterior)
            {
                if (e.tabS.Contains(id))
                {
                    e.tabS.Remove(id);
                    return e;
                }
            }
            return this;
        }

        public bool buscar(String id)
        {
            id = id.ToLower();
            for (Entor e = this; e != null; e = e.anterior)
            {
                if (e.tabS.Contains(id)) 
                    return true;
               
            }
            return false;

        }

        public bool buscarActual(String id)
        {
            id = id.ToLower();
            Simb encontrar = (Simb)(tabS[id]);
            return encontrar != null;

        }

        public void NewEntor(Entor entorno)
        {
            bool band = false;
            Entor local = this;

            do
            {
                if (local.anterior == null)
                {
                    local.anterior = entorno;
                    band = true;
                }
                else
                    local = local.anterior;
            } while (band);//se detiene cuando llega al ultimo entorno
        }

        public Simb getSimb(String id)
        {
            id = id.ToLower();

            for (Entor e = this; e != null; e = e.anterior)
            {
                Simb simbolo = (Simb)(e.tabS[id]);
                if (simbolo != null)
                    return simbolo;


            }
            return null;
        }

        public void actualizar(String id, Simb val)
        {
            id = id.ToLower();

            for (Entor e = this; e != null; e = e.anterior)
            {
                Simb simbolo = (Simb)(e.tabS[id]);
                if (simbolo != null)
                {
                    e.tabS[id] = val;//los nuevos valores para dicho id
                    return;
                }
                    


            }
            Console.WriteLine("El id no ha sido declarado");

        }

        public void actParam(String id, Simb param)
        {
            id = id.ToLower();

            for (Entor e = this; e != null; e = e.anterior)
            {
                Simb simbolo = (Simb)(e.tabS[id]);
                if (simbolo != null)
                {
                    e.tabS[id] = param;//los nuevos valores para dicho id
                    return;
                }



            }
            System.Diagnostics.Debug.WriteLine("El id no ha sido declarado");

        }

        public object Clone()
        {
            return new Entor(this);
        }

        public Hashtable TabSimb
        {
            get
            {
                return tabS;
            }
            set
            {
                tabS = value;
            }

        }

        public Entor Anterior
        {
            get
            {
                return anterior;
            }
            set
            {
                anterior = value;
            }

        }


    }
}
