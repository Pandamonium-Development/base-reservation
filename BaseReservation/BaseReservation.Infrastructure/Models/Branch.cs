﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Branch")]
[Index("DistrictId", Name = "IX_Branch_DistrictId")]
public partial class Branch : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(150)]
    public string Description { get; set; } = null!;

    public int Telephone { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    public bool Active { get; set; }

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("DistrictId")]
    [InverseProperty("Branches")]
    public virtual District DistrictIdNavigation { get; set; } = null!;

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();

    [InverseProperty("BranchIdNavigation")]
    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}