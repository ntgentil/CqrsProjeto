using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Queries.Results;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ProdutoCommand = Core.Application.Importacao.Commands.Inputs.ProdutoCommand;

namespace Core.Helps
{
    public class ReadFile
    {
        public ImportacaoCommand ReadAllLines(Stream file)
        {

            var result = new ImportacaoCommand();
            PropertyInfo[] propertyInfo = typeof(ProdutoCommand).GetProperties();

            using (var document = SpreadsheetDocument.Open(file, false))
            {
                var sheet = document.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault() ?? document.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(sheet => true);

                var worksheet = (WorksheetPart)(document.WorkbookPart.GetPartById(sheet.Id));
                var rows = worksheet.Worksheet.Descendants<Row>().ToList();

                var headerRow = rows.First();
                var headerCells = headerRow.Elements<Cell>();
                int totalColumns = headerCells.Count();

                rows.RemoveAt(0);

                foreach (var row in rows)
                {
                    ProdutoCommand produtoResult = new ProdutoCommand();
                    result.Produtos.Add(produtoResult);

                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        var valueCell = cell.CellValue;
                        var text = (valueCell == null) ? cell.InnerText : valueCell.Text;
                        if (cell.DataType?.Value == CellValues.SharedString)
                        {
                            text = document.WorkbookPart.SharedStringTablePart.SharedStringTable
                                .Elements<SharedStringItem>().ElementAt(
                                    Convert.ToInt32(cell.CellValue.Text)).InnerText;
                        }
                        var cellText = (text ?? string.Empty).Trim();

                        var linhaText = cell.CellReference.ToString();
                        produtoResult.Linha = Convert.ToInt32(linhaText.Substring(1, linhaText.Length-1));


                        switch (GetColumnIndex(cell.CellReference))
                        {
                            case 1:
                                var cellFormat = document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[
                                int.Parse(cell.StyleIndex.InnerText)] as CellFormat;
                                var dateFormat = GetDateTimeFormat(cellFormat.NumberFormatId);
                                if (double.TryParse(cellText, out var cellDouble))
                                {
                                    produtoResult.DataEntrega = DateTime.FromOADate(cellDouble);
                                }
                                //produtoResult.Linha = 
                                break;

                            case 2:
                                produtoResult.Nome = cellText;
                                break;

                            case 3:
                                produtoResult.Quantidade = long.Parse(cellText);
                                break;

                            case 4:
                                produtoResult.Valor = decimal.Parse(cellText, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                                break;


                        }
                    }

                }
            }


            return result;
        }

        private static int? GetColumnIndex(string cellReference)
        {
            if (string.IsNullOrEmpty(cellReference))
            {
                return null;
            }

            string columnReference = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);

            int columnNumber = -1;
            int mulitplier = 1;

            foreach (char c in columnReference.ToCharArray().Reverse())
            {
                columnNumber += mulitplier * ((int)c - 64);

                mulitplier = mulitplier * 26;
            }

            return columnNumber + 1;
        }


        private readonly Dictionary<uint, string> DateFormatDictionary = new Dictionary<uint, string>()
        {
            [14] = "dd/MM/yyyy",
            [15] = "d-MMM-yy",
            [16] = "d-MMM",
            [17] = "MMM-yy",
            [18] = "h:mm AM/PM",
            [19] = "h:mm:ss AM/PM",
            [20] = "h:mm",
            [21] = "h:mm:ss",
            [22] = "M/d/yy h:mm",
            [30] = "M/d/yy",
            [34] = "yyyy-MM-dd",
            [45] = "mm:ss",
            [46] = "[h]:mm:ss",
            [47] = "mmss.0",
            [51] = "MM-dd",
            [52] = "yyyy-MM-dd",
            [53] = "yyyy-MM-dd",
            [55] = "yyyy-MM-dd",
            [56] = "yyyy-MM-dd",
            [58] = "MM-dd",
            [165] = "M/d/yy",
            [166] = "dd MMMM yyyy",
            [167] = "dd/MM/yyyy",
            [168] = "dd/MM/yy",
            [169] = "d.M.yy",
            [170] = "yyyy-MM-dd",
            [171] = "dd MMMM yyyy",
            [172] = "d MMMM yyyy",
            [173] = "M/d",
            [174] = "M/d/yy",
            [175] = "MM/dd/yy",
            [176] = "d-MMM",
            [177] = "d-MMM-yy",
            [178] = "dd-MMM-yy",
            [179] = "MMM-yy",
            [180] = "MMMM-yy",
            [181] = "MMMM d, yyyy",
            [182] = "M/d/yy hh:mm t",
            [183] = "M/d/y HH:mm",
            [184] = "MMM",
            [185] = "MMM-dd",
            [186] = "M/d/yyyy",
            [187] = "d-MMM-yyyy"
        };

        private string GetDateTimeFormat(UInt32Value numberFormatId)
        {
            return DateFormatDictionary.ContainsKey(numberFormatId) ? DateFormatDictionary[numberFormatId] : string.Empty;
        }
    }
}
