using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppResources;
using Entities;
using Model;
using Presenter;

namespace View
{
    public partial class EditView : View.ViewBase, IEditView
    {
        private readonly PEdit _presenter;

        public EditView()
        {
            InitializeComponent();
            InitializeLanguage();
            _presenter = new PEdit(this, new HostManagerFileDal());
        }

        private void InitializeLanguage()
        {
            lblName.Text = LocalizableStringHelper.GetLocalizableString(lblName.Text);
            lblContent.Text = LocalizableStringHelper.GetLocalizableString(lblContent.Text);
            btnCancel.Text = LocalizableStringHelper.GetLocalizableString(btnCancel.Text);
            btnSave.Text = LocalizableStringHelper.GetLocalizableString(btnSave.Text);
            this.Text = LocalizableStringHelper.GetLocalizableString(this.Text);
        }

        public EConfiguration Configuration { get; set; }
        public string Content { get; set; }
        public EditMode EditMode { get; set; }

        private void EditView_Load(object sender, EventArgs e)
        {
            if (EditMode == EditMode.Edit)
            {
                txtName.Enabled = false;
            }
            else
            {
                txtName.Enabled = true;
            }
            txtName.Text = Configuration.Name;
            txtContent.Text = Configuration.Content;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.Save();
        }

        private void txtContent_Validating(object sender, CancelEventArgs e)
        {
            Configuration.Content = ((TextBox) sender).Text;
        }
       
        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            Configuration.Name = ((TextBox)sender).Text;
        }
    }
}
