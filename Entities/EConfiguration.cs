using System;

namespace Entities
{
    public class EConfiguration
    {
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public string Path { get; set; }
        public string Content { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(EConfiguration))
                return false;

            var toCompare = obj as EConfiguration;
            return toCompare.Path.Equals(Path)
                   && toCompare.Content.Equals(Content);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode() ^ Content.GetHashCode();
        }
    }
}