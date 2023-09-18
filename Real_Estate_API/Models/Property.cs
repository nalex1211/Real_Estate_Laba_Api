using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Real_Estate_API.Models;

public partial class Property
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int BedroomCount { get; set; }
    public int BathroomCount { get; set; }
    public double Price { get; set; }
    public double Area { get; set; }
    public string AgentId { get; set; } = null!;
    public int? ImageId { get; set; }
    [JsonIgnore]
    public virtual Location Location { get; set; } = null!;
    [JsonIgnore]
    public virtual Image? Image { get; set; }
}
