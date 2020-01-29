﻿using FYP.Data;
using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business
{
    public class PatientBusiness
    {
        public PatientViewModel PatientRegister(PatientViewModel vm)
        {
            PatientViewModel result = new PatientViewModel();

            try
            {
                if(String.IsNullOrEmpty(vm.ContactNumber3))
                {
                    vm.ContactNumber3 = "";
                }

                var salt = HashingHelper.GenerateSalt();
                vm.Salt = salt;
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(vm.Password), vm.Salt);
                vm.Password = Convert.ToBase64String(hashedPassword);

                PatientData dataLayer = new PatientData();
                result = dataLayer.CreatePatient(vm);
            }
            catch (Exception err)
            {

            }

            return result;
        }

        public LoginInfo PatientLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();
            result = loginInfo;

            try
            {
                PatientData dataLayer = new PatientData();

                //hashing password
                var salt = dataLayer.GetSalt(loginInfo.EmailAddress);
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(loginInfo.Password), salt);
                loginInfo.Password = Convert.ToBase64String(hashedPassword);

                result = dataLayer.PatientLogin(loginInfo);
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
                PatientData dataLayer = new PatientData();
                result = dataLayer.PatientVerification(accId);
            }
            catch (Exception err)
            {

            }

            return result;
        }
    }
}
