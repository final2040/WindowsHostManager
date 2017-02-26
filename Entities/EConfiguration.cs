using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppResources;

namespace Entities
{
    public class EConfiguration
    {
        // TODO: Encontrar una forma de parametrizar los mensajes de error

        public EConfiguration()
        {
        }

        public EConfiguration(uint id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public uint Id { get; set; }

        [Required(ErrorMessage = "Debe especificar un nombre para la configuración")]
        [RegularExpression("([A-Za-z0-9])", ErrorMessage = "El nombre de la configuración solo puede contener carácteres alfanúmericos, guión medio y guión bajo")]
        [StringLength(20,ErrorMessage = "El nombre de la configuración no puede ser mayor a 20 carácteres")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Debe de especificar un contenido para la configuración")]
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