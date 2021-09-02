using Lufuno.DataAccess;
using Lufuno.DataAccessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DataAccessLogic
{
    public class FileUploadLogic : IFileUploadLogic
    {
        public List<DataAccess.FileUpload> GetFileUpoads()
        {
            try
            {
                using (var ctx = new EnergyContext())
                {
                    var query = ctx.FileUploads.OrderByDescending(x=>x.LoadDate).ToList();
                    return query;
                }
            }
            catch(Exception ex)
            {
                
                throw new Exception("There was an issue getting file uploads" + ex.InnerException);
            }

        }

        public int SaveFile(FileUpload file)
        {
            try
            {
                int saveId = 0;
                using (var ctx = new EnergyContext())
                {
                    ctx.FileUploads.Add(file);
                    saveId = ctx.SaveChanges();
                }
                return file.Id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
