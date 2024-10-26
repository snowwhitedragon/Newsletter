using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newsletter.Entities;

namespace Newsletter.Entities.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Newsletter> Newsletters { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Subcontractor> Subcontractors { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071BAB57FB");

            entity.HasIndex(e => e.CreatedById, "IX_Articles_Creator");

            entity.HasIndex(e => e.NewsletterId, "IX_Articles_Newsletter");

            entity.HasIndex(e => e.PublishedById, "IX_Articles_Publisher");

            entity.HasIndex(e => e.UpdatedById, "IX_Articles_Updated");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Summary).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.ArticleCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Articles__Create__1F63A897");

            entity.HasOne(d => d.Newsletter).WithMany(p => p.Articles)
                .HasForeignKey(d => d.NewsletterId)
                .HasConstraintName("FK__Articles__Newsle__1E6F845E");

            entity.HasOne(d => d.Organization).WithMany(p => p.Articles)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Articles__Organi__214BF109");

            entity.HasOne(d => d.PublishedBy).WithMany(p => p.ArticlePublishedBies)
                .HasForeignKey(d => d.PublishedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Articles__Publis__1D7B6025");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.ArticleUpdatedBies)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Articles__Update__2057CCD0");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC07EE55F82D");

            entity.ToTable(tb => tb.HasTrigger("Trigger_GenerateReadable_ForContacts"));

            entity.HasIndex(e => e.StateId, "IX_Contact_State");

            entity.HasIndex(e => e.LanguageId, "IX_Contacts_Language");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.ReadableId).HasMaxLength(15);
            entity.Property(e => e.Salutation).HasMaxLength(10);

            entity.HasOne(d => d.Language).WithMany(p => p.ContactLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacts__Langua__7A3223E8");

            entity.HasOne(d => d.State).WithMany(p => p.ContactStates)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacts__StateI__7755B73D");

            entity.HasMany(d => d.Newsletters).WithMany(p => p.Contacts)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscription",
                    r => r.HasOne<Newsletter>().WithMany()
                        .HasForeignKey("NewsletterId")
                        .HasConstraintName("FK__Subscript__Newsl__69FBBC1F"),
                    l => l.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__Subscript__Conta__6AEFE058"),
                    j =>
                    {
                        j.HasKey("ContactId", "NewsletterId").HasName("PK__Subscrip__9F2C3864C76C0BCE");
                        j.ToTable("Subscriptions");
                        j.HasIndex(new[] { "ContactId" }, "IX_Subscriptions_Contact");
                        j.HasIndex(new[] { "NewsletterId" }, "IX_Subscriptions_Newsletter");
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC077726D01B");

            entity.ToTable(tb => tb.HasTrigger("Trigger_GenerateReadable_ForCustomers"));

            entity.HasIndex(e => e.ContactId, "IX_Customers_Contact");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ReadableId).HasMaxLength(15);

            entity.HasOne(d => d.Contact).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Conta__65370702");
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Newslett__3214EC07399C52A4");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC070EC7FF12");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasMany(d => d.Newsletters).WithMany(p => p.Organizations)
                .UsingEntity<Dictionary<string, object>>(
                    "OrganizationNewsletter",
                    r => r.HasOne<Newsletter>().WithMany()
                        .HasForeignKey("NewsletterId")
                        .HasConstraintName("FK__Organizat__Newsl__671F4F74"),
                    l => l.HasOne<Organization>().WithMany()
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK__Organizat__Organ__662B2B3B"),
                    j =>
                    {
                        j.HasKey("OrganizationId", "NewsletterId").HasName("PK__Organiza__099116EDF2063989");
                        j.ToTable("OrganizationNewsletters");
                        j.HasIndex(new[] { "NewsletterId" }, "IX_OrganizationNewsletters_Newsletter");
                        j.HasIndex(new[] { "OrganizationId" }, "IX_OrganizationNewsletters_Organization");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07336C1F07");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__States__3214EC0785E078BD");

            entity.HasIndex(e => e.Title, "UQ__States__2CB664DC2034B2EF").IsUnique();

            entity.HasIndex(e => e.LanguageCode, "UQ__States__8B8C8A34392D791F").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Language).HasMaxLength(100);
            entity.Property(e => e.LanguageCode).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Subcontractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subcontr__3214EC0768BF9C51");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanyName).HasMaxLength(100);

            entity.HasMany(d => d.Contacts).WithMany(p => p.Subcontractors)
                .UsingEntity<Dictionary<string, object>>(
                    "SubcontractorContact",
                    r => r.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__Subcontra__Conta__690797E6"),
                    l => l.HasOne<Subcontractor>().WithMany()
                        .HasForeignKey("SubcontractorId")
                        .HasConstraintName("FK__Subcontra__Subco__681373AD"),
                    j =>
                    {
                        j.HasKey("SubcontractorId", "ContactId").HasName("PK__Subcontr__47CDD96A4176EC5B");
                        j.ToTable("SubcontractorContacts");
                        j.HasIndex(new[] { "ContactId" }, "IX_SubcontractorContacts_Contact");
                        j.HasIndex(new[] { "SubcontractorId" }, "IX_SubcontractorContacts_Subcontractor");
                    });
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC0767A70F8D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanyName).HasMaxLength(100);

            entity.HasMany(d => d.Contacts).WithMany(p => p.Suppliers)
                .UsingEntity<Dictionary<string, object>>(
                    "SupplierContact",
                    r => r.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__SupplierC__Conta__6CD828CA"),
                    l => l.HasOne<Supplier>().WithMany()
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK__SupplierC__Suppl__6BE40491"),
                    j =>
                    {
                        j.HasKey("SupplierId", "ContactId").HasName("PK__Supplier__EE2004ED308C8D1D");
                        j.ToTable("SupplierContacts");
                        j.HasIndex(new[] { "ContactId" }, "IX_SupplierContacts_Contact");
                        j.HasIndex(new[] { "SupplierId" }, "IX_SupplierContacts_Supplier");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07DD65953D");

            entity.HasIndex(e => e.ContactId, "IX_Users_Contact");

            entity.HasIndex(e => e.OrganizationId, "IX_Users_Organization");

            entity.HasIndex(e => e.Username, "UQ__tmp_ms_x__536C85E4AB504FAA").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.RegistratedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Contact).WithMany(p => p.Users)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK__Users__ContactId__03BB8E22");

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Users__Organizat__02C769E9");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__UserRoles__RoleI__6EC0713C"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserRoles__UserI__01D345B0"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD2783AA04");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_Role");
                        j.HasIndex(new[] { "UserId" }, "IX_UserRoles_User");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
