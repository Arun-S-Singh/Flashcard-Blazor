using System;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data.Card;
using System.Configuration;
using Serilog;
using Flashcard.Pages.Subscription;
using Flashcard.Pages.Bundle;


namespace Flashcard.Data.Card
{
    public class CardService
    {
        #region Property
        private readonly IDbContextFactory<FlashcardDBContext> _appDBFactory;
        #endregion

        #region Constructor
        public CardService(IDbContextFactory<FlashcardDBContext> appDBContext)
        {
            _appDBFactory = appDBContext;
        }
        #endregion

        public async Task<List<CardModel>> GetAllCardsAsync(int bundleid, string userid, bool includeAnswered)
        {
            try
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    if (includeAnswered)
                    {
                        List<CardModel> cards =  await context.Cards
                        .Where<CardModel>(b => b.BundleId == bundleid)
                        .ToListAsync();

                        List<UserAnswerModel> userresp = await context.UserAnswers
                          .Where<UserAnswerModel>(u => u.UserId == userid && u.BundleId == bundleid && u.Answered == true)
                          //.Select(ans => ans.CardId)                          
                          .Select(ans => new UserAnswerModel { CardId = ans.CardId, Answered = ans.Answered })
                          .ToListAsync();

                        foreach (var resp in userresp)
                        {
                            int index = cards.FindIndex(c => c.CardId == resp.CardId);
                            cards[index].Answered = resp.Answered;
                        }

                        return cards;
                    }
                    else
                    {
                        var userresp = await context.UserAnswers
                              .Where<UserAnswerModel>(u => u.UserId == userid && u.BundleId == bundleid && u.Answered == true)
                              .Select(ans => ans.CardId)
                              .ToListAsync();

                        return await context.Cards
                        .Where<CardModel>(c => c.BundleId == bundleid && !userresp.Contains(c.CardId))
                        .ToListAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving cards for Bundle : {BundleId}", bundleid);
                return null;
            }
        }
        public async Task<List<CardModel>> GetAllCardsAsync(int bundleid, String category, string userid, bool includeAnswered)
        {
            try
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    if (includeAnswered)
                    {
                        List<CardModel> cards = await context.Cards
                        .Where<CardModel>(b => b.BundleId == bundleid && b.Category == category)
                        .ToListAsync();

                        List<UserAnswerModel> userresp = await context.UserAnswers
                          .Where<UserAnswerModel>(u => u.UserId == userid && u.BundleId == bundleid && u.Answered == true)
                          //.Select(ans => ans.CardId)                          
                          .Select(ans => new UserAnswerModel { CardId = ans.CardId, Answered = ans.Answered })
                          .ToListAsync();

                        int index=0;
                        foreach (var resp in userresp)
                        {
                            index = cards.FindIndex(c => c.CardId == resp.CardId);
                            if (index != -1)
                            {
                                cards[index].Answered = resp.Answered;
                            }
                            
                        }

                        return cards;
                    }
                    else
                    {
                        var userresp = await context.UserAnswers
                             .Where<UserAnswerModel>(u => u.UserId == userid && u.BundleId == bundleid && u.Answered == true)
                             .Select(ans => ans.CardId)
                             .ToListAsync();

                        return await context.Cards
                        .Where<CardModel>(c => c.BundleId == bundleid && c.Category == category && !userresp.Contains(c.CardId))
                        .ToListAsync();
                    }


                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving cards for Bundle : {BundleId}, {Category}", bundleid, category);
                return null;
            }
        }
        public async Task<List<CardModel>> GetAllCategoriesAsync(int bundleid)
        {
            try
            {
                using (var context = _appDBFactory.CreateDbContext())
                {

                    return await context.Cards
                        .Where<CardModel>(b => b.BundleId == bundleid)
                        .Select(x => new CardModel { Category = x.Category }).Distinct()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving categories for Bundle : {BundleId}", bundleid);
                return null;
            }

        }
        public async Task<bool> UpsertUserResponse(UserAnswerModel useranswer)
        {
            try
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    var useranswerdb = await context.UserAnswers.FirstOrDefaultAsync(u => u.UserId == useranswer.UserId
                        && u.BundleId == useranswer.BundleId && u.CardId == useranswer.CardId);
                    if (useranswerdb == null)
                    {
                        await context.UserAnswers.AddAsync(useranswer);
                    }
                    else
                    {
                        //useranswerdb.Answered = useranswer.Answered;
                        //context.UserAnswers.Update(useranswerdb);
                        context.UserAnswers.Remove(useranswerdb);
                    }
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding user answer {UserId} , {BundleId}, {CardId}", useranswer.UserId, useranswer.BundleId, useranswer.CardId);
                return false;
            }
        }
    }
}
