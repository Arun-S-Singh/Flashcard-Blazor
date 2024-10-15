using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Data.Card;

namespace Flashcard.Pages.CRUD.Flashcard
{
    public class DeleteModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public DeleteModel(FlashcardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CardModel CardModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var cardmodel = await _context.Cards.FirstOrDefaultAsync(m => m.Id == id);

            if (cardmodel == null)
            {
                return NotFound();
            }
            else 
            {
                CardModel = cardmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }
            var cardmodel = await _context.Cards.FindAsync(id);

            if (cardmodel != null)
            {
                CardModel = cardmodel;
                _context.Cards.Remove(CardModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
