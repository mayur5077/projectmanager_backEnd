using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Databasefirestapproc.Models;

[Table("projects")]
public partial class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("descreption")]
    public string? Descreption { get; set; }

    [Column("startdate", TypeName = "date")]
    public DateTime? Startdate { get; set; }

    [Column("targetdate", TypeName = "date")]
    public DateTime? Targetdate { get; set; }

    [Column("manegerid")]
    public int? Manegerid { get; set; }

    [ForeignKey("Manegerid")]
    [InverseProperty("Projects")]
    public virtual Employee? Maneger { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Projectmodule> Projectmodules { get; set; } = new List<Projectmodule>();

    [InverseProperty("Project")]
    public virtual ICollection<Projecttask> Projecttasks { get; set; } = new List<Projecttask>();
}
