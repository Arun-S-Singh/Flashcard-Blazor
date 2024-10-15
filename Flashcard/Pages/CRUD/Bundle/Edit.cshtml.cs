using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Data.Bundle;

namespace Flashcard.Pages.CRUD.Bundle
{
    public class EditModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public EditModel(FlashcardDBContext context)
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

            var bundlemodel =  await _context.Bundles.FirstOrDefaultAsync(m => m.Id == id);
            if (bundlemodel == null)
            {
                return NotFound();
            }
            BundleModel = bundlemodel;
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

            _context.Attach(BundleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BundleModelExists(BundleModel.Id))
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

        private bool BundleModelExists(int id)
        {
          return (_context.Bundles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
