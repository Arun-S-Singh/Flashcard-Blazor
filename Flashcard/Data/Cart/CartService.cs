using Flashcard.Data.Bundle;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Flashcard.Data.Cart
{
    public class CartService
    {
        #region Property
        private readonly IDbContextFactory<FlashcardDBContext> _appDBFactory;
        
        public List<BundleModel> SelectedItems { get; set; } = new();
        #endregion

        public CartService(IDbContextFactory<FlashcardDBContext> dbContextFactory)
        {
            _appDBFactory = dbContextFactory;
        }

       public async Task<bool> AddBundleToCart(CartModel cartitem) {
            using (var context = _appDBFactory.CreateDbContext()) {
                try 
                {
                    context.Cart.Add(cartitem);
                    await context.SaveChangesAsync();
                    return true;
                } catch (Exception ex) 
                {
                    Log.Error(ex, "Error while adding Bundle {userid} {BundleId} to the cart",cartitem.Userid,cartitem.BundleId);
                    return false;
                }
                
            }
        }

        public async Task<bool> RemoveBundleFromCart(CartModel item)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                var cartitem = context.Cart.FirstOrDefault( x => x.Userid == item.Userid &&
                           x.BundleId == item.BundleId );
                if(cartitem != null)
                {
                    try 
                    {
                        context.Cart.Remove(cartitem);
                        await context.SaveChangesAsync();
                        return true;
                    } catch (Exception ex) 
                    {
                        Log.Error(ex, "Error while removing Bundle {BundleId} from the cart",item.BundleId);
                        return false;
                    }
                }
                else 
                {
                    Log.Error("Cart item {userid}, {cartitem} not found in the database",item.Userid,item.BundleId);
                    return false; 
                }
                
            }
        }

        public async Task<bool> RemoveBundlesFromCart(List<CartModel> cartitems)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                try 
                
                {
                    await context.BulkDeleteAsync(cartitems);
                    await context.BulkSaveChangesAsync();
                    return true;
                } catch (Exception ex)
                {
                    Log.Error(ex, "Error while bulk removing Bundles");
                    return false;
                }
            }
        }

        public async Task<List<BundleModel>> GetSelectedItemsAsync(string userid)
        {
            try 
            {
                using (var context = _appDBFactory.CreateDbContext())
                {
                    return await context.Bundles
                                .Where(bundle => context.Cart.Any(cartitem => cartitem.Userid == userid && cartitem.BundleId == bundle.Id))
                                .ToListAsync();
                }
            } catch (Exception ex) 
            {
                Log.Error(ex, "Error while retrieving selected items from cart {UserId}",userid);
                return null;
            }
            
                
        }
    }
}
