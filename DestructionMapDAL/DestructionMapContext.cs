using System.Configuration;
using DestructionMapDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DestructionMapDAL;

public class DestructionMapContext : DbContext
{
    //private string connectionString;
    
    public DestructionMapContext(DbContextOptions<DestructionMapContext> options) : base(options)
    {
        //connectionString = "Default connection";
        Database.EnsureCreated();
    }
    
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(connectionString);
    */


    public virtual DbSet<EventEntity> Events { get; set; }
    
    public virtual  DbSet<UserEntity> Users { get; set; }
    
    public virtual  DbSet<SourceEntity> Sources { get; set; }
    
    public virtual DbSet<ManagerEntity> Managers { get; set; }
    
    public virtual DbSet<Managers_Approvals> Approvals { get; set; } //


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EventEntity>()
            .HasMany(e => e.SourceList)
            .WithOne(s => s.Event_Id)
            .HasForeignKey(s => s.Event_Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder
            .Entity<EventEntity>()
            .HasMany<SourceEntity>(e => e.SourceList)
            .WithOptional()
            */
    }
}