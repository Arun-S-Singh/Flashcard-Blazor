using Microsoft.AspNetCore.Identity;

namespace Flashcard.Data
{
    public class FlashcardUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
