using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreAssessment.Models
{
    public class ValidationError
    {
        public int LineNumber { get; set; }
        public int RowNumber { get; set; }

        public string Message { get; set; }
    }
}
