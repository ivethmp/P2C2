
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
            errores = new System.Windows.Forms.RichTextBox();
            salida = new System.Windows.Forms.RichTextBox();
            this.tabSimbol = new System.Windows.Forms.Button();
            this.astG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Analizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // entrada
            // 
            this.entrada.Location = new System.Drawing.Point(51, 61);
            this.entrada.Multiline = true;
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(345, 320);
            this.entrada.TabIndex = 1;
            // 
            // errores
            // 
            errores.Location = new System.Drawing.Point(428, 254);
            errores.Name = "errores";
            errores.Size = new System.Drawing.Size(335, 99);
            errores.TabIndex = 3;
            errores.Text = "";
            errores.TextChanged += new System.EventHandler(this.errores_TextChanged);
            // 
            // salida
            // 
            salida.Location = new System.Drawing.Point(428, 61);
            salida.Name = "salida";
            salida.Size = new System.Drawing.Size(335, 187);
            salida.TabIndex = 4;
            salida.Text = "";
            // 
            // tabSimbol
            // 
            this.tabSimbol.Location = new System.Drawing.Point(595, 371);
            this.tabSimbol.Name = "tabSimbol";
            this.tabSimbol.Size = new System.Drawing.Size(146, 26);
            this.tabSimbol.TabIndex = 5;
            this.tabSimbol.Text = "Tabla Simbolo";
            this.tabSimbol.UseVisualStyleBackColor = true;
            this.tabSimbol.Click += new System.EventHandler(this.tabSimbol_Click);
            // 
            // astG
            // 
            this.astG.Location = new System.Drawing.Point(595, 421);
            this.astG.Name = "astG";
            this.astG.Size = new System.Drawing.Size(146, 26);
            this.astG.TabIndex = 6;
            this.astG.Text = "Arbol AST";
            this.astG.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Controls.Add(this.astG);
            this.Controls.Add(this.tabSimbol);
            this.Controls.Add(salida);
            this.Controls.Add(errores);
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
        public static System.Windows.Forms.RichTextBox salida;
        public static System.Windows.Forms.RichTextBox errores;
    }
}

