using Flashcard.Data.Bundle;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Z.BulkOperations;
using Serilog;



namespace Flashcard.Data.Subscription
{
    public class SubscriptionService
    {
        #region Property
        private readonly IDbContextFactory<FlashcardDBContext> _appDBFactory;
        #endregion

        #region Constructor
        public SubscriptionService(IDbContextFactory<FlashcardDBContext> dbContextFactory)
        {
            _appDBFactory = dbContextFactory;
        }
        #endregion

        #region Get List of Subscription
        public async Task<List<SubscriptionModel>> GetAllSubscriptionsAsync()
        {
            using (var context = _appDBFactory.CreateDbContext()) {
                return await context.Subscriptions.ToListAsync();
            }
        }

        public async Task<List<BundleModel>> GetAllSubscriptionsAsync(string userid)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                return await context.Bundles
                    .Where(bundle => context.Subscriptions
                            .Any(subs => subs.UserId == userid && 
                                subs.BundleID == bundle.Id && 
                                subs.EndDate >= DateTime.Today))
                    .ToListAsync();

                //return await context.Bundles
                //.FromSqlInterpolated($"EXECUTE dbo.GetSubscriptions {userid}")
                //.ToListAsync();
            }
            
        }

        #endregion

        #region Insert Subscription
        public async Task<bool> InsertSubscriptionAsync(SubscriptionModel subscription)
        {
            using (var context = _appDBFactory.CreateDbContext()) {

                try 
                {
                    await context.Subscriptions.AddAsync(subscription);
                    await context.SaveChangesAsync();
                    return true;
                } catch (Exception ex) 
                {
                    Log.Error(ex, "Error adding user answer {UserId} , {BundleId}", subscription.UserId,subscription.BundleID);
                    return false;    
                }
                
            }
               
        }

        public async Task<bool> InsertSubscriptionsAsync(List<SubscriptionModel> subscriptions)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                try 
                {
                    await context.BulkInsertAsync(subscriptions);
                    await context.BulkSaveChangesAsync();
                    return true;
                }
                catch (Exception ex) 
                {
                    Log.Error(ex, "Error adding subscriptions ");
                    return false;
                }
            }

        }
        #endregion

        #region Get subscription by Id
        public async Task<SubscriptionModel> GetSubscriptionAsync(string userid, int bundleid)
        {
            using (var context = _appDBFactory.CreateDbContext()) {
                
                try
                {
                    return await context.Subscriptions.FirstOrDefaultAsync(c => c.UserId.Equals(userid) && c.BundleID.Equals(bundleid));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error retrieving subscription {UserId} , {BundleId} ",userid,bundleid);
                    return null;
                }
            }
               
        }
        #endregion

        #region Delete Subscription
        public async Task<bool> DeleteSubscriptionAsync(SubscriptionModel subscription)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                var subsitem = context.Subscriptions.FirstOrDefault(x => x.UserId == subscription.UserId &&
                           x.BundleID == subscription.BundleID);
                if (subsitem != null)
                {
                    try
                    {
                        context.Subscriptions.Remove(subsitem);
                        await context.SaveChangesAsync();
                        return true;
                    }catch (Exception ex)
                    {
                        Log.Error(ex, "Error removing subscription {UserId}, {BundleId}", subscription.UserId, subscription.BundleID);
                        return false;
                    }
                    
                }
                else 
                {
                    Log.Error("Subscription not found in database {UserId}, {BundleId}", subscription.UserId, subscription.BundleID);
                    return false; 
                }

            }
        }
        #endregion
    }
}
