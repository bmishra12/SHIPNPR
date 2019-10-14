//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ShiptalkLogic.BusinessObjects
//{
//    class ShiptalkPrincipal
//    {
//        public bool IsAdmin { get; set; }

        

//    }

//    public enum ShiptalkPrincipalTypes
//    {
//        CMS = 1,
//        State = 2

//    }

//    public class ShiptalkPrincipalFactory
//    {
//        private ShiptalkPrincipal obj = null;

//        protected ShiptalkPrincipalFactory() { }

//        public static ShiptalkPrincipal CreateShiptalkPrincipal(ShiptalkPrincipalTypes encType)
//        {
//            ShiptalkPrincipalFactory fact = new ShiptalkPrincipalFactory();
//            return fact.Create(encType);
//        }

//        protected ShiptalkPrincipal Create(ShiptalkPrincipalTypes encType)
//        {
//            switch (encType)
//            {
//                case ShiptalkPrincipalTypes.CMS:
//                    obj = new TripleDESShiptalkPrincipal();
//                    break;
//                case ShiptalkPrincipalTypes.State:
//                default:
//                    throw new NotImplementedException("Requested ShiptalkPrincipalTypes is not handled");
//            }
//            return obj;
//        }
//    }
//}
