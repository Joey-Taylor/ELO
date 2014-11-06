using System;

namespace EloWeb.Models
{
    /// <summary>
    /// Represents a player's rating
    /// </summary>
    public class Rating : IComparable<Rating>, IComparable, IEquatable<Rating>
    {
        public long PlayerId { get; set; }
        /// <summary>
        /// The numeric value of the rating.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The time from which this rating applies
        /// Note: The time is in UTC so will need localising
        /// </summary>
        public DateTime TimeFrom { get; set; }

        public bool Equals(Rating other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || obj.GetType() != GetType()) return false;
            return Equals((Rating) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value*397) ^ TimeFrom.GetHashCode();
            }
        }

        public int CompareTo(Rating other)
        {
            // We don't care about Time as we want ratings 
            // of the same value but at different times to equal
            // each other.
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj.GetType() != GetType()) throw new ArgumentException("Can't compare as object isn't a Rating");
            return CompareTo((Rating)obj);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Value, TimeFrom);
        }

        public static bool operator >(Rating left, Rating right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(Rating left, Rating right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >=(Rating left, Rating right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(Rating left, Rating right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator ==(Rating left, Rating right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rating left, Rating right)
        {
            return !(left == right);
        }
    }
}