using System;
using System.Collections.Generic;
using System.Text;

namespace ALCodeGenerator
{
    class TableExtension
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string TableCaption { get; set; }
        public string OriginalTableName { get; set; }
        public int OriginalTableId { get; set; }
        public DataClassification DataClassification { get; set; }
        public List<TableField> TableFields { get; set; }

        public TableExtension()
        {
            TableFields = new List<TableField>();
        }
    }
}
