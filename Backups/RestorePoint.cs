using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Backups
{
    public class RestorePoint
    {
        private static int _idGenerator = 1;
        public int Id { get; private set; }
        public uint Size { get; private set; }
        public DateTime Date { get; private set; }
        public List<File> FilesList { get; private set; }

        public RestorePoint(DateTime date, List<File> files)
        {
            Date = date;
            FilesList = files;
            Id = _idGenerator;
            _idGenerator++;
            foreach (var file in files)
            {
                Size += file.Size;
            }
        }
    }

    public class FullRestorePoint : RestorePoint
    {
        public FullRestorePoint(DateTime date, List<File> files) : base(date, files)
        {
        }
    }

    public class IncrementalRestorePoint : RestorePoint
    {
        public IncrementalRestorePoint(DateTime date, List<File> files) : base(date, files)
        {
        }
    }
}