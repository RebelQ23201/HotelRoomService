using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CleanService.DBContext
{
    public partial class CleanContext : DbContext
    {
        public CleanContext()
        {
        }

        public CleanContext(DbContextOptions<CleanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelMember> HotelMembers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomOrder> RoomOrders { get; set; }
        public virtual DbSet<RoomService> RoomServices { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<SystemRoomType> SystemRoomTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString=GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
            string GetConnectionString()
            {
                IConfiguration config = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json", true, true)
                                   .Build();
                string connectionString = config["ConnectionStrings:CleanContext"];
                return connectionString;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId)
                    .ValueGeneratedNever()
                    .HasColumnName("CompanyID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Employee_Company");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.HotelId)
                    .ValueGeneratedNever()
                    .HasColumnName("HotelID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<HotelMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("HotelMember");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelMembers)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_HotelMember_Hotel");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Order_Company");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Order_Hotel");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RoomOrderId).HasColumnName("RoomOrderID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_OrderDetail_Employee");

                entity.HasOne(d => d.RoomOrder)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.RoomOrderId)
                    .HasConstraintName("FK_OrderDetail_RoomOrder");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_OrderDetail_Service");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoomID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Room_Hotel");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK_Room_RoomType");
            });

            modelBuilder.Entity<RoomOrder>(entity =>
            {
                entity.ToTable("RoomOrder");

                entity.Property(e => e.RoomOrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoomOrderID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.RoomOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_RoomOrder_Order");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomOrders)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomOrder_Room");
            });

            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RoomService");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SystemRoomTypeId).HasColumnName("SystemRoomTypeID");

                entity.HasOne(d => d.Service)
                    .WithMany()
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_RoomService_Service");

                entity.HasOne(d => d.SystemRoomType)
                    .WithMany()
                    .HasForeignKey(d => d.SystemRoomTypeId)
                    .HasConstraintName("FK_RoomService_SystemRoomType1");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");

                entity.Property(e => e.RoomTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoomTypeID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SystemRoomTypeId).HasColumnName("SystemRoomTypeID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.RoomTypes)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_RoomType_Hotel");

                entity.HasOne(d => d.SystemRoomType)
                    .WithMany(p => p.RoomTypes)
                    .HasForeignKey(d => d.SystemRoomTypeId)
                    .HasConstraintName("FK_RoomType_SystemRoomType1");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Service_Company");
            });

            modelBuilder.Entity<SystemRoomType>(entity =>
            {
                entity.ToTable("SystemRoomType");

                entity.Property(e => e.SystemRoomTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("SystemRoomTypeID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
