using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkCommon
{
    public enum EncryptionType
    {
        DES = 1,
        TripleDES = 2
    }

    public class EncryptorFactory
    {
        private Encryptor obj = null;

        protected EncryptorFactory() { }

        public static Encryptor CreateEncryptor(EncryptionType encType)
        {
            EncryptorFactory fact = new EncryptorFactory();
            return fact.Create(encType);
        }

        protected Encryptor Create(EncryptionType encType)
        {
            switch (encType)
            {
                case EncryptionType.TripleDES:
                    obj = new TripleDESEncryptor();
                    break;
                case EncryptionType.DES:
                    obj = new DESEncryptor();
                    break;
                default:
                    throw new NotImplementedException("Requested encryption is not handled");
            }
            return obj;
        }
    }
}
