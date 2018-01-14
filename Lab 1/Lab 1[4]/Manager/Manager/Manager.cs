using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    class Manager: Person
    {
        public string phoneNumber, officeLocation;

        public Manager(string firstName, string lastName, int age, Genders gender, string phoneNumber, string officeLocation) : base(firstName, lastName, age, gender)
        {
            this.phoneNumber = phoneNumber;
            this.officeLocation = officeLocation;
        }

        public override string ToString()
        {
            return "First name: " + firstName + "\n" + "Last name: " + lastName + "\n" + "Age: " + age + "\n" + "Gender: " + gender + "\n" + "Phone number: " + phoneNumber + "\n" + "Office location: " + officeLocation;

        }
    }
}
