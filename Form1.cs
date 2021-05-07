using P1.Analizador;
using P1.Optimizacion.Analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P1
{
    public partial class Form1 : Form
    {
        public static RichTextBox error;
        public static RichTextBox salir;
        public static RichTextBox coptimizado;
        public static int sp;

        public Form1()
        {
            InitializeComponent();
            error = errores;
            salir = salida;
            coptimizado = optimizado;
            
            sp = 0;
    }
        //boton analizaar
        private void button1_Click(object sender, EventArgs e)
        {

            salida.Clear();
            error.Clear();
            String texto = entrada.Text;
            
            Sintactico sintac = new Sintactico();

           sintac.Analizar(texto);


        }

        private void salida_TextChanged(object sender, EventArgs e)
        {

        }

        private void errores_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabSimbol_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            //process.StartInfo.Arguments = "chrome.exe";
            process.StartInfo.FileName = "chrome.exe";
            process.StartInfo.Arguments = "ReporteTablaS.html";
            //process.StartInfo.FileName = output;
            //debo usar userShellExecute en net core
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        private void astG_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "salida.jpg";
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            optimizado.Clear();

            String texto = salida.Text;//envio el codigo generado en 3d por le codigo anterior

            Sintax sintac = new Sintax();

            sintac.Analizar(texto);
        }
    }
}
