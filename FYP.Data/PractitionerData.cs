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
                    }
                    else
                    {
                        newCompany.ConflictEmailAddress = 1;
                    }
                }
            }
            catch (Exception err)
            {

            }

            return newCompany;
        }

        public NewPractitionerViewModel GetCompanyList(NewPractitionerViewModel vm)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Company
                                select pt;

                    var key = query.Select(p => p.Id.ToString()).ToList();
                    var value = query.Select(p => p.CompanyName).ToList();

                    vm.CompanyIdList = key;
                    vm.CompanyNameList = value;
                }
            }
            catch (Exception err)
            {

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
                    result.DateOfBirth = Convert.ToDateTime(practitioner[ConstantHelper.SQLColumn.Practitioner.DateOfBirth].ToString()).ToLocalTime();
                    result.EmailAddress = practitioner[ConstantHelper.SQLColumn.Practitioner.EmailAddress].ToString();
                    result.OfficePhoneNumber = practitioner[ConstantHelper.SQLColumn.Practitioner.OfficePhoneNumber].ToString();
                    result.Role = practitioner[ConstantHelper.SQLColumn.Practitioner.Role].ToString();
                    result.Specialist = practitioner[ConstantHelper.SQLColumn.Practitioner.Specialist].ToString();
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
