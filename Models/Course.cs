using System;
using System.Collections.Generic;

namespace ElearningMVC.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? Cname { get; set; }

    public string? Subname { get; set; }

    public decimal? Price { get; set; }

    public string? Banner { get; set; }
}
