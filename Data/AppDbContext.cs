using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Incident> Incidents => Set<Incident>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>().HasKey(i => i.Name);
        modelBuilder.Entity<Account>().HasIndex(a => a.Name).IsUnique();
        modelBuilder.Entity<Contact>().HasIndex(c => c.Email).IsUnique();

        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Account)
            .WithMany(a => a.Contacts)
            .HasForeignKey(c => c.AccountId);

        modelBuilder.Entity<Incident>()
            .HasOne(i => i.Account)
            .WithMany(a => a.Incidents)
            .HasForeignKey(i => i.AccountId);

        modelBuilder.Entity<Incident>()
            .HasOne(i => i.Contact)
            .WithMany(c => c.Incidents)
            .HasForeignKey(i => i.ContactId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
