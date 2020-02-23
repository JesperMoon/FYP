using FYP.Entities;
using FYP.Entities.Data;
using FYP.Entities.ViewModel;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Data
{
    public class PractitionerData
    {
        public static string connString = ConstantHelper.DBAppSettings.FYP;

        public NewPractitionerViewModel CreatePractitioner(NewPractitionerViewModel newUser)
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
                            Company = newUser.CompanyId,
                            Role = newUser.Role,
                            Specialist = newUser.Specialist.ToString(),
                            Qualification = newUser.Qualification,
                            UserName = newUser.UserName,
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Practitioner.Add(practitioner);
                        context.SaveChanges();

                        newUser.AccId = query.Where(p => p.EmailAddress == newUser.EmailAddress).Select(p => p.Id).FirstOrDefault();
                        newUser.CompanyEmailAddress = context.Company.Where(p => p.Id == newUser.CompanyId).Select(p => p.CompanyEmailAddress).FirstOrDefault();
                    }
                    else
                    {
                        newUser.ConflictEmailAddress = 1;
                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CreatePractitioner : " + err);
            }

            return newUser;
        }

        public string PractitionerApproved(Guid accId)
        {
            string emailAddress = String.Empty;
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var practitioner = context.Practitioner.Where(p => p.Id == accId).FirstOrDefault();
                    practitioner.Status = ConstantHelper.AccountStatus.Active;
                    result = context.SaveChanges();

                    emailAddress = practitioner.EmailAddress;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.PractitioenrApproved : " + err);
            }

            return emailAddress;
        }

        public string PractitionerRejected(Guid accId)
        {
            string emailAddress = String.Empty;
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var practitioner = context.Practitioner.Where(p => p.Id == accId).FirstOrDefault();
                    practitioner.Status = ConstantHelper.AccountStatus.Rejected;
                    result = context.SaveChanges();

                    emailAddress = practitioner.EmailAddress;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.PractitionerRejected : " + err);
            }

            return emailAddress;
        }

        public CompanyViewModel CreateCompany(CompanyViewModel newCompany)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Company
                                where pt.CompanyEmailAddress.Equals(newCompany.CompanyEmailAddress)
                                select pt;

                    var result = query.Select(p => p.Id).FirstOrDefault();

                    if (result.Equals(Guid.Empty))
                    {
                        var company = new Company()
                        {
                            CompanyName = newCompany.CompanyName,
                            CompanyPhoneNumber = newCompany.CompanyPhoneNumber,
                            CompanyEmailAddress = newCompany.CompanyEmailAddress,
                            CompanyAddressLine1 = newCompany.CompanyAddressLine1,
                            CompanyAddressLine2 = newCompany.CompanyAddressLine2,
                            CompanyAddressLine3 = newCompany.CompanyAddressLine3,
                            PostalCode = Convert.ToInt32(newCompany.PostalCode),
                            City = newCompany.City,
                            State = newCompany.State.ToString(),
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Company.Add(company);
                        context.SaveChanges();

                        //Getting new company account Guid
                        newCompany.CompanyId = query.Where(p => p.CompanyEmailAddress.Equals(newCompany.CompanyEmailAddress)).Select(p => p.Id).FirstOrDefault();
                    }
                    else
                    {
                        newCompany.ConflictEmailAddress = 1;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CreateCompany : " + err);
            }

            return newCompany;
        }

        public string CompanyApproved(Guid accId)
        {
            string emailAddress = String.Empty;
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var company = context.Company.Where(p => p.Id == accId).FirstOrDefault();
                    company.Status = ConstantHelper.AccountStatus.Active;
                    result = context.SaveChanges();

                    emailAddress = company.CompanyEmailAddress;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CompanyApproved : " + err);
            }

            return emailAddress;
        }

        public string CompanyRejected(Guid accId)
        {
            string emailAddress = String.Empty;
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var company = context.Company.Where(p => p.Id == accId).FirstOrDefault();
                    company.Status = ConstantHelper.AccountStatus.Rejected;
                    result = context.SaveChanges();

                    emailAddress = company.CompanyEmailAddress;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CompanyRejected : " + err);
            }

            return emailAddress;
        }

        public NewPractitionerViewModel GetCompanyList(NewPractitionerViewModel vm)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Company
                                where pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt;

                    var key = query.Select(p => p.Id.ToString()).ToList();
                    var value = query.Select(p => p.CompanyName).ToList();

                    vm.CompanyIdList = key;
                    vm.CompanyNameList = value;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetCompanyList : " + err);
            }

            return vm;
        }

        public PractitionerBaseViewModel GetProfile(PractitionerBaseViewModel vm)
        {
            PractitionerBaseViewModel result = new PractitionerBaseViewModel();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.GetPractitionerProfile, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, vm.AccId));
                SqlDataReader practitioner = cmd.ExecuteReader();

                while(practitioner.Read())
                {
                    result.AccId = Guid.Parse(practitioner[ConstantHelper.SQLColumn.Practitioner.Id].ToString());
                    result.FirstName = practitioner[ConstantHelper.SQLColumn.Practitioner.FirstName].ToString();
                    result.LastName = practitioner[ConstantHelper.SQLColumn.Practitioner.LastName].ToString();
                    result.UserName = practitioner[ConstantHelper.SQLColumn.Practitioner.UserName].ToString();
                    result.Gender = practitioner[ConstantHelper.SQLColumn.Practitioner.Gender].ToString();
                    result.ReligionString = practitioner[ConstantHelper.SQLColumn.Practitioner.Religion].ToString();
                    result.DateOfBirth = Convert.ToDateTime(practitioner[ConstantHelper.SQLColumn.Practitioner.DateOfBirth].ToString()).AddHours(6).ToLocalTime();
                    result.EmailAddress = practitioner[ConstantHelper.SQLColumn.Practitioner.EmailAddress].ToString();
                    result.OfficePhoneNumber = practitioner[ConstantHelper.SQLColumn.Practitioner.OfficePhoneNumber].ToString();
                    result.Role = practitioner[ConstantHelper.SQLColumn.Practitioner.Role].ToString();
                    result.SpecialistString = practitioner[ConstantHelper.SQLColumn.Practitioner.Specialist].ToString();
                    result.Qualification = practitioner[ConstantHelper.SQLColumn.Practitioner.Qualification].ToString();

                    //Company
                    result.CompanyName = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyName].ToString();
                    result.CompanyPhoneNumber = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyPhoneNumber].ToString();
                    result.CompanyEmailAddress = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyEmailAddress].ToString();
                    result.CompanyAddressLine1 = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyAddressLine1].ToString();
                    result.CompanyAddressLine2 = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyAddressLine2].ToString();
                    result.CompanyAddressLine3 = practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyAddressLine3].ToString();
                    result.PostalCode = practitioner[ConstantHelper.SQLColumn.Practitioner.PostalCode].ToString();
                    result.City = practitioner[ConstantHelper.SQLColumn.Practitioner.City].ToString();
                    result.State = practitioner[ConstantHelper.SQLColumn.Practitioner.State].ToString();
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetProfile : " + err);
            }

            return result;
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
                new LogHelper().LogMessage("PractitionerData.PractitionerLogin : " + err);
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
                new LogHelper().LogMessage("PractitionerData.GetSalt : " + err);
            }

            return result;
        }

        public string GetPractitionerEmail(Guid practitionerId)
        {
            string practitionerEmail = String.Empty;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.Id.Equals(practitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt.EmailAddress;

                    practitionerEmail = query.Select(p => p).FirstOrDefault().ToString();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetPractitionerEmail : " + err);
            }

            return practitionerEmail;
        }
    }
}
