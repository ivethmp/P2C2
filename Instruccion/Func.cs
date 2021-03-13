using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P1.Instruccion
{
    class Func : Instruc
    {
        public int lin { get ; set; }
        public int col { get; set; }
        LinkedList<Declara> param;
        String id;
        private Simb.Tipo tipoF;
        LinkedList<Instruc> instrucciones;
        LinkedList<Expr> valParam;

        public Func(String id, LinkedList<Declara> param, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.param = param;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
        }
        public Func(String id, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor en, TabS tabS)
        {
            Entor tabL = new Entor(en);

            if (valParam == null)//cuando no tiene parametros inicializados
            {
                valParam = new LinkedList<Expr>();
            }

            //Valor de los parametros en su entorno
            LinkedList<Expr> valores = new LinkedList<Expr>();
            foreach (Expr e in valParam)//valParam son de la llamada a funcion 
            {
                valores.AddLast(new Prim(e.getValImp(en, tabS), 0, 0));
            }

            if (param.Count == valores.Count)
            {
                //declaracion de variables
                for (int i = 0; i < param.Count; i++)
                {
                    param.ElementAt(i).ejecutar(tabL,tabS);
                }


                foreach (Instruc e in instrucciones)
                {
                    Object resultado = e.ejecutar(tabL, tabS);

                    if (resultado != null)
                    {
                        return resultado;
                    }
                }
            }
            else
            {
                Form1.error.AppendText("Error semantico en ejecutar " + this.id + ", lin:"+ lin + " y col:" + col + ", no se tienen los mismos parametros!!\n");
                return null;
            }
            return null;
        }
        //tiene que ver con la llamada a funcion
        public void setValoresParametros(LinkedList<Expr> a)
        {
            valParam = a;
        }
    }
}

