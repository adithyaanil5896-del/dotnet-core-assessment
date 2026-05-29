using DotNetCoreAssessment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreAssessment.Services
{
    public class ValidationService
    {
        public List<ValidationError> Validate(List<SaleRecord> records)
        {
            List<ValidationError> errors = new List<ValidationError>();

            for (int i = 0; i < records.Count; i++)
            {
                SaleRecord r = records[i];

                if (r.UnitPrice * r.Quantity != r.TotalPrice)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 12,
                        RowNumber = i,
                        Message = "Total price mismatch"
                    });
                }

                if (r.Quantity < 1)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 12,
                        RowNumber = i,
                        Message = "Quantity less than 1"
                    });
                }

                if (r.UnitPrice < 0)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 12,
                        RowNumber = i,
                        Message = "Negative unit price"
                    });
                }

                if (r.TotalPrice < 0)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 12,
                        RowNumber = i,
                        Message = "Negative total price"
                    });
                }

                if (r.Date == DateTime.MinValue)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 12,
                        RowNumber = i,
                        Message = "Malformed date"
                    });
                }
            }

            return errors;
        }
    }
}
