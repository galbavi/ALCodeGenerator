using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace ALGenerator
{
    public class ALTableGenerator
    {
        public string Path { get; set; }
        public string[] AlCSV { get; private set; }

        public Table Table { get; set; }
        public TableExtension TableExt { get; set; }

        public string ALCode { get; set; }

        public void ImportTableCSVFile()
        {
            AlCSV = File.ReadAllLines(Path);

            Table = new Table();

            string[] tableHeaderLine = AlCSV[0].Split(';');

            Table.Id = int.Parse(tableHeaderLine[0]);
            Table.TableName = tableHeaderLine[1];
            Table.TableCaption = tableHeaderLine[2];
            Table.DataClassification = DataClassification.ToBeClassified; //tableHeaderLine[3] == "" ? DataClassification.CustomerContent : (DataClassification) Enum.Parse(typeof(DataClassification), tableHeaderLine[3]);

            for (int i = 1; i < AlCSV.Length; i++)
            {
                tableHeaderLine = AlCSV[i].Split(';');
                Table.TableFields.Add(new TableField()
                {
                    Id = int.Parse(tableHeaderLine[0]),
                    PrimaryKey = tableHeaderLine[5] == "Yes",
                    Name = tableHeaderLine[1],
                    Caption = tableHeaderLine[2],
                    Type = Enum.Parse<FieldType>(tableHeaderLine[3]),
                    Length = tableHeaderLine[4] != "" ? int.Parse(tableHeaderLine[4]) : 0,
                    DataClassification = DataClassification.ToBeClassified // Enum.Parse<DataClassification>(tableHeaderLine[6])
                });
            }
        }

        public void ImportTableExtensionCSVFile()
        {
            AlCSV = File.ReadAllLines(Path);

            TableExt = new TableExtension();

            string[] tableHeaderLine = AlCSV[0].Split(';');

            TableExt.Id = int.Parse(tableHeaderLine[0]);
            TableExt.TableName = tableHeaderLine[1];
            TableExt.TableCaption = tableHeaderLine[2];
            TableExt.DataClassification = DataClassification.ToBeClassified; //tableHeaderLine[3] == "" ? DataClassification.CustomerContent : (DataClassification) Enum.Parse(typeof(DataClassification), tableHeaderLine[3]);

            for (int i = 1; i < AlCSV.Length; i++)
            {
                tableHeaderLine = AlCSV[i].Split(';');
                TableExt.TableFields.Add(new TableField()
                {
                    Id = int.Parse(tableHeaderLine[0]),
                    Name = tableHeaderLine[1],
                    Caption = tableHeaderLine[2],
                    Type = Enum.Parse<FieldType>(tableHeaderLine[3]),
                    Length = tableHeaderLine[4] != "" ? int.Parse(tableHeaderLine[4]) : 0,
                    DataClassification = DataClassification.ToBeClassified // Enum.Parse<DataClassification>(tableHeaderLine[6])
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

        public void GenerateALTableExtensionCode()
        {
            ALCode = "";

            ALCode += string.Format("tableextension {0} \"{1}\" extends \"{2}\"\n", TableExt.Id, TableExt.TableName, TableExt.OriginalTableName);
            ALCode += "{\n";
            ALCode += "    fields\n";
            ALCode += "    {\n";
            foreach (var tablefield in TableExt.TableFields)
            {
                ALCode += string.Format("        field({0}; \"{1}\"; {2})\n", tablefield.Id, tablefield.Name, tablefield.Length == 0 ? tablefield.Type.ToString() : tablefield.Type.ToString() + "[" + tablefield.Length + "]");
                ALCode += "        {\n";
                ALCode += string.Format("            Caption = '{0}';\n", tablefield.Caption);
                ALCode += string.Format("            DataClassification = {0};\n", tablefield.DataClassification.ToString());
                ALCode += "        }\n";
            }
            ALCode += "    }\n";
            ALCode += "}";
        }

        public void ExportALTableCode()
        {
            string path = string.Format(".\\Tab{0}.{1}.al", Table.Id, Regex.Replace(Table.TableName, "[^a-zA-Z0-9]", ""));

            File.WriteAllText(path, ALCode);
        }

        public void ExportALTableExtCode()
        {
            string path = string.Format(".\\Tab{0}-Ext{1}.{2}.al", TableExt.Id, TableExt.OriginalTableId, Regex.Replace(Table.TableName, "[^a-zA-Z0-9]", ""));

            File.WriteAllText(path, ALCode);
        }
    }
}
