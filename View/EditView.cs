using System;
using System.Windows.Forms;
using AppResources;
using Entities;
using Model;
using Presenter;
using WinFormsSyntaxHighlighter;
using System.Drawing;
using System.Text.RegularExpressions;

namespace View
{
    public partial class EditView : ViewBase, IEditView
    {
        private readonly PEdit _presenter;
        private readonly string _commentPattern = "(^#.*$)|(#.*$)";
        private readonly string _ipPattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        private SyntaxHighlighter _highlighter;

        public EditView()
        {
            InitializeComponent();
            InitializeLanguage();
            InitializeSyntaxHighLight();
            _presenter = new PEdit(this, new HostManagerFileDal());
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

        public EConfiguration Configuration { get; set; }
        public string Content { get; set; }
        public EditMode EditMode { get; set; }
        public bool IsDirty { get; set; }

        private void EditView_Load(object sender, EventArgs e)
        {
            IsDirty = false;
            if (EditMode == EditMode.Edit)
            {
                txtName.Enabled = false;
            }
            else
            {
                txtName.Enabled = true;
                Configuration = new EConfiguration();
            }
            txtName.Text = Configuration.Name;
            txtContent.Text = Configuration.Content;
            _highlighter.ReHighlight();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.Save();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextBox controlSender = (TextBox)sender;
            if (Configuration.Name != controlSender.Text)
            {
                Configuration.Name = controlSender.Text;
                IsDirty = true;
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            RichTextBox controlSender = (RichTextBox)sender;
            if (Configuration.Content != controlSender.Text)
            {
                Configuration.Content = controlSender.Text;
                IsDirty = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.Cancel();
        }
    }
}
