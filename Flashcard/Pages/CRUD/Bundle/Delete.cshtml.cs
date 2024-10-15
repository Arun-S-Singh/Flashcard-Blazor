using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Data.Bundle;

namespace Flashcard.Pages.CRUD.Bundle
{
    public class DeleteModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public DeleteModel(FlashcardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BundleModel BundleModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bundles == null)
            {
                return NotFound();
            }

            var bundlemodel = await _context.Bundles.FirstOrDefaultAsync(m => m.Id == id);

            if (bundlemodel == null)
            {
                return NotFound();
            }
            else 
            {
                BundleModel = bundlemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bundles == null)
            {
                return NotFound();
            }
            var bundlemodel = await _context.Bundles.FindAsync(id);

            if (bundlemodel != null)
            {
                BundleModel = bundlemodel;
                _context.Bundles.Remove(BundleModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
