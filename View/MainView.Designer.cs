using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using View.Properties;

namespace View
{
    partial class MainView
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private IContainer components = null;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainView));
            this.btnNew = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnImportConfig = new Button();
            this.btnSetConfig = new Button();
            this.listBoxConfiguration = new ListBox();
            this.menuStripMain = new MenuStrip();
            this.menuFile = new ToolStripMenuItem();
            this.menuExit = new ToolStripMenuItem();
            this.menuEdit = new ToolStripMenuItem();
            this.menuSet = new ToolStripMenuItem();
            this.menuImport = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.menuNewCommand = new ToolStripMenuItem();
            this.menuEditCommand = new ToolStripMenuItem();
            this.menuDelete = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.menuBackup = new ToolStripMenuItem();
            this.menuHelp = new ToolStripMenuItem();
            this.menuAbout = new ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Image = ((Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnNew.Location = new Point(246, 74);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(149, 29);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "Interface_NewCommand";
            this.btnNew.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNew.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = Resources.cog_edit1;
            this.btnEdit.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new Point(246, 109);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(149, 29);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Interface_EditCommand";
            this.btnEdit.TextAlign = ContentAlignment.MiddleLeft;
            this.btnEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.EditClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = Resources.cog_delete;
            this.btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new Point(246, 179);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(149, 29);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Interface_DeleteCommand";
            this.btnDelete.TextAlign = ContentAlignment.MiddleLeft;
            this.btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.DeleteConfigClick);
            // 
            // btnImportConfig
            // 
            this.btnImportConfig.Image = ((Image)(resources.GetObject("btnImportConfig.Image")));
            this.btnImportConfig.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnImportConfig.Location = new Point(246, 144);
            this.btnImportConfig.Name = "btnImportConfig";
            this.btnImportConfig.Size = new Size(149, 29);
            this.btnImportConfig.TabIndex = 2;
            this.btnImportConfig.Text = "Interface_ImportCommand";
            this.btnImportConfig.TextAlign = ContentAlignment.MiddleLeft;
            this.btnImportConfig.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnImportConfig.UseVisualStyleBackColor = true;
            this.btnImportConfig.Click += new EventHandler(this.ImportConfigClick);
            // 
            // btnSetConfig
            // 
            this.btnSetConfig.Image = Resources.cog_go;
            this.btnSetConfig.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnSetConfig.Location = new Point(246, 39);
            this.btnSetConfig.Name = "btnSetConfig";
            this.btnSetConfig.Size = new Size(149, 29);
            this.btnSetConfig.TabIndex = 1;
            this.btnSetConfig.Text = "Interface_SetCommand";
            this.btnSetConfig.TextAlign = ContentAlignment.MiddleLeft;
            this.btnSetConfig.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnSetConfig.UseVisualStyleBackColor = true;
            this.btnSetConfig.Click += new EventHandler(this.SetConfigConfigClick);
            // 
            // listBoxConfiguration
            // 
            this.listBoxConfiguration.FormattingEnabled = true;
            this.listBoxConfiguration.Location = new Point(12, 39);
            this.listBoxConfiguration.Name = "listBoxConfiguration";
            this.listBoxConfiguration.Size = new Size(228, 225);
            this.listBoxConfiguration.TabIndex = 0;
            this.listBoxConfiguration.SelectedIndexChanged += new EventHandler(this.listBoxConfiguration_SelectedIndexChanged);
            this.listBoxConfiguration.MouseDoubleClick += new MouseEventHandler(this.listBoxConfiguration_MouseDoubleClick);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuHelp});
            this.menuStripMain.Location = new Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new Size(407, 24);
            this.menuStripMain.TabIndex = 4;
            this.menuStripMain.Text = "Main Menú";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new Size(143, 20);
            this.menuFile.Text = "Interface_FileMenuItem";
            // 
            // menuExit
            // 
            this.menuExit.Image = Resources.door_out;
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new Size(198, 22);
            this.menuExit.Text = "Interface_ExitMenuItem";
            this.menuExit.Click += new EventHandler(this.ExitClick);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new ToolStripItem[] {
            this.menuSet,
            this.menuImport,
            this.toolStripSeparator1,
            this.menuNewCommand,
            this.menuEditCommand,
            this.menuDelete,
            this.toolStripSeparator2,
            this.menuBackup});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new Size(145, 20);
            this.menuEdit.Text = "Interface_EditMenuItem";
            // 
            // menuSet
            // 
            this.menuSet.Image = Resources.cog_go;
            this.menuSet.Name = "menuSet";
            this.menuSet.Size = new Size(221, 22);
            this.menuSet.Text = "Interface_SetCommand";
            this.menuSet.Click += new EventHandler(this.SetConfigConfigClick);
            // 
            // menuImport
            // 
            this.menuImport.Image = ((Image)(resources.GetObject("menuImport.Image")));
            this.menuImport.Name = "menuImport";
            this.menuImport.Size = new Size(221, 22);
            this.menuImport.Text = "Interface_ImportCommand";
            this.menuImport.Click += new EventHandler(this.ImportConfigClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(218, 6);
            // 
            // menuNewCommand
            // 
            this.menuNewCommand.Image = ((Image)(resources.GetObject("menuNewCommand.Image")));
            this.menuNewCommand.Name = "menuNewCommand";
            this.menuNewCommand.Size = new Size(221, 22);
            this.menuNewCommand.Text = "Interface_NewCommand";
            this.menuNewCommand.Click += new EventHandler(this.btnNew_Click);
            // 
            // menuEditCommand
            // 
            this.menuEditCommand.Image = Resources.cog_edit1;
            this.menuEditCommand.Name = "menuEditCommand";
            this.menuEditCommand.Size = new Size(221, 22);
            this.menuEditCommand.Text = "Interface_EditCommand";
            this.menuEditCommand.Click += new EventHandler(this.EditClick);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = Resources.cog_delete;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new Size(221, 22);
            this.menuDelete.Text = "Interface_DeleteCommand";
            this.menuDelete.Click += new EventHandler(this.DeleteConfigClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(218, 6);
            // 
            // menuBackup
            // 
            this.menuBackup.Image = Resources.cog_error;
            this.menuBackup.Name = "menuBackup";
            this.menuBackup.Size = new Size(221, 22);
            this.menuBackup.Text = "Interface_BackupCommand";
            this.menuBackup.Click += new EventHandler(this.menuBackup_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new Size(150, 20);
            this.menuHelp.Text = "Interface_HelpMenuItem";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new Size(213, 22);
            this.menuAbout.Text = "Interface_AboutMenuItem";
            this.menuAbout.Click += new EventHandler(this.AboutClick);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(407, 269);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnImportConfig);
            this.Controls.Add(this.btnSetConfig);
            this.Controls.Add(this.listBoxConfiguration);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = ".:: Windows Host Manager ::.";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBoxConfiguration;
        private Button btnSetConfig;
        private Button btnImportConfig;
        private Button btnDelete;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuExit;
        private ToolStripMenuItem menuEdit;
        private ToolStripMenuItem menuHelp;
        private ToolStripMenuItem menuAbout;
        private ToolStripMenuItem menuSet;
        private ToolStripMenuItem menuImport;
        private ToolStripMenuItem menuDelete;
        private Button btnEdit;
        private ToolStripMenuItem menuEditCommand;
        private ToolStripSeparator toolStripSeparator1;
        private Button btnNew;
        private ToolStripMenuItem menuNewCommand;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuBackup;
    }
}

