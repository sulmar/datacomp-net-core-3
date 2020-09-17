using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DataComp.Training.Models
{

    public class User : BaseEntity
    {
        private string firstName;

        public string FirstName
        {
            get => firstName; set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        //[Required(ErrorMessage = "Brak nazwiska")]
        //[StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime Birthday { get; set; }
        public string Pesel { get; set; }
        public bool IsRemoved { get; set; }
        public string Nationality { get; set; }
        public void DoWork() => Console.WriteLine();
    }
}
