using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Data.Bundle
{
    public enum StateOfBundle
    {
        New,
        AddedToCart,
        Subscribed
    }
    public class BundleModel
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }

        [Required]
        [Precision(9, 2)]
        public decimal Price { get; set; }

        [Required] public string Category { get; set; }

        public string? Image { get; set; }

        public string? BundleState {  get; set; }

        public int SubscriptionPeriod { get; set; }

        public bool IsPrivate { get; set; } = false;

    }
}
