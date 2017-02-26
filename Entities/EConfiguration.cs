using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class EConfiguration
    {
        public EConfiguration(){}

        public EConfiguration(uint id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }
        public uint Id { get; set; }
        [Required]
        [StringLength(20,ErrorMessage = "El nombre de la configuración no puede ser mayor a 20 carácteres")]
        public string Name { get; set; } = "";
        [Required]
        public string Content { get; set; } = "";

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