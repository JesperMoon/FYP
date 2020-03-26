using FYP.Entities;
using FYP.Services.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FYP.Framework.EnumConstant;
using FYP.Data;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class PractitionerTest
    {
        //Test Connection String get correctly
        //Required to remove static in PractitionerData var connString
        [TestMethod]
        public void GetDatabaseConnectionString()
        {
            PractitionerData controller = new PractitionerData();

            //string actualConnStr = controller.connString;
            //string expectedConnStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FYP;User ID=admin;Password=admin123";

            //Assert.AreEqual(expectedConnStr, actualConnStr);
        }

        //on RegistrationController.CreateNewCompany
        [TestMethod]
        public void RegisteringNewCompany()
        {
            RegistrationController controller = new RegistrationController();
            CompanyViewModel testModel = new CompanyViewModel();
            TestHelper helper = new TestHelper();

            testModel.CompanyName = helper.RandomString(50, false);
            testModel.CompanyEmailAddress = helper.RandomString(250, false);
            testModel.CompanyPhoneNumber = helper.RandomString(20, false);
            testModel.CompanyAddressLine1 = helper.RandomString(250, false);
            testModel.CompanyAddressLine2 = helper.RandomString(250, false);
            testModel.CompanyAddressLine3 = helper.RandomString(250, false);
            testModel.PostalCode = helper.RandomNumber(1000, 9999).ToString();
            testModel.City = helper.RandomString(50, false);

            Array values = Enum.GetValues(typeof(State));
            Random random = new Random();
            State rdmState = (State)values.GetValue(helper.RandomNumber(0, 12));

            //Passing empty model
            int returnedResult = controller.CreateNewCompany(null);
            Assert.AreEqual(0, returnedResult);

            //Register company successfully
            int secondReturnedResult = controller.CreateNewCompany(testModel);
            Assert.AreEqual(1, secondReturnedResult);

            //Same company registering
            //testModel.CompanyEmailAddress = "chewcarlyong@gmail.com";
            //int thirdReturnedResult = controller.CreateNewCompany(testModel);
            //Assert.AreEqual(2, thirdReturnedResult);
        }

        [TestMethod]
        public void RetrievingCompaniesList()
        {
            NewPractitionerViewModel vm = new NewPractitionerViewModel();
            PractitionerData controller = new PractitionerData();

            NewPractitionerViewModel result = controller.GetCompanyList(vm);
            Assert.IsNotNull(result.CompanyIdList);
            Assert.IsNotNull(result.CompanyNameList);
        }

        [TestMethod]
        public void NewPractitionerRegistration()
        {
            //Created Successfully
            NewPractitionerViewModel vm = new NewPractitionerViewModel();
            TestHelper helper = new TestHelper();
            PractitionerData controller = new PractitionerData();


            vm.CompanyEmailAddress = helper.RandomString(50, false);
            vm.FirstName = helper.RandomString(20, false);
            vm.LastName = helper.RandomString(20, false);
            string rdmPassword = helper.RandomString(16, false);
            vm.Password = rdmPassword;
            vm.Gender = helper.RandomString(20, false);
            vm.DateOfBirth = DateTime.UtcNow;

            Array values = Enum.GetValues(typeof(State));
            Religion rdmReligion = (Religion)values.GetValue(helper.RandomNumber(0, 4));

            vm.Religion = rdmReligion;
            vm.EmailAddress = helper.RandomString(50,false);
            vm.OfficePhoneNumber = helper.RandomString(12, false);
            string hrdCodedCompanyId = "14694BBE-A650-EA11-B77B-28C2DDBBBA40";
            vm.CompanyId = Guid.Parse(hrdCodedCompanyId);
            vm.Role = helper.RandomString(10, false);
            Specialist rdmSpecialist = (Specialist)values.GetValue(helper.RandomNumber(0, 24));
            vm.Specialist = rdmSpecialist;
            vm.Qualification = String.Empty;
            vm.UserName = helper.RandomString(15, false);

            NewPractitionerViewModel result = controller.CreatePractitioner(vm);

            Assert.AreNotEqual(Guid.Empty, result.AccId);
            Assert.IsNotNull(result.CompanyEmailAddress);
        }

        //Test on user login
        [TestMethod]
        public void PractitionerLoginAccountFound()
        {
            //Positive Case
            PractitionerData controller = new PractitionerData();
            LoginInfo sampleUser = new LoginInfo();
            sampleUser.EmailAddress = "chewcarlyong@gmail.com";
            sampleUser.Password = "admin123";

            string expectedPassword = "MHywcsmZOuw2mjqSOoUremcwdEOLMkB7xhG9sbE4Tpc=";       //copied from database

            LoginInfo result = new LoginInfo();
            result = controller.PractitionerLogin(sampleUser);


            Assert.AreNotEqual(Guid.Empty, result.AccountNo);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.IsNotNull(result.Salt);
            Assert.IsNotNull(result.AccountStatus);

            //Negative Case - Wrong Email
            LoginInfo sampleUser2 = new LoginInfo();
            sampleUser2.EmailAddress = "carlyong@gmail.com";
            sampleUser2.Password = "admin123";

            result = new LoginInfo();
            result = controller.PractitionerLogin(sampleUser2);

            Assert.AreEqual(Guid.Empty, result.AccountNo);
            Assert.IsNull(result.AccountStatus);
            Assert.IsNull(result.Salt);


            //Negative Case - Wrong Password
            LoginInfo sampleUser3 = new LoginInfo();
            sampleUser3.EmailAddress = "chewcarlyong@gmail.com";
            sampleUser3.Password = "Admin456";

            result = new LoginInfo();
            result = controller.PractitionerLogin(sampleUser3);

            Assert.AreNotEqual(Guid.Empty, result.AccountNo);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.IsNotNull(result.AccountStatus);
            Assert.IsNotNull(result.Salt);
        }
    }
}
