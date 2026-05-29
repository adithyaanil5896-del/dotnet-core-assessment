using DotNetCoreAssessment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreAssessment.Services
{
    public class CsvParser
    {
        public List<SaleRecord> Parse(string data)
        {
            List<SaleRecord> records = new List<SaleRecord>();

            string[] lines = data.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                SaleRecord record = new SaleRecord();

                DateTime.TryParse(parts[0], out DateTime date);

                record.Date = date;
                record.SKU = parts[1];
                record.UnitPrice = Convert.ToInt32(parts[2]);
                record.Quantity = Convert.ToInt32(parts[3]);
                record.TotalPrice = Convert.ToInt32(parts[4]);

                records.Add(record);
            }

            return records;
        }
    }
}
