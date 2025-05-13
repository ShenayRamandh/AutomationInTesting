using AutomationInTesting.API.Models;
using Bogus;

namespace AutomationInTesting.API.Fakers;

public sealed class BookingRequestFaker : Faker<BookingRequest>
{
    public BookingRequestFaker()
    {
        RuleFor(x => x.FirstName, f => f.Person.FirstName);
        RuleFor(x => x.LastName, f => f.Person.LastName);
        RuleFor(x => x.TotalPrice, f => f.Random.Int(1, 1000));
        RuleFor(x => x.DepositPaid, f => f.Random.Bool());
        RuleFor(x => x.BookingDates, f => new BookingDates
        {
            CheckIn = f.Date.Future(),
            CheckOut = f.Date.Future()
        });
        RuleFor(x => x.AdditionalNeeds, f => f.Lorem.Sentence());
    }
}