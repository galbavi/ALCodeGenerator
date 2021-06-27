using System;
using System.Collections.Generic;
using System.Text;

namespace ALGenerator
{
    class TableField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public int Length { get; set; }
        public string Caption { get; set; }
        public bool PrimaryKey { get; set; }
        public DataClassification DataClassification { get; set; }
    }
}
