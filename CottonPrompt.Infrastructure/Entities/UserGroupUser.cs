﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CottonPrompt.Infrastructure.Entities;

public partial class UserGroupUser
{
    public int UserGroupId { get; set; }

    public Guid UserId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual User User { get; set; }

    public virtual UserGroup UserGroup { get; set; }
}