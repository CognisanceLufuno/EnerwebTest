using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lufuno.DataAccess
{
    public class FileUpload
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public DateTime LoadDate { get; set; }
        
        [Required]
        public string UserId { get; set; }

        [Required]
        public string base64 { get; set; }

        [Required]
        public int NumberOfRecords { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
