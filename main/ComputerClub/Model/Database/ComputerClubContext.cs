using Microsoft.EntityFrameworkCore;

namespace ComputerClub.Model.Database;

public partial class ComputerClubContext : DbContext
{
    public ComputerClubContext()
    {
    }

    public ComputerClubContext(DbContextOptions<ComputerClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Rent> Rents { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=HOME-PC;Initial Catalog=ComputerClub;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clubs__3213E83F9C5F08A6");

            entity.ToTable("clubs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.ClubLogin)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("club_login");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__computer__3213E83F569F54ED");

            entity.ToTable("computers");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.RateName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("rate_name");

            entity.HasOne(d => d.Club).WithMany(p => p.Computers)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__computers__clubI__5812160E");

            entity.HasOne(d => d.RateNameNavigation).WithMany(p => p.Computers)
                .HasForeignKey(d => d.RateName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__computers__rate___40058253");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__incomes__3213E83F4C68E736");

            entity.ToTable("incomes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ClubId).HasColumnName("club_id");

            entity.HasOne(d => d.Club).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__incomes__clubId__3C69FB99");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("rates");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rents__3213E83FB4F8AF02");

            entity.ToTable("rents");

            entity.HasIndex(e => e.ComputerId, "UQ__rents__805FE7FEAD83A0B5").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ComputerId).HasColumnName("computer_id");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Computer).WithOne(p => p.Rent)
                .HasForeignKey<Rent>(d => d.ComputerId)
                .HasConstraintName("FK__rents__computer___71D1E811");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("staff");

            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PassHash)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("pass_hash");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("role");

            entity.HasOne(d => d.Club).WithMany()
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__stuff__clubId__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
