using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    enum Genders {Male, Female};
    class Person
    {
        public string firstName, lastName;
        public int age;
        public Genders gender;

        public Person(string firstName, string lastName, int age, Genders gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.gender = gender;
        }

        public Person(Genders gender)
        {
            this.gender = gender;
        }

        public override string ToString()
        {
            return "First name: " + firstName + "\n" + "Last name: " + lastName + "\n" + "Age: " + age + "\n" + "Gender: " + gender;
        }

    }
}
