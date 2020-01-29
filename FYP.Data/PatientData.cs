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
    public class PatientData
    {
        public static string connString = ConstantHelper.DBAppSettings.FYP;

        public PatientViewModel CreatePatient(PatientViewModel newUser)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Patient
                                where pt.EmailAddress.Equals(newUser.EmailAddress)
                                select pt;

                    var result = query.Select(p => p.Id).FirstOrDefault();

                    if (result.Equals(Guid.Empty))
                    {
                        var patient = new Patient()
                        {
                            FirstName = newUser.FirstName,
                            LastName = newUser.LastName,
                            Password = newUser.Password,
                            Salt = newUser.Salt,
                            Gender = newUser.Gender,
                            DateOfBirth = newUser.DateOfBirth.ToUniversalTime(),
                            Religion = newUser.Religion.ToString(),
                            EmailAddress = newUser.EmailAddress,
                            ContactNumber1 = newUser.ContactNumber1,
                            ContactNumber2 = newUser.ContactNumber2,
                            ContactNumber3 = newUser.ContactNumber3,
                            HomeAddress = newUser.HomeAddressLine1 + " " + newUser.HomeAddressLine2 + " " + newUser.HomeAddressLine3,
                            PostalCode = Convert.ToInt32(newUser.PostalCode),
                            State = newUser.State.ToString(),
                            BloodType = newUser.BloodType.ToString(),
                            UserName = newUser.UserName,
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Patient.Add(patient);
                        context.SaveChanges();

                        //Getting new account Guid
                        newUser.accId = query.Where(p => p.EmailAddress.Equals(newUser.EmailAddress)).Select(p => p.Id).FirstOrDefault();
                    }
                    else
                    {
                        newUser.ConflictEmailAddress = 1;
                    }
                }
            }
            catch (Exception err)
            {

            }

            return newUser;
        }

        public LoginInfo PatientLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Patient
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
            catch (Exception err)
            {

            }

            return result;
        }

        public int PatientVerification(Guid accId)
        {
            int result = 0;

            try
            {
                //var patient = new Patient { Id = accId , Status = ConstantHelper.AccountStatus.Active };

                using (var context = new ApplicationContext())
                {
                    var patient = context.Patient.Where(p => p.Id == accId).FirstOrDefault();
                    patient.Status = ConstantHelper.AccountStatus.Active;
                    result = context.SaveChanges();

                    //context.Patient.Attach(patient);
                    //context.Entry(patient).Property(x => x.Status).IsModified = true;
                    //result = context.SaveChanges();
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
                    var query = from pt in context.Patient
                                where pt.EmailAddress == emailAddress
                                select pt;

                    result = query.Select(p => p.Salt).FirstOrDefault();
                }

            }
            catch (Exception err)
            {

            }

            return result;
        }
    }
}
