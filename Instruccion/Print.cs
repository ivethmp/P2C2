using P1.Arbol;
using P1.Generacion;
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

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            object val = imprimir.getValImp(gen,en, arbol,inter);

            if (val != null)
            {
                if (val is int)
                {
                    inter.AddLast(new GenCod("printf(\"%d\",(int)" + val.ToString() + ");\n", "", "", "TEXTO", "", ""));
                }
                else if (val is Double || val is Decimal)
                {
                    inter.AddLast(new GenCod("printf(\"%f\",(float)" + val.ToString() + ");\n", "", "", "TEXTO", "", ""));
                }
                else
                {
                    foreach(Instruc ins in inter)
                    {
                        if(ins is Temp)
                        {
                            String temporal = ins.ejecutar(gen, en, arbol, inter).ToString();
                            if (val.ToString() == temporal)
                            {
                                System.Diagnostics.Debug.WriteLine("valor 1" + val.ToString() + "el del temporal" + temporal);
                                inter.AddLast(new GenCod("printf(\"%f\",(float)" + val.ToString() + ");\n", "", "", "TEXTO", "", ""));
                                return null;
                            }
                        }
                    }

                    string salida = val.ToString();
                    byte[] byteArray = Encoding.ASCII.GetBytes(salida);
                    foreach (byte caracter in byteArray)
                    {
                        inter.AddLast(new GenCod("printf(\"%c\",(char)" + caracter + ");\n", "", "", "TEXTO", "", ""));
                    }

                }
               /* if (bandera == true)//significa que es con salto de lines writeln
                {
                    Form1.salir.AppendText(val.ToString() + "\n");
                    return true;
                }
                Form1.salir.AppendText(val.ToString());*/
                return true;

            }

            Form1.error.AppendText("Error en write(ln), lin:" + lin + " col:" + col);
            return false;
        }
    }
}
