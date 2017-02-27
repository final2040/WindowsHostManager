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
            this.btnDelete = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEdit = new System.Windows.Forms.Button();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripMain.SuspendLayout();
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
            this.btnSetConfig.Text = "Interface_SetCommand";
            this.btnSetConfig.UseVisualStyleBackColor = true;
            this.btnSetConfig.Click += new System.EventHandler(this.SetConfigConfigClick);
            // 
            // btnImportConfig
            // 
            this.btnImportConfig.Location = new System.Drawing.Point(223, 68);
            this.btnImportConfig.Name = "btnImportConfig";
            this.btnImportConfig.Size = new System.Drawing.Size(92, 23);
            this.btnImportConfig.TabIndex = 2;
            this.btnImportConfig.Text = "Interface_ImportCommand";
            this.btnImportConfig.UseVisualStyleBackColor = true;
            this.btnImportConfig.Click += new System.EventHandler(this.ImportConfigClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(223, 126);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Interface_DeleteCommand";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteConfigClick);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(323, 24);
            this.menuStripMain.TabIndex = 4;
            this.menuStripMain.Text = "Main Menú";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(143, 20);
            this.menuFile.Text = "Interface_FileMenuItem";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(198, 22);
            this.menuExit.Text = "Interface_ExitMenuItem";
            this.menuExit.Click += new System.EventHandler(this.ExitClick);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSet,
            this.menuImport,
            this.toolStripSeparator1,
            this.menuEditCommand,
            this.menuDelete});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(145, 20);
            this.menuEdit.Text = "Interface_EditMenuItem";
            // 
            // menuSet
            // 
            this.menuSet.Name = "menuSet";
            this.menuSet.Size = new System.Drawing.Size(218, 22);
            this.menuSet.Text = "Interface_SetCommand";
            this.menuSet.Click += new System.EventHandler(this.SetConfigConfigClick);
            // 
            // menuImport
            // 
            this.menuImport.Name = "menuImport";
            this.menuImport.Size = new System.Drawing.Size(218, 22);
            this.menuImport.Text = "Interface_ImportCommand";
            this.menuImport.Click += new System.EventHandler(this.ImportConfigClick);
            // 
            // menuEditCommand
            // 
            this.menuEditCommand.Name = "menuEditCommand";
            this.menuEditCommand.Size = new System.Drawing.Size(218, 22);
            this.menuEditCommand.Text = "Interface_EditCommand";
            this.menuEditCommand.Click += new System.EventHandler(this.EditClick);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(218, 22);
            this.menuDelete.Text = "Interface_DeleteCommand";
            this.menuDelete.Click += new System.EventHandler(this.DeleteConfigClick);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(150, 20);
            this.menuHelp.Text = "Interface_HelpMenuItem";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(213, 22);
            this.menuAbout.Text = "Interface_AboutMenuItem";
            this.menuAbout.Click += new System.EventHandler(this.AboutClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(223, 97);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Interface_EditCommand";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.EditClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 261);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnImportConfig);
            this.Controls.Add(this.btnSetConfig);
            this.Controls.Add(this.listBoxConfiguration);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: Windows Host Manager ::.";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxConfiguration;
        private System.Windows.Forms.Button btnSetConfig;
        private System.Windows.Forms.Button btnImportConfig;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripMenuItem menuSet;
        private System.Windows.Forms.ToolStripMenuItem menuImport;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ToolStripMenuItem menuEditCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

