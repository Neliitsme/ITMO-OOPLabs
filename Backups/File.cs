using System;

namespace Backups
{
    public class File
    {
        public DateTime LastChangeTime { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; } // Assuming its in mb

        public File(string name, uint size, DateTime date)
        {
            this.Name = name;
            this.Size = size;
            LastChangeTime = date;
        }
    }
}