using System;
using System.Collections.Generic;
using System.Linq;

using Dynamics.UITestsBase.PageObject;

using Bogus;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.Misc
{
    public class ContactRandomDataHandler : IContactRandomDataHandler
    {
        public const string RANDOM_DATA_MARKER = "<RANDOM>";

        public QuickCreateContactPage quickCreateContactPage;

        private Faker faker;

        private readonly Dictionary<string, Action<string>> fieldToMethodMapping;

        private Dictionary<string, string> data;

        private string firstName;

        private string lastName;

        public ContactRandomDataHandler(QuickCreateContactPage quickCreateContactPage, Dictionary<string, string> data)
        {
            this.quickCreateContactPage = quickCreateContactPage ?? new QuickCreateContactPage();

            this.data = data;

            faker = new Faker();

            fieldToMethodMapping = new Dictionary<string, Action<string>>
            {
                { nameof(this.quickCreateContactPage.FirstName), new Action<string>(GenerateFirstName) },
                { nameof(this.quickCreateContactPage.LastName), new Action<string>(GenerateLastName) },
                { nameof(this.quickCreateContactPage.Email), new Action<string>(GenerateEmail) }
            };
        }


        private void GenerateFirstName(string key)
        {
            this.firstName = faker.Name.FirstName();
            data[key] = firstName;
        }


        public void GenerateLastName(string key)
        {
            this.lastName = faker.Name.LastName();
            data[key] = $"UITest_{lastName}";
        }


        public void GenerateEmail(string key)
        {
            var firstName = this.firstName ?? faker.Name.FirstName();
            var lastName = this.lastName ?? faker.Name.LastName();
            data[key] = faker.Internet.Email(firstName, lastName, null, "UITest");
        }


        public void RandomizeData()
        {
            foreach (var key in this.data.Keys.ToList())
            {
                if (data[key].Equals(RANDOM_DATA_MARKER))
                {
                    fieldToMethodMapping[key].Invoke(key);
                }
            }
        }
    }
}
