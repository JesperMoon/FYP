using FYP.Entities;
using FYP.Entities.Data;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Data
{
    public class PractitionerData
    {
        public static string connString = ConstantHelper.DBAppSettings.FYP;

        public PractitionerViewModel CreatePractitioner(PractitionerViewModel newUser)
        {
            using (var context = new ApplicationContext())
            {
                var practitioner = new Practitioner()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Password = newUser.Password,
                    Gender = newUser.Gender,
                    DateOfBirth = newUser.DateOfBirth.ToUniversalTime(),
                    Religion = newUser.Religion.ToString(),
                    EmailAddress = newUser.EmailAddress,
                    OfficePhoneNumber = newUser.OfficePhoneNumber,
                    Company = newUser.Company,
                    PostalCode = Convert.ToInt32(newUser.PostalCode),
                    State = newUser.State.ToString(),
                    Role = newUser.Role,
                    Specialist = newUser.Specialist,
                    Qualification = newUser.Qualification,
                    UserName = newUser.UserName,
                    Status = 1,
                };

                context.Practitioner.Add(practitioner);
                context.SaveChanges();
            }

            return newUser;
        }

    }
}
