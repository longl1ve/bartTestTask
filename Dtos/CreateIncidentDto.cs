public class CreateIncidentRequest
{
    public string AccountName { get; set; } = null!;
    public string ContactFirstName { get; set; } = null!;
    public string ContactLastName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string IncidentDescription { get; set; } = null!;
}