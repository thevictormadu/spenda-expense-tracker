using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    FormattedAmount = k.Sum(a => a.Amount).ToString("C0"),
                })
                .OrderByDescending(d => d.Amount)
                .ToList();

            return View();
        }
    }
}