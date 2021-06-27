using System;
using System.Collections.Generic;
using System.Text;

namespace ALGenerator
{
    class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string TableCaption { get; set; }
        public DataClassification DataClassification { get; set; }
        public List<TableField> TableFields { get; set; }

        public Table()
        {
            TableFields = new List<TableField>();
        }
    }
}
