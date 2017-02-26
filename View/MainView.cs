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
using Model;
using Presenter;

namespace View
{
    public partial class MainView : ViewBase, IMainView
    {
        // TODO: Crear un mecanismo para deshabilitar botones de borrado actualizado y edición cuando no hay items
        private PMain _presenter;
        public MainView()
        {
            InitializeComponent();
            _presenter = new PMain(this, new ImportView(), new HostManagerFileDal(new FileHelper()));
            listBoxConfiguration.DisplayMember = "Name";
            InitializeLanguaje();
            UpdateView();
        }

        private void UpdateView()
        {
            _presenter.UpdateView();
        }

        private void InitializeLanguaje()
        {
            btnSetConfig.Text = LocalizableStringHelper.GetLocalizableString("Interface_SetCommand");
            btnImportConfig.Text = LocalizableStringHelper.GetLocalizableString("Interface_ImportCommand");
            btnDelete.Text = LocalizableStringHelper.GetLocalizableString("Interface_DeleteCommand");
        }

        public List<EConfiguration> Configurations
        {
            get { return listBoxConfiguration.DataSource as List<EConfiguration>; }
            set { listBoxConfiguration.DataSource = value; }
        }

        private void btnSetConfig_Click(object sender, EventArgs e)
        {
            _presenter.SetConfig((EConfiguration)listBoxConfiguration.SelectedItem);
        }

        private void btnImportConfig_Click(object sender, EventArgs e)
        {
            _presenter.ImportConfig();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.Delete((EConfiguration)listBoxConfiguration.SelectedItem);
        }
    }
}
