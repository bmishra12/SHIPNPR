using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkCommon
{
    internal sealed class DESEncryptor : Encryptor
    {
        public override string Encrypt(string ClearTextPassword)
        {
            return ShiptalkCommon.EncryptionUtil.EncryptDES(ClearTextPassword);
        }

        public override string Decrypt(string EncryptedPassword)
        {
            throw new NotImplementedException("Decrypt DES password is not implemented");
            //return ShiptalkCommon.EncryptionUtil.DESDecrypt(EncryptedPassword);
        }

        
    }
}
