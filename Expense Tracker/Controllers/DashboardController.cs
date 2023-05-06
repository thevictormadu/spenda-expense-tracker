using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Charts;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CultureInfo ngCulture = CultureInfo.CreateSpecificCulture("en-NG");

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Last 7 days transactions
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(c => c.Category)
                .Where(d => d.Date >= StartDate && d.Date <= EndDate)
                .ToListAsync();

            //Total income
            int totalIncome = selectedTransactions
                .Where(c => c.Category.Type == "Income")
                .Sum(a => a.Amount);
            ViewBag.TotalIncome = string.Format(ngCulture, "{0:C0}", totalIncome);

            //Total Expense
            int totalExpense = selectedTransactions
                .Where(t => t.Category.Type == "Expense")
                .Sum(b => b.Amount);

            ViewBag.TotalExpense = string.Format(ngCulture, "{0:C0}", totalExpense);

            //Balance
            int balance = totalIncome - totalExpense;

            ngCulture.NumberFormat.CurrencyNegativePattern = 1;

            ViewBag.Balance = string.Format(ngCulture, "{0:C0}", balance);

            //Dougnut chart -expense by category
            ViewBag.DoughnutChartData = selectedTransactions
                .Where(x => x.Category.Type == "Expense")
                .GroupBy(y => y.CategoryId)
                .Select(k => new
                {
                    CategoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    Amount = k.Sum(a => a.Amount),
                    FormattedAmount = string.Format(ngCulture, "{0:C0}", (k.Sum(a => a.Amount)))
                })
                .OrderByDescending(d => d.Amount);

            //SplineChat chart income vs expense
            //Income

            List<SplineChartData> incomeSummary = selectedTransactions
                .Where(j => j.Category.Type == "Income")
                .GroupBy(y => y.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    income = k.Sum(l => l.Amount)
                }).ToList();

            //Expense

            List<SplineChartData> expenseSummary = selectedTransactions
                .Where(j => j.Category.Type == "Expense")
                .GroupBy(y => y.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Amount)
                }).ToList();

            //Combine income & expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(x => StartDate.AddDays(x).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in incomeSummary on day equals income.day
                                      into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in expenseSummary on day equals expense.day
                                      into dayExpenseJoined
                                      from expense in dayExpenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}