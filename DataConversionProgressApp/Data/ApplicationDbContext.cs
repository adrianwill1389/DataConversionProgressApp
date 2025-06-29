using DataConversionProgressApp.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CourtProgressRecord> CourtProgressRecords { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }


}


