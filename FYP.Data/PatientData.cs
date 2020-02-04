using FYP.Entities;
using FYP.Entities.Data;
using FYP.Entities.ViewModel;
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
                            HomeAddress1 = newUser.HomeAddressLine1,
                            HomeAddress2 = newUser.HomeAddressLine2,
                            HomeAddress3 = newUser.HomeAddressLine3,
                            City = newUser.City,
                            PostalCode = Convert.ToInt32(newUser.PostalCode),
                            State = newUser.State.ToString(),
                            BloodType = newUser.BloodType.ToString(),
                            UserName = newUser.UserName,
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Patient.Add(patient);
                        context.SaveChanges();

                        //Getting new account Guid
                        newUser.AccId = query.Where(p => p.EmailAddress.Equals(newUser.EmailAddress)).Select(p => p.Id).FirstOrDefault();
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

        public PatientBaseViewModel PatientProfile(Guid accId)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Patient
                                where pt.Id.Equals(accId)
                                select pt;

                    var patient = query.Select(p => p).FirstOrDefault();

                    if(query != null)
                    {
                        result.AccId = accId;
                        result.FirstName = patient.FirstName;
                        result.LastName = patient.LastName;
                        result.UserName = patient.UserName;
                        result.Gender = patient.Gender;
                        result.ReligionString = patient.Religion;
                        result.DateOfBirth = patient.DateOfBirth.ToLocalTime();
                        result.EmailAddress = patient.EmailAddress;
                        result.ContactNumber1 = patient.ContactNumber1;
                        result.ContactNumber2 = patient.ContactNumber2;
                        result.ContactNumber3 = patient.ContactNumber3;
                        result.HomeAddressLine1 = patient.HomeAddress1;
                        result.HomeAddressLine2 = patient.HomeAddress2;
                        result.HomeAddressLine3 = patient.HomeAddress3;
                        result.City = patient.City;
                        result.PostalCode = patient.PostalCode.ToString();
                        result.StateString = patient.State;
                        result.BloodTypeString = patient.BloodType;
                    }
                }
            }
            catch (Exception err)
            {

            }

            return result;
        }
    }
}
