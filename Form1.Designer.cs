
using System.Windows.Forms;

namespace P1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }
         
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.entrada = new System.Windows.Forms.TextBox();
            this.errores = new System.Windows.Forms.RichTextBox();
            this.salida = new System.Windows.Forms.RichTextBox();
            this.tabSimbol = new System.Windows.Forms.Button();
            this.astG = new System.Windows.Forms.Button();
            this.optimizar = new System.Windows.Forms.Button();
            this.optimizado = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Analizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // entrada
            // 
            this.entrada.Location = new System.Drawing.Point(12, 61);
            this.entrada.Multiline = true;
            this.entrada.Name = "entrada";
            this.entrada.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.entrada.Size = new System.Drawing.Size(462, 372);
            this.entrada.TabIndex = 1;
            // 
            // errores
            // 
            this.errores.Location = new System.Drawing.Point(496, 294);
            this.errores.Name = "errores";
            this.errores.ReadOnly = true;
            this.errores.Size = new System.Drawing.Size(413, 139);
            this.errores.TabIndex = 3;
            this.errores.Text = "";
            this.errores.TextChanged += new System.EventHandler(this.errores_TextChanged);
            // 
            // salida
            // 
            this.salida.Location = new System.Drawing.Point(496, 61);
            this.salida.Name = "salida";
            this.salida.Size = new System.Drawing.Size(413, 227);
            this.salida.TabIndex = 4;
            this.salida.Text = "";
            // 
            // tabSimbol
            // 
            this.tabSimbol.Location = new System.Drawing.Point(384, 464);
            this.tabSimbol.Name = "tabSimbol";
            this.tabSimbol.Size = new System.Drawing.Size(146, 26);
            this.tabSimbol.TabIndex = 5;
            this.tabSimbol.Text = "Tabla Simbolo";
            this.tabSimbol.UseVisualStyleBackColor = true;
            this.tabSimbol.Click += new System.EventHandler(this.tabSimbol_Click);
            // 
            // astG
            // 
            this.astG.Location = new System.Drawing.Point(384, 496);
            this.astG.Name = "astG";
            this.astG.Size = new System.Drawing.Size(146, 26);
            this.astG.TabIndex = 6;
            this.astG.Text = "Arbol AST";
            this.astG.UseVisualStyleBackColor = true;
            this.astG.Click += new System.EventHandler(this.astG_Click);
            // 
            // optimizar
            // 
            this.optimizar.AutoSize = true;
            this.optimizar.Location = new System.Drawing.Point(655, 464);
            this.optimizar.Name = "optimizar";
            this.optimizar.Size = new System.Drawing.Size(146, 44);
            this.optimizar.TabIndex = 7;
            this.optimizar.Text = "Optimizar";
            this.optimizar.UseVisualStyleBackColor = true;
            this.optimizar.Click += new System.EventHandler(this.button2_Click);
            // 
            // optimizado
            // 
            this.optimizado.Location = new System.Drawing.Point(63, 538);
            this.optimizado.Name = "optimizado";
            this.optimizado.Size = new System.Drawing.Size(793, 181);
            this.optimizado.TabIndex = 8;
            this.optimizado.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(921, 741);
            this.Controls.Add(this.optimizado);
            this.Controls.Add(this.optimizar);
            this.Controls.Add(this.astG);
            this.Controls.Add(this.tabSimbol);
            this.Controls.Add(this.salida);
            this.Controls.Add(this.errores);
            this.Controls.Add(this.entrada);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox entrada;
        private System.Windows.Forms.Button tabSimbol;
        private System.Windows.Forms.Button astG;
        private RichTextBox salida;
        private RichTextBox errores;
        private Button optimizar;
        private RichTextBox optimizado;
    }
}

