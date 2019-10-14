using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkCommon
{
    public abstract class Encryptor
    {
        protected byte[] Key1 { get; set; }
        protected byte[] Key2 { get; set; }

        public abstract string Encrypt(string ClearTextPassword);

        public abstract string Decrypt(string ClearTextPassword);

        //protected Encryptor CreateEncryptor(EncryptionType encType)
        //{
        //    return EncryptorFactory.CreateEncryptor(encType);
        //}

    }

  


   

}
