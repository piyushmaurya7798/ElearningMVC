using System;
using System.Collections.Generic;

namespace ElearningMVC.Models;

public partial class PaymentPlace
{
    public int Pid { get; set; }

    public string? Subcourse { get; set; }

    public string? Course { get; set; }

    public decimal? Price { get; set; }

    public string? Dt { get; set; }

    public string? Suser { get; set; }

    public string? Banner { get; set; }
}
