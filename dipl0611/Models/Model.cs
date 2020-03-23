namespace dipl0611.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=dipl")
        {
        }

        public virtual DbSet<kontragents> kontragents { get; set; }
        public virtual DbSet<operation> operation { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<TTN> TTN { get; set; }
        public virtual DbSet<type_kontr> type_kontr { get; set; }
        public virtual DbSet<type_TTN> type_TTN { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<kontragents>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .Property(e => e.adress)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .Property(e => e.kontact_name)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .Property(e => e.kontact_telephone)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .Property(e => e.dogovor)
                .IsFixedLength();

            modelBuilder.Entity<kontragents>()
                .HasMany(e => e.products)
                .WithRequired(e => e.kontragents)
                .HasForeignKey(e => e.id_kontr)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<kontragents>()
                .HasMany(e => e.TTN)
                .WithRequired(e => e.kontragents)
                .HasForeignKey(e => e.id_kontr)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<products>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<products>()
                .Property(e => e.price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<products>()
                .HasMany(e => e.operation)
                .WithRequired(e => e.products)
                .HasForeignKey(e => e.id_product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TTN>()
                .Property(e => e.nomer)
                .IsFixedLength();

            modelBuilder.Entity<TTN>()
                .HasMany(e => e.operation)
                .WithRequired(e => e.TTN)
                .HasForeignKey(e => e.id_ttn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<type_kontr>()
                .HasMany(e => e.kontragents)
                .WithOptional(e => e.type_kontr)
                .HasForeignKey(e => e.type_kontr_id);

            modelBuilder.Entity<type_TTN>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<type_TTN>()
                .HasMany(e => e.TTN)
                .WithRequired(e => e.type_TTN)
                .HasForeignKey(e => e.id_type)
                .WillCascadeOnDelete(false);
        }
    }
}
