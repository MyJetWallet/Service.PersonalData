using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Postgres
{
public class DatabaseContext : MyDbContext
    {
        public const string Schema = "personaldata";

        private const string PersonalDataTableName = "personaldata";
        private const string BillingDetailsTableName = "billing_details";
        private const string DocumentsTableName = "traderdocuments";

        private Activity _activity;

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PersonalDataPostgresEntity> PersonalDataSet { get; set; }

        public DbSet<BillingDetailsEntity> BillingDetails { get; set; }

        public DbSet<TraderDocument> TraderDocuments { get; set; }


        public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
        {
            var activity = MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

            var ctx = new DatabaseContext(options.Options) {_activity = activity};

            return ctx;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            SetPersonalDataEntry(modelBuilder);
            SetDocumentsEntry(modelBuilder);
            SetBillingDetailsEntry(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetPersonalDataEntry(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDataPostgresEntity>().ToTable(PersonalDataTableName);
            modelBuilder.Entity<PersonalDataPostgresEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.Email).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.FirstName).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.LastName).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.City).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.Phone).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.DateOfBirth).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.PostalCode).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.CountryOfCitizenship).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.CountryOfResidence).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.EmailHash).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.Sex).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.KYC).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.Confirm).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.ConfirmPhone).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.Address).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.USCitizen).IsRequired(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.IpOfRegistration).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.CountryOfRegistration).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.IsInternal);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.EmailGroupId).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.BrandId).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.PlatformType).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.IsTechnical).HasDefaultValue(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.CreatedAt).HasDefaultValue(DateTime.MinValue);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.PhoneCode).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.PhoneNumber).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<PersonalDataPostgresEntity>().Property(e => e.PhoneIso).IsRequired(false).HasMaxLength(512);

            modelBuilder.Entity<PersonalDataPostgresEntity>().HasIndex(e => e.Id).IsUnique();
            modelBuilder.Entity<PersonalDataPostgresEntity>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<PersonalDataPostgresEntity>().HasIndex(e => e.CreatedAt).IsUnique(false);
            modelBuilder.Entity<PersonalDataPostgresEntity>().HasIndex(e => e.IsInternal).IsUnique(false);
        }

        private void SetDocumentsEntry(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TraderDocument>().ToTable(DocumentsTableName);
            modelBuilder.Entity<TraderDocument>().HasKey(e => e.Id);
            modelBuilder.Entity<TraderDocument>().Property(e => e.TraderId).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<TraderDocument>().Property(e => e.DocumentType).IsRequired();
            modelBuilder.Entity<TraderDocument>().Property(e => e.DateTime).IsRequired();
            modelBuilder.Entity<TraderDocument>().Property(e => e.Mime).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<TraderDocument>().Property(e => e.FileName).IsRequired(false).HasMaxLength(512);
            modelBuilder.Entity<TraderDocument>().Property(e => e.IsDeleted);
            modelBuilder.Entity<TraderDocument>().HasIndex(e => e.Id).IsUnique();
        }

        private void SetBillingDetailsEntry(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingDetailsEntity>().ToTable(BillingDetailsTableName);
            modelBuilder.Entity<BillingDetailsEntity>().HasKey(e => e.ClientId);
        }
    }
}