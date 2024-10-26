namespace Conge.Domain.Models
{
    public class Supervisor : Entity<SupervisorId>
    {
        public SupervisorId Id { get; private set; } = default!;
        public Guid PersonnelId { get; private set; } = default!;
        public string Prenom { get; private set; } = default!;
        public string Nom { get; private set; } = default!;
        public string Image { get; private set; } = default!;
        public LeaveId LeaveId { get; private set; } = default!;
        public LeaveAgreement Accord { get; private set; } = LeaveAgreement.Waiting;

        public static Supervisor Create(Guid personnelId, string prenom, string nom, string image, LeaveId leaveId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(prenom);
            ArgumentException.ThrowIfNullOrWhiteSpace(nom);
            ArgumentException.ThrowIfNullOrWhiteSpace(image);

            var supervisor = new Supervisor
            {
                Id = SupervisorId.Of(Guid.NewGuid()),
                PersonnelId = personnelId,
                Prenom = prenom,
                Nom = nom,
                Image = image,
                LeaveId = leaveId,
            };

            return supervisor;
        }

        // Method to update supervisor's leave agreement
        public void Update(LeaveAgreement accord)
        {
            Accord = accord;
        }
    }
}