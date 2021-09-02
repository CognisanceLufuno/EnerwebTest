using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.Domain
{
    public class FileToUpload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        //public string FileSize { get; set; }
        // string FileType { get; set; }
        //public long LastModifiedTime { get; set; }
        //public string LastModifiedDate { get; set; }
        public string CreateDate { get; set; }

        public string FileAsBase64 { get; set; }
        //public byte[] FileAsByteArray { get; set; }
    }
}
