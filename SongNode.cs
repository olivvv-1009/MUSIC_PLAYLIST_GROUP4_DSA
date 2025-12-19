using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDL
{
    internal class SongNode
    {
        public string FilePath { get; set; } // Đường dẫn bài hát
        public string FileName { get; set; } // Tên bài hát
        public SongNode Next { get; set; }   // Node tiếp theo
        public SongNode Previous { get; set; } // Node trước đó

        public SongNode(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
            Next = null;
            Previous = null;
        }
        public override string ToString()
        {
            return FileName;
        }
    }
}