using Flashcard.Data.Bundle;
using Flashcard.Data.Cart;
using Flashcard.Data.Order;
using Z.BulkOperations;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Flashcard.Data.Order
{
    public class OrderService
    {
        #region Property
        private readonly IDbContextFactory<FlashcardDBContext> _appDBFactory;

        #endregion

        public OrderService(IDbContextFactory<FlashcardDBContext> dbContextFactory)
        {
            _appDBFactory = dbContextFactory;
        }

        public async Task<bool> AddOrder(OrderModel orderitem)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                try 
                {
                    await context.Orders.AddAsync(orderitem);
                    await context.SaveChangesAsync();
                    return true;
                } catch (Exception ex) 
                {
                    Log.Error(ex, "Error adding order {OrderId},{UserId},{BundleId}", orderitem.OrderId, orderitem.UserId, orderitem.BundleId);
                    return false;
                }
                
            }
        }

        public async Task<bool> AddOrders(List<OrderModel> orderitems)
        {
            using (var context = _appDBFactory.CreateDbContext())
            {
                try 
                {
                    await context.BulkInsertAsync(orderitems);
                    await context.BulkSaveChangesAsync();
                    return true;
                } catch (Exception ex)
                {
                    Log.Error(ex, "Error adding orders");
                    return false;
                }
               
            }
        }
    }
}
