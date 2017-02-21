using System;

namespace Entities
{
    public class EConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Content { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(EConfiguration))
                return false;

            var toCompare = obj as EConfiguration;
            return toCompare.Content.Equals(Content)
                   && toCompare.Id.Equals(Id)
                   && toCompare.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Content.GetHashCode() ^ Name.GetHashCode();
        }
    }
}