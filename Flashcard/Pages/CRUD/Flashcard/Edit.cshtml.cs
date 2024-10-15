using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Data.Card;

namespace Flashcard.Pages.CRUD.Flashcard
{
    public class EditModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public EditModel(FlashcardDBContext context)
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

            var cardmodel =  await _context.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if (cardmodel == null)
            {
                return NotFound();
            }
            CardModel = cardmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CardModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardModelExists(CardModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CardModelExists(Guid id)
        {
          return (_context.Cards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
