using P1.Analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //boton analizaar
        private void button1_Click(object sender, EventArgs e)
        {
            String texto = entrada.Text;
            Console.WriteLine(texto);
            System.Diagnostics.Debug.WriteLine(texto);

            Sintactico sintac = new Sintactico();

           sintac.Analizar(salida,texto);


        }
    }
}
