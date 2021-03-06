﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using DesafioQSTI.Helpers;
using DesafioQSTI.Data.Repositories.Interfaces;
using DesafioQSTI.Models;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using DesafioQSTI.Data;
using DesafioQSTI.Data.Repositories;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DesafioQSTI.Services
{
    public class LoginService : ServiceBase
    {
        public MySqlDbContext _service;

        public LoginService(MySqlDbContext context) : base(context)
        {
            _service = context;
        }

        public LoginService() : this(new MySqlDbContext(new DbContextOptions<MySqlDbContext>()))
        {
        }

        public bool SignInService()
        {
            //SecretMD5Key must be b49d6c03fe471ee720b5a4d56c5a9bf2
            //SecretKey = 2011 and password = 123
            

            //var appSetting = _service.Account.GetService<IConfiguration>().GetSection("SecretMD5Key").GetSection("SecretKey");
            //var passwordMd5 = GetPasswordMD5Hash(obj.password, appSetting.Value);
            //var accountExist = _service.Account.Select(account => new { account.account, account.password }).FirstOrDefault(account => account.account == obj.account && account.password == passwordMd5);

            //var g = _service.Block.Select(a => new { a.Account.account, a.Account.password, a.Account.account_id, a.ban_disc }).Where(s => s.password == passwordMd5); //Include(d => d.Account).FirstOrDefault(c => c.account_id == c.Account.account_id);
            
            //if (accountExist != null)
            //{
            //    return true;
            //}

            return false;
        }

        public object SignUpService()
        {
            

            /*
            //var accountExist = _service.Account.Select(account => new { account.account, account.email }).FirstOrDefault(account => account.account == obj.account || account.email == obj.email);
            //var emailExist = _service.Account.Select(account => new { account.email }).FirstOrDefault(account => account.email == obj.email);
            
            if (string.IsNullOrEmpty(accountExist?.account) && string.IsNullOrEmpty(accountExist?.email))
            {
                var newRecord = new Account();
                MapHelper.MapFrom(obj, newRecord);

                //This fields cannot be null in database and must be filled.
                newRecord.IP_user = "127.0.0.1";
                newRecord.block = 0;
                newRecord.password2 = !string.IsNullOrEmpty(obj.password) ? obj.password : "0";
                newRecord.phone = DateTime.Now.TimeOfDay.ToString();
                newRecord.last_login_server_idx = 1;
                newRecord.point = 1;
                newRecord.datePassword = DateTime.Now;

                try
                {
                    //_context.Account.Add(newRecord);
                    //_context.SaveChanges();
                    MapHelper.MapFrom(newRecord, obj);
                    obj.dateRegisterCreated = DateTime.Now;
                    obj._EventSuccess = true;
                    obj._EventMesssage = "Successfully created account!";
                    return obj;
                }
                catch (DbUpdateException ex)
                {
                    obj._EventSuccess = false;
                    return obj;
                }
            }

            if(!string.IsNullOrEmpty(accountExist.account))
                obj._EventMesssage = " Account name already exists!";
            if (!string.IsNullOrEmpty(accountExist.email))
                obj._EventMesssage += " Email already exists!";
            */
            var t = new SerializableAttribute();
            return t;
        }
        
        public string GetPasswordMD5Hash(string password, string secretKey)
        {
            var bytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey + password));
            StringBuilder builder = new StringBuilder();

            foreach (var item in bytes)
            {
                builder.Append(item.ToString("x2"));
            }
            
            return builder.ToString();
        }
    }
}
