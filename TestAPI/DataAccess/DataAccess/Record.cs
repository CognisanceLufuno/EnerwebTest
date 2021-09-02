using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lufuno.DataAccess
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public int FileId { get; set; }
        
        [Required]
        public DateTime OperatingDate { get; set; }
        public string ServicePoint { get; set; }

        [Required]
        public int HourNumber { get; set; }
        public string UserId { get; set; }
        public double ImportEnergy { get; set; }

        [Required]
        public double ExportEnergy { get; set; }
        public double ImportLeadingReactive { get; set; }
        public double ExportLeadingReactive { get; set; }
        public double ImportLaggingReactive { get; set; }
        public double ExportLaggingReactive { get; set; }
        public bool IsOfficial { get; set; }

        public bool IsApproved { get; set; }

        public FileUpload Upload { get; set; }
    }
}
