using System;
using System.Numerics;
using System.Security.Policy;
using Flashcard.Data.Bundle;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Flashcard.Data.Bundle
{
    public class BundleService
    {
        #region Property
        private readonly IDbContextFactory<FlashcardDBContext> _appDBFactory;
        #endregion

        #region Constructor
        public BundleService(IDbContextFactory<FlashcardDBContext> dbContextFactory)
        {
            _appDBFactory = dbContextFactory;
        }
        #endregion

        #region Get List of Bundles
        public async Task<List<BundleModel>> GetAllBundlesAsync()
        {
            try 
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    return await context.Bundles.ToListAsync();
                }
            } catch (Exception ex) 
            {
                Log.Error(ex, "Error retrieving all Bundles");
                return null;
            }
            
        }

        public async Task<List<BundleModel>> GetAllBundlesAsync(string userid)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                try 
                {
                    // Select all subscribed bundles
                    var subsbundleIds = await context.Subscriptions
                    .Where(subs => subs.UserId == userid && subs.EndDate >= DateTime.Now)
                    .Select(subs => subs.BundleID )
                    .ToArrayAsync();

                    return await context.Bundles
                        .Where(bundle => !subsbundleIds.Contains(bundle.Id))
                        .ToListAsync();
                } catch (Exception ex) 
                { 
                    Log.Error(ex, "Error retrieving all Bundles for User {UserId}",userid);
                    return null;
                }
                
            }
        }

        public async Task<BundleModel> GetBundleById(int id)
        {
            try 
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    return await context.Bundles.FirstOrDefaultAsync(x => x.Id == id);
                }
            } catch(Exception ex) 
            {
                Log.Error(ex, "Error retrieving Bundle {BundleId}", id);
                return null;
            }

        }

        #endregion
    }
    
}
