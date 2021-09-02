using System;
using Lufuno.DataAccessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class FileUploadTest
    {
        [TestMethod]
        public void GetAllFileUploads()
        {
            FileUploadLogic fileuploadLogic = new FileUploadLogic();

            var files = fileuploadLogic.GetFileUpoads();

            Assert.IsTrue(files.Count > 0);
        }
    }
}
