namespace GenOR
{
    partial class FormBackup_Restore
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
        private new void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackup_Restore));
            this.gb_Backup_Restore = new System.Windows.Forms.GroupBox();
            this.lb_Restore = new System.Windows.Forms.Label();
            this.lb_Backup = new System.Windows.Forms.Label();
            this.btn_Restore = new System.Windows.Forms.Button();
            this.btn_Backup = new System.Windows.Forms.Button();
            this.btn_TelaEspera_SemImagens = new System.Windows.Forms.Button();
            this.bgw_ProcessosBackground = new System.ComponentModel.BackgroundWorker();
            this.gb_Backup_Restore.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Backup_Restore
            // 
            this.gb_Backup_Restore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Backup_Restore.Controls.Add(this.lb_Restore);
            this.gb_Backup_Restore.Controls.Add(this.lb_Backup);
            this.gb_Backup_Restore.Controls.Add(this.btn_Restore);
            this.gb_Backup_Restore.Controls.Add(this.btn_Backup);
            this.gb_Backup_Restore.Controls.Add(this.btn_TelaEspera_SemImagens);
            this.gb_Backup_Restore.Location = new System.Drawing.Point(-4, -6);
            this.gb_Backup_Restore.Name = "gb_Backup_Restore";
            this.gb_Backup_Restore.Size = new System.Drawing.Size(592, 100);
            this.gb_Backup_Restore.TabIndex = 0;
            this.gb_Backup_Restore.TabStop = false;
            // 
            // lb_Restore
            // 
            this.lb_Restore.AutoSize = true;
            this.lb_Restore.BackColor = System.Drawing.Color.Transparent;
            this.lb_Restore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Restore.Location = new System.Drawing.Point(274, 15);
            this.lb_Restore.Name = "lb_Restore";
            this.lb_Restore.Size = new System.Drawing.Size(313, 18);
            this.lb_Restore.TabIndex = 67;
            this.lb_Restore.Text = "REALIZA RESTAURAÇÃO DO SISTEMA.";
            // 
            // lb_Backup
            // 
            this.lb_Backup.AutoSize = true;
            this.lb_Backup.BackColor = System.Drawing.Color.Transparent;
            this.lb_Backup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Backup.Location = new System.Drawing.Point(5, 15);
            this.lb_Backup.Name = "lb_Backup";
            this.lb_Backup.Size = new System.Drawing.Size(240, 18);
            this.lb_Backup.TabIndex = 66;
            this.lb_Backup.Text = "REALIZA CÓPIA DO SISTEMA.";
            // 
            // btn_Restore
            // 
            this.btn_Restore.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Restore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Restore.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Restore.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Restore.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Restore.Font = new System.Drawing.Font("Microsoft Tai Le", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Restore.Location = new System.Drawing.Point(328, 36);
            this.btn_Restore.Name = "btn_Restore";
            this.btn_Restore.Size = new System.Drawing.Size(200, 50);
            this.btn_Restore.TabIndex = 2;
            this.btn_Restore.Text = "RESTORE";
            this.btn_Restore.UseVisualStyleBackColor = false;
            this.btn_Restore.Click += new System.EventHandler(this.btn_Restore_Click);
            // 
            // btn_Backup
            // 
            this.btn_Backup.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Backup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Backup.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Backup.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Backup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Backup.Font = new System.Drawing.Font("Microsoft Tai Le", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Backup.Location = new System.Drawing.Point(17, 36);
            this.btn_Backup.Name = "btn_Backup";
            this.btn_Backup.Size = new System.Drawing.Size(200, 50);
            this.btn_Backup.TabIndex = 1;
            this.btn_Backup.Text = "BACKUP";
            this.btn_Backup.UseVisualStyleBackColor = false;
            this.btn_Backup.Click += new System.EventHandler(this.btn_Backup_Click);
            // 
            // btn_TelaEspera_SemImagens
            // 
            this.btn_TelaEspera_SemImagens.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_TelaEspera_SemImagens.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_TelaEspera_SemImagens.Enabled = false;
            this.btn_TelaEspera_SemImagens.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_TelaEspera_SemImagens.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_TelaEspera_SemImagens.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_TelaEspera_SemImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TelaEspera_SemImagens.Font = new System.Drawing.Font("Microsoft Tai Le", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TelaEspera_SemImagens.Location = new System.Drawing.Point(4, 6);
            this.btn_TelaEspera_SemImagens.Name = "btn_TelaEspera_SemImagens";
            this.btn_TelaEspera_SemImagens.Size = new System.Drawing.Size(584, 91);
            this.btn_TelaEspera_SemImagens.TabIndex = 2;
            this.btn_TelaEspera_SemImagens.UseVisualStyleBackColor = false;
            this.btn_TelaEspera_SemImagens.Visible = false;
            // 
            // FormBackup_Restore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(584, 91);
            this.Controls.Add(this.gb_Backup_Restore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBackup_Restore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BACKUP / RESTORE do sistema";
            this.Load += new System.EventHandler(this.FormBackup_Restore_Load);
            this.gb_Backup_Restore.ResumeLayout(false);
            this.gb_Backup_Restore.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Backup_Restore;
        private System.Windows.Forms.Label lb_Restore;
        private System.Windows.Forms.Label lb_Backup;
        private System.Windows.Forms.Button btn_Restore;
        private System.Windows.Forms.Button btn_Backup;
        private System.ComponentModel.BackgroundWorker bgw_ProcessosBackground;
        private System.Windows.Forms.Button btn_TelaEspera_SemImagens;
    }
}