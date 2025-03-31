using TechFood.Domain.Shared;
using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Domain.ValueObjects
{
    public class Rating : ValueObject
    {
        public Rating(double note)
        {
            if (note > 5 || note < 0)
            {
                throw new DomainException("Rating must be between 0 and 5");
            }

            Note = note;
        }

        public double Note { get; private set; }

        public static implicit operator Rating(double note) => new(note);

        public static implicit operator double(Rating rating) => rating.Note;
    }
}