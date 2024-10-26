namespace Conge.Application.Leaves.Commands.CreateLeave;

public record CreateLeaveCommand(CreateLeaveDto Leave)
    : ICommand<CreateLeaveResult>;

public record CreateLeaveResult(LeaveDto Payload);

public class CreateLeaveCommandValidator : AbstractValidator<CreateLeaveCommand>
{
    public CreateLeaveCommandValidator()
    {
        RuleFor(x => x.Leave.StaffId)
            .NotEmpty().WithMessage("StaffId is required.")
            .WithMessage("StaffId must be a valid GUID.");

        RuleFor(x => x.Leave.LeaveType)
            .IsInEnum().WithMessage("Invalid LeaveType.");

        RuleFor(x => x.Leave.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^[2-9]\d{7}$").WithMessage("Phone number must be a valid 8-digit Tunisian number.");
        RuleFor(x => x.Leave.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .GreaterThan(DateTime.Today).WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.Leave.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.Leave.StartDate)
            .WithMessage("End date must be after or equal to the start date.");
    }
}