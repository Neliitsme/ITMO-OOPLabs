using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups
{
    public class Backup
    {
        private static int _idGenerator = 1;
        public int Id { get; private set; }
        public DateTime CreationTime { get; set; }
        public uint Size { get; private set; }
        public List<RestorePoint> RestorePointList { get; private set; } = new List<RestorePoint>();
        private List<File> _trackingFilesList = new List<File>();
        private List<IRestorePointCleaner> _rpcList = new List<IRestorePointCleaner>();
        private IStoreable Storage { get; set; }

        private void CleanFirstPoint()
        {
            Size -= RestorePointList.First().Size;
            RestorePointList.Remove(RestorePointList.First());
            while (RestorePointList.Count != 0 && RestorePointList.First() is IncrementalRestorePoint)
            {
                Size -= RestorePointList.First().Size;
                RestorePointList.Remove(RestorePointList.First());
            }
        }

        public Backup(IStoreable storage)
        {
            Id = _idGenerator;
            _idGenerator++;
            CreationTime = DateTime.Now;
            this.Storage = storage;
        }

        public void AddCleaner(params IRestorePointCleaner[] rpc)
        {
            foreach (var restorePointCleaner in rpc)
            {
                if (_rpcList.Contains(restorePointCleaner))
                {
                    Console.WriteLine("Can't have more than 1 cleaner of the same type");
                    continue;
                }

                _rpcList.Add(restorePointCleaner);
            }
        }

        public void CallCleaner()
        {
            if (_rpcList.Count == 0 || RestorePointList.Count == 0)
            {
                return;
            }

            foreach (var cleaner in _rpcList)
            {
                while (RestorePointList.Count != 0 && cleaner.Invoke(this))
                {
                    CleanFirstPoint();
                }
            }
        }

        public void RemoveCleaner(params IRestorePointCleaner[] rpc)
        {
            foreach (var restorePointCleaner in rpc)
            {
                if (_rpcList.Contains(restorePointCleaner))
                {
                    _rpcList.Remove(restorePointCleaner);
                }
            }
        }
        
        public void AddTracking(List<File> files)
        {
            foreach (var file in files.Where(file => !_trackingFilesList.Contains(file)))
            {
                _trackingFilesList.Add(file);
            }
        }

        public void RemoveTracking(List<File> files)
        {
            foreach (var file in files.Where(file => _trackingFilesList.Contains(file)))
            {
                _trackingFilesList.Remove(file);
            }
        }

        public void CreateFullRestorePoint()
        {
            var backedFiles = _trackingFilesList.Select(file => new File(file.Name, file.Size, file.LastChangeTime))
                .ToList();
            var rp = new FullRestorePoint(DateTime.Now, backedFiles);
            Size += rp.Size;
            RestorePointList.Add(rp);
            Storage.CopyFiles(backedFiles);
            CallCleaner();
        }

        public void CreateIncrementalRestorePoint()
        {
            if (RestorePointList.Count == 0)
            {
                Console.WriteLine("Need to create a full restore point beforehand, skipping");
                return;
            }

            var lastRestorePointFilesList = RestorePointList.Last().FilesList;
            var changedFiles = _trackingFilesList
                .SelectMany(file => lastRestorePointFilesList, (file, lastFile) => new {file, lastFile})
                .Where(t => t.file.Name == t.lastFile.Name && t.lastFile.LastChangeTime < t.file.LastChangeTime)
                .Select(t => new File(t.file.Name, t.file.Size, t.file.LastChangeTime)).ToList();

            if (changedFiles.Count == 0)
            {
                return;
            }

            IncrementalRestorePoint rp = new IncrementalRestorePoint(DateTime.Now, changedFiles);
            Size += rp.Size;
            RestorePointList.Add(rp);
            Storage.CopyFiles(changedFiles);
            CallCleaner();
        }
        
    }
}