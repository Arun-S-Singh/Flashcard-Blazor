using Microsoft.EntityFrameworkCore;

namespace Flashcard.Data.Order
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }

        public string UserId { get; set; }

        public int BundleId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Precision(9, 2)]
        public decimal BundlePrice { get; set; }

        [Precision(9, 2)]
        public decimal TotalCost {  get; set; }
    }
}
