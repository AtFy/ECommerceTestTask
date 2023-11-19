using Lib.DbController.Models;
using Microsoft.EntityFrameworkCore;

namespace Lib.DbController.Context;

public partial class ECommerceContext : DbContext
{
    public ECommerceContext()
    {
    }

    public ECommerceContext(DbContextOptions<ECommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }
    
#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("ECOMMERCE_CONNECTION_STRING", 
                                       EnvironmentVariableTarget.User) ?? 
                                   throw new Exception("Couldn't get connection string."), 
            ServerVersion.Parse("11.1.2-mariadb"));
#else // Change EnvironmentVariableTarget to Process to run with docker-compose.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("ECOMMERCE_CONNECTION_STRING", 
                                       EnvironmentVariableTarget.User) ?? 
                                   throw new Exception("Couldn't get connection string."), 
            ServerVersion.Parse("11.1.2-mariadb"));
    
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");
        

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.ToTable("event");

            entity.HasIndex(e => e.EventType, "event_event_type_index");

            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("event_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(150)
                .HasColumnName("brand");
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(150)
                .HasColumnName("category_code");
            entity.Property(e => e.CategoryId)
                .HasColumnType("bigint(20)")
                .HasColumnName("category_id");
            entity.Property(e => e.EventTime)
                .HasColumnType("datetime")
                .HasColumnName("event_time");
            entity.Property(e => e.EventType)
                .HasMaxLength(100)
                .HasColumnName("event_type");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.UserSession).HasColumnName("user_session");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
