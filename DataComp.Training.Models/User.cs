using System;

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
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime Birthday { get; set; }
        public string Pesel { get; set; }
        public bool IsRemoved { get; set; }
        public void DoWork() => Console.WriteLine();
    }
}
