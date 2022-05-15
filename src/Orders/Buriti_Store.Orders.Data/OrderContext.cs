using Microsoft.EntityFrameworkCore;
using Buriti_Store.Core.Data;
using Buriti_Store.Core.Messages;
using Buriti_Store.Orders.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using Buriti_Store.Vendas.Data;
using Buriti_Store.Core.Communication.Mediator;

namespace Buriti_Store.Orders.Data
{
    public class OrderContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatrHandler;

        public OrderContext(DbContextOptions<OrderContext> options, IMediatorHandler mediatrHandler)
            : base(options)
        {
            _mediatrHandler = mediatrHandler;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateRegister") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateRegister").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateRegister").IsModified = false;
                }
            }

            var sucess = await base.SaveChangesAsync() > 0;
            if (sucess) await _mediatrHandler.PublishEvents(this);

            return sucess;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }
    }
}
