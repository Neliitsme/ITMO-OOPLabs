using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public interface IStoreable
    {
        void CopyFiles(List<File> files);
    }
    
    public class Archive : IStoreable
    {
        public void CopyFiles(List<File> files)
        {
        }
    }

    public class Folder : IStoreable
    {
        List<string> folder = new List<string>();
        public void CopyFiles(List<File> files)
        {
        }
    }
}