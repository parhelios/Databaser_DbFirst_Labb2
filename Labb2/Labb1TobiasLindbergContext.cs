using Labb2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2;

public partial class Labb1TobiasLindbergContext : DbContext
{
    public Labb1TobiasLindbergContext()
    {
    }

    public Labb1TobiasLindbergContext(DbContextOptions<Labb1TobiasLindbergContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<AuthorBirthplace> AuthorBirthplaces { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookFormat> BookFormats { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<VAllInfo> VAllInfos { get; set; }

    public virtual DbSet<VAuthorBirthplaceByCountry> VAuthorBirthplaceByCountries { get; set; }

    public virtual DbSet<VTitlarPerFörfattare> VTitlarPerFörfattares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=EPHEMERAL;Initial Catalog=Labb1_TobiasLindberg;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC343A67C39C");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();

            entity.HasOne(d => d.AuthorOriginNavigation).WithMany(p => p.Authors)
                .HasForeignKey(d => d.AuthorOrigin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Authors__AuthorO__2E1BDC42");

            entity.HasMany(d => d.Isbns).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorJunction",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorBooks_BookISBN13"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorBooks_Author"),
                    j =>
                    {
                        j.HasKey("AuthorId", "Isbn").HasName("PK_AuthorBooks");
                        j.ToTable("AuthorJunction");
                        j.IndexerProperty<long>("Isbn").HasColumnName("ISBN");
                    });
        });

        modelBuilder.Entity<AuthorBirthplace>(entity =>
        {
            entity.HasKey(e => e.AuthorBirthplaceId).HasName("PK__AuthorBi__D654B29B39491AFA");

            entity.ToTable("AuthorBirthplace");

            entity.Property(e => e.AuthorBirthplaceId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Books__447D36EB5DA04CC5");

            entity.Property(e => e.Isbn)
                .ValueGeneratedNever()
                .HasColumnName("ISBN");
            entity.Property(e => e.Format).HasDefaultValue(1);
            entity.Property(e => e.Language).HasDefaultValue(1);

            entity.HasOne(d => d.FormatNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Format)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Format__35BCFE0A");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Genre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Genre__33D4B598");

            entity.HasOne(d => d.LanguageNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Language)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Language__32E0915F");

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Publisher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Publisher__36B12243");
        });

        modelBuilder.Entity<BookFormat>(entity =>
        {
            entity.HasKey(e => e.BookFormatId).HasName("PK__BookForm__185412CAFF5BC254");

            entity.ToTable("BookFormat");

            entity.Property(e => e.BookFormatId).ValueGeneratedNever();
            entity.Property(e => e.BookFormat1)
                .HasMaxLength(20)
                .HasColumnName("BookFormat");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__0385057E35F74A79");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).ValueGeneratedNever();
            entity.Property(e => e.GenreName).HasMaxLength(30);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbn });

            entity.ToTable("Inventory");

            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISBN_Books");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreId_Stores");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Language__B93855AB8848E6B2");

            entity.ToTable("Language");

            entity.Property(e => e.LanguageId).ValueGeneratedNever();
            entity.Property(e => e.BookLanguage).HasMaxLength(3);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FABE7D37222");

            entity.Property(e => e.PublisherId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F101B3507A66");

            entity.Property(e => e.StoreId).ValueGeneratedNever();
        });

        modelBuilder.Entity<VAllInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAllInfo");

            entity.Property(e => e.AuthorDateOfBirth).HasColumnName("Author_DateOfBirth");
            entity.Property(e => e.AuthorOrigin).HasColumnName("Author_Origin");
            entity.Property(e => e.Format).HasMaxLength(20);
            entity.Property(e => e.Genre).HasMaxLength(30);
            entity.Property(e => e.Isbn).HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(3);
        });

        modelBuilder.Entity<VAuthorBirthplaceByCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAuthorBirthplaceByCountry");
        });

        modelBuilder.Entity<VTitlarPerFörfattare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTitlarPerFörfattare");

            entity.Property(e => e.AntalBöcker)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Lagervärde)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Ålder)
                .HasMaxLength(19)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
