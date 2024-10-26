namespace Personnel.API.Models;

public class Person
{
    public Guid Id { get; set; }
    
    public string Matricule { get; set; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string NumeroTelephone { get; set; }
    public string Image { get; set; }
    public string Direction { get; set; }
    public string Poste { get; set; }
    public string Diplome { get; set; }
    
    // Nullable SupervisorId for persons who don't have a supervisor
    public Guid? SupervisorId { get; set; }
    
    // Optional navigation property to represent the supervisor
    public Person Supervisor { get; set; }
}