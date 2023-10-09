using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium.Support.UI;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IOrdersPage
    {
        WebDriverWait Wait { get; }

        OrderPage NewOrder();
        void Open();
    }
}