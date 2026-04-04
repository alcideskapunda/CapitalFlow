using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.BuyOrders;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Distributions;
using CapitalFlow.Domain.Entities.Quotes;
using CapitalFlow.Domain.Entities.Rebalancing.EventIRs;
using CapitalFlow.Domain.Entities.Rebalancing.RebalancingEvent;
using CapitalFlow.Domain.Entities.RecommendationBasket.BasketItems;
using CapitalFlow.Domain.Entities.RecommendationBasket.RecommendationBasket;
using CapitalFlow.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Persistence.Database;

public class CapitalFlowDbContext : DbContext
{
    public CapitalFlowDbContext(DbContextOptions<CapitalFlowDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CapitalFlowDbContext).Assembly);
    }

    public DbSet<User> Users { get; private set; }
    public DbSet<Customer> Customers { get; private set; }
    public DbSet<GraphicAccount> GraphicAccounts { get; private set; }
    public DbSet<Custody> Custodies { get; private set; }
    public DbSet<Distribution> Distributions { get; private set; }
    public DbSet<BuyOrder> BuyOrders { get; private set; }
    public DbSet<Quote> Quotes { get; private set; }
    public DbSet<EventIR> EventIRs { get; private set; }
    public DbSet<RebalancingEvent> RebalancingEvents { get; private set; }
    public DbSet<Basket> Baskets { get; private set; }
    public DbSet<BasketItem> BasketItems { get; private set; }
}
