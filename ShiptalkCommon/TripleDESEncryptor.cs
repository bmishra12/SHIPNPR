using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkCommon
{
    internal sealed class TripleDESEncryptor : Encryptor
    {
        public override string Encrypt(string ClearTextPassword)
        {
            return ShiptalkCommon.EncryptionUtil.EncryptTripleDES(ClearTextPassword);
        }

        public override string Decrypt(string encryptedPassword)
        {
            return ShiptalkCommon.EncryptionUtil.DecryptTripleDES(encryptedPassword);
        }
    }
}
