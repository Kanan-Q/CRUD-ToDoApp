using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public bool? Isdone { get; set; }
}
