using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRental.Models
{
    public partial class Car_Rental_DBContext : DbContext
    {
        public Car_Rental_DBContext()
        {
        }

        public Car_Rental_DBContext(DbContextOptions<Car_Rental_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Rent> Rent { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.IdCar);

                entity.Property(e => e.IdCar).HasColumnName("Id_car");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rent>(entity =>
            {
                entity.HasKey(e => e.IdRent);

                entity.Property(e => e.IdRent)
                    .HasColumnName("Id_rent")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdCar).HasColumnName("Id_car");

                entity.Property(e => e.Login)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.Rent)
                    .HasForeignKey(d => d.IdCar)
                    .HasConstraintName("FK__Rent__Id_car__71D1E811");

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.Rent)
                    .HasForeignKey(d => d.Login)
                    .HasConstraintName("FK__Rent__Login__72C60C4A");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Mail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
