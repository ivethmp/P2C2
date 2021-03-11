
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 377);
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
            this.entrada.Size = new System.Drawing.Size(345, 292);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
        public static System.Windows.Forms.RichTextBox salida;
        public static System.Windows.Forms.RichTextBox errores;
    }
}

