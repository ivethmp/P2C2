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
            String esuno = "";
            String valFin = "";
            String variable = "";
            if (sal is String[])
            {
                Object[] resultado = sal as object[];
                sal = resultado [1];
                valFin = sal.ToString();
                esuno = resultado[0].ToString();
                variable = resultado[2].ToString();
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
                        temporales(id, temp);
                        nuevo.AddLast(new NewCod(salida+";"));
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
                                    temporales(id, temp);
                                    
                                    nuevo.AddLast(new NewCod(codigosalida+";"));
                                    return codigosalida;
                                }
                                else//se le asigno otro balor a b
                                {
                                    temporales(id, temp);
                                    nuevo.AddLast(new NewCod(codigosalida+";"));
                                    return codigosalida;
                                }

                            }
                        }else if(((Asignacion)ins).id == id)//se le asigno otro valor a la primera variable
                        {
                            temporales(id, temp);
                            nuevo.AddLast(new NewCod(codigosalida+";"));
                            return codigosalida; 
                        }
                    }
                }

                /*if(esuno == "uno" && variable!="")
                {
                    bandera = 4;
                    if(ins is Asignacion)
                    {
                        if (((Asignacion)ins).id == variable)//encontre la variable
                        {
                            object valor2 = ((Asignacion)ins).val;
                            if (valor2 is string)//quiere decir que es un valor concreto{
                            {
                                if (valor2.ToString() == "0")//quiere decir que se asigno con 0
                                {
                                    bandera = 4;
                                }
                                else bandera = 5;//encontrro el id pero este no esta asignado con 0
                            }
                        }
                    }
                   // bandera = 4;//significa que no se encontro la variable
                }*/


                else if(ins.lin == lin && ins.col == col && ins is Asignacion)//la que verifica que igual a la entrada
                {
                   /* if (bandera == 4)//4 no encontro el valor anterior asignado o el valor es 0
                    {

                        
                    }if(bandera == 5)//no se cumplio condicion de que la variable valga 0
                    {
                        nuevo.AddLast(new NewCod(codigosalida + ";"));
                        return codigosalida;
                    }*/
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
                            variable = resultado[2].ToString();
                            opcion = valor.ToString();
                        }
                        if (opcion == "false" && variable==id)
                        {
                            fila = ins.lin;
                            ins.lin = -1;
                            Operacion.tipOper ooper= ((Operacion)val).operador;
                            if (ooper == Operacion.getOperador("-")) regla = 7;
                            else if (ooper == Operacion.getOperador("+")) regla = 6;
                            else if (ooper == Operacion.getOperador("*")) regla = 8;
                            else if (ooper == Operacion.getOperador("/")) regla = 9;
                            ins.lin = -10;
                            elimi = codigosalida; //a = b

                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, "", fila));
                            
                            return codigosalida;
                        
                        }
                        else if (opcion == "false" && variable != id)
                        {
                            fila = ins.lin;
                            ins.lin = -1;
                            Operacion.tipOper ooper = ((Operacion)val).operador;
                            //String oper1 = "";
                            if (ooper == Operacion.getOperador("+")) regla = 10;
                            else if (ooper == Operacion.getOperador("-")) regla = 11;
                            else if (ooper == Operacion.getOperador("*")) regla = 12;
                            else if (ooper == Operacion.getOperador("/")) regla = 13;
                            ins.lin = -10;
                            elimi = codigosalida; //a = b
                            codigosalida = id + " = " + variable;
                            nuevo.AddLast(new NewCod(codigosalida + ";"));
                            
                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, codigosalida, fila));
                            temporales(id, temp);
                            temporales(variable, temp);
                            return codigosalida;
                        }

                        else if (opcion == "xdos")
                        {
                            fila = ins.lin;
                            Operacion.tipOper ooper = ((Operacion)val).operador;
                            regla = 14;
                            ins.lin = -10;
                            elimi = codigosalida; //a = b
                            codigosalida = id + " = " + variable +"+"+variable;
                            nuevo.AddLast(new NewCod(codigosalida + ";"));

                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, codigosalida, fila));
                            temporales(id, temp);
                            temporales(variable, temp);
                            return codigosalida;
                        }
                        else if (opcion == "xcero")
                        {
                            fila = ins.lin;
                            Operacion.tipOper ooper = ((Operacion)val).operador;
                            if (ooper == Operacion.getOperador("*")) regla = 15;
                            else if (ooper == Operacion.getOperador("/")) regla = 16;
                            ins.lin = -10;
                            elimi = codigosalida; //a = b
                            codigosalida = id + " = 0";
                            nuevo.AddLast(new NewCod(codigosalida + ";"));

                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, codigosalida, fila));
                            temporales(id, temp);
                            return codigosalida;
                        }

                        else
                        {
                            temporales(id, temp);
                            nuevo.AddLast(new NewCod(codigosalida+";"));
                            return codigosalida;
                        }
                    }
                }
            }
            temporales(id, temp);
            nuevo.AddLast(new NewCod(codigosalida+";"));
           return codigosalida;
        }

        public void temporales(String tempo, LinkedList<String>temp)
        {
            if (tempo is String)
            {
                if (temp.Contains(tempo.ToString())==false) //System.Diagnostics.Debug.WriteLine("el id dentro del array es " + val.ToString());
                 temp.AddLast(tempo.ToString());
            }
        }
    }
}
