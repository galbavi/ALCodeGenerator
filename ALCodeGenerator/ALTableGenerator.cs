using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace ALCodeGenerator
{
    class ALTableGenerator
    {
        public string Path { get; set; }
        public string[] AlCSV { get; private set; }

        public Table Table { get; set; }

        public string ALCode { get; set; }

        public void ImportCSVFile()
        {
            AlCSV = File.ReadAllLines(Path);

            Table = new Table();

            string[] tableHeaderLine = AlCSV[0].Split(';');

            Table.Id = int.Parse(tableHeaderLine[0]);
            Table.TableName = tableHeaderLine[1];
            Table.TableCaption = tableHeaderLine[2];
            Table.DataClassification = tableHeaderLine[3] == "" ? DataClassification.CustomerContent : (DataClassification) Enum.Parse(typeof(DataClassification), tableHeaderLine[3]);

            for (int i = 1; i < AlCSV.Length; i++)
            {
                tableHeaderLine = AlCSV[i].Split(';');
                Table.TableFields.Add(new TableField()
                {
                    Id = int.Parse(tableHeaderLine[0]),
                    PrimaryKey = tableHeaderLine[1] == "1",
                    Name = tableHeaderLine[2],
                    Caption = tableHeaderLine[3],
                    Type = Enum.Parse<FieldType>(tableHeaderLine[4]),
                    Length = tableHeaderLine[5] != "" ? int.Parse(tableHeaderLine[5]) : 0,
                    DataClassification = Enum.Parse<DataClassification>(tableHeaderLine[6])
                });
            }
        }

        public void GenerateALTableCode()
        {
            ALCode = "";
            string keys = "";

            ALCode += string.Format("table {0} \"{1}\"\n", Table.Id, Table.TableName);
            ALCode += "{\n";
            ALCode += string.Format("    Caption = '{0}';\n",Table.TableCaption);
            ALCode += string.Format("    DataClassification = {0};\n", Table.DataClassification.ToString());
            ALCode += "\n";
            ALCode += "    fields\n";
            ALCode += "    {\n";
            foreach (var tablefield in Table.TableFields)
            {
                if (tablefield.PrimaryKey) keys += string.Format("\"{0}\"; ",tablefield.Name);

                ALCode += string.Format("        field({0}; \"{1}\"; {2})\n", tablefield.Id, tablefield.Name, tablefield.Length == 0 ? tablefield.Type.ToString() : tablefield.Type.ToString() + "[" + tablefield.Length+ "]");
                ALCode += "        {\n";
                ALCode += string.Format("            Caption = '{0}';\n", tablefield.Caption);
                ALCode += string.Format("            DataClassification = {0};\n", tablefield.DataClassification.ToString());
                ALCode += "        }\n";
            }
            ALCode += "    }\n";

            ALCode += "    keys\n";
            ALCode += "    {\n";
            ALCode += string.Format("        key(PK; {0})\n", keys.Substring(0, keys.Length - 2));
            ALCode += "        {\n";
            ALCode += string.Format("            Clustered = true;\n");
            ALCode += "        }\n";
            ALCode += "    }\n";
            ALCode += "}";
        }

        public void ExportALTableCode()
        {
            string path = string.Format(".\\Table{0}.{1}.al", Table.Id, Regex.Replace(Table.TableName, "[^a-zA-Z0-9]", ""));

            File.WriteAllText(path, ALCode);
        }
    }
}
