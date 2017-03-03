using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppResources;
using Entities;
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
            _presenter = new PEdit(this);
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
            Configuration.Name = txtName.Text;
            Configuration.Content = txtContent.Text;
            _presenter.Save();
        }
    }
}
