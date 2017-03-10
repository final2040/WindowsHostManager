using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using View.Properties;

namespace View
{
    partial class About
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(About));
            this.tableLayoutPanel = new TableLayoutPanel();
            this.logoPictureBox = new PictureBox();
            this.labelProductName = new Label();
            this.labelVersion = new Label();
            this.labelCompanyName = new Label();
            this.textBoxDescription = new TextBox();
            this.labelCopyright = new Label();
            this.okButton = new Button();
            this.pictureBoxlicence = new PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            ((ISupportInitialize)(this.logoPictureBox)).BeginInit();
            ((ISupportInitialize)(this.pictureBoxlicence)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.pictureBoxlicence, 1, 6);
            this.tableLayoutPanel.Dock = DockStyle.Fill;
            this.tableLayoutPanel.Location = new Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 11.11111F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.22222F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new Size(443, 314);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = DockStyle.Fill;
            this.logoPictureBox.Image = Resources.background;
            this.logoPictureBox.Location = new Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
            this.logoPictureBox.Size = new Size(140, 265);
            this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = DockStyle.Fill;
            this.labelProductName.Location = new Point(152, 0);
            this.labelProductName.Margin = new Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new Size(288, 17);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Nombre de producto";
            this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = DockStyle.Fill;
            this.labelVersion.Location = new Point(152, 30);
            this.labelVersion.Margin = new Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new Size(288, 17);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Versión";
            this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Dock = DockStyle.Fill;
            this.labelCompanyName.Location = new Point(152, 90);
            this.labelCompanyName.Margin = new Padding(6, 0, 3, 0);
            this.labelCompanyName.MaximumSize = new Size(0, 17);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new Size(288, 17);
            this.labelCompanyName.TabIndex = 22;
            this.labelCompanyName.Text = "Nombre de la compañía";
            this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Dock = DockStyle.Fill;
            this.textBoxDescription.Location = new Point(152, 123);
            this.textBoxDescription.Margin = new Padding(6, 3, 3, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = ScrollBars.Both;
            this.textBoxDescription.Size = new Size(288, 85);
            this.textBoxDescription.TabIndex = 23;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Descripción";
            this.textBoxDescription.WordWrap = false;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = DockStyle.Fill;
            this.labelCopyright.Location = new Point(152, 60);
            this.labelCopyright.Margin = new Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new Size(288, 17);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.okButton.DialogResult = DialogResult.Cancel;
            this.okButton.Location = new Point(365, 245);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&Aceptar";
            // 
            // pictureBoxlicence
            // 
            this.pictureBoxlicence.Dock = DockStyle.Right;
            this.pictureBoxlicence.Image = ((Image)(resources.GetObject("pictureBoxlicence.Image")));
            this.pictureBoxlicence.Location = new Point(337, 274);
            this.pictureBoxlicence.Name = "pictureBoxlicence";
            this.pictureBoxlicence.Size = new Size(103, 37);
            this.pictureBoxlicence.TabIndex = 25;
            this.pictureBoxlicence.TabStop = false;
            // 
            // About
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(461, 332);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Padding = new Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "About";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((ISupportInitialize)(this.logoPictureBox)).EndInit();
            ((ISupportInitialize)(this.pictureBoxlicence)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private PictureBox logoPictureBox;
        private Label labelProductName;
        private Label labelVersion;
        private Label labelCopyright;
        private Label labelCompanyName;
        private TextBox textBoxDescription;
        private Button okButton;
        private PictureBox pictureBoxlicence;
    }
}
