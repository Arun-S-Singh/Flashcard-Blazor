using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Data.Card
{
    public class CardModel
    {
        public Guid Id { get; set; }  

        public int BundleId { get; set; }

        public int CardId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string?  Image { get; set; }

        public string? BackImage { get; set; }

        public string? Category { get; set; }

        public string? Explanation { get; set; }

        [NotMapped]
        public bool? Answered { get; set; }    
    }
}
