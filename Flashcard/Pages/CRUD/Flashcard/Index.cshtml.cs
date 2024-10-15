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
    public class IndexModel : PageModel
    {
        private readonly FlashcardDBContext _context;

        public IndexModel(FlashcardDBContext context)
        {
            _context = context;
        }

        public IList<CardModel> CardModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cards != null)
            {
                //CardModel = await _context.Cards.ToListAsync();
                CardModel = await _context.Cards.OrderBy(x => x.BundleId).ToListAsync();
            }
        }
    }
}
