using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Databasefirestapproc.Models;

[Table("employees")]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string? Code { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("gender")]
    [StringLength(50)]
    public string? Gender { get; set; }

    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    [Column("mobileno")]
    [StringLength(50)]
    public string? Mobileno { get; set; }

    [Column("password")]
    [StringLength(50)]
    public string? Password { get; set; }

    [InverseProperty("Maneger")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Employee")]
    public virtual ICollection<Projecttask> Projecttasks { get; set; } = new List<Projecttask>();
}
