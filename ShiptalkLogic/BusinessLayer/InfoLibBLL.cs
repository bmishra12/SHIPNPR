using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects.UI;

namespace ShiptalkLogic.BusinessLayer
{
    public class InfoLibBLL
    {

        /// <summary>
        /// Returns new InfoLibItem record Id value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int? AddInfoLibItem(InfoLibItem item, out string FailureReason)
        {
            int NewInfoLibItemId;

            InfoLibDAL dal = new InfoLibDAL();
            if (dal.AddInfoLibItem(item, out NewInfoLibItemId, out FailureReason))
                return NewInfoLibItemId;
            else
                return null;
        }

        /// <summary>
        /// Updates an InfoLibitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool UpdateInfoLibItem(InfoLibItem item, out string FailureReason)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.UpdateInfoLibItem(item, out FailureReason);
        }

        /// <summary>
        /// Deletes an InfoLibitem and all its child items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool DeleteInfoLibItem(int InfoLibItemId, Scope? scope)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.DeleteInfoLibItem(InfoLibItemId, scope.HasValue ? scope.Value.EnumValue<int>() : (int?)null);
        }


        public static IEnumerable<InfoLibItem> GetInfoLibItems(Scope? ScopeId, int ParentId)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetInfoLibItems(ScopeId, ParentId);
        }

        public static IEnumerable<InfoLibItem> GetTopLevelInfoLibItems(bool IsArchived)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetTopLevelInfoLibItems(IsArchived);
        }

        public static InfoLibItem GetInfoLibItem(int InfoLibItemId, bool IncludeInfoLibItemLinkFile)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetInfoLibItem(InfoLibItemId, IncludeInfoLibItemLinkFile);
        }

        public static InfoLibItem GetInfoLibTopLevelParent(int InfoLibItemId, bool IsArchived)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetInfoLibTopLevelParent(InfoLibItemId, IsArchived);
        }


        public static InfoLibItemFile GetInfoLibFile(int InfoLibItemId, InfoLibFileServerTypes type)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetInfoLibFile(InfoLibItemId, type);
        }

        public static IEnumerable<InfoLibItem> SearchInfoLibItems(Scope? ScopeOfSearcher, string SearchText, bool IsArchived)
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.SearchInfoLibItems(ScopeOfSearcher, SearchText, IsArchived);

        }

        //public static IEnumerable<InfoLibSpecialIdentifiers> GetSpecialIdentifiers()
        //{
        //    InfoLibDAL dal = new InfoLibDAL();
        //    var identifiers = dal.GetSpecialIdentifiers();
        //    if (identifiers != null && identifiers.Any())
        //    {
        //        foreach (var id in identifiers)
        //            yield return id.Key.ToEnumObject<InfoLibSpecialIdentifiers>();
        //    }
        //}

        public static IEnumerable<KeyValuePair<int, string>> GetInfoLibSpecialIdentifiers()
        {
            var vals = Enum.GetValues(typeof(InfoLibSpecialIdentifiers)).AsQueryable();
            foreach (int val in vals)
                yield return new KeyValuePair<int, string>(val, val.ToEnumObject<InfoLibSpecialIdentifiers>().Description());
        }

        public static KeyValuePair<string, int> GetInfoLibPublicScope()
        {
            return new KeyValuePair<string, int>("Everyone, including public", 0);
        }

        public static InfoLibForumCallItem GetInfoLibForumCallItem()
        {
            InfoLibDAL dal = new InfoLibDAL();
            return dal.GetInfoLibForumCallItem();
        }

    }
}

