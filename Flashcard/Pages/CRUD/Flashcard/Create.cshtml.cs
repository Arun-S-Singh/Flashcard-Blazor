using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flashcard.Data;
using Flashcard.Data.Card;

namespace Flashcard.Pages.CRUD.Flashcard
{
    public class CreateModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public CreateModel(FlashcardDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CardModel CardModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Cards == null || CardModel == null)
            {
                return Page();
            }

            _context.Cards.Add(CardModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
