using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

using AutoMapper;

namespace ShiptalkLogic.BusinessObjects
{

    public enum InfoLibHeaderType
    {
        TextOnly = 1,
        ImageOnly = 2,
        HtmlText = 3
    }

    public enum InfoLibLinkType
    {
        Url = 1,
        UploadedFile = 2
    }

    public enum InfoLibFileServerTypes
    {
        HeaderImageFile = 1,
        LinkAttachmentFile = 2
    }

    public enum InfoLibSpecialIdentifiers
    {
        [Description("Forum Call")]
        Forum_call = 1,
        [Description("FAQs")]
        FAQs = 2
    }

    public sealed class InfoLibItem : IModified
    {

        public InfoLibItem() {  }

        public InfoLibItem(int ParentId) : this ()
        {
            this.ParentId = ParentId; 
        }

        public int InfoLibItemId { get; set; }
        //public short ItemType { get; set; }
        public Scope? ViewerScope { get; set; }
        public int ParentId { get; set; }
        public InfoLibSpecialIdentifiers? SpecialIdentifier{get;set;}

        //Header definition
        public InfoLibItemHeader ItemHeader { get; set; }
        public class InfoLibItemHeader : ShiptalkLogic.BusinessObjects.InfoLibItemFile
        {
            public InfoLibHeaderType HeaderType { get; set; }
            public string HeaderText { get; set; }
        }
        

        
        //Link definition
        public InfoLibItemLink ItemLink { get; set; }
        public class InfoLibItemLink : ShiptalkLogic.BusinessObjects.InfoLibItemFile
        {
            public InfoLibLinkType? LinkType { get; set; }
            public string LinkUrl { get; set; }
            public bool OpenLinkInNewWindow { get; set; }
        }
        

        #region Implementation of IModified
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        #endregion


        public bool IsArchived { get; set; }

    }
}
