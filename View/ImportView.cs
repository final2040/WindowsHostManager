using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppResources;
using Entities;

namespace View
{
    public partial class ImportView : Form, IImportFileView
    {
        public ImportView()
        {
            InitializeComponent();
            InitializeLanguage();
        }

        private void InitializeLanguage()
        {
            lblName.Text = LocalizableStringHelper.GetLocalizableString("Interface_Import_NameLabel");
            lblPath.Text = LocalizableStringHelper.GetLocalizableString("Interface_Import_PathLabel");
            btnBrowse.Text = LocalizableStringHelper.GetLocalizableString("Interface_Impot_BrowseCommand");
            btnCancel.Text = LocalizableStringHelper.GetLocalizableString("Interface_Impot_CancelCommand");
            btnOk.Text = LocalizableStringHelper.GetLocalizableString("Interface_Impot_OkCommand");
            Text = LocalizableStringHelper.GetLocalizableString("Interface_Import_WindowTittle");

        }

        private void ImportView_Load(object sender, EventArgs e)
        {

        }

        public string ConfigName { get { return txtName.Text; } }

        public string Path { get { return txtPath.Text; } }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog.FileName;
                if (string.IsNullOrWhiteSpace(txtName.Text))
                    txtName.Text = System.IO.Path.GetFileNameWithoutExtension(txtPath.Text);
            }
            
            
        }
    }
}
