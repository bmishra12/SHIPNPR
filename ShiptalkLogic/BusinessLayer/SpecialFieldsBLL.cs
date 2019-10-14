using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using ShiptalkLogic.DataLayer;
using ShiptalkLogic.BusinessObjects;
using System.Configuration;
using System.Xml;
using System.Data;
using ShiptalkLogic.BusinessObjects.UI;
using System.Data.SqlClient;
namespace ShiptalkLogic.BusinessLayer
{
    public class SpecialFieldsBLL
    {
        /// <summary>
        /// Returns the special fiels rules for the state.
        /// </summary>
        /// <param name="RuleType">Rule category type</param>
        /// <param name="StateFIPS">sTATE</param>
        /// <returns>IEnumerable<SpecialField></returns>
        public static IEnumerable<ViewSpecialFieldsViewData> GetSpecialFieldRulesForState(FormType RuleType, string StateFIPS)
        {
            State StateValue = new State(StateFIPS);
            IEnumerable<SpecialField> spFieldsRules = FileUploadDAL.GetSpecialUploadFieldsValues(RuleType, StateValue);
            List<ViewSpecialFieldsViewData> spFieldsRulesData = new List<ViewSpecialFieldsViewData>();
            foreach(SpecialField sf in spFieldsRules)
            {
                ViewSpecialFieldsViewData viewData = new ViewSpecialFieldsViewData();
                viewData.Id = sf.Id;
                viewData.Description = sf.Description;
                viewData.EndDate = sf.EndDate.ToLongDateString();
                viewData.FormType = sf.FormType.Description();
                viewData.IsRequired = sf.IsRequired.ToString();
                viewData.Name = sf.Name;
                viewData.Ordinal = sf.Ordinal;
                viewData.StartDate = sf.StartDate.ToLongDateString();
                viewData.State = sf.State;
                viewData.StateName = sf.State.StateName;
                viewData.ValidationType = sf.ValidationType;
                viewData.ValidationName = sf.ValidationType.Description();
                

                //viewData.ValidationName 
                spFieldsRulesData.Add(viewData);

            }
            return spFieldsRulesData;
        }

        public static IEnumerable<ViewSpecialFieldsViewData> GetSpecialFieldsView(string StateFIPS, DateTime StartDate, DateTime EndDate, FormType DataForm)
        {
            return SpecialFieldsDAL.GetSpecialFields(StateFIPS, StartDate, EndDate, DataForm);
        }

       
       
        public static ViewSpecialFieldsViewData GetSpecialFieldsView(int SpecialFieldId)
        {
           SpecialField sf =  SpecialFieldsDAL.GetSpecialFieldsById(SpecialFieldId);
           return GetSpecialFieldsViewData(sf);

        }

        public static void DeleteSpecialField(int SpecialFieldId)
        {
            try
            {
                SpecialFieldsDAL.DeleteSpecialField(SpecialFieldId);
            }
            catch (SqlException exDB)
            {
                throw (new ApplicationException(exDB.Message, exDB));
            }
            
        }


        public static void SetSpecialFieldsValidStartEndDate(ref DateTime? StartDate, ref DateTime? EndDate, string StateFIPS, FormType DataForm)
        {
            SpecialFieldsDAL.SetSpecialFieldsValidStartEndDate(ref StartDate, ref EndDate, StateFIPS, DataForm);
        }

        //Lavanya: Added Range
        private static ViewSpecialFieldsViewData GetSpecialFieldsViewData(SpecialField sf)
        {
            ViewSpecialFieldsViewData viewData = new ViewSpecialFieldsViewData();
           viewData.Id = sf.Id;
           viewData.Description = sf.Description;
           viewData.EndDate = sf.EndDate.ToLongDateString();
           viewData.FormType = sf.FormType.Description();
           viewData.IsRequired = sf.IsRequired.ToString();
           viewData.Name = sf.Name;
           viewData.Ordinal = sf.Ordinal;
           viewData.StartDate = sf.StartDate.ToLongDateString();
           viewData.State = sf.State;
           viewData.StateName = sf.State.StateName;
           viewData.ValidationType = sf.ValidationType;
           viewData.ValidationName = sf.ValidationType.Description();
           viewData.CreatedDate = sf.CreatedDate.Value.ToLongDateString();
           viewData.Range = sf.Range.Trim();
           return viewData;


        }

        public static int AddUpdateSpecialField(ViewSpecialFieldsViewData FieldData)
        {
            SpecialField FieldRec = new SpecialField();
            FieldRec.Description = FieldData.Description;
            FieldRec.StartDate = Convert.ToDateTime(FieldData.StartDate);
            FieldRec.EndDate = Convert.ToDateTime(FieldData.EndDate);
            if (FormType.ClientContact.ToString() == FieldData.FormType)
                FieldRec.FormType = FormType.ClientContact;
            if (FormType.PublicMediaActivity.ToString() == FieldData.FormType)
                FieldRec.FormType = FormType.PublicMediaActivity;
            

            FieldRec.IsRequired = false;
            if (FieldData.IsRequired.ToUpper() == "YES")
            {
                FieldRec.IsRequired = true;
            }

            FieldRec.Name = FieldData.Name;
            FieldRec.State = FieldData.State;
            FieldRec.Ordinal = FieldData.Ordinal;
            FieldRec.Id = FieldData.Id;

            if (FieldData.ValidationName == ShiptalkLogic.BusinessObjects.ValidationType.None.ToString())
            {
                FieldRec.ValidationType = ShiptalkLogic.BusinessObjects.ValidationType.None;
            }

            if (FieldData.ValidationName == ShiptalkLogic.BusinessObjects.ValidationType.AlphaNumeric.ToString())
            {
                FieldRec.ValidationType = ShiptalkLogic.BusinessObjects.ValidationType.AlphaNumeric;
            }


            if (FieldData.ValidationName == ShiptalkLogic.BusinessObjects.ValidationType.Numeric.ToString())
            {
                FieldRec.ValidationType = ShiptalkLogic.BusinessObjects.ValidationType.Numeric;
            }

            //Added by Lavanya Maram: 06/27/2013
            if (FieldData.ValidationName == ShiptalkLogic.BusinessObjects.ValidationType.Range.ToString())
            {
                FieldRec.ValidationType = ShiptalkLogic.BusinessObjects.ValidationType.Range;
            }

            if (FieldData.ValidationName == ShiptalkLogic.BusinessObjects.ValidationType.Option.ToString())
            {
                FieldRec.ValidationType = ShiptalkLogic.BusinessObjects.ValidationType.Option;
            }

            FieldRec.Range = FieldData.Range;
            // end

            FieldRec.CreatedBy = FieldData.CreatedBy;
            try
            {
                return SpecialFieldsDAL.AddSpecialField(FieldRec);
            }
            catch (SqlException exSql)
            {
                //2601 is thrown when the unique constraint is has been violated.
                if (exSql.Number == 2601)
                {

                    throw (new ApplicationException("Existing field  has been defined for ordinal value  " + FieldData.Ordinal.ToString(), exSql));
                }
                else
                {
                    throw (new ApplicationException(exSql.Message, exSql));
                }
            }

        }
        
    }
}
