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
    public partial class MainView : Form, IMainView
    {
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
        }

        public List<EConfiguration> Configurations
        {
            get { return listBoxConfiguration.DataSource as List<EConfiguration>; }
            set { listBoxConfiguration.DataSource = value; }
        }

        public DialogResult ShowMessage(MessageType type, string title, string message)
        {
            switch (type)
            {
                case MessageType.Alert:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                case MessageType.Error:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                case MessageType.Info:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                case MessageType.Retry:
                    return MessageBox.Show(message, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                case MessageType.YesNo:
                    return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            }
            return DialogResult.Cancel;
        }

        private void btnSetConfig_Click(object sender, EventArgs e)
        {
            _presenter.SetConfig((EConfiguration)listBoxConfiguration.SelectedItem);
        }

        private void btnImportConfig_Click(object sender, EventArgs e)
        {
            _presenter.ImportConfig();
        }
    }
}
