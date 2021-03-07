
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
            this.salida = new System.Windows.Forms.TextBox();
            this.errores = new System.Windows.Forms.RichTextBox();
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
            // salida
            // 
            this.salida.Location = new System.Drawing.Point(418, 61);
            this.salida.Multiline = true;
            this.salida.Name = "salida";
            this.salida.Size = new System.Drawing.Size(345, 146);
            this.salida.TabIndex = 2;
            this.salida.TextChanged += new System.EventHandler(this.salida_TextChanged);
            // 
            // errores
            // 
            this.errores.Location = new System.Drawing.Point(428, 241);
            this.errores.Name = "errores";
            this.errores.Size = new System.Drawing.Size(335, 96);
            this.errores.TabIndex = 3;
            this.errores.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.errores);
            this.Controls.Add(this.salida);
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
        private System.Windows.Forms.TextBox salida;
        public static System.Windows.Forms.RichTextBox errores;
    }
}

