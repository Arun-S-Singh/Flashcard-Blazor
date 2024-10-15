using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flashcard.Data;
using Flashcard.Data.Bundle;

namespace Flashcard.Pages.CRUD.Bundle
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
        public BundleModel BundleModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bundles == null || BundleModel == null)
            {
                return Page();
            }

            _context.Bundles.Add(BundleModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
