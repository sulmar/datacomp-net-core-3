using Bogus;
using Bogus.Extensions.Poland;
using DataComp.Training.Models;
using System;

namespace DataComp.Training.Fakers
{
    public class UserFaker : Faker<User>
    {
        public UserFaker()
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.Random.Guid());
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Pesel, f => f.Person.Pesel());
            RuleFor(p => p.Birthday, f => f.Person.DateOfBirth);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            Ignore(p => p.Nationality);

            RuleFor(p => p.UserName, f => f.Person.UserName);
            RuleFor(p => p.HashedPassword, f => "12345");

            // imie.nazwisko@datacomp.pl
            RuleFor(p => p.Email, (f, u) => $"{u.FirstName}.{u.LastName}@datacomp.pl");

        }
    }
}
