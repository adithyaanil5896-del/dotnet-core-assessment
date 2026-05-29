using DotNetCoreAssessment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreAssessment.Services
{
    public class ReportGenerator
    {
        public int GetTotalSales(List<SaleRecord> records)
        {
            int total = 0;

            foreach (var record in records)
            {
                total += record.TotalPrice;
            }

            return total;
        }

        public Dictionary<string, int> GetMonthWiseSales(List<SaleRecord> records)
        {
            Dictionary<string, int> sales = new Dictionary<string, int>();

            foreach (var record in records)
            {
                string month = record.Date.ToString("yyyy-MM");

                if (!sales.ContainsKey(month))
                {
                    sales[month] = 0;
                }

                sales[month] += record.TotalPrice;
            }

            return sales;
        }

        public void GetMostPopularItemPerMonth(List<SaleRecord> records)
        {
            Dictionary<string, Dictionary<string, List<int>>> data = new Dictionary<string, Dictionary<string, List<int>>>();

            foreach (var record in records)
            {
                string month = record.Date.ToString("yyyy-MM");

                if (!data.ContainsKey(month))
                {
                    data[month] = new Dictionary<string, List<int>>();
                }

                if (!data[month].ContainsKey(record.SKU))
                {
                    data[month][record.SKU] = new List<int>();
                }

                data[month][record.SKU].Add(record.Quantity);
            }

            Console.WriteLine();
            Console.WriteLine("MOST POPULAR ITEMS");
            Console.WriteLine("------------------");

            foreach (var monthData in data)
            {
                string month = monthData.Key;

                string popularItem = "";
                int highestQuantity = 0;

                foreach (var item in monthData.Value)
                {
                    int totalQty = 0;

                    foreach (var qty in item.Value)
                    {
                        totalQty += qty;
                    }

                    if (totalQty > highestQuantity)
                    {
                        highestQuantity = totalQty;
                        popularItem = item.Key;
                    }
                }

                List<int> orders = monthData.Value[popularItem];

                int min = orders[0];
                int max = orders[0];
                int sum = 0;

                foreach (int qty in orders)
                {
                    if (qty < min)
                        min = qty;

                    if (qty > max)
                        max = qty;

                    sum += qty;
                }

                double average =
                    (double)sum / orders.Count;

                Console.WriteLine(
                    $"{month} -> {popularItem}");

                Console.WriteLine(
                    $"Min Orders: {min}");

                Console.WriteLine(
                    $"Max Orders: {max}");

                Console.WriteLine(
                    $"Average Orders: {average:F2}");

                Console.WriteLine();
            }
        }

        public void GetHighestRevenueItemPerMonth(List<SaleRecord> records)
        {
            Dictionary<string, Dictionary<string, int>> revenue = new Dictionary<string, Dictionary<string, int>>();

            foreach (var record in records)
            {
                string month = record.Date.ToString("yyyy-MM");

                if (!revenue.ContainsKey(month))
                {
                    revenue[month] = new Dictionary<string, int>();
                }

                if (!revenue[month].ContainsKey(record.SKU))
                {
                    revenue[month][record.SKU] = 0;
                }

                revenue[month][record.SKU] += record.TotalPrice;
            }

            Console.WriteLine();
            Console.WriteLine("HIGHEST REVENUE ITEMS");
            Console.WriteLine("---------------------");

            foreach (var monthData in revenue)
            {
                string topItem = "";
                int highestRevenue = 0;

                foreach (var item in monthData.Value)
                {
                    if (item.Value > highestRevenue)
                    {
                        highestRevenue = item.Value;
                        topItem = item.Key;
                    }
                }

                Console.WriteLine($"{monthData.Key} -> {topItem} : {highestRevenue}");
            }
        }

        public void GetMonthToMonthGrowth(List<SaleRecord> records)
        {
            Dictionary<string, Dictionary<string, int>> revenue = new Dictionary<string, Dictionary<string, int>>();

            foreach (var record in records)
            {
                string month = record.Date.ToString("yyyy-MM");

                if (!revenue.ContainsKey(month))
                {
                    revenue[month] = new Dictionary<string, int>();
                }

                if (!revenue[month].ContainsKey(record.SKU))
                {
                    revenue[month][record.SKU] = 0;
                }

                revenue[month][record.SKU] += record.TotalPrice;
            }

            List<string> months = new List<string>(revenue.Keys);

            months.Sort();

            Console.WriteLine();
            Console.WriteLine("MONTH TO MONTH GROWTH");
            Console.WriteLine("---------------------");

            for (int i = 1; i < months.Count; i++)
            {
                string previousMonth = months[i - 1];
                string currentMonth = months[i];

                Console.WriteLine();
                Console.WriteLine($"{previousMonth} -> {currentMonth}");

                foreach (var item in revenue[currentMonth])
                {
                    string sku = item.Key;

                    int currentRevenue = item.Value;

                    int previousRevenue = 0;

                    if (revenue[previousMonth].ContainsKey(sku))
                    {
                        previousRevenue = revenue[previousMonth][sku];
                    }

                    if (previousRevenue > 0)
                    {
                        double growth = ((currentRevenue - previousRevenue) / (double)previousRevenue) * 100;

                        Console.WriteLine($"{sku} : {growth:F2}%");
                    }
                }
            }
        }
    }
}
