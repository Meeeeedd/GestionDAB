using GestionDAB.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionDAB.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Banque> Banques { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<DAB> DABs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionRetrait> TransactionRetraits { get; set; }
        public DbSet<TransactionTransfert> TransactionTransferts { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Fix Identity columns for SQLite ───────────────────
            // SQLite does not support nvarchar(max) — cap everything
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.Property(u => u.Id).HasMaxLength(450);
                entity.Property(u => u.UserName).HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(256);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);
                entity.Property(u => u.PasswordHash).HasMaxLength(500);
                entity.Property(u => u.SecurityStamp).HasMaxLength(500);
                entity.Property(u => u.ConcurrencyStamp).HasMaxLength(500);
                entity.Property(u => u.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Id).HasMaxLength(450);
                entity.Property(r => r.Name).HasMaxLength(256);
                entity.Property(r => r.NormalizedName).HasMaxLength(256);
                entity.Property(r => r.ConcurrencyStamp).HasMaxLength(500);
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(c => c.ClaimType).HasMaxLength(500);
                entity.Property(c => c.ClaimValue).HasMaxLength(500);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(t => t.LoginProvider).HasMaxLength(256);
                entity.Property(t => t.Name).HasMaxLength(256);
                entity.Property(t => t.Value).HasMaxLength(500);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(l => l.LoginProvider).HasMaxLength(256);
                entity.Property(l => l.ProviderKey).HasMaxLength(256);
                entity.Property(l => l.ProviderDisplayName).HasMaxLength(256);
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(c => c.ClaimType).HasMaxLength(500);
                entity.Property(c => c.ClaimValue).HasMaxLength(500);
            });

            // ── Banque ────────────────────────────────────────────
            modelBuilder.Entity<Banque>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Code).IsRequired();
                entity.HasIndex(b => b.Code).IsUnique();
                entity.Property(b => b.Email).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Nom).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Rue).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Ville).IsRequired().HasMaxLength(100);
            });

            // ── Compte ────────────────────────────────────────────
            modelBuilder.Entity<Compte>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.NumeroCompte).IsRequired().HasMaxLength(20);
                entity.HasIndex(c => c.NumeroCompte).IsUnique();
                entity.Property(c => c.Proprietaire).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Solde).IsRequired().HasDefaultValue(0.0);
                entity.Property(c => c.Type)
                      .IsRequired()
                      .HasConversion<string>();

                entity.HasOne(c => c.Banque)
                      .WithMany(b => b.Comptes)
                      .HasForeignKey(c => c.BanqueId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── DAB ───────────────────────────────────────────────
            modelBuilder.Entity<DAB>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.DABId).IsRequired().HasMaxLength(20);
                entity.HasIndex(d => d.DABId).IsUnique();
                entity.Property(d => d.Localisation).IsRequired().HasMaxLength(200);
                entity.Property(d => d.EstEnService).HasDefaultValue(true);
            });

            // ── Transaction (TPH) ─────────────────────────────────
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Date).IsRequired();
                entity.Property(t => t.Montant).IsRequired();
                entity.Property(t => t.Libelle).HasMaxLength(200);
                entity.Property(t => t.Type).HasMaxLength(50);

                entity.HasDiscriminator(t => t.Type)
                      .HasValue<TransactionRetrait>("Retrait")
                      .HasValue<TransactionTransfert>("Transfert");

                entity.HasOne(t => t.Compte)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.CompteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.DAB)
                      .WithMany(d => d.Transactions)
                      .HasForeignKey(t => t.DABId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── TransactionRetrait ────────────────────────────────
            modelBuilder.Entity<TransactionRetrait>(entity =>
            {
                entity.Property(t => t.AutreAgence).HasDefaultValue(false);
            });

            // ── TransactionTransfert ──────────────────────────────
            modelBuilder.Entity<TransactionTransfert>(entity =>
            {
                entity.Property(t => t.NumeroCompteDestinataire)
                      .IsRequired()
                      .HasMaxLength(20);
            });

            // ── AuditLog ──────────────────────────────────────────
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Action).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Entite).IsRequired().HasMaxLength(100);
                entity.Property(a => a.UserId).HasMaxLength(450);
                entity.Property(a => a.UserEmail).HasMaxLength(256);
                entity.Property(a => a.Details).HasMaxLength(500);
                entity.Property(a => a.DateAction).IsRequired();
            });
        }
    }
}