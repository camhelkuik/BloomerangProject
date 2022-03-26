using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace BloomerangProject
{
    public class Tests
    {
        string test_url = "https://jenkins-bloomerang.github.io/qa4.bloomerang.co/qa4bloomautomation01/volunteer/31759.html";

        IWebDriver driver;
        public IWebElement FirstName => driver.FindElement(By.Id("first-name"));
        public IWebElement LastName => driver.FindElement(By.Id("last-name"));
        public IWebElement Email => driver.FindElement(By.Id("email-address"));
        public IWebElement Phone => driver.FindElement(By.Id("phone-number"));
        public IWebElement Country => driver.FindElement(By.Id("country"));
        public IWebElement Address => driver.FindElement(By.Id("street-address"));
        public IWebElement City => driver.FindElement(By.Id("city"));
        public IWebElement State => driver.FindElement(By.Id("state"));
        public IWebElement ZipCode => driver.FindElement(By.Id("zip-code"));
        public IWebElement VolunteerDate => driver.FindElement(By.Id("volunteer-date"));
        public IWebElement Comments => driver.FindElement(By.Id("comment"));
        public IWebElement SubmitButton => driver.FindElement(By.ClassName("btn-submit-interaction"));

        [SetUp]
        public void start_Browser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void FillOutFullFormTest()
        {
            driver.Url = test_url;

            Thread.Sleep(3000);

            FirstName.SendKeys("Cam");
            LastName.SendKeys("Helkuik");
            Email.SendKeys("cam@email.com");
            Phone.SendKeys("123-123-1234");

            SelectElement countrySelect = new SelectElement(Country);
            countrySelect.SelectByText("United States");
            Address.SendKeys("123 S South St");
            City.SendKeys("Omaha");
            SelectElement stateSelect = new SelectElement(State);
            stateSelect.SelectByText("Nebraska");
            ZipCode.SendKeys("12345");

            VolunteerDate.SendKeys("04/22/2022");
            Comments.SendKeys("Hello! I am a comment.");

            SubmitButton.Click();

            Thread.Sleep(3000);

            string expectedTitle = "Thank you for volunteering!";
            IWebElement successPageTitle = driver.FindElement(By.XPath("//*[@id='interaction-form-container']/div/h2"));

            Assert.That(expectedTitle, Is.EqualTo(successPageTitle.Text));
        }

        [Test]
        public void FillOutRequiredFormFieldsTest()
        {
            driver.Url = test_url;

            Thread.Sleep(3000);

            FirstName.SendKeys("Cam");
            LastName.SendKeys("Helkuik");
            Email.SendKeys("cam@email.com");
           
            VolunteerDate.SendKeys("04/22/2022");

            SubmitButton.Click();

            Thread.Sleep(3000);

            string expectedTitle = "Thank you for volunteering!";
            IWebElement successPageTitle = driver.FindElement(By.XPath("//*[@id='interaction-form-container']/div/h2"));

            Assert.That(expectedTitle, Is.EqualTo(successPageTitle.Text));
        }

        [Test]
        public void FillOutRequiredFormFieldsExceptVolunteerDateTest()
        {
            driver.Url = test_url;

            Thread.Sleep(3000);

            FirstName.SendKeys("Cam");
            LastName.SendKeys("Helkuik");
            Email.SendKeys("cam@email.com");

            SubmitButton.Click();

            Thread.Sleep(3000);

            string expectedError = "This field is required.";
            IWebElement actualErrorLabel = driver.FindElement(By.XPath("//*[@id='interaction-form']/div[4]/div/label[2]"));
           
            Assert.That(expectedError, Is.EqualTo(actualErrorLabel.Text));
        }

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}