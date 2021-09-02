using Lufuno.DataAccess;
using Lufuno.DataAccessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DataAccessLogic
{
    public class RecordLogic : IRecordLogic
    {
        public int SaveRecord(List<Record> records)
        {
            int saveId = 0;
            using (var ctx = new EnergyContext())
            {
                ctx.Set<Record>().AddRange(records);
                //ctx.Records.Add(record);
                saveId = ctx.SaveChanges();
            }
            return saveId;
        }

        public List<DataAccess.Record> GetFileRecords(int fileId)
        {
            try
            {
                using (var ctx = new EnergyContext())
                {
                    var query = ctx.Records.Where(r => r.FileId == fileId).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue getting file Records" + ex.InnerException);
            }

        }

        public List<Record> GetUnApprovedRecords()
        {
            using (var ctx = new EnergyContext())
            {
                return ctx.Records.Where(s => !s.IsApproved).ToList();
            }
        }

        public void UpdateRecord(Record record)
        {
            using (var ctx = new EnergyContext())
            {                
                ctx.Entry(record).State = record.Id == 0 ?
                EntityState.Added :
                EntityState.Modified;

                ctx.SaveChanges();
            }
        }
    }
}
