using System;
using System.Collections.Generic;
using System.Text;

namespace Lufuno.DataAccess
{
    public class FileUpload
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public string FileName { get; set; }

        public DateTime LoadDate { get; set; }

        public string UserId { get; set; }

        public string base64 { get; set; }

        public int NumberOfRecords { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
