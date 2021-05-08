using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;


namespace P1.Optimizacion.Instrucciones
{
    class Asignacion : Instr2
    {
        public int lin { get ; set; }
        public int col { get ; set; }

        public String id;
        public Expr2 val;


        public Asignacion(String id, Expr2 val,int lin, int col)
        {
            this.lin = lin;
            this.col = col;
            this.id = id;
            this.val = val;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            object sal = val.getOptimizar(arbol, nuevo, reporte, temp);
            String valFin = "";
            if (sal is String[])
            {
                Object[] resultado = sal as object[];
                sal = resultado [1];
                valFin = sal.ToString();
            }
            else valFin = sal.ToString();



            String codigosalida = id + "=" + valFin;
            if (lin == -10) return codigosalida;

            String idAux = "";
            object valor = "";
            int bandera = 0;
            String salida = "";
            String optimi = "Eliminación de instrucciones redundantes de carga y almacenamiento";
            String elimi = codigosalida;
            int regla = 0;
            int fila = 0;
            String salto2 = "";
            String etiqu = "";
            String etiquF = "";
            String cond2 = "";


            foreach (Instr2 ins in arbol.Instrucciones)
            {
                
                if (bandera == 1)
                {
                    if(ins is ETIQUETA)
                    {
                        salida = id + "=" + valFin;
                        nuevo.AddLast(new NewCod(salida));
                        return salida;
                    }

                    if(ins is Asignacion)
                    {
                        if(((Asignacion)ins).id == idAux)
                        {
                            valor = ((Asignacion)ins).val.getOptimizar(arbol, nuevo, reporte, temp);
                            if (valor is String)
                            {
                                if (valor.ToString() == id)
                                {
                                    regla = 5;
                                    ins.lin = -10;
                                    elimi = id + "=" + idAux + "<br>" + idAux + "="+ id; //a = b

                                    if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, codigosalida, fila));
                                    nuevo.AddLast(new NewCod(codigosalida+";"));
                                    return codigosalida;
                                }
                                else//se le asigno otro balor a b
                                {
                                    nuevo.AddLast(new NewCod(codigosalida+";"));
                                    return codigosalida;
                                }

                            }
                        }else if(((Asignacion)ins).id == id)//se le asigno otro valor a la primera variable
                        {
                            nuevo.AddLast(new NewCod(codigosalida+";"));
                            return codigosalida; 
                        }
                    }
                }


                if(ins.lin == lin && ins.col == col && ins is Asignacion)//la que verifica que igual a la entrada
                {
                    valor = val.getOptimizar(arbol,nuevo,reporte,temp);
                    if (valor is String)
                    {
                        fila = lin;
                        bandera = 1;
                        idAux = valor.ToString();
                    }
                    else
                    {
                        String opcion = "";
                        if (valor is String[])
                        {
                            Object[] resultado = valor as object[];
                            valor = resultado[0];
                            opcion = valor.ToString();
                        }
                        if (opcion == "false")
                        {
                            ins.lin = -1;
                            Operacion.tipOper ooper= ((Operacion)val).operador;
                            if (ooper == Operacion.getOperador("-")) regla = 7;
                            else if (ooper == Operacion.getOperador("+")) regla = 6;
                            else if (ooper == Operacion.getOperador("*")) regla = 8;
                            else if (ooper == Operacion.getOperador("/")) regla = 9;
                            ins.lin = -10;
                            elimi = id + "=" +valFin; //a = b

                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, "", fila));
                            
                            return codigosalida;
                        }else
                        {
                            nuevo.AddLast(new NewCod(codigosalida+";"));
                            return codigosalida;
                        }
                    }
                }
            }
            nuevo.AddLast(new NewCod(codigosalida+";"));
           return codigosalida;
        }
    }
}
