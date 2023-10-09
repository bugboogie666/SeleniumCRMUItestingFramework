using System;
using System.Collections.Generic;
using System.Linq;

using Dynamics.UITestsBase.PageObject;

using Bogus;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.Misc
{
    internal class AccountRandomDataHandler : IAccountRandomDataHandler
    {
        public const string RANDOM_DATA_MARKER = "<RANDOM>";

        public AccountPage accountPage;

        private Faker faker;

        private readonly Dictionary<string, Action<string>> fieldToMethodMapping;

        private Dictionary<string, string> data;

        public AccountRandomDataHandler(AccountPage accountPage, Dictionary<string, string> data)
        {
            this.accountPage = accountPage ?? new AccountPage();

            this.data = data;

            faker = new Faker();

            fieldToMethodMapping = new Dictionary<string, Action<string>>
            {
                { nameof(this.accountPage.AccountName), new Action<string>(GenerateCompanyName) }
            };
        }


        public void GenerateCompanyName(string key)
        {
            var seed = faker.Random.AlphaNumeric(4);
            var baseCompanyName = faker.Company.CompanyName();
            data[key] = $"UITest_{baseCompanyName}.{seed}";
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
