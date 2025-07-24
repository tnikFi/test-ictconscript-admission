using FluentValidation;

namespace Application.Commands.AddLogbookEntry;

public class AddLogbookEntryCommandValidator : AbstractValidator<AddLogbookEntryCommand>
{
    public AddLogbookEntryCommandValidator()
    {
        RuleFor(c => c)
            .NotNull()
            .WithMessage("Invalid entry");
        
        // Title is required and should be at most 120 chars.
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Title is required");
            
        RuleFor(c => c.Title)
            .MaximumLength(120)
            .WithMessage("Title is too long");

        // Body can be an empty string, but shouldn't be null.
        // We'll use a maximum length of 2000 here to limit entries to a reasonable size
        RuleFor(c => c.Body)
            .NotNull()
            .WithMessage("Invalid body");
            
        RuleFor(c => c.Body)
            .MaximumLength(2000)
            .WithMessage("Entry body is too long");

        // When an entry is given a location, both latitude and longitude must be defined.
        When(c => c.Lat != null || c.Lon != null, () =>
        {
            RuleFor(c => c.Lat)
                .NotNull()
                .WithMessage("Latitude is required when longitude is defined");
                
            RuleFor(c => c.Lat)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90");

            RuleFor(c => c.Lon)
                .NotNull()
                .WithMessage("Longitude is required when latitude is defined");
                
            RuleFor(c => c.Lon)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180");
        });
    }
}