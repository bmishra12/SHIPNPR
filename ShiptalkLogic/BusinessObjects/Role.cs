using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{

    [Serializable]
    public sealed class Role 
        //: IComparable<Role>
    {

        public Int16 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public Scope scope { get; set; }
        public short ScopeId {
            get { return (short)scope; }
        }

        private Role()
        {
        }

        public Role(Int16 id, string name, string description, bool isAdmin, Scope scope)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.IsAdmin = isAdmin;
            this.scope = scope;
        }

        /// <summary>
        /// Will return true if the object's scope is higher than scope of roleToCompare.
        /// The lower the scope is in the enumeration, the higher is its precedence in business logic.
        /// e.g., CMS is higher than State. However, the numeric value of the enumeration is higher for State.
        /// </summary>
        /// <param name="scopeToCompare"></param>
        /// <returns></returns>
        public bool Compare(Role roleToCompare, ComparisonCriteria criteria)
        {
            return Compare(roleToCompare.scope, criteria);
        }

        /// <summary>
        /// Will return true if the object's scope is higher than scope of roleToCompare.
        /// The lower the scope is in the enumeration, the higher is its precedence in business logic.
        /// e.g., CMS is higher than State. However, the numeric value of the enumeration is higher for State.
        /// </summary>
        /// <param name="scopeToCompare"></param>
        /// <returns></returns>
        public bool Compare(Scope scopeToCompare, ComparisonCriteria criteria)
        {
            bool result = false;
            switch (criteria)
            {
                case ComparisonCriteria.IsHigher:
                    result = (this.scope < scopeToCompare);
                    break;
                case ComparisonCriteria.IsHigherThanOrEqualTo:
                    result = (this.scope <= scopeToCompare);
                    break;
                case ComparisonCriteria.IsLower:
                    result = (this.scope > scopeToCompare);
                    break;
                case ComparisonCriteria.IsLowerThanOrEqualTo:
                    result = (this.scope >= scopeToCompare);
                    break;
                case ComparisonCriteria.IsEqual:
                    result = (this.scope == scopeToCompare);
                    break;
                default:
                    ShiptalkCommon.ShiptalkException.ThrowSecurityException("Unspecific ComparisonCriteria.", "An error occured while execution of request. Please contact support for assistance.");
                    break;
            }
            return result;
        }

        public bool IsStateAdmin
        {
            get
            {
                return (this.scope == Scope.State && this.IsAdmin);
            }
        }

        public bool IsCMSAdmin
        {
            get
            {
                return (this.scope == Scope.CMS && this.IsAdmin);
            }
        }

        


        //#region IComparable<Role> Members

        //int IComparable<Role>.CompareTo(Role other)
        //{
        //    if (this.scope == null)
        //    {
        //        if (other.scope == null)
        //        {
        //            // If this.scope is null and other.scope is null, they're
        //            // equal. 
        //            return 0;
        //        }
        //        else
        //        {
        //            // If this.scope is null and other.scope is not null, other.scope
        //            // is greater. 
        //            return -1;
        //        }
        //    }
        //    else
        //    {
        //        // If this.scope is not null...
        //        //
        //        if (other.scope == null)
        //        // ...and other.scope is null, this.scope is greater.
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            // ...and other.scope is not null, compare the 
        //            // lengths of the two strings.
        //            //
        //            int retval = this.scope.Length.CompareTo(other.scope.Length);

        //            if (retval != 0)
        //            {
        //                // If the strings are not of equal length,
        //                // the longer string is greater.
        //                //
        //                return retval;
        //            }
        //            else
        //            {
        //                // If the strings are of equal length,
        //                // sort them with ordinary string comparison.
        //                //
        //                return this.scope.CompareTo(other.scope);
        //            }
        //        }
        //    }
        //}

        //#endregion
    }

}
