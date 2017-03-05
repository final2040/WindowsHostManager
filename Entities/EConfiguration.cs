using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppResources;

namespace Entities
{
    public class EConfiguration
    {

        public EConfiguration()
        {
        }

        public EConfiguration(uint id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public virtual uint Id { get; set; }
        public virtual string Name { get; set; } = "";
        public virtual string Content { get; set; } = "";

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
    }
}