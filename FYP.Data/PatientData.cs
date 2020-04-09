using FYP.Entities;
using FYP.Entities.Data;
using FYP.Entities.Model;
using FYP.Entities.ViewModel;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FYP.Framework.EnumConstant;

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
                new LogHelper().LogMessage("PatientData.CreatePatient : " + err);
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
                                where pt.EmailAddress.Equals(loginInfo.EmailAddress)
                                select pt;

                    result.AccountNo = query.Select(p => p.Id).FirstOrDefault();

                    //account found
                    if (!result.AccountNo.Equals(Guid.Empty))
                    {
                        result.Salt = query.Select(p => p.Salt).FirstOrDefault();
                        result.Password = query.Select(p => p.Password).FirstOrDefault();
                        result.AccountStatus = query.Select(p => p.Status).FirstOrDefault();
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.PatientLogin : " + err);
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
                new LogHelper().LogMessage("PatientData.PatientVerification : " + err);
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
                new LogHelper().LogMessage("PatientData.PatientProfile : " + err);
            }

            return result;
        }

        public SpecialistNearbyViewModel SpecialistSearch(SpecialistNearbyViewModel vm)
        {
            SpecialistNearbyViewModel result = new SpecialistNearbyViewModel();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.SpecialistSearch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.SearchText, vm.SearchText));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.Specialist, vm.SpecialistSelected));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.State, vm.StateSelected.ToString()));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.PostalCode, vm.PostalCode));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SpecialistNearby specialist = new SpecialistNearby();
                    specialist.AccId = Guid.Parse(reader[ConstantHelper.SQLColumn.SpecialistSearch.Id].ToString());
                    specialist.SpecialistName = reader[ConstantHelper.SQLColumn.SpecialistSearch.SpecialistName].ToString();
                    specialist.SpecialistType = reader[ConstantHelper.SQLColumn.SpecialistSearch.Specialist].ToString();
                    specialist.CompanyName = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyName].ToString();
                    specialist.CompanyAddressLine1 = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyAddressLine1].ToString();
                    specialist.CompanyAddressLine2 = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyAddressLine2].ToString();
                    specialist.CompanyAddressLine3 = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyAddressLine3].ToString();
                    specialist.CompanyPhoneNumber = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyPhoneNumber].ToString();
                    specialist.PostalCode = Convert.ToInt32(reader[ConstantHelper.SQLColumn.SpecialistSearch.PostalCode].ToString());
                    specialist.City = reader[ConstantHelper.SQLColumn.SpecialistSearch.City].ToString();
                    specialist.State = reader[ConstantHelper.SQLColumn.SpecialistSearch.State].ToString();

                    result.ResultTable.Add(specialist);

                    ////place data accordingly
                    //SpecialistNearby specialist = new SpecialistNearby();
                    //result.ResultTable.Add(specialist);
                    
                }
            }

            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.SpecialistSearch : " + err);
            }

            return result;
        }

        public AuthorizePractitionerModel AddAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            AuthorizePractitionerModel result = new AuthorizePractitionerModel();
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.AuthorizePractitioner
                                where pt.PatientId.Equals(authorizePractitioner.PatientId) && pt.PractitionerId.Equals(authorizePractitioner.PractitionerId)
                                select pt;

                    var check = query.Select(p => p.Id).FirstOrDefault();

                    if (check.Equals(Guid.Empty))
                    {
                        var authorize = new AuthorizePractitioner()
                        {
                            PractitionerId = authorizePractitioner.PractitionerId,
                            PatientId = authorizePractitioner.PatientId,
                            CreatedOn = DateTime.Now,
                        };

                        context.AuthorizePractitioner.Add(authorize);
                        context.SaveChanges();

                        result = authorizePractitioner;
                    }
                    else
                    {
                        result = authorizePractitioner;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.AddAuthorizePractitioner : " + err);
            }

            return result;
        }

        public AuthorizePractitionerModel GetAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            AuthorizePractitionerModel result = new AuthorizePractitionerModel();
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.AuthorizePractitioner
                                where pt.PatientId.Equals(authorizePractitioner.PatientId)
                                select pt;

                    var check = query.Select(p => p.Id).FirstOrDefault();

                    if (check.Equals(Guid.Empty))
                    {
                        var authorize = new AuthorizePractitioner()
                        {
                            PractitionerId = authorizePractitioner.PractitionerId,
                            PatientId = authorizePractitioner.PatientId,
                            CreatedOn = DateTime.Now,
                        };

                        context.AuthorizePractitioner.Add(authorize);
                        context.SaveChanges();

                        result = authorizePractitioner;
                    }
                    else
                    {
                        result = authorizePractitioner;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.AddAuthorizePractitioner : " + err);
            }

            return result;
        }

        public List<AuthorizedPractitionersTable> GetAuthorizePractitioners(Guid patientId)
        {
            List<AuthorizedPractitionersTable> result = new List<AuthorizedPractitionersTable>();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.GetAuthorizedPractitioners, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, patientId));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AuthorizedPractitionersTable authorizedPractitioner = new AuthorizedPractitionersTable();
                    authorizedPractitioner.PractitionerId = Guid.Parse(reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.PractitionerId].ToString());
                    authorizedPractitioner.PractitionerName = reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.PractitionerName].ToString();
                    authorizedPractitioner.Specialist = reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.Specialist].ToString();
                    authorizedPractitioner.CompanyName = reader[ConstantHelper.SQLColumn.SpecialistSearch.CompanyName].ToString();
                    authorizedPractitioner.PostalCode = Convert.ToInt32(reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.PostalCode].ToString());
                    authorizedPractitioner.City = reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.City].ToString();
                    authorizedPractitioner.State = reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.State].ToString();
                    authorizedPractitioner.CreatedOn = Convert.ToDateTime(reader[ConstantHelper.SQLColumn.AuthorizedPractitionersTable.CreatedOn].ToString()).ToString("dd-MM-yyyy");

                    result.Add(authorizedPractitioner);
                }
            }

            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.GetAuthorizedractitioners : " + err);
            }

            return result;
        }

        public AppointmentModel MakeAppointment(AppointmentModel appointmentModel)
        {
            AppointmentModel result = new AppointmentModel();
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Appointment
                                where pt.PractitionerId.Equals(appointmentModel.PractitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt;

                    var check = query.Where(p => p.AppointmentDateTime == appointmentModel.AppointmentDate).Select(p => p.Id).FirstOrDefault();

                    if (check.Equals(Guid.Empty))
                    {
                        var appointment = new Appointment()
                        {
                            PractitionerId = appointmentModel.PractitionerId,
                            PatientId = appointmentModel.PatientId,
                            AppointmentDateTime = appointmentModel.AppointmentDate.GetValueOrDefault() + appointmentModel.AppointmentTime,
                            Description = appointmentModel.Description,
                            Remarks = appointmentModel.Remarks,
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.Appointment.Add(appointment);
                        context.SaveChanges();

                        result.Status = ConstantHelper.AccountStatus.Pending;
                    }
                    else
                    {
                        result.Status = ConstantHelper.AccountStatus.Rejected;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.AddAuthorizePractitioner : " + err);
            }

            return result;
        }

        public string GetPatientEmail(Guid patientId)
        {
            string patientEmail = String.Empty;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Patient
                                where pt.Id.Equals(patientId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt.EmailAddress;

                    patientEmail = query.Select(p => p).FirstOrDefault().ToString();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.GetPatientEmail : " + err);
            }

            return patientEmail;
        }

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(Guid patientId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.RecordFile
                                where pt.PatientId.Equals(patientId)
                                select pt;

                    var records = query.OrderBy(x => x.CreatedOn).Select(x => x);

                    foreach (var record in records)
                    {
                        PractitionerRecordsDirectory row = new PractitionerRecordsDirectory();
                        row.RecordId = record.Id;
                        DateTime recordTime = Convert.ToDateTime(record.CreatedOn);
                        row.CreatedOn = recordTime.ToString("dd/MM/yyyy");
                        row.CreationTime = recordTime.ToString("hh:mm tt");

                        result.Add(row);
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.GetRecordsDirectory : " + err);
            }
            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(Guid patientId, int? year,int month)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.PatientSearchRecords, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, patientId));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.Year, year));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.Month, month));

                SqlDataReader records = cmd.ExecuteReader();
                bool check = records.HasRows;

                while (records.Read())
                {
                    PractitionerRecordsDirectory record = new PractitionerRecordsDirectory();
                    record.RecordId = Guid.Parse(records[ConstantHelper.SQLColumn.GetRecordsDirectory.Id].ToString());
                    DateTime recordTime = Convert.ToDateTime(records[ConstantHelper.SQLColumn.GetRecordsDirectory.CreatedOn].ToString());
                    record.CreatedOn = recordTime.ToString("dd/MM/yyyy");
                    record.CreationTime = recordTime.ToString("hh:mm tt");

                    result.Add(record);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.SearchRecords :" + err);
            }

            return result;
        }

        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            RecordFileSystem result = new RecordFileSystem();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.RecordFile
                                where pt.Id.Equals(record.Id) && pt.PatientId.Equals(record.PatientId)
                                select pt;

                    var medicalRecord = query.Select(p => p).FirstOrDefault();

                    //account found
                    if (medicalRecord != null)
                    {
                        result.FileContents = medicalRecord.FileContents;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.GetRecord : " + err);
            }

            return result;
        }

        public int ProfileEdit(PatientBaseViewModel profile)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var patientProfile = context.Patient.Where(p => p.Id.Equals(profile.AccId) && p.Status.Equals(ConstantHelper.AccountStatus.Active)).FirstOrDefault();

                    //profile found
                    if (patientProfile != null)
                    {
                        patientProfile.FirstName = profile.FirstName;
                        patientProfile.LastName = profile.LastName;
                        patientProfile.UserName = profile.UserName;
                        patientProfile.Gender = profile.Gender;
                        patientProfile.Religion = profile.Religion.ToString();
                        patientProfile.DateOfBirth = profile.DateOfBirth;
                        patientProfile.EmailAddress = profile.EmailAddress;
                        patientProfile.ContactNumber1 = profile.ContactNumber1;
                        patientProfile.ContactNumber2 = profile.ContactNumber2;
                        patientProfile.ContactNumber3 = profile.ContactNumber3;
                        patientProfile.HomeAddress1 = profile.HomeAddressLine1;
                        patientProfile.HomeAddress2 = profile.HomeAddressLine2;
                        patientProfile.HomeAddress3 = profile.HomeAddressLine3;
                        patientProfile.PostalCode = Convert.ToInt32(profile.PostalCode);
                        patientProfile.City = profile.City;
                        patientProfile.State = profile.State.ToString();

                        result = context.SaveChanges();
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientData.ProfileEdit : " + err);
            }

            return result;
        }

        public List<MedicineModel> ProductSearch(MedicineViewModel search)
        {
            List<MedicineModel> result = new List<MedicineModel>();
            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.ProductSearch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.SearchText, search.SearchText));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MedicineModel medicine = new MedicineModel();
                    medicine.ProductCode = reader[ConstantHelper.SQLColumn.ProductSearch.ProductCode].ToString();
                    medicine.ProductName = reader[ConstantHelper.SQLColumn.ProductSearch.ProductName].ToString();
                    medicine.TotalAmount = Convert.ToInt32(reader[ConstantHelper.SQLColumn.ProductSearch.TotalAmount].ToString());
                    medicine.CompanyId = Guid.Parse(reader[ConstantHelper.SQLColumn.ProductSearch.CompanyId].ToString());
                    medicine.CompanyName = reader[ConstantHelper.SQLColumn.ProductSearch.CompanyName].ToString();
                    medicine.CompanyPostalCode = reader[ConstantHelper.SQLColumn.ProductSearch.PostalCode].ToString();
                    medicine.CompanyState = reader[ConstantHelper.SQLColumn.ProductSearch.State].ToString();

                    result.Add(medicine);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PatientData.ProductSearch : " + err);
            }

            return result;
        }

        public CompanyViewModel ViewCompanyProfile(Guid companyId)
        {
            CompanyViewModel result = new CompanyViewModel();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Company
                                where pt.Id.Equals(companyId)
                                select pt;

                    var company = query.Select(p => p).FirstOrDefault();

                    if (query != null)
                    {
                        result.CompanyName = company.CompanyName;
                        result.CompanyPhoneNumber = company.CompanyPhoneNumber;
                        result.CompanyEmailAddress = company.CompanyEmailAddress;
                        result.CompanyAddressLine1 = company.CompanyAddressLine1;
                        result.CompanyAddressLine2 = company.CompanyAddressLine2;
                        result.CompanyAddressLine3 = company.CompanyAddressLine3;
                        result.PostalCode = company.PostalCode.ToString();
                        result.State = (State)Enum.Parse(typeof(State), company.State);
                        result.City = company.City;
                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("Patientdata.ViewCompanyProfile : " + err);
            }
            return result;
        }
    }
}
