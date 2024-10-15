using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Flashcard.Data.Subscription
{
    public class SubscriptionModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int BundleID { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

    }
}
