using Real_Estate_API.Models;

namespace Real_Estate_API.Dto;

public class PropertyDto
{
    public string Status { get; set; }
    public string Type { get; set; }
    public int BedroomCount { get; set; }
    public int BathroomCount { get; set; }
    public double Price { get; set; }
    public double Area { get; set; }
    public string? AgentId { get; set; }
    public int? ImageId { get; set; }

    public string Country { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Street { get; set; }
    public IFormFile ImageFile { get; set; }
}
