﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CottonPrompt.Infrastructure.Entities;

public partial class Setting
{
    public int Id { get; set; }

    public decimal QualityControlRate { get; set; }

    public decimal ChangeRequestRate { get; set; }

    public int ChangeRequestArtistsGroupId { get; set; }

    public int TrainingGroupArtistsGroupId { get; set; }

    public int TrainingGroupCheckersGroupId { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}