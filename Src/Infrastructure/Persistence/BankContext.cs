using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BankContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Persist Security Info=False;Server=tcp:localhost,1433;Database=FinTech;User Id=sa;Password=PassWord_1234$;TrustServerCertificate=true;");
    }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }
    
    public DbSet<BankAccountEntity> BankAccounts { get; set; }
    public DbSet<BankAccountAuditLogEntity> BankAccountAuditLogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BankAccountEntity>().HasData(
            new BankAccountEntity(){AccountNumber = 1,Balance = 100,Name = "Nikheel"},
            new BankAccountEntity(){AccountNumber = 2,Balance = 500,Name = "Tom"}
        );
    }
}