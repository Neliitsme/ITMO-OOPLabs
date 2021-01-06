using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Backups
{
    public interface IRestorePointCleaner
    {
        bool Invoke(Backup backup);
        
    }
    //True means needs removal
    public class RPCleanerByQuantity : IRestorePointCleaner
    {
        private int MaxNumberOfPoints { get; set; }

        public RPCleanerByQuantity(int maxNumberOfPoints)
        {
            this.MaxNumberOfPoints = maxNumberOfPoints;
        }

        public bool Invoke(Backup backup) => backup.RestorePointList.Count > MaxNumberOfPoints;
    }

    public class RPCleanerByDate : IRestorePointCleaner
    {
        private DateTime NoOlderThan { get; set; }

        public RPCleanerByDate(DateTime noOlderThan)
        {
            this.NoOlderThan = noOlderThan;
        }

        public bool Invoke(Backup backup) => backup.RestorePointList.First().Date < NoOlderThan;
    }

    public class RPCleanerBySize : IRestorePointCleaner
    {
        private uint MaxSize { get; set; }

        public RPCleanerBySize(uint maxSize)
        {
            this.MaxSize = maxSize;
        }

        public bool Invoke(Backup backup) => backup.Size > MaxSize;
    }
}