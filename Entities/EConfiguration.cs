using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AppResources;

namespace Entities
{
    public class EConfiguration:INotifyPropertyChanged
    {
        private uint _id;
        private string _name = "";
        private string _content = "";

        public EConfiguration()
        {
        }

        public EConfiguration(uint id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public virtual uint Id
        {
            get { return _id; }
            set {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual string Name
        {
            get { return _name; }
            set {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual string Content
        {
            get { return _content; }
            set {
                if (_name != value)
                {
                    _content = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(EConfiguration))
                return false;

            var toCompare = obj as EConfiguration;
            return toCompare.Id.Equals(Id)
                   && toCompare.Content.Equals(Content)
                   && toCompare.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()^ Name.GetHashCode() ^ Content.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}