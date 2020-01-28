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
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.EmailAddress.Equals(newUser.EmailAddress)
                                select pt;

                    var result = query.Select(p => p.Id).FirstOrDefault();

                    if(result.Equals(Guid.Empty))
                    {
                        var practitioner = new Practitioner()
                        {
                            FirstName = newUser.FirstName,
                            LastName = newUser.LastName,
                            Password = newUser.Password,
                            Salt = newUser.Salt,
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
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Practitioner.Add(practitioner);
                        context.SaveChanges();
                    }
                    else
                    {
                        newUser.ConflictEmailAddress = 1;
                    }
                }
            }
            catch(Exception err)
            {

            }
            
            return newUser;
        }

        public LoginInfo PractitionerLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.EmailAddress.Equals(loginInfo.EmailAddress) && pt.Password.Equals(loginInfo.Password)
                                select pt;

                    result.AccountNo = query.Select(p => p.Id).FirstOrDefault();

                    //account found
                    if (!result.AccountNo.Equals(Guid.Empty))
                    {
                        result.AccountStatus = query.Select(p => p.Status).FirstOrDefault();
                    }
                }
            }
            catch(Exception err)
            {

            }

            return result;
        }

        public byte[] GetSalt(string emailAddress)
        {
            byte[] result = null;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.EmailAddress == emailAddress
                                select pt;

                    result = query.Select(p => p.Salt).FirstOrDefault();
                }

            }
            catch(Exception err)
            {

            }

            return result;
        }

    }
}
