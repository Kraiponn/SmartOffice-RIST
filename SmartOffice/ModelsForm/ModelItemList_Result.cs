using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartOffice.ModelsForm
{
    public class ModelItemList_Result
    {
        [Key, Column("DocumentCode", Order = 0)]
        public string DocumentCode          { get; set; }
        [Key, Column("ItemCateg", Order = 1)]
        public string ItemCateg             { get; set; }
        public string InputOption           { get; set; }
        public int? DisplayOrder          { get; set; }
        public string DocumentGroupCode     { get; set; }
        public string DocumentNameE         { get; set; }
        public string DocumentNameT         { get; set; }
        public string DocumentNameJ { get; set; }
        public string DocumentKind          { get; set; }
        public string OpeGroupCateg         { get; set; }
        public string AttachedFile          { get; set; }
        public string EmailSend             { get; set; }
        public string ItemCategNameT        { get; set; }
        public string ItemCategNameE        { get; set; }
        public string ItemCategNameJ { get; set; }
        public string RemarksTitleJ1 { get; set; }
        public string RemarksTitleJ2 { get; set; }
        public string RemarksTitleJ3 { get; set; }
        public string RemarksTitleJ4 { get; set; }
        public string RemarksTitleJ5 { get; set; }
        public string RemarksTitleJ6 { get; set; }
        public string RemarksTitleJ7 { get; set; }
        public string RemarksTitleJ8 { get; set; }
        public string RemarksTitleJ9 { get; set; }
        public string RemarksTitleJ10 { get; set; }
        public string RemarksTitleE1         { get; set; }
        public string RemarksTitleE2         { get; set; }
        public string RemarksTitleE3         { get; set; }
        public string RemarksTitleE4         { get; set; }
        public string RemarksTitleE5         { get; set; }
        public string RemarksTitleE6         { get; set; }
        public string RemarksTitleE7         { get; set; }
        public string RemarksTitleE9         { get; set; }
        public string RemarksTitleE10        { get; set; }
        public string RemarksTitleT1 { get; set; }
        public string RemarksTitleT2 { get; set; }
        public string RemarksTitleT3 { get; set; }
        public string RemarksTitleT4 { get; set; }
        public string RemarksTitleT5 { get; set; }
        public string RemarksTitleT6 { get; set; }
        public string RemarksTitleT7 { get; set; }
        public string RemarksTitleT9 { get; set; }
        public string RemarksTitleT10 { get; set; }
        public string InputItemListItemCateg { get; set; }
        [Key, Column("ItemCode", Order = 2)]
        public string ItemCode              { get; set; }
        public int? InputItemListDisplayOrder { get; set; }
        public string ItemNameE             { get; set; }
        public string ItemNameT             { get; set; }
        public string ItemNameJ { get; set; }
        public string InputItemCode         { get; set; }
        public string InputType             { get; set; }
        public string DataType              { get; set; }
        public int? DecimalNo             { get; set; }
        public string Unit                  { get; set; }
        public string ValueCode             { get; set; }
        public string InputItemOption       { get; set; }
        public string DefaultValue          { get; set; }
        public bool ReadOnly              { get; set; }
        [Key, Column("DocumentNo", Order = 3)]
        public string DocumentNo            { get; set; }
        public string FinalResult           { get; set; }
        public DateTime AddDate               { get; set; }
        public DateTime UpdDate               { get; set; }
        public bool Required { get; set; }
        public Int32? Minlength { get; set; }
        public Int32? Maxlength { get; set; }
        public Int32? Min { get; set; }
        public Int32? Max { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        public decimal? Step { get; set; }

        public string Language { get; set; }
    }
}
