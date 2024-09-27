using System.Data;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaseReservation.Infrastructure.Data;

public partial class BaseReservationContext(DbContextOptions<BaseReservationContext> options) : DbContext(options)
{
    const string CREATEDNAME = "Created";
    const string UPDATEDNAME = "Updated";

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceDetailProduct> InvoiceDetailProducts { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailProduct> OrderDetailProducts { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<StatusOrder> StatusOrders { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryProduct> InventoryProducts { get; set; }

    public virtual DbSet<InventoryProductTransaction> InventoryProductTransactions { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<Province> Province { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationQuestion> ReservationQuestion { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchHoliday> BranchHolidays { get; set; }

    public virtual DbSet<BranchSchedule> BranchSchedules { get; set; }

    public virtual DbSet<BranchScheduleBlock> BranchScheduleBlocks { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<TypeService> TypeServices { get; set; }

    public virtual DbSet<TokenMaster> TokenMasters { get; set; }

    public virtual DbSet<UnitMeasure> UnitMeasures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBranch> UserBranches { get; set; }

    public IDbConnection Connection => Database.GetDbConnection();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canton>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProvinceIdNavigation).WithMany(p => p.Cantons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canton_Province");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasDefaultValue("");

            entity.HasOne(d => d.DistrictIdNavigation).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_District");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.VendorIdNavigation).WithMany(p => p.Contacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Vendor");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasOne(d => d.InvoiceIdNavigation).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");

            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.InvoiceDetails).HasConstraintName("FK_InvoiceDetail_Product");

            entity.HasOne(d => d.ServiceIdNavigation).WithMany(p => p.InvoiceDetails).HasConstraintName("FK_InvoiceDetail_Service");
        });

        modelBuilder.Entity<InvoiceDetailProduct>(entity =>
        {
            entity.HasOne(d => d.InvoiceDetailIdNavigation).WithMany(p => p.InvoiceDetailProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetailProduct_InvoiceDetail");

            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.InvoiceDetailProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetailProduct_Product");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasOne(d => d.OrderIdNavigation).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetail_Product");

            entity.HasOne(d => d.ServiceIdNavigation).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetail_Service");
        });

        modelBuilder.Entity<OrderDetailProduct>(entity =>
        {
            entity.HasOne(d => d.OrderDetailIdNavigation).WithMany(p => p.OrderDetailProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetailProduct_OrderDetail");

            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.OrderDetailProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetailProduct_Product");
        });

        modelBuilder.Entity<ReservationDetail>(entity =>
        {
            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.ReservationDetails).HasConstraintName("FK_ReservationDetail_Product");

            entity.HasOne(d => d.ReservationIdNavigation).WithMany(p => p.ReservationDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationDetail_Reservation");

            entity.HasOne(d => d.ServiceIdNavigation).WithMany(p => p.ReservationDetails).HasConstraintName("FK_ReservationDetail_Service");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasOne(d => d.CantonIdNavigation).WithMany(p => p.Districts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Canton");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(d => d.CustomerIdNavigation).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");

            entity.HasOne(d => d.TaxIdNavigation).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Tax");

            entity.HasOne(d => d.OrderIdNavigation).WithMany(p => p.Invoices).HasConstraintName("FK_Invoice_Order");

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Branch");

            entity.HasOne(d => d.PaymentTypeIdNavigation).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_PaymentType");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.Property(e => e.Month).HasConversion(x => x.ToString(), y => (Month)Enum.Parse(typeof(Month), y));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.Property(a => a.Day).HasConversion(m => m.ToString(), b => (WeekDay)Enum.Parse(typeof(WeekDay), b));
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.Property(a => a.TypeInventory).HasConversion(m => m.ToString(), b => (TypeInventory)Enum.Parse(typeof(TypeInventory), b));

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.Inventories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Branch");
        });

        modelBuilder.Entity<InventoryProduct>(entity =>
        {
            entity.HasOne(d => d.InventoryIdNavigation).WithMany(p => p.InventoryProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProduct_Inventory");

            entity.HasOne(d => d.ProductIdNavigation).WithMany(p => p.InventoryProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProduct_Product");
        });

        modelBuilder.Entity<InventoryProductTransaction>(entity =>
        {
            entity.Property(a => a.TransactionType).HasConversion(m => m.ToString(), b => (TransactionTypeInventory)Enum.Parse(typeof(TransactionTypeInventory), b));

            entity.HasOne(d => d.InventoryProductIdNavigation).WithMany(p => p.InventoryProductTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProductTransaction_InventoryProduct");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.CustomerIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.StatusOrderIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_StatusOrder");

            entity.HasOne(d => d.TaxIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Tax");

            entity.HasOne(d => d.ReservationIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Reservation");

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Branch");

            entity.HasOne(d => d.PaymentTypeIdNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_PaymentType");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.CategoryIdNavigation).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.UnitMeasureIdNavigation).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_UnitMeasure");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.DistrictIdNavigation).WithMany(p => p.Vendors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendor_District");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasDefaultValue("P")
                .IsFixedLength();

            entity.HasOne(d => d.CustomerIdNavigation).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Customer");

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Branch");
        });

        modelBuilder.Entity<ReservationQuestion>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.ReservationIdNavigation).WithMany(p => p.ReservationQuestions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationQuestion_Reservation");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.TypeServiceIdNavigation).WithMany(p => p.Services)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_TypeService");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.DistrictIdNavigation).WithMany(p => p.Branches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_District");
        });

        modelBuilder.Entity<BranchHoliday>(entity =>
        {
            entity.HasOne(d => d.HolidayIdNavigation).WithMany(p => p.BranchHolidays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchHoliday_Holiday");

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.BranchHolidays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchHoliday_Sucursal");
        });

        modelBuilder.Entity<BranchSchedule>(entity =>
        {
            entity.HasOne(d => d.ScheduleIdNavigation).WithMany(p => p.BranchSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchSchedule_Schedule");

            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.BranchSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchSchedule_Branch");
        });

        modelBuilder.Entity<BranchScheduleBlock>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.BranchScheduleIdNavigation).WithMany(p => p.BranchScheduleBlocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchScheduleBlock_BranchSchedule");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TypeService>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TokenMaster>(entity =>
        {
            entity.HasOne(d => d.UserIdNavigation).WithMany(p => p.TokenMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TokenMaster_User");
        });

        modelBuilder.Entity<UnitMeasure>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Symbol).IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.DistrictIdNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_District");

            entity.HasOne(d => d.GenderIdNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Gender");

            entity.HasOne(d => d.RoleIdNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserBranch>(entity =>
        {
            entity.HasOne(d => d.BranchIdNavigation).WithMany(p => p.UserBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranch_Branch");

            entity.HasOne(d => d.UserIdNavigation).WithMany(p => p.UserBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranch_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        DefaultProperties();
    }

    private void DefaultProperties()
    {
        string createdByName = "CreatedBy";
        string updatedByName = "UpdatedBy";

        DateTime created = DateTime.Now;
        DateTime updated = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries())
        {
            string createdBy = string.Empty;
            string updatedBy = null!;
            if (entry.Entity.GetType().GetProperty(createdByName) != null) createdBy = entry.Property(createdByName).CurrentValue!.ToString()!;
            if (entry.Entity.GetType().GetProperty(updatedByName) != null)
            {
                var modificacion = entry.Property(updatedByName).CurrentValue;
                if (modificacion != null) updatedBy = modificacion.ToString()!;
            }

            if (entry.State == EntityState.Added)
            {
                GenerateAdded(entry, createdByName, createdBy, updatedByName, created);
            }
            else
            {
                GenerateModified(entry, createdByName, updatedByName, updatedBy, updated);
            }
        }
    }

    private void GenerateAdded(EntityEntry entry, string createdByName, string createdBy, string updatedByName, DateTime created)
    {
        string activeName = "Active";

        if (entry.Entity.GetType().GetProperty(CREATEDNAME) != null && entry.Property(CREATEDNAME).CurrentValue != null) entry.Property(CREATEDNAME).CurrentValue = created;
        if (entry.Entity.GetType().GetProperty(activeName) != null && !(bool)entry.Property(activeName).CurrentValue!) entry.Property(activeName).CurrentValue = true;

        if (entry.Entity.GetType().GetProperty(createdByName) != null && entry.Property(updatedByName).CurrentValue != null)
        {
            entry.Property(createdByName).CurrentValue = entry.Property(updatedByName).CurrentValue;
            entry.Property(updatedByName).CurrentValue = null;
        }

        if (entry.Entity.GetType().GetProperty(createdByName) != null) entry.Property(createdByName).CurrentValue = createdBy;
        if (entry.Entity.GetType().GetProperty(UPDATEDNAME) != null) entry.Property(UPDATEDNAME).IsModified = false;
        if (entry.Entity.GetType().GetProperty(updatedByName) != null) entry.Property(updatedByName).IsModified = false;
    }

    private void GenerateModified(EntityEntry entry, string createdByName, string updatedByName, string updatedBy, DateTime updated)
    {
        if (entry.State == EntityState.Modified)
        {
            if (entry.Entity.GetType().GetProperty(UPDATEDNAME) != null) entry.Property(UPDATEDNAME).CurrentValue = updated;

            if (entry.Entity.GetType().GetProperty(updatedByName) != null) entry.Property(updatedByName).CurrentValue = updatedBy;
            if (entry.Entity.GetType().GetProperty(CREATEDNAME) != null) entry.Property(CREATEDNAME).IsModified = false;
            if (entry.Entity.GetType().GetProperty(createdByName) != null) entry.Property(createdByName).IsModified = false;
        }
    }
}
