using System;
using System.Collections.Generic;
using System.Text;

namespace Lufuno.DataAccess
{
    public class Record
    {
        public int MyProperty { get; set; }
        public DateTime OperatingDate { get; set; }
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
        public FileUpload Upload { get; set; }
    }
}
