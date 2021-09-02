using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.Domain
{
    public class FileRecord
    {
        public int Id { get; set; }

        public string Uid { get; set; }

        public int FileId { get; set; }

        public string OperatingDate { get; set; }
        public string ServicePoint { get; set; }
        public int HourNumber { get; set; }
        public string UserId { get; set; }
        public double ImportEnergy { get; set; }

        public double ExportEnergy { get; set; }
        public double ImportLeadingReactive { get; set; }
        public double ExportLeadingReactive { get; set; }
        public double ImportLaggingReactive { get; set; }
        public double ExportLaggingReactive { get; set; }
        public bool IsOfficial { get; set; }

        public bool IsApproved { get; set; }
        //public FileToUpload Upload { get; set; }
    }
}
