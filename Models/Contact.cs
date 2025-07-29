public class Contact {
    public int Id {get; set;}
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public string Email {get; set;} = null!;

    public int? AccountId {get; set;}
    public Account Account {get; set;} = null!;

    public ICollection<Incident> Incidents = new List<Incident>();
}