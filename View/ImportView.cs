using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppResources;
using Entities;
using Model;
using Presenter;

namespace View
{
    public partial class ImportView : ViewBase, IImportFileView
    {
        private readonly PImport _presenter;

        #region Properties
        public string ConfigName
        {
            get { return txtName.Text; }
        }
        public string Path
        {
            get { return txtPath.Text; }
        }
        #endregion

        #region Initialize
        public ImportView()
        {
            InitializeComponent();
            _presenter = new PImport(this, new HostManagerFileDal());
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
        #endregion

        #region Methods

        private void ImportView_Load(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPath.Text = "";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _presenter.Import();
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

        #endregion

    }
}
