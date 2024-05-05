namespace GenOR
{
    partial class FormTelaLoading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pn_PainelFundo = new System.Windows.Forms.Panel();
            this.lb_Carregando = new System.Windows.Forms.Label();
            this.lb_ImagemCarregando = new System.Windows.Forms.Label();
            this.pn_PainelFundo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_PainelFundo
            // 
            this.pn_PainelFundo.BackColor = System.Drawing.Color.Transparent;
            this.pn_PainelFundo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_PainelFundo.Controls.Add(this.lb_Carregando);
            this.pn_PainelFundo.Controls.Add(this.lb_ImagemCarregando);
            this.pn_PainelFundo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_PainelFundo.Location = new System.Drawing.Point(0, 0);
            this.pn_PainelFundo.Name = "pn_PainelFundo";
            this.pn_PainelFundo.Size = new System.Drawing.Size(425, 207);
            this.pn_PainelFundo.TabIndex = 0;
            // 
            // lb_Carregando
            // 
            this.lb_Carregando.AutoSize = true;
            this.lb_Carregando.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lb_Carregando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_Carregando.Font = new System.Drawing.Font("Impact", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Carregando.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_Carregando.Location = new System.Drawing.Point(208, 84);
            this.lb_Carregando.Name = "lb_Carregando";
            this.lb_Carregando.Size = new System.Drawing.Size(209, 38);
            this.lb_Carregando.TabIndex = 0;
            this.lb_Carregando.Text = "CARREGANDO. . .";
            // 
            // lb_ImagemCarregando
            // 
            this.lb_ImagemCarregando.BackColor = System.Drawing.Color.Transparent;
            this.lb_ImagemCarregando.Image = global::GenOR.Properties.Resources.Loading_GIF_3;
            this.lb_ImagemCarregando.Location = new System.Drawing.Point(-27, 0);
            this.lb_ImagemCarregando.Name = "lb_ImagemCarregando";
            this.lb_ImagemCarregando.Size = new System.Drawing.Size(262, 206);
            this.lb_ImagemCarregando.TabIndex = 2;
            // 
            // FormTelaLoading
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(425, 207);
            this.Controls.Add(this.pn_PainelFundo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTelaLoading";
            this.Text = "FormTelaLoading";
            this.pn_PainelFundo.ResumeLayout(false);
            this.pn_PainelFundo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_PainelFundo;
        private System.Windows.Forms.Label lb_Carregando;
        private System.Windows.Forms.Label lb_ImagemCarregando;
    }
}