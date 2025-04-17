﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ASP.NET.DataBase.Entities;


[Table("tbl_сategories")]
public class CategoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string? Description { get; set; } = string.Empty;

    [Required, StringLength(255)]
    public string ImageUrl { get; set; } = string.Empty;
}
