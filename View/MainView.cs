using System;
using System.Collections.Generic;
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
        private readonly PMain _presenter;
        private readonly About _about = new About();
        public MainView()
        {
            InitializeComponent();
            _presenter = new PMain(this, new ImportView(),new EditView(), new HostManagerFileDal(new FileHelper()));
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
            menuFile.Text = LocalizableStringHelper.GetLocalizableString("Interface_FileMenuItem");
            menuExit.Text = LocalizableStringHelper.GetLocalizableString("Interface_ExitMenuItem");
            menuEdit.Text = LocalizableStringHelper.GetLocalizableString("Interface_EditMenuItem");
            menuEditCommand.Text = LocalizableStringHelper.GetLocalizableString("Interface_EditCommand");
            menuSet.Text = LocalizableStringHelper.GetLocalizableString("Interface_SetCommand");
            menuImport.Text = LocalizableStringHelper.GetLocalizableString("Interface_ImportCommand");
            menuDelete.Text = LocalizableStringHelper.GetLocalizableString("Interface_DeleteCommand");
            menuHelp.Text = LocalizableStringHelper.GetLocalizableString("Interface_HelpMenuItem");
            menuAbout.Text = LocalizableStringHelper.GetLocalizableString("Interface_AboutMenuItem");
            btnEdit.Text = LocalizableStringHelper.GetLocalizableString("Interface_EditCommand");
        }

        public List<EConfiguration> Configurations
        {
            get { return listBoxConfiguration.DataSource as List<EConfiguration>; }
            set { listBoxConfiguration.DataSource = value; }
        }

        private void SetConfigConfigClick(object sender, EventArgs e)
        {
            _presenter.SetConfig((EConfiguration)listBoxConfiguration.SelectedItem);
        }

        private void ImportConfigClick(object sender, EventArgs e)
        {
            _presenter.ImportConfig();
        }

        private void DeleteConfigClick(object sender, EventArgs e)
        {
            _presenter.Delete((EConfiguration)listBoxConfiguration.SelectedItem);
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutClick(object sender, EventArgs e)
        {
            _about.ShowDialog();
        }

        private void EditClick(object sender, EventArgs e)
        {
            _presenter.Edit((EConfiguration)listBoxConfiguration.SelectedItem);
        }
    }
}
