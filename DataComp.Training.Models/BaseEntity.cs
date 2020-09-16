using System;

namespace DataComp.Training.Models
{
    // https://github.com/Fody/PropertyChanged
    public abstract class BaseEntity : Base
    {
        public Guid Id { get; set; }
    }
}
