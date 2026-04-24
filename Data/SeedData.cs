using GestionDAB.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace GestionDAB.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            // ── Roles ──────────────────────────────────────────
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ── Admin user ─────────────────────────────────────
            const string adminEmail = "admin@dab.tn";
            const string adminPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // ── Test user ──────────────────────────────────────
            const string userEmail = "user@dab.tn";
            const string userPassword = "User@123";

            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "User");
            }

            // ── Stop if data already exists ────────────────────
            if (context.Banques.Any()) return;

            // ── Banques ────────────────────────────────────────
            var banques = new List<Banque>
            {
                new Banque { Code = 1001, Email = "contact@biat.tn", Nom = "BIAT", Rue = "Rue de la Liberté", Ville = "Tunis" },
                new Banque { Code = 1002, Email = "contact@attijari.tn", Nom = "Attijari Bank", Rue = "Avenue Habib Bourguiba", Ville = "Tunis" },
                new Banque { Code = 1003, Email = "contact@stb.tn", Nom = "STB", Rue = "Rue Hédi Nouira", Ville = "Sfax" }
            };
            await context.Banques.AddRangeAsync(banques);
            await context.SaveChangesAsync();

            // ── DABs ───────────────────────────────────────────
            var dabs = new List<DAB>
            {
                new DAB { DABId = "DAB-001", Localisation = "Avenue Habib Bourguiba, Tunis", EstEnService = true },
                new DAB { DABId = "DAB-002", Localisation = "Centre Commercial Lac, Tunis", EstEnService = true },
                new DAB { DABId = "DAB-003", Localisation = "Aéroport Tunis-Carthage", EstEnService = true },
                new DAB { DABId = "DAB-004", Localisation = "Avenue Farhat Hached, Sfax", EstEnService = false }
            };
            await context.DABs.AddRangeAsync(dabs);
            await context.SaveChangesAsync();

            // ── Comptes ────────────────────────────────────────
            var comptes = new List<Compte>
            {
                new Compte { NumeroCompte = "TN001234567", Proprietaire = "Youssef Ben Ali", Solde = 5200.00, Type = TypeCompte.Courant, BanqueId = banques[0].Id },
                new Compte { NumeroCompte = "TN001234568", Proprietaire = "Abdelaziz Trabelsi", Solde = 12500.50, Type = TypeCompte.Epargne, BanqueId = banques[0].Id },
                new Compte { NumeroCompte = "TN009876543", Proprietaire = "Mohamed Chaabane", Solde = 3800.00, Type = TypeCompte.Courant, BanqueId = banques[1].Id },
                new Compte { NumeroCompte = "TN009876544", Proprietaire = "Sarra Mansour", Solde = 750.00, Type = TypeCompte.Courant, BanqueId = banques[2].Id }
            };
            await context.Comptes.AddRangeAsync(comptes);
            await context.SaveChangesAsync();

            // ── Transactions ───────────────────────────────────
            var transactions = new List<Transaction>
            {
                new TransactionRetrait
                {
                    Date = DateTime.Now.AddDays(-10),
                    Montant = 200.00,
                    Libelle = "Retrait espèces",
                    CompteId = comptes[0].Id,
                    DABId = dabs[0].Id,
                    AutreAgence = false
                },
                new TransactionRetrait
                {
                    Date = DateTime.Now.AddDays(-7),
                    Montant = 500.00,
                    Libelle = "Retrait urgent",
                    CompteId = comptes[1].Id,
                    DABId = dabs[1].Id,
                    AutreAgence = true
                },
                new TransactionTransfert
                {
                    Date = DateTime.Now.AddDays(-5),
                    Montant = 1000.00,
                    Libelle = "Virement loyer",
                    CompteId = comptes[0].Id,
                    DABId = dabs[0].Id,
                    NumeroCompteDestinataire = "TN009876543"
                },
                new TransactionTransfert
                {
                    Date = DateTime.Now.AddDays(-2),
                    Montant = 350.00,
                    Libelle = "Remboursement",
                    CompteId = comptes[2].Id,
                    DABId = dabs[2].Id,
                    NumeroCompteDestinataire = "TN001234567"
                },
                new TransactionRetrait
                {
                    Date = DateTime.Now.AddDays(-1),
                    Montant = 150.00,
                    Libelle = "Retrait quotidien",
                    CompteId = comptes[3].Id,
                    DABId = dabs[1].Id,
                    AutreAgence = false
                }
            };
            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }
    }
}