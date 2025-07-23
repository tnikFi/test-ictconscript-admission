using FluentValidation;

namespace Application.Queries.GetLogbookEntry;

public class GetLogbookEntryQueryValidator : AbstractValidator<GetLogbookEntryQuery>
{
    public GetLogbookEntryQueryValidator()
    {
        RuleFor(q => q.Id).NotNull();
    }
}