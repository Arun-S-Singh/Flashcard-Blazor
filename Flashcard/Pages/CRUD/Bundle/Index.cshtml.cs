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
    public class IndexModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public IndexModel(FlashcardDBContext context)
        {
            _context = context;
        }

        public IList<BundleModel> BundleModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bundles != null)
            {
                BundleModel = await _context.Bundles.ToListAsync();
            }
        }
    }
}
