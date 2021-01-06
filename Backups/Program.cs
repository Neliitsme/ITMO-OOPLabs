using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups
{
    class Program
    {
        public static void Case1()
        {
            Console.WriteLine("\tCase #1");
            var filesToBackup = new List<File>()
            {
                new File("name", 100, DateTime.Now),
                new File("name2", 50, DateTime.Now)
            };
            var cleaner = new RPCleanerByQuantity(1);
            var bckp = new Backup(new Folder());
            bckp.AddCleaner(cleaner);
            bckp.AddTracking(filesToBackup);
            Console.WriteLine($"Restore point count: {bckp.RestorePointList.Count}");
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"Restore point count: {bckp.RestorePointList.Count}");
            Console.WriteLine($"ID of first backup in the list: {bckp.RestorePointList.First().Id}");
            Console.WriteLine($"Size of whole backup: {bckp.Size}");
            filesToBackup[0].Size = 1000;
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"Restore point count: {bckp.RestorePointList.Count}");
            Console.WriteLine($"ID of first backup in the list: {bckp.RestorePointList.First().Id}");
            Console.WriteLine($"Size of whole backup: {bckp.Size}");
        }

        public static void Case2()
        {
            Console.WriteLine("\tCase #2");
            var filesToBackup = new List<File>()
            {
                new File("name", 50, DateTime.Now),
                new File("name2", 50, DateTime.Now)
            };
            var cleaner = new RPCleanerBySize(150);
            var bckp = new Backup(new Archive());
            bckp.AddCleaner(cleaner);
            bckp.AddTracking(filesToBackup);
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"Restore point count: {bckp.RestorePointList.Count}");
            Console.WriteLine(bckp.RestorePointList.First().Id);
        }

        public static void Case3()
        {
            Console.WriteLine("\tCase #3");
            var filesToBackup = new List<File>()
            {
                new File("name", 50, new DateTime(2020, 11, 30)),
                new File("name2", 50, new DateTime(2020, 11, 30))
            };
            var bckp = new Backup(new Folder());
            bckp.AddTracking(filesToBackup);
            bckp.CreateIncrementalRestorePoint();
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"RP count: {bckp.RestorePointList.Count}");
            filesToBackup[0].Size = 51;
            filesToBackup[0].LastChangeTime = new DateTime(2020, 12, 1);
            bckp.CreateIncrementalRestorePoint();
            Console.WriteLine($"RP count: {bckp.RestorePointList.Count}");
            Console.WriteLine("Files in this list:");
            foreach (var file in bckp.RestorePointList.Last().FilesList)
            {
                Console.WriteLine(file.Name);
            }
            var cleaner = new RPCleanerByDate(new DateTime(2020, 12, 2));
            bckp.AddCleaner(cleaner);
            bckp.CallCleaner();
            Console.WriteLine(bckp.RestorePointList.Count);
        }

        public static void Case4()
        {
            Console.WriteLine("\tCase #4");
            var sizeCleaner = new RPCleanerBySize(250);
            var quantityCleaner = new RPCleanerByQuantity(3);
            var filesToBackup = new List<File>()
            {
                new File("name", 50, DateTime.Now),
                new File("name2", 10, DateTime.Now)
            };
            var bckp = new Backup(new Archive());
            bckp.AddTracking(filesToBackup);
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"RP count: {bckp.RestorePointList.Count}");
            bckp.AddCleaner(sizeCleaner, quantityCleaner);
            bckp.CallCleaner();
            Console.WriteLine($"RP count: {bckp.RestorePointList.Count}");
            filesToBackup[1].Size = 200;
            bckp.CreateFullRestorePoint();
            Console.WriteLine($"RP count: {bckp.RestorePointList.Count}");
        }
        
        
        
        static void Main(string[] args)
        {
            Case1();
            Case2();
            Case3();
            Case4();
        }
    }
}