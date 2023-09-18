using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Real_Estate_API.Models;

public partial class Image
{
    public int Id { get; set; }

    public byte[] ImageData { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
