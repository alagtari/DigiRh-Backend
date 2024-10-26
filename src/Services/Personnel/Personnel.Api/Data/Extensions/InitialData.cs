namespace Personnel.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Person> Persons =>
        new List<Person>
        {
            new Person
            {
                Id = Guid.Parse("f7a9e4b4-c3a5-4e44-bf5f-476f5d7b8cb4"),
                Matricule = "4591A",
                Prenom = "Ala",
                Nom = "Gtari",
                NumeroTelephone = "28734227",
                Image = "image1.png",
                Direction = "DIRECTION DEVELOPPEMENT DIGITAL",
                Poste = "Chef de Service",
                Diplome = "Ingénieur Informaticien",
                SupervisorId = Guid.Parse("4e02d5b1-7529-4388-9133-76e8e6e3d96f") // Makrem is the supervisor
            },
            new Person
            {
                Id = Guid.Parse("4e02d5b1-7529-4388-9133-76e8e6e3d96f"),
                Matricule = "4592B",
                Prenom = "Makrem",
                Nom = "Taieb",
                NumeroTelephone = "28734228",
                Image = "image2.png",
                Direction = "DIRECTION DEVELOPPEMENT DIGITAL",
                Poste = "Chef de Service",
                Diplome = "Ingénieur Informaticien",
                SupervisorId = Guid.Parse("ce4789b5-6fe3-4399-b6a6-20e2dc91419d") // Faycel is the supervisor
            },
            new Person
            {
                Id = Guid.Parse("ce4789b5-6fe3-4399-b6a6-20e2dc91419d"),
                Matricule = "4593C",
                Prenom = "Faycel",
                Nom = "Kahloun",
                NumeroTelephone = "28734229",
                Image = "image3.png",
                Direction = "DIRECTION DEVELOPPEMENT DIGITAL",
                Poste = "Directeur",
                Diplome = "Ingénieur Informaticien",
                SupervisorId = null // Top-level person with no supervisor
            }
        };
}
