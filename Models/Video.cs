using System;
using System.Collections.Generic;

namespace ElearningMVC.Models;

public partial class Video
{
    public int Vid { get; set; }

    public string? Topic { get; set; }

    public string? Url { get; set; }

    public string? Course { get; set; }

    public string? Subcourse { get; set; }
}
