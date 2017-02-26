using AppResources;

namespace View
{
    partial class MainView
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxConfiguration = new System.Windows.Forms.ListBox();
            this.btnSetConfig = new System.Windows.Forms.Button();
            this.btnImportConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxConfiguration
            // 
            this.listBoxConfiguration.FormattingEnabled = true;
            this.listBoxConfiguration.Location = new System.Drawing.Point(12, 39);
            this.listBoxConfiguration.Name = "listBoxConfiguration";
            this.listBoxConfiguration.Size = new System.Drawing.Size(205, 186);
            this.listBoxConfiguration.TabIndex = 0;
            // 
            // btnSetConfig
            // 
            this.btnSetConfig.Location = new System.Drawing.Point(223, 39);
            this.btnSetConfig.Name = "btnSetConfig";
            this.btnSetConfig.Size = new System.Drawing.Size(92, 23);
            this.btnSetConfig.TabIndex = 1;
            this.btnSetConfig.Text = "TextButton";
            this.btnSetConfig.UseVisualStyleBackColor = true;
            this.btnSetConfig.Click += new System.EventHandler(this.btnSetConfig_Click);
            // 
            // btnImportConfig
            // 
            this.btnImportConfig.Location = new System.Drawing.Point(223, 68);
            this.btnImportConfig.Name = "btnImportConfig";
            this.btnImportConfig.Size = new System.Drawing.Size(92, 23);
            this.btnImportConfig.TabIndex = 2;
            this.btnImportConfig.Text = "text";
            this.btnImportConfig.UseVisualStyleBackColor = true;
            this.btnImportConfig.Click += new System.EventHandler(this.btnImportConfig_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 261);
            this.Controls.Add(this.btnImportConfig);
            this.Controls.Add(this.btnSetConfig);
            this.Controls.Add(this.listBoxConfiguration);
            this.Name = "MainView";
            this.Text = ".:: Windows Host Manager ::.";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxConfiguration;
        private System.Windows.Forms.Button btnSetConfig;
        private System.Windows.Forms.Button btnImportConfig;
    }
}

