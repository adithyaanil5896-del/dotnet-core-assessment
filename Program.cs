using DotNetCoreAssessment.Models;
using DotNetCoreAssessment.Services;
using System.Collections;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IceCreamSalesAssessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = @"Date, SKU, Unit Price,Quantity,Total Price
                         2019 - 01 - 01, Death by Chocolate,180,5,900 
                         2019 - 01 - 01,Cake Fudge,150,1,150 
                         2019 - 01 - 01,Cake Fudge,150,1,150 
                         2019 - 01 - 01,Cake Fudge,150,3,450 
                         2019 - 01 - 01,Death by Chocolate,180,1,180 
                         2019 - 01 - 01,Vanilla Double Scoop,80,3,240 
                         2019 - 01 - 01,Butterscotch Single Scoop,60,5,300 
                         2019 - 01 - 01,Vanilla Single Scoop,50,5,250 
                         2019 - 01 - 01,Cake Fudge,150,5,750 
                         2019 - 01 - 01,Hot Chocolate Fudge,120,3,360 
                         2019 - 01 - 01,Butterscotch Single Scoop,60,5,300 
                         2019 - 01 - 01,Chocolate Europa Double Scoop,100,1,100 
                         2019 - 01 - 01,Hot Chocolate Fudge,120,2,240 
                         2019 - 01 - 01,Caramel Crunch Single Scoop,70,4,280 
                         2019 - 01 - 01,Hot Chocolate Fudge,120,2,240 
                         2019 - 01 - 01,Hot Chocolate Fudge,120,4,480 
                         2019 - 01 - 01,Hot Chocolate Fudge,120,2,240 
                         2019 - 01 - 01,Cafe Caramel,160,5,800 
                         2019 - 01 - 01,Vanilla Double Scoop,80,4,320 
                         2019 - 01 - 01,Butterscotch Single Scoop,60,3,180 
                         2019 - 02 - 01,Butterscotch Single Scoop,60,3,180 
                         2019 - 02 - 01,Vanilla Single Scoop,50,2,100 
                         2019 - 02 - 01,Butterscotch Single Scoop,60,3,180 
                         2019 - 02 - 01,Vanilla Double Scoop,80,1,80 
                         2019 - 02 - 01,Death by Chocolate,180,2,360 
                         2019 - 02 - 01,Cafe Caramel,160,2,320 
                         2019 - 02 - 01,Pista Single Scoop,60,3,180 
                         2019 - 02 - 01,Hot Chocolate Fudge,120,2,240 
                         2019 - 02 - 01,Vanilla Single Scoop,50,3,150 
                         2019 - 02 - 01,Vanilla Single Scoop,50,5,250 
                         2019 - 02 - 01,Cake Fudge,150,1,150 
                         2019 - 02 - 01,Vanilla Single Scoop,50,4,200 
                         2019 - 02 - 01,Vanilla Double Scoop,80,3,240 
                         2019 - 02 - 01,Cake Fudge,150,1,150 
                         2019 - 02 - 01,Vanilla Double Scoop,80,5,400 
                         2019 - 02 - 01,Hot Chocolate Fudge,120,5,600 
                         2019 - 02 - 01,Vanilla Double Scoop,80,2,160 
                         2019 - 02 - 01,Vanilla Double Scoop,80,3,240 
                         2019 - 02 - 01,Hot Chocolate Fudge,120,5,600 
                         2019 - 02 - 01,Cake Fudge,150,5,750 
                         2019 - 03 - 01,Vanilla Single Scoop,50,5,250 
                         2019 - 03 - 01,Cake Fudge,150,5,750 
                         2019 - 03 - 01,Pista Single Scoop,60,1,60 
                         2019 - 03 - 01,Butterscotch Single Scoop,60,2,120 
                         2019 - 03 - 01,Vanilla Double Scoop,80,1,80 
                         2019 - 03 - 01,Cafe Caramel,160,1,160 
                         2019 - 03 - 01,Cake Fudge,150,5,750 
                         2019 - 03 - 01,Trilogy,160,5,800 
                         2019 - 03 - 01,Butterscotch Single Scoop,60,3,180 
                         2019 - 03 - 01,Death by Chocolate,180,2,360 
                         2019 - 03 - 01,Butterscotch Single Scoop,60,1,60 
                         2019 - 03 - 01,Hot Chocolate Fudge,120,3,360 
                         2019 - 03 - 01,Cake Fudge,150,2,300 
                         2019 - 03 - 01,Cake Fudge,150,2,300 
                         2019 - 03 - 01,Vanilla Single Scoop,50,4,100 
                         2019 - 03 - 01,Cafe Caramel,160,0,160 
                         2019 - 03 - 01,Cake Fudge,150,5,750 
                         2019 - 03 - 01,Cafe Caramel,160,5,800 
                         2019 - 03 - 01,Almond Fudge,150,1,150 
                         2019 - 03 - 01,Cake Fudge,150,1,150";

            CsvParser parser = new CsvParser();

            List<SaleRecord> records = parser.Parse(data);
            ValidationService validator = new ValidationService();
            ReportGenerator reportGenerator = new ReportGenerator();

            int totalSales = reportGenerator.GetTotalSales(records);

            Console.WriteLine();
            Console.WriteLine("1. Total sales of the store.");
            Console.WriteLine();
            Console.WriteLine("TOTAL SALES");
            Console.WriteLine("-----------");
            Console.WriteLine(totalSales);
            Console.WriteLine();

            Dictionary<string, int> monthlySales = reportGenerator.GetMonthWiseSales(records);

            Console.WriteLine("2. Month-wise sales totals.");
            Console.WriteLine();
            Console.WriteLine("MONTH WISE SALES");
            Console.WriteLine("----------------");

            foreach (var item in monthlySales)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Console.WriteLine();

            List<ValidationError> errors = validator.Validate(records);

            Console.WriteLine("3. Most popular item (most quantity sold) each month. For the most popular item, find the min, max, and average number of orders each month.");
            reportGenerator.GetMostPopularItemPerMonth(records);

            Console.WriteLine();
            Console.WriteLine("4. Items generate the most revenue each month.");
            reportGenerator.GetHighestRevenueItemPerMonth(records);

            Console.WriteLine();
            Console.WriteLine("5. Identify month-to-month growth per item in percentage.");
            reportGenerator.GetMonthToMonthGrowth(records);

            Console.WriteLine();
            Console.WriteLine("6. Detect data inconsistencies.");
            Console.WriteLine();
            Console.WriteLine("VALIDATION ERRORS");
            Console.WriteLine("-----------------");

            foreach (var error in errors)
            {
                Console.WriteLine(
                    $"Program.cs Line: {error.LineNumber} | " +
                    $"Data Row: {error.RowNumber} | " +
                    $"Validation Error: {error.Message}");
            }



            Console.ReadLine();
        }
    }
}

//ASSESSMENT QUESTIONS
//--------------------

//1.What was the most complex part of the assignment for you personally and why?

//Ans: The most complex part of the assignment was implementing the month-to-month growth calculation and the monthly aggregation 
//     logic without using LINQ or a database. Since the requirement restricted the use of SQL, LINQ, and external libraries,
//     I had to manually design the aggregation logic using nested dictionaries and loops. Managing the data structure efficiently
//     while keeping the code readable required careful planning.

//     Another challenge was handling edge cases such as:

//     * Missing items in previous months
//     * Division-by-zero during growth calculation
//     * Invalid data rows affecting report accuracy

//     To solve this, I used dictionaries grouped by month and SKU, and I separated parsing, validation, and reporting into
//     different service classes to keep the code modular and maintainable. 

//2.Describe a bug you expect to hit while implementing this and how you would debug it.

//Ans: One bug I expected during implementation was incorrect aggregation of monthly sales due to inconsistent dictionary keys or
//     malformed date values. For example, if the month formatting was inconsistent, the same month could accidentally be stored
//     as different keys, which would produce incorrect totals and growth calculations.

//     To debug this, I would:

//     * Print intermediate dictionary values to verify grouping logic
//     * Validate parsed dates before processing
//     * Use console logging to inspect month keys and aggregated totals
//     * Test with small sample datasets before running the full dataset

//     I would also create validation checks to isolate problematic rows early in the processing stage so that invalid data does
//     not affect the final reports.

//3.Does your solution handle larger data sets without any performance implications?

//Ans: Yes.The solution is designed to handle larger datasets efficiently using lists and dictionaries.Most operations run in
//     linear time because the data is processed sequentially without repeated full scans or database overhead.