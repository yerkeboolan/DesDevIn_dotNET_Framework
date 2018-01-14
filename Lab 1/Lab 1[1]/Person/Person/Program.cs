using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    enum Genders { Male, Female};
    class Person
    {
        public string firstName, lastName;
        public int age;
        public Genders gender;

        public Person(string firstName, string lastName, int age, Genders gender) : this(gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        public Person(Genders gender)
        {
            this.gender = gender;
        }

        public override string ToString()
        {
            return "First name: " + firstName + "\n" + "Last name: " + lastName + "\n" + "Age: " + age + "\n" + "Gender: " + gender;
        }

        static void Main(string[] args)
        {
            Person person = new Person("Manas", "Satzhanov", 24, Genders.Male);
            Console.WriteLine(person);
            Console.ReadKey();
        }
    }
}
