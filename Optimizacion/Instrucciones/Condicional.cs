using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Condicional : Instr2
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }

        Expr2 condicion;
        Instr2 GOTOO;
        public Condicional(Expr2 condicion, Instr2 GOTOO, int lin, int col)
        {
            this.condicion = condicion;
            this.GOTOO = GOTOO;
            this.lin = lin;
            this.col = col;
        }
        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            int aux = GOTOO.lin;
            GOTOO.lin = -10;
            String salto = (String)GOTOO.getOptimizar(arbol, nuevo, reporte, temp);
            GOTOO.lin = aux;
            aux = condicion.lin;
            condicion.lin = -10;
            String condi = "";
            object sal = condicion.getOptimizar(arbol, nuevo, reporte, temp);
            if (sal is String[])
            {
                Object[] val = sal as object[];
                sal = val[1];
                condi = sal.ToString();
            }
            else condi = sal.ToString();
            
            condicion.lin = aux;
            

            string cadenagoto = "if (" + condi + ")"+salto + ";";
            if (lin == -10) return cadenagoto;//codigo eliminado

            int bandera = 0;
            String optimi = "Eliminación de código muerto";
            String elimi = cadenagoto;
            int regla = 0;
            int fila = 0;
            String salto2 = "";
            String etiqu = "";
            String etiquF = "";
            String cond2 = "";
            Instr2 auxI = null;
            foreach (Instr2 ins in arbol.Instrucciones)
            {
                if (bandera == 1)
                {
                    if (ins is GOTO)//siguiente goto
                    {
                        if (condi.Equals("true"))
                        {
                            ins.lin = -10;
                            salto2 = (String)ins.getOptimizar(arbol, nuevo, reporte, temp);
                            elimi = "if(" + cond2 + ") " + salto + "<br>"+salto2;
                            ins.lin = aux; 
                            regla = 3;
                            nuevo.AddLast(new NewCod(salto));
                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, salto, fila));

                            return cadenagoto;

                        }else if (condi.Equals("false"))
                        {
                            aux = ins.lin;
                            ins.lin = -10;
                            salto2 = (String)ins.getOptimizar(arbol, nuevo, reporte, temp);
                            ins.lin = aux;
                            elimi = "if(" + cond2 + ") " + salto + "<br>" + salto2;
                            ins.lin = -10;
                            regla = 4;
                            nuevo.AddLast(new NewCod(salto2));
                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, salto2, fila));

                            return cadenagoto;
                        }
                        aux = ins.lin;
                        ins.lin = -10;
                        salto2 = (String)ins.getOptimizar(arbol, nuevo, reporte, temp);
                        etiquF = ((GOTO)ins).id;
                        elimi = elimi +"<br>\n"+ salto2;
                        ins.lin = aux;
                        bandera = 2;
                    }
                    else {
                        nuevo.AddLast(new NewCod(cadenagoto));
                        return cadenagoto; }

                }
                if (bandera == 2)
                    {
                    if(ins is ETIQUETA)
                        {
                        aux = ins.lin;
                        ins.lin = -10;
                        String etiq = (String)ins.getOptimizar(arbol, nuevo, reporte, temp);
                        ins.lin = aux;
                        if (etiqu.Equals(etiq))
                        {
                            cadenagoto = "if(" + condi + ")  ";
                            ins.lin = -10;
                            regla = 2;
                            nuevo.AddLast(new NewCod(cadenagoto));
                            if (regla != 0) reporte.AddLast(new CodigoC(optimi, regla, elimi, cadenagoto, fila));
                            return cadenagoto;
                        }
                        else
                        {
                            nuevo.AddLast(new NewCod(cadenagoto));
                            return cadenagoto;
                        }
                   }
                 }

                if(ins is Condicional && ins.lin == lin && ins.col == col)//es la actual
                {
                    if(condicion is Operacion)
                    {

                    }else
                    {
                        nuevo.AddLast(new NewCod(cadenagoto));
                        return cadenagoto;
                    }
                    if (((Operacion)condicion).operador == Operacion.tipOper.IIGUAL || ((Operacion)condicion).operador == Operacion.tipOper.DIF)
                    {
                       object salida = condicion.getOptimizar(arbol, nuevo, reporte, temp);
                        if (salida is String[])
                            
                        {
                            Object[] val = salida as object[];
                            salida = val[1];
                            cond2 = val[0].ToString();
                            condi = salida.ToString();
                            GOTOO.lin = -10;
                            salto = (String)GOTOO.getOptimizar(arbol, nuevo, reporte, temp);
                            etiqu = ((GOTO)GOTOO).id;
                            bandera = 1;
                            fila = lin;

                        }
                        else
                        {
                            condi = (String)condicion.getOptimizar(arbol, nuevo, reporte, temp);
                            GOTOO.lin = -10;
                            salto = (String)GOTOO.getOptimizar(arbol, nuevo, reporte, temp);
                            etiqu = ((GOTO)GOTOO).id;
                            bandera = 1;
                            fila = lin;
                        }
                        
                    }
                    else
                    {
                        nuevo.AddLast(new NewCod(cadenagoto));
                        return cadenagoto;
                    }
                    
                }
            }

            nuevo.AddLast(new NewCod(cadenagoto));
            return null;
        }
    }
}
