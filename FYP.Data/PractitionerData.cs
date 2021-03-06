﻿using FYP.Entities;
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
                    result.CompanyId = Guid.Parse(practitioner[ConstantHelper.SQLColumn.Practitioner.CompanyId].ToString());
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
                                where pt.EmailAddress.Equals(loginInfo.EmailAddress)
                                select pt;

                    result.AccountNo = query.Select(p => p.Id).FirstOrDefault();

                    //account found
                    if (!result.AccountNo.Equals(Guid.Empty))
                    {
                        result.Salt = query.Select(p => p.Salt).FirstOrDefault();
                        result.Password = query.Select(p => p.Password).FirstOrDefault();
                        result.AccountStatus = query.Select(p => p.Status).FirstOrDefault();

                        return result;
                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.PractitionerLogin : " + err);
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

        public List<AppointmentModel> GetAppointmentsTable(Guid practitionerId)
        {
            List<AppointmentModel> result = new List<AppointmentModel>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var today = DateTime.Today;
                    var query = from pt in context.Appointment
                                where pt.PractitionerId.Equals(practitionerId)
                                select pt;
                    var query2 = query.OrderBy(x => x.Status);
                    var pendingQuery = query2.Where(x => x.Status.Equals(ConstantHelper.AccountStatus.Pending)).OrderBy(x => x.CreatedOn).ThenBy(x=> x.AppointmentDateTime).Select(x => x);
                    var acceptedQuery = query2.Where(x => x.Status.Equals(ConstantHelper.AccountStatus.Accepted) && x.AppointmentDateTime.Day == today.Day && x.AppointmentDateTime.Month == today.Month && x.AppointmentDateTime.Year == today.Year).OrderBy(x => x.AppointmentDateTime).Select(x => x);
                    var rejectedQuery = query2.Where(x => x.Status.Equals(ConstantHelper.AccountStatus.Rejected) && x.AppointmentDateTime.Day == today.Day && x.AppointmentDateTime.Month == today.Month && x.AppointmentDateTime.Year == today.Year).OrderBy(x => x.ModifiedOn).Select(x => x);
                    var absentQuery = query2.Where(x => x.Status.Equals(ConstantHelper.AccountStatus.Absent) && x.AppointmentDateTime.Day == today.Day && x.AppointmentDateTime.Month == today.Month && x.AppointmentDateTime.Year == today.Year).OrderBy(x => x.AppointmentDateTime).Select(x => x);

                    foreach (var item in pendingQuery)
                    {
                        AppointmentModel appointment = new AppointmentModel();
                        appointment.AppointmentId = Guid.Parse(item.Id.ToString());
                        appointment.AppointmentDateString = item.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        appointment.AppointmentTimeString = item.AppointmentDateTime.TimeOfDay.ToString();
                        appointment.CreatedOnString = item.CreatedOn.Date.ToString("dd-MM-yyyy");
                        appointment.PatientId = item.PatientId;
                        appointment.Status = item.Status;

                        result.Add(appointment);
                    }

                    foreach (var item in acceptedQuery)
                    {
                        AppointmentModel appointment = new AppointmentModel();
                        appointment.AppointmentId = Guid.Parse(item.Id.ToString());
                        appointment.AppointmentDateString = item.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        appointment.AppointmentTimeString = item.AppointmentDateTime.TimeOfDay.ToString();
                        appointment.CreatedOnString = item.CreatedOn.Date.ToString("dd-MM-yyyy");
                        appointment.PatientId = item.PatientId;
                        appointment.Status = item.Status;

                        result.Add(appointment);
                    }
         
                    foreach (var item in rejectedQuery)
                    {
                        AppointmentModel appointment = new AppointmentModel();
                        appointment.AppointmentId = Guid.Parse(item.Id.ToString());
                        appointment.AppointmentDateString = item.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        appointment.AppointmentTimeString = item.AppointmentDateTime.TimeOfDay.ToString();
                        appointment.CreatedOnString = item.CreatedOn.Date.ToString("dd-MM-yyyy");
                        appointment.PatientId = item.PatientId;
                        appointment.Status = item.Status;
                        appointment.RejectReasons = item.RejectedReasons;

                        result.Add(appointment);
                    }

                    foreach (var item in absentQuery)
                    {
                        AppointmentModel appointment = new AppointmentModel();
                        appointment.AppointmentId = Guid.Parse(item.Id.ToString());
                        appointment.AppointmentDateString = item.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        appointment.AppointmentTimeString = item.AppointmentDateTime.TimeOfDay.ToString();
                        appointment.CreatedOnString = item.CreatedOn.Date.ToString("dd-MM-yyyy");
                        appointment.PatientId = item.PatientId;
                        appointment.Status = item.Status;

                        result.Add(appointment);
                    }

                    foreach (var item in result)
                    {
                        var id = item.PatientId;
                        var patientQuery = from pt in context.Patient
                                           where pt.Id.Equals(id)
                                           select pt;
                        var patient = patientQuery.Select(x => x).FirstOrDefault();
                        item.FirstName = patient.FirstName;

                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetAppointmentsTable : " + err);
            }

            return result;
        }

        public AppointmentModel AppointmentAccepted(AppointmentModel appointmentModel)
        {
            AppointmentModel returnAppointment = new AppointmentModel();
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var appointment = context.Appointment.Where(p => p.Id == appointmentModel.AppointmentId).FirstOrDefault();
                    appointment.Status = ConstantHelper.AccountStatus.Accepted;
                    result = context.SaveChanges();

                    if(result != 0)
                    {
                        returnAppointment.PatientId = appointment.PatientId;
                        returnAppointment.AppointmentDateString = appointment.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        returnAppointment.AppointmentTimeString = appointment.AppointmentDateTime.TimeOfDay.ToString();
                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.AppointmentAccepted : " + err);
            }

            return returnAppointment;
        }

        public AppointmentModel AppointmentRejected(AppointmentModel appointmentModel)
        {
            AppointmentModel returnAppointment = new AppointmentModel();
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var appointment = context.Appointment.Where(p => p.Id == appointmentModel.AppointmentId).FirstOrDefault();
                    appointment.RejectedReasons = appointmentModel.RejectReasons;
                    appointment.Status = ConstantHelper.AccountStatus.Rejected;
                    result = context.SaveChanges();

                    if (result != 0)
                    {
                        returnAppointment.PatientId = appointment.PatientId;
                        returnAppointment.PractitionerId = appointment.PractitionerId;
                        returnAppointment.AppointmentDateString = appointment.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        returnAppointment.AppointmentTimeString = appointment.AppointmentDateTime.TimeOfDay.ToString();
                        returnAppointment.RejectReasons = appointment.RejectedReasons;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.AppointmentRejected : " + err);
            }

            return returnAppointment;
        }

        public AppointmentModel AppointmentAbsent(AppointmentModel appointmentModel)
        {
            AppointmentModel returnAppointment = new AppointmentModel();
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var appointment = context.Appointment.Where(p => p.Id == appointmentModel.AppointmentId).FirstOrDefault();
                    appointment.Status = ConstantHelper.AccountStatus.Absent;
                    result = context.SaveChanges();

                    if (result != 0)
                    {
                        returnAppointment.PatientId = appointment.PatientId;
                        returnAppointment.AppointmentDateString = appointment.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        returnAppointment.AppointmentTimeString = appointment.AppointmentDateTime.TimeOfDay.ToString();
                        returnAppointment.RejectReasons = "Patient is absent.";
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.AppointmentAbsent : " + err);
            }

            return returnAppointment;
        }

        public AppointmentModel AppointmentPending(AppointmentModel appointmentModel)
        {
            AppointmentModel returnAppointment = new AppointmentModel();
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var appointment = context.Appointment.Where(p => p.Id == appointmentModel.AppointmentId).FirstOrDefault();
                    appointment.Status = ConstantHelper.AccountStatus.Pending;
                    appointment.RejectedReasons = "";
                    result = context.SaveChanges();

                    if (result != 0)
                    {
                        returnAppointment.PatientId = appointment.PatientId;
                        returnAppointment.AppointmentDateString = appointment.AppointmentDateTime.Date.ToString("dd-MM-yyyy");
                        returnAppointment.AppointmentTimeString = appointment.AppointmentDateTime.TimeOfDay.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.AppointmentPending : " + err);
            }

            return returnAppointment;
        }

        public PatientRecordModel GetMedicinesList(Guid companyId)
        {
            PatientRecordModel result = new PatientRecordModel();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Medicine
                                where pt.CompanyId.Equals(companyId) && pt.TotalAmount > 0
                                select pt;

                    var key = query.Select(p => p.Id.ToString()).ToList();
                    var value = query.Select(p => p.ProductName).ToList();

                    List<string> medicineIdList = key;
                    List<string> medicineNameList = value;

                    result.MedicineDropDown = medicineIdList.Zip(medicineNameList, (k,v) => new { k,v }).ToDictionary(x => x.k, x => x.v);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetMedicinesList : " + err);
            }

            return result;
        }

        public Guid CreatePatientRecord(RecordFileSystem fileRecord)
        {
            Guid result = new Guid();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.RecordFile
                                where pt.PatientId.Equals(fileRecord.PatientId) && pt.PractitionerId.Equals(fileRecord.PractitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Pending)
                                select pt;

                    var record = query.Select(p => p.Id).FirstOrDefault();

                    if(record.Equals(Guid.Empty))
                    {
                        var newRecord = new PatientRecord()
                        {
                            ContentType = fileRecord.ContentType,
                            FileContents = fileRecord.FileContents,
                            FileDownloadname = fileRecord.FileDownloadname,
                            PatientId = fileRecord.PatientId,
                            PractitionerId = fileRecord.PractitionerId,
                            Status = ConstantHelper.AccountStatus.Pending,
                        };

                        context.RecordFile.Add(newRecord);
                        context.SaveChanges();
                    }

                    result = query.Select(p => p.Id).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CreatePatientRecord : " + err);
            }

            return result;
        }

        public int StoreRecordToDB(RecordFileSystem fileRecord)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var record = context.RecordFile.Where(p => p.Id.Equals(fileRecord.Id)).FirstOrDefault();
                    record.FileContents = fileRecord.FileContents;
                    record.FileDownloadname = fileRecord.FileDownloadname;
                    record.Status = ConstantHelper.AccountStatus.Active;
                    result = context.SaveChanges();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.StoreRecordToDB : " + err);
            }

            return result;
        }

        public void StockUpdate(List<StockUpdate> stock, PatientRecordModel medicineGiven)
        {
            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Medicine
                                where pt.CompanyId.Equals(medicineGiven.CompanyId)
                                //join sc in stock on pt.Id equals sc.MedicineId
                                select pt;

                    //var test = query.Where();
                    //var medicines = query.Where(x => stock.Any(y =>y.MedicineId.Equals(x.Id)));

                    foreach(var medicine in stock)
                    {
                        var medicines = query.Where(x => x.Id.Equals(medicine.MedicineId)).FirstOrDefault();
                        medicines.TotalAmount = medicines.TotalAmount - medicine.Quantity;
                        //var updateValue = stock.Where(x => x.MedicineId.Equals(medicine.Id)).Select(y => y.Quantity).FirstOrDefault();
                        //medicine.TotalAmount = medicine.TotalAmount - updateValue;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.StockUpdate : " + err);
            }
        }

        public int CloseAppointment(Guid appointmentId)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var today = DateTime.Today;
                    var appointment = context.Appointment.Where(x => x.AppointmentDateTime.Day == today.Day && x.AppointmentDateTime.Month == today.Month && x.AppointmentDateTime.Year == today.Year && x.Id.Equals(appointmentId)).Select(x => x).FirstOrDefault();
                    appointment.Status = ConstantHelper.AccountStatus.Closed;
                    context.SaveChanges();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CloseAppointment : " + err);
            }

            return result;
        }

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(Guid practitionerId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.GetRecordsDirectory, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, practitionerId));
                SqlDataReader records = cmd.ExecuteReader();

                while (records.Read())
                {
                    PractitionerRecordsDirectory record = new PractitionerRecordsDirectory();
                    record.RecordId = Guid.Parse(records[ConstantHelper.SQLColumn.GetRecordsDirectory.Id].ToString());
                    record.PatientFirstName = records[ConstantHelper.SQLColumn.GetRecordsDirectory.FirstName].ToString();
                    record.PatientLastName = records[ConstantHelper.SQLColumn.GetRecordsDirectory.LastName].ToString();
                    DateTime recordTime = Convert.ToDateTime(records[ConstantHelper.SQLColumn.GetRecordsDirectory.CreatedOn].ToString());
                    record.CreatedOn = recordTime.ToString("dd/MM/yyyy");
                    record.CreationTime = recordTime.ToString("hh:mm tt");

                    result.Add(record);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetRecordsDirectory : " + err);
            }
            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            try
            {
                int month = (int)vm.Month;
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.SearchRecords, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, vm.AccId));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.RecordId, vm.RecordId));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.Year, vm.Year));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.Month, month));

                SqlDataReader records = cmd.ExecuteReader();
                bool check = records.HasRows;

                while (records.Read())
                {
                    PractitionerRecordsDirectory record = new PractitionerRecordsDirectory();
                    record.RecordId = Guid.Parse(records[ConstantHelper.SQLColumn.GetRecordsDirectory.Id].ToString());
                    record.PatientFirstName = records[ConstantHelper.SQLColumn.GetRecordsDirectory.FirstName].ToString();
                    record.PatientLastName = records[ConstantHelper.SQLColumn.GetRecordsDirectory.LastName].ToString();
                    DateTime recordTime = Convert.ToDateTime(records[ConstantHelper.SQLColumn.GetRecordsDirectory.CreatedOn].ToString());
                    record.CreatedOn = recordTime.ToString("dd/MM/yyyy");
                    record.CreationTime = recordTime.ToString("hh:mm tt");

                    result.Add(record);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.Searchrecords :" + err);
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
                                where pt.Id.Equals(record.Id) && pt.PractitionerId.Equals(record.PractitionerId)
                                select pt;

                    var medicalRecord = query.Select(p => p).FirstOrDefault();

                    //account found
                    if(medicalRecord != null)
                    {
                        result.FileContents = medicalRecord.FileContents;
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetRecord : " + err);
            }

            return result;
        }

        public int ProfileEdit(PractitionerBaseViewModel profile)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var practitioenrProfile = context.Practitioner.Where(p => p.Id.Equals(profile.AccId) && p.Status.Equals(ConstantHelper.AccountStatus.Active)).FirstOrDefault();

                    //profile found
                    if (practitioenrProfile != null)
                    {
                        practitioenrProfile.FirstName = profile.FirstName;
                        practitioenrProfile.LastName = profile.LastName;
                        practitioenrProfile.UserName = profile.UserName;
                        practitioenrProfile.Gender = profile.Gender;
                        practitioenrProfile.Religion = profile.Religion.ToString();
                        practitioenrProfile.DateOfBirth = profile.DateOfBirth;
                        practitioenrProfile.EmailAddress = profile.EmailAddress;
                        practitioenrProfile.OfficePhoneNumber = profile.OfficePhoneNumber;
                        practitioenrProfile.Role = profile.Role;
                        practitioenrProfile.Specialist = profile.Specialist.ToString();
                        practitioenrProfile.Qualification = profile.Qualification;

                        result = context.SaveChanges();
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.ProfileEdit : " + err);
            }

            return result;
        }

        public int CreateProduct(MedicineModel newMedicine)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.Id.Equals(newMedicine.PractitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt;

                    var companyId = query.Select(p => p.Company).FirstOrDefault();

                    var medicine = new Medicine()
                    {
                        ProductCode = newMedicine.ProductCode,
                        ProductName = newMedicine.ProductName,
                        ProductionDate = newMedicine.ProductionDate,
                        ExpiryDate = newMedicine.ExpiryDate,
                        TotalAmount = newMedicine.TotalAmount,
                        Threshold = newMedicine.Threshold,
                        CreatedBy = newMedicine.PractitionerId,
                        CompanyId = companyId,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };

                        context.Medicine.Add(medicine);
                    result = context.SaveChanges();
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.CreateProduct : " + err);
            }
            return result;
        }

        public List<MedicineModel> GetProducts(Guid practitionerId)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.Practitioner
                                where pt.Id.Equals(practitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                select pt;

                    var companyId = query.Select(p => p.Company).FirstOrDefault();

                    var query2 = from pt in context.Medicine
                                 where pt.CompanyId.Equals(companyId)
                                 select pt;

                    if(query2 != null)
                    {
                        query2 = query2.OrderBy(p => p.ProductCode).Select(p => p);

                        foreach (var item in query2)
                        {
                            MedicineModel medicine = new MedicineModel();
                            medicine.MedicineId = item.Id;
                            medicine.ProductCode = item.ProductCode;
                            medicine.ProductName = item.ProductName;
                            medicine.ExpiryDateString = item.ExpiryDate.ToString("dd/MM/yyyy");
                            medicine.TotalAmount = item.TotalAmount;
                            medicine.Threshold = item.Threshold;

                            result.Add(medicine);
                        }
                    }

                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetProducts : " + err);
            }
            return result;
        }

        public List<MedicineModel> SearchProduct(MedicineViewModel vm)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            try
            {
                SqlConnection conn = new SqlConnection(ConstantHelper.DBAppSettings.FYP);
                conn.Open();
                SqlCommand cmd = new SqlCommand(ConstantHelper.StoredProcedure.PractitionerSearchProduct, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.AccId, vm.PractitionerId));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.SearchText, vm.SearchText));
                cmd.Parameters.Add(new SqlParameter(ConstantHelper.StoredProcedure.Parameter.ProductCode, vm.ProductCode));
                SqlDataReader medicines = cmd.ExecuteReader();

                while (medicines.Read())
                {
                    MedicineModel medicine = new MedicineModel();
                    medicine.MedicineId = Guid.Parse(medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.Id].ToString());
                    medicine.ProductCode = medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.ProductCode].ToString();
                    medicine.ProductName = medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.ProductName].ToString();
                    medicine.ExpiryDateString = Convert.ToDateTime(medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.ExpiryDate]).ToString("dd/MM/yyyy");
                    medicine.TotalAmount = Convert.ToInt32(medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.TotalAmount]);
                    medicine.Threshold = Convert.ToInt32(medicines[ConstantHelper.SQLColumn.PractitionerSearchProduct.Threshold]);

                    result.Add(medicine);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetProducts : " + err);
            }
            return result;
        }

        public MedicineModel GetProduct(MedicineModel input)
        {
            MedicineModel result = new MedicineModel();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query1 = from pt in context.Practitioner
                                 where pt.Id.Equals(input.PractitionerId) && pt.Status.Equals(ConstantHelper.AccountStatus.Active)
                                 select pt;

                    var companyId = query1.Select(p => p.Company).FirstOrDefault();

                    var query2 = from pt in context.Medicine
                                 where pt.Id.Equals(input.MedicineId) && pt.CompanyId.Equals(companyId)
                                 select pt;

                    var medicine = query2.Select(p => p).FirstOrDefault();

                    if (query2 != null)
                    {
                        result.MedicineId = medicine.Id;
                        result.ProductCode = medicine.ProductCode;
                        result.ProductName = medicine.ProductName;
                        result.ExpiryDateString = medicine.ExpiryDate.ToString("yyyy-MM-dd");
                        result.ProductionDateString = medicine.ProductionDate.ToString("yyyy-MM-dd");
                        result.TotalAmount = medicine.TotalAmount;
                        result.Threshold = medicine.Threshold;
                        result.PractitionerId = input.PractitionerId;
                    }

                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetProduct : " + err);
            }

            return result;
        }

        public int UpdateProduct(MedicineModel input)
        {
            int result = 0;

            try
            {
                using (var context = new ApplicationContext())
                {
                    var companyId = context.Practitioner.Where(p => p.Id.Equals(input.PractitionerId) && p.Status.Equals(ConstantHelper.AccountStatus.Active)).Select(p => p.Company).FirstOrDefault();
                    var existingProduct = context.Medicine.Where(p => p.Id.Equals(input.MedicineId) && p.CompanyId.Equals(companyId)).FirstOrDefault();

                    //profile found
                    if (existingProduct != null)
                    {
                        existingProduct.ProductCode = input.ProductCode;
                        existingProduct.ProductName = input.ProductName;
                        existingProduct.ProductionDate = Convert.ToDateTime(input.ProductionDateString);
                        existingProduct.ExpiryDate = Convert.ToDateTime(input.ExpiryDateString);
                        existingProduct.TotalAmount = input.TotalAmount;
                        existingProduct.Threshold = input.Threshold;
                        existingProduct.ModifiedOn = DateTime.Now;

                        result = context.SaveChanges();
                    }
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.UpdateProduct : " + err);
            }

            return result;
        }

        public List<PatientsDirectory> GetPatientsDirectory(Guid practitionerId)
        {
            List<PatientsDirectory> result = new List<PatientsDirectory>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.AuthorizePractitioner
                                where pt.PractitionerId.Equals(practitionerId)
                                join pa in context.Patient on pt.PatientId equals pa.Id
                                orderby pa.FirstName
                                select new
                                {
                                    pa.Id,
                                    pa.FirstName,
                                    pa.LastName,
                                    pa.Gender,
                                    pa.ContactNumber1
                                };

                    foreach (var item in query)
                    {
                        PatientsDirectory patient = new PatientsDirectory();
                        patient.PatientId = item.Id;
                        patient.FirstName = item.FirstName;
                        patient.LastName = item.LastName;
                        patient.Gender = item.Gender;
                        patient.ContactNumber = item.ContactNumber1;
                        result.Add(patient);
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.GetPatientsDirectory : " + err);
            }

            return result;
        }

        public List<PatientsDirectory> SearchPatients(PatientsDirectorySearch search)
        {
            List<PatientsDirectory> result = new List<PatientsDirectory>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    var query = from pt in context.AuthorizePractitioner
                                where pt.PractitionerId.Equals(search.PractitionerId)
                                join pa in context.Patient on pt.PatientId equals pa.Id
                                select new
                                {
                                    pa.Id,
                                    pa.FirstName,
                                    pa.LastName,
                                    pa.Gender,
                                    pa.ContactNumber1
                                };

                    var filter = query.Where(p => p.FirstName.Contains(search.FirstName) || p.LastName.Contains(search.LastName)).OrderBy(p => p.LastName);

                    foreach (var item in filter)
                    {
                        PatientsDirectory patient = new PatientsDirectory();
                        patient.PatientId = item.Id;
                        patient.FirstName = item.FirstName;
                        patient.LastName = item.LastName;
                        patient.Gender = item.Gender;
                        patient.ContactNumber = item.ContactNumber1;
                        result.Add(patient);
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.SearchPatients : " + err);
            }

            return result;
        }

        public List<PractitionerRecordsDirectory> PatientPractitionerRecords(Guid practitionerId, Guid patientId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            try
            {
                using (var context = new ApplicationContext())
                {
                    byte[] emptyFile = new byte[1];
                    var query = from pt in context.RecordFile
                                where pt.PractitionerId.Equals(practitionerId) && pt.PatientId.Equals(patientId) && !pt.FileContents.Equals(emptyFile)
                                select pt;

                    var records = query.OrderByDescending(p => p.CreatedOn);

                    foreach (var item in records)
                    {
                        PractitionerRecordsDirectory record = new PractitionerRecordsDirectory();
                        record.RecordId = item.Id;
                        DateTime recordTime = item.CreatedOn;
                        record.CreatedOn = recordTime.ToString("dd/MM/yyyy");
                        record.CreationTime = recordTime.ToString("hh:mm tt");

                        result.Add(record);
                    }
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.PatientPractitionerRecords : " + err);
            }

            return result;
        }
    }
}
