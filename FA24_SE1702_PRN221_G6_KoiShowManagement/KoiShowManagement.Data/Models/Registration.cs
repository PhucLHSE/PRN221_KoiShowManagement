﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiShowManagement.Data.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int? CompetitionId { get; set; }

    public int? AnimalId { get; set; }

    public int? UserId { get; set; }

    public string EntryTitle { get; set; }

    public bool? CheckInStatus { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public bool? ApprovalStatus { get; set; }

    public string Notes { get; set; }

    [NotMapped] 
    public IFormFile? ImageFile { get; set; }
    public string Image { get; set; }

    public string HealthStatus { get; set; }

    public string Color { get; set; }

    public string Shape { get; set; }

    public string Pattern { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Animal Animal { get; set; }

    public virtual Competition Competition { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PointOnProgressing> PointOnProgressings { get; set; } = new List<PointOnProgressing>();

    public virtual User User { get; set; }
}