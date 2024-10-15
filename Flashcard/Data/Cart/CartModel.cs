using Flashcard.Data;
using System.ComponentModel.DataAnnotations;

namespace Flashcard.Data.Cart
{
    public class CartModel
    {
        public string Userid { get; set; }

        public int BundleId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
