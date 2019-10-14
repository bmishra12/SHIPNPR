//using System.ComponentModel;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;

//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Common;
//using System;
//using System.Data.Common;
//using System.Data;

//namespace ShiptalkLogic.BusinessObjects
//{

//    public enum LookupClass
//    {
//        NotPicked = 0,
//        Descriptor = 1,
//        Role = 2
//    }


//    /// <summary>
//    /// Technically: It is not possible to constrain K(key) and V(value) to be Int16, Int32 etc.
//    /// However, the objective of the class is to allow an ID (int, Int32, Int16) and a string value.
//    /// Anything else might throw an exception
//    /// </summary>
//    /// <typeparam name="K">Pass int, Int16, Int32</typeparam>
//    /// <typeparam name="V">string</typeparam>
//    public abstract class ShiptalkIDValuePairBase<K, V>  : Dictionary<K, V> 
//    {
//        private LookupClass _classSel = LookupClass.NotPicked;
//        private List<KeyValuePair<int, string>> _kvPairList = new List<KeyValuePair<int, string>>();


//        public void Add(int key, string val)
//        {
//            _kvPairList.Add(new KeyValuePair<int,string>(key, val));
//        }

//        public void Add(NameValueCollection nvColl)
//        {
//            foreach (string s in nvColl.AllKeys)
//            {
//                Add(int.Parse(s), nvColl[s]);
//            }
//        }

//        protected void Infer(IDataReader reader)
//        {
//            using (reader)
//            {
//                while (reader.Read())
//                {
//                    Add(reader.GetInt32(0), reader.GetString(1));
//                }
//            }
//        }

//        public void Load()
//        {
//            if (_classSel == LookupClass.NotPicked)
//                HandleLookupSeletionNotPicked();
//            else
//                Load(_classSel);
//        }

//        public void Load(LookupClass classSel)
//        {
//            if (_classSel != LookupClass.NotPicked)
//            {
//               switch(_classSel)
//               {
//                   case LookupClass.Descriptor:
//                       (new Descriptor()).HandleDataPopulation();
//                       break;
//               }
//                   this.HandleDataPopulation();
//            }
//            else
//                HandleLookupSeletionNotPicked();
//        }

//        private void HandleLookupSeletionNotPicked()
//        {
//            throw new ShiptalkCommon.ShiptalkException("Unable to load ID/Pair values. The Lookup table was not set.");
//        }

//        //Inheriting class will call database 
//        protected abstract void HandleDataPopulation() { }
//    }





//    public class Descriptor : ShiptalkIDValuePairBase
//    {
//        public override void HandleDataPopulation()
//        {
//            //Call DB
//            //Call infer method
//        }
//    }

//    public class Test
//    {
//        public Test(){}

//        public void TestMethod() {
//            ShiptalkKeyValuePairBase pairBase = new Descriptor();
//            pairBase.Load();

            
//        }
//    }
//}
