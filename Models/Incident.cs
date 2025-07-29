public class Incident {
    public string Name {get; set;} = Guid.NewGuid().ToString();
    public string Description {get; set;} = null!;

    public int AccountId {get; set;}
    public Account Account {get; set;} = null!;

    public int? ContactId {get; set;}
    public Contact? Contact {get; set;}
}