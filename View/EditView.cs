using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppResources;
using Entities;
using Microsoft.Practices.Unity;
using Model;
using Presenter;
using WinFormsSyntaxHighlighter;

namespace View
{
    public partial class EditView : ViewBase, IEditView
    {
        private EditPresenter _presenter;
        private readonly string _commentPattern = "(^#.*$)|(#.*$)";
        private readonly string _ipPattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        private SyntaxHighlighter _highlighter;
        private EConfiguration _configuration = new EConfiguration();


        #region InitializeView

        public EditView()
        {
            InitializeComponent();
            InitializePresenter();
            InitializeLanguage();
            InitializeSyntaxHighLight();
        }

        private void InitializePresenter()
        {
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<IEditView>(this);
            unityContainer.RegisterType<IHostManager, HostManagerFileDal>();
            _presenter = unityContainer.Resolve<EditPresenter>();
        }

        private void InitializeSyntaxHighLight()
        {
            _highlighter = new SyntaxHighlighter(txtContent);
            _highlighter.AddPattern(new PatternDefinition(new Regex(_commentPattern, RegexOptions.Multiline)),
                new SyntaxStyle(Color.DarkGreen));
            _highlighter.AddPattern(new PatternDefinition(new Regex(_ipPattern)),
                new SyntaxStyle(Color.Blue, true, false));
        }

        private void InitializeLanguage()
        {
            lblName.Text = LocalizableStringHelper.GetLocalizableString(lblName.Text);
            lblContent.Text = LocalizableStringHelper.GetLocalizableString(lblContent.Text);
            btnCancel.Text = LocalizableStringHelper.GetLocalizableString(btnCancel.Text);
            btnSave.Text = LocalizableStringHelper.GetLocalizableString(btnSave.Text);
            Text = LocalizableStringHelper.GetLocalizableString(Text);
        }

        #endregion


        #region Properties

        public EConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        public EditMode EditMode { get; set; }
        public bool IsDirty { get; set; }

        #endregion

        #region Methods

        private void EditView_Load(object sender, EventArgs e)
        {
            txtName.Enabled = EditMode != EditMode.Edit;
            BindView();
        }

        private void ConfigurationOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            IsDirty = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.Cancel();
        }

        private void BindView()
        {
            txtName.DataBindings.Add("Text", Configuration, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            txtContent.DataBindings.Add("Text", Configuration, "Content", false, DataSourceUpdateMode.OnPropertyChanged);
            Configuration.PropertyChanged += ConfigurationOnPropertyChanged;
        }

        #endregion

    }
}
