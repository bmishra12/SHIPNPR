using System;
namespace ShiptalkLogic.BusinessObjects
{
    public class InfoLibItemFile
    {
        public byte[] ItemFile { get; set; }
        public string ItemFileExtension { get; set; }
        public string ItemFileMimeType { get; set; }
        public string ItemFileName { get; set; }
        public int? ItemFileSizeInBytes { get; set; }
        public InfoLibItemFile(){}
    }
}
