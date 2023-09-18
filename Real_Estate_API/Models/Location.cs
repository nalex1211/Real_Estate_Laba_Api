using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Real_Estate_API.Models;

public partial class Location
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Street { get; set; } = null!;
    [JsonIgnore]
    public virtual Property? Property { get; set; }
}
