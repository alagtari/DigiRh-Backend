namespace Conge.Application.Dtos;

public record SupervisorDesisionDto(
    Guid Id,
    string Prenom,
    string Nom,
    string Image,
    LeaveAgreement Accord,
    DateTime? CreatedAt,
    DateTime? LastModified);