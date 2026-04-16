using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CapitalFlow.Persistence.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CapitalFlowDbContext>
{
    public CapitalFlowDbContext CreateDbContext(string[] args)
    {
        var connectionString = "server=localhost;port=3307;database=capitalflow;user=root;password=root123";

        var optionsBuilder = new DbContextOptionsBuilder<CapitalFlowDbContext>();

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );

        return new CapitalFlowDbContext(optionsBuilder.Options);
    }
}
