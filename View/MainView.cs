using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AppResources;
using Entities;
using Microsoft.Practices.Unity;
using Model;
using Presenter;

namespace View
{
    public partial class MainView : ViewBase, IMainView
    {
        // TODO: Crear un mecanismo para deshabilitar botones de borrado actualizado y edición cuando no hay items

        #region Fields

        private PMain _presenter;
        private readonly About _about = new About();
        private List<EConfiguration> _configurations;
        private EConfiguration _selectedConfiguration;

        #endregion


        #region InitializeView

        public MainView()
        {
            InitializeComponent();
            InitializePresenter();
            InitializeLanguaje();
            BindControls();
            _presenter.BackupConfig(false);
        }

        private void BindControls()
        {
            listBoxConfiguration.DisplayMember = "Name";
            listBoxConfiguration.DataBindings.Add("DataSource", this, "Configurations", false,
                DataSourceUpdateMode.OnPropertyChanged);
            listBoxConfiguration.DataBindings.Add("SelectedItem", this, "SelectedConfiguration", false,
                DataSourceUpdateMode.OnValidation);
        }

        private void InitializePresenter()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterInstance<IMainView>(this);
            container.RegisterType<IViewFactory, ViewFactory>();
            container.RegisterType<IHostManager, HostManagerFileDal>();
            _presenter = container.Resolve<PMain>();
        }

        private void InitializeLanguaje()
        {
            btnSetConfig.Text = LocalizableStringHelper.GetLocalizableString(btnSetConfig.Text);
            btnImportConfig.Text = LocalizableStringHelper.GetLocalizableString(btnImportConfig.Text);
            btnDelete.Text = LocalizableStringHelper.GetLocalizableString(btnDelete.Text);
            menuFile.Text = LocalizableStringHelper.GetLocalizableString(menuFile.Text);
            menuExit.Text = LocalizableStringHelper.GetLocalizableString(menuExit.Text);
            menuEdit.Text = LocalizableStringHelper.GetLocalizableString(menuEdit.Text);
            menuEditCommand.Text = LocalizableStringHelper.GetLocalizableString(menuEditCommand.Text);
            menuSet.Text = LocalizableStringHelper.GetLocalizableString(menuSet.Text);
            menuImport.Text = LocalizableStringHelper.GetLocalizableString(menuImport.Text);
            menuDelete.Text = LocalizableStringHelper.GetLocalizableString(menuDelete.Text);
            menuHelp.Text = LocalizableStringHelper.GetLocalizableString(menuHelp.Text);
            menuAbout.Text = LocalizableStringHelper.GetLocalizableString(menuAbout.Text);
            btnEdit.Text = LocalizableStringHelper.GetLocalizableString(btnEdit.Text);
            btnNew.Text = LocalizableStringHelper.GetLocalizableString(btnNew.Text);
            menuNewCommand.Text = LocalizableStringHelper.GetLocalizableString(menuNewCommand.Text);
            menuBackup.Text = LocalizableStringHelper.GetLocalizableString(menuBackup.Text);
        }

        #endregion


        #region Properties

        public List<EConfiguration> Configurations
        {
            get { return _configurations; }
            set
            {
                if (_configurations != value)
                {
                    _configurations = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public EConfiguration SelectedConfiguration
        {
            get { return _selectedConfiguration; }
            set
            {
                if (_selectedConfiguration != value)
                {
                    _selectedConfiguration = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion


        #region Methods

        private void SetConfigConfigClick(object sender, EventArgs e)
        {
            _presenter.SetConfig();
        }

        private void ImportConfigClick(object sender, EventArgs e)
        {
            _presenter.ImportConfig();
        }

        private void DeleteConfigClick(object sender, EventArgs e)
        {
            _presenter.Delete();
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
            _presenter.Edit();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _presenter.New();
        }

        private void listBoxConfiguration_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListBox listbox = (ListBox) sender;
            if (listbox.SelectedItem != null)
                _presenter.Edit();

        }

        private void menuBackup_Click(object sender, EventArgs e)
        {
            _presenter.BackupConfig(true);
        }

        // fix for binding property - selected index only change when control lost focus
        // so is necesary update bindings when user change index
        private void listBoxConfiguration_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ListBox) sender).DataBindings["SelectedItem"]?.WriteValue();
        }

        #endregion

    }
}
