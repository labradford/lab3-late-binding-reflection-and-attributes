using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    [SpecialClass(1)]
    public class Person
    {
        public enum Genders { Unknown, Male, Female, Other};

        public string LastName { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public DateTime DOB { get; set; } = DateTime.MinValue;
        public Genders Gender { get; set; } = Genders.Unknown;

        public Person()
        {
        }

        public Person(string lastName, string firstName, DateTime dob, Genders gender)
        {
            LastName = lastName;
            FirstName = firstName;
            DOB = dob;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"{LastName, -15} {FirstName, -15} {DOB:  yyyy-MM-dd} {Gender, -7}";
        }
    }
}
