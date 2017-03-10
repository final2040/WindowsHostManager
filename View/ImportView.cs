using System;
using System.Windows.Forms;
using AppResources;
using Entities;
using Microsoft.Practices.Unity;
using Model;
using Presenter;

namespace View
{
    public partial class ImportView : ViewBase, IImportFileView
    {
        private PImport _presenter;
        private string _configName;
        private string _path;

        #region Properties

        public string ConfigName
        {
            get { return _configName; }
            set {
                if (_configName != value)
                {
                    _configName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Path
        {
            get { return _path; }
            set {
                if (_path != value)
                {
                    _path = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Initialize
        public ImportView()
        {
            InitializeComponent();
            InitializePresenter();
            InitializeLanguage();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            txtName.DataBindings.Add("Text", this, "ConfigName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPath.DataBindings.Add("Text", this, "Path", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void InitializePresenter()
        {
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<IImportFileView>(this);
            unityContainer.RegisterType<IHostManager, HostManagerFileDal>();
            _presenter = unityContainer.Resolve<PImport>();
        }

        private void InitializeLanguage()
        {
            lblName.Text = LocalizableStringHelper.GetLocalizableString(lblName.Text);
            lblPath.Text = LocalizableStringHelper.GetLocalizableString(lblPath.Text);
            btnBrowse.Text = LocalizableStringHelper.GetLocalizableString(btnBrowse.Text);
            btnCancel.Text = LocalizableStringHelper.GetLocalizableString(btnCancel.Text);
            btnOk.Text = LocalizableStringHelper.GetLocalizableString(btnOk.Text);
            Text = LocalizableStringHelper.GetLocalizableString(Text);

        }
        #endregion

        #region Methods

        private void ImportView_Load(object sender, EventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _presenter.Import();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Path = openFileDialog.FileName;
                if (string.IsNullOrWhiteSpace(ConfigName))
                    ConfigName = System.IO.Path.GetFileNameWithoutExtension(Path);
            }
        }

        #endregion

    }
}
