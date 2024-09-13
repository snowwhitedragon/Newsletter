using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newsletter.Entities;

// auto-generated
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

    public virtual DbSet<Subcontractor> Subcontractors { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC077FA79C33");

            entity.HasIndex(e => e.CreatedById, "IX_Articles_Creator");

            entity.HasIndex(e => e.NewsletterId, "IX_Articles_Newsletter");

            entity.HasIndex(e => e.PublishedById, "IX_Articles_Publisher");

            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.Summary).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.ArticleCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Articles__Create__151B244E");

            entity.HasOne(d => d.Newsletter).WithMany(p => p.Articles)
                .HasForeignKey(d => d.NewsletterId)
                .HasConstraintName("FK__Articles__Newsle__0B91BA14");

            entity.HasOne(d => d.PublishedBy).WithMany(p => p.ArticlePublishedBies)
                .HasForeignKey(d => d.PublishedById)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Articles__Publis__17036CC0");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07380E33FB");

            entity.ToTable(tb => tb.HasTrigger("Trigger_GenerateReadable_ForContacts"));

            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValue("Deutschland");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(5)
                .HasDefaultValue("de");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.ReadableId).HasMaxLength(15);
            entity.Property(e => e.Salutation).HasMaxLength(10);

            entity.HasMany(d => d.Newsletters).WithMany(p => p.Contacts)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscription",
                    r => r.HasOne<Newsletter>().WithMany()
                        .HasForeignKey("NewsletterId")
                        .HasConstraintName("FK__Subscript__Newsl__0A9D95DB"),
                    l => l.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__Subscript__Conta__7A672E12"),
                    j =>
                    {
                        j.HasKey("ContactId", "NewsletterId").HasName("PK__Subscrip__9F2C3864DC0F7294");
                        j.ToTable("Subscriptions");
                        j.HasIndex(new[] { "ContactId" }, "IX_Subscriptions_Contact");
                        j.HasIndex(new[] { "NewsletterId" }, "IX_Subscriptions_Newsletter");
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0792F85FB6");

            entity.ToTable(tb => tb.HasTrigger("Trigger_GenerateReadable_ForCustomers"));

            entity.HasIndex(e => e.ContactId, "IX_Customers_Contact");

            entity.Property(e => e.ReadableId).HasMaxLength(15);

            entity.HasOne(d => d.Contact).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Conta__787EE5A0");
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC076536F42E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07EC632FC1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasMany(d => d.Newsletters).WithMany(p => p.Organizations)
                .UsingEntity<Dictionary<string, object>>(
                    "OrganizationNewsletter",
                    r => r.HasOne<Newsletter>().WithMany()
                        .HasForeignKey("NewsletterId")
                        .HasConstraintName("FK__Organizat__Newsl__09A971A2"),
                    l => l.HasOne<Organization>().WithMany()
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK__Organizat__Organ__0C85DE4D"),
                    j =>
                    {
                        j.HasKey("OrganizationId", "NewsletterId").HasName("PK__Organiza__099116ED3D8C884F");
                        j.ToTable("OrganizationNewsletters");
                        j.HasIndex(new[] { "NewsletterId" }, "IX_OrganizationNewsletters_Newsletter");
                        j.HasIndex(new[] { "OrganizationId" }, "IX_OrganizationNewsletters_Organization");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07B50329E2");

            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Subcontractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0719877713");

            entity.Property(e => e.CompanyName).HasMaxLength(100);

            entity.HasMany(d => d.Contacts).WithMany(p => p.Subcontractors)
                .UsingEntity<Dictionary<string, object>>(
                    "SubcontractorContact",
                    r => r.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__Subcontra__Conta__797309D9"),
                    l => l.HasOne<Subcontractor>().WithMany()
                        .HasForeignKey("SubcontractorId")
                        .HasConstraintName("FK__Subcontra__Subco__01142BA1"),
                    j =>
                    {
                        j.HasKey("SubcontractorId", "ContactId").HasName("PK__Subcontr__47CDD96A39EEEA55");
                        j.ToTable("SubcontractorContacts");
                        j.HasIndex(new[] { "ContactId" }, "IX_SubcontractorContacts_Contact");
                        j.HasIndex(new[] { "SubcontractorId" }, "IX_SubcontractorContacts_Subcontractor");
                    });
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079CA89C61");

            entity.Property(e => e.CompanyName).HasMaxLength(100);

            entity.HasMany(d => d.Contacts).WithMany(p => p.Suppliers)
                .UsingEntity<Dictionary<string, object>>(
                    "SupplierContact",
                    r => r.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK__SupplierC__Conta__7B5B524B"),
                    l => l.HasOne<Supplier>().WithMany()
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK__SupplierC__Suppl__02084FDA"),
                    j =>
                    {
                        j.HasKey("SupplierId", "ContactId").HasName("PK__Supplier__EE2004ED2928A683");
                        j.ToTable("SupplierContacts");
                        j.HasIndex(new[] { "ContactId" }, "IX_SupplierContacts_Contact");
                        j.HasIndex(new[] { "SupplierId" }, "IX_SupplierContacts_Supplier");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07595D0EFD");

            entity.HasIndex(e => e.Username, "UQ__tmp_ms_x__536C85E4B5605901").IsUnique();

            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.RegistratedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Users__Organizat__1332DBDC");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__UserRoles__RoleI__00200768"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserRoles__UserI__123EB7A3"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD1088615C");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_Role");
                        j.HasIndex(new[] { "UserId" }, "IX_UserRoles_User");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
