using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    class Manager: Person
    {
        /// <summary>
        /// The phone number
        /// </summary>
        public string phoneNumber, officeLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="age">The age.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="officeLocation">The office location.</param>
        public Manager(string firstName, string lastName, int age, Genders gender, string phoneNumber, string officeLocation) : base(firstName, lastName, age, gender)
        {
            this.phoneNumber = phoneNumber;
            this.officeLocation = officeLocation;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "First name: " + firstName + "\n" + "Last name: " + lastName + "\n" + "Age: " + age + "\n" + "Gender: " + gender + "\n" + "Phone number: " + phoneNumber + "\n" + "Office location: " + officeLocation;

        }
    }
}
