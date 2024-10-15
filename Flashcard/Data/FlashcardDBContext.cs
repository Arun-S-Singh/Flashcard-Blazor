using Flashcard.Data.Bundle;
using Flashcard.Data.Card;
using Flashcard.Data.Cart;
using Flashcard.Data.Order;
using Flashcard.Data.Subscription;
using Flashcard.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Flashcard.Data
{
    public class FlashcardDBContext : IdentityDbContext<FlashcardUser>
    {
        public DbSet<CardModel> Cards { get; set; }

        public DbSet<BundleModel> Bundles { get; set; }

        public DbSet<CartModel> Cart { get; set; }

        public DbSet<SubscriptionModel> Subscriptions { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<UserAnswerModel> UserAnswers { get; set; }

        public FlashcardDBContext(DbContextOptions<FlashcardDBContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");
            builder.Entity<FlashcardUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            //Primary key for Bundle
            builder.Entity<BundleModel>()
                .HasKey(o => new { o.Id });

            //Primary key for Subscription
            builder.Entity<SubscriptionModel>()
            .HasKey(o => new { o.Id,o.UserId, o.BundleID });

            //Primary key for Card
            builder.Entity<CardModel>()
            .HasIndex(o => new { o.BundleId, o.CardId});

            // Default value for Card Id
            builder.Entity<CardModel>().Property(p => p.Id).HasDefaultValueSql("NEWID()");

            //Primary Key for Cart
            builder.Entity<CartModel>()
            .HasKey(o => new { o.Userid, o.BundleId });

            //Primary Key for Order
            builder.Entity<OrderModel>()
            .HasKey(o => new { o.OrderId, o.UserId, o.BundleId});

            builder.Entity<UserAnswerModel>()
                .HasKey(o => new { o.UserId, o.BundleId, o.CardId });

            //Seed data for Role Table
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "SuperAdmin", NormalizedName = "SuperAdmin", ConcurrencyStamp = "1" },
                new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "2" },
                new IdentityRole { Id = "3", Name = "Basic", NormalizedName = "Basic", ConcurrencyStamp = "3" }
                );

        }
    }
}