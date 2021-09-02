using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.Utilities.Interfaces
{
    public interface IUtil
    {
        string getASCIIFromByte(byte[] input);

        string getASCIIFromBase64(string input);

        DataAccess.FileUpload GetFileDetailsFromAscii(string FileAsBase64, Lufuno.DataAccess.FileUpload fileUpload);
    }
}
