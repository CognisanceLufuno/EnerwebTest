using Lufuno.DataAccess;
using Lufuno.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lufuno.Utilities
{
    public class Util : IUtil
    {
        public string getASCIIFromByte(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException();
            return System.Text.Encoding.ASCII.GetString(input);
        }

        public string getASCIIFromBase64(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            input = input.Replace("data:application/octet-stream;base64,", "");
            var base64EncodedBytes = System.Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public bool IsValidFile(string base64Input)
        {
            string ascii = getASCIIFromBase64(base64Input);
            if (ascii.Length >= 5)
            {
                string firstFewCharacters = ascii.Substring(0, 6);
                string lastFewCharacters = ascii.Substring(Math.Max(0, ascii.Length - 10));

                return firstFewCharacters.ToLowerInvariant().Contains("a00")
                       && lastFewCharacters.ToLowerInvariant().Contains("z99");

            }
            return false;
        }

        public List<string> GetRecordsFromFile(string base64Input)
        {
            var separator = '\n';
            string ascii = getASCIIFromBase64(base64Input);

            //Each record is separated by a new line
            var records = ascii.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            /*Each record is separated by a new line... except if a comma is the last character on the line
            Then in that case, the record spans more than one line.
             */
            string stringToJoin = string.Empty;
            List<string> joinedList = new List<string>();
            foreach (var record in records)
            {
                /*if we saved the previous line in stringToJoin in our previous iteration,
                 * append it to the current record (so they form one continous record
                */
                if (!string.IsNullOrEmpty(stringToJoin))
                {
                    joinedList.Add(string.Format("{0} {1}", stringToJoin, record));
                    stringToJoin = string.Empty;
                }
                else
                {
                    joinedList.Add(record);
                }

                //A record may continue on a new line if the proceeding line ends with a comma
                if (record.Substring(record.Length - 1) == ",")
                {
                    stringToJoin = record;
                }
            }
            return joinedList;
        }

        public FileUpload GetFileDetailsFromAscii(string base64Input, FileUpload fileUpload)
        {
            base64Input = base64Input.Replace("data:application/octet-stream;base64,", "");
            List<string> recordEntries = GetRecordsFromFile(base64Input);
            List<Record> recordList = new List<Record>();

            DateTime currentOperatingDate = DateTime.MinValue;
            string currentServicePoint = string.Empty;
            string UserId = string.Empty;

            foreach (var recordEntry in recordEntries)
            {
                if (!recordEntry.Contains("Z99"))
                {
                    if (recordEntry.Contains("A00"))
                    {
                        UserId = recordEntry.Split(',').ElementAt(1).Replace("\"", "");
                        fileUpload.UserId = UserId;
                    }

                    if (recordEntry.Contains("S10"))
                    {
                        string sDate = recordEntry.Split(',').ElementAt(1).Replace("\"", "");
                        currentOperatingDate = DateTime.ParseExact(sDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    if (recordEntry.Contains("S12"))
                    {
                        currentServicePoint = recordEntry.Split(',').ElementAt(1).Replace("\"", "");
                    }

                    if (recordEntry.Contains("S22"))
                    {
                        recordList.Add(PopulateDataAccessRecordFields(recordEntry, UserId, currentOperatingDate, currentServicePoint));
                    }
                }
            }

            fileUpload.Records = recordList;
            fileUpload.NumberOfRecords = recordList.Count;

            return fileUpload;
        }

        private DataAccess.Record PopulateDataAccessRecordFields(string RecordEntry, string userId, DateTime OperatingDate, string servicePoint)
        {
            DataAccess.Record DataAccessRecord = new Record();
            List<string> fields = RecordEntry.Split(',').ToList();

            DataAccessRecord.Uid = Guid.NewGuid();
            DataAccessRecord.UserId = userId;
            DataAccessRecord.OperatingDate = OperatingDate;
            DataAccessRecord.ServicePoint = servicePoint;
            DataAccessRecord.HourNumber = fields[1] != null? Convert.ToInt16(fields[1]): 0;
            DataAccessRecord.ImportEnergy = fields[2] != null ? Convert.ToDouble(fields[2]) : 0;
            DataAccessRecord.ExportEnergy = fields[3] != null ? Convert.ToDouble(fields[3]) : 0;
            DataAccessRecord.ImportLeadingReactive = fields[4] != null ? Convert.ToDouble(fields[4]) : 0;
            DataAccessRecord.ExportLeadingReactive = fields[5] != null ? Convert.ToDouble(fields[5]) : 0;
            DataAccessRecord.ImportLaggingReactive = fields[6] != null ? Convert.ToDouble(fields[6]) : 0;
            DataAccessRecord.ExportLaggingReactive = fields[7] != null ? Convert.ToDouble(fields[7]) : 0;
            DataAccessRecord.IsOfficial = fields[8] != null ? Convert.ToBoolean(Convert.ToInt16(fields[8])) : false;

            return DataAccessRecord;
        }
    }
}
