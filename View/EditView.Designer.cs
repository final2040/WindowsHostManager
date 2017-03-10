using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    partial class EditView
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
            this.lblName = new Label();
            this.lblContent = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.txtName = new TextBox();
            this.txtContent = new RichTextBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(12, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(133, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Interface_Edit_NameLabel";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new Point(12, 59);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new Size(142, 13);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "Interface_Edit_ContentLabel";
            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(532, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Interface_Edit_SaveBtn";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(613, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Interface_Edit_CancelBtn";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new Point(12, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(676, 20);
            this.txtName.TabIndex = 5;
            // 
            // txtContent
            // 
            this.txtContent.AcceptsTab = true;
            this.txtContent.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Location = new Point(12, 75);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new Size(676, 339);
            this.txtContent.TabIndex = 6;
            this.txtContent.Text = "";
            // 
            // EditView
            // 
            this.ClientSize = new Size(700, 455);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditView";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Interface_Edit_FormTittle";
            this.Load += new EventHandler(this.EditView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblName;
        private Label lblContent;
        private Button btnSave;
        private Button btnCancel;
        private TextBox txtName;
        private RichTextBox txtContent;
    }
}
