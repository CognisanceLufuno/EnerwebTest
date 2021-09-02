using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lufuno.Domain;
using Lufuno.DomainManager;
using Lufuno.Utilities.Interfaces;
using Lufuno.DomainManager.Interfaces;
using Lufuno.Utilities;
using System.Net.Http;
using System.Web;
using System.IO;

namespace Lufuno.Host.Controllers
{
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadManager _IfileUploadManager;
        private readonly IRecordManager _IrecordManager;

        /// <summary>
        /// An implementation of a logger.
        /// </summary>
        private readonly ILogger _logger;

        public FileUploadController(IFileUploadManager fileUploadManager, ILogger logger, IRecordManager recordManager)
        {
            _IfileUploadManager = fileUploadManager;
            _logger = logger;
            _IrecordManager = recordManager;
        }

        [HttpGet]
        [Route("api/GetFileUploads")]
        public List<FileToUpload> GetFileUploads()
        {
            try
            {
                return _IfileUploadManager.GetUploadedFiles();
            }
            catch (Exception ex)
            {
                _logger.LogCriticalError(ex, "Issue Getting FileUploads");
                throw new Exception("Issue Getting FileUploads : " + ex);
            }            
        }

        [HttpGet]
        [Route("api/GetFileRecordsByFileId")]
        public List<FileRecord> GetFileRecordsByFileId(int FileId)
        {
            try
            {
                return _IrecordManager.GetFileRecords(FileId);
            }
            catch (Exception ex)
            {
                _logger.LogCriticalError(ex, "Issue Getting FileUploads");
                throw new Exception("Issue Getting FileUploads : " + ex);
            }
            
        }

        [HttpPost]
        [Route("api/fileUpload")]
        public IActionResult FileUpload([FromBody] FileToUpload file)
        {
            try
            {
                _IfileUploadManager.ProcessFile(file);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCriticalError(ex, "Issue Uploading File");
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("api/UpdateRecord")]
        public IActionResult UpdateRecord([FromBody] FileRecord record)
        {
            try
            {
                _IrecordManager.UpdateRecord(record);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCriticalError(ex, "Issue Updating Records");
                return BadRequest(ex.InnerException);
            }
        }        
    }
}