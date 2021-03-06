﻿using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Entities;

namespace View
{
    public class ViewBase:Form, IView, INotifyPropertyChanged
    {

        public virtual DialogResult ShowMessage(MessageType type, string title, string message)
        {
            switch (type)
            {
                case MessageType.Warning:
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ViewBase
            // 
            this.ClientSize = new Size(284, 261);
            this.Name = "ViewBase";
            this.ResumeLayout(false);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}