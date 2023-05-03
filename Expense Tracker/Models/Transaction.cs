using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage ="Amount should be greater than 0.")]
        public int Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue, ErrorMessage ="Please select a category.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }




        
        [NotMapped]
        public string CategoryTitleWithIcon
        {
            get
            {
                return Category==null? "" : Category.Icon + " " + Category.Title;

            }
        }

        [NotMapped]
        public string AmountWithPrefix
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");

            }
        }

    }
}
