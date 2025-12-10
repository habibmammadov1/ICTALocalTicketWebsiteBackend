using Business.Abstract;
using DTOs.Concrete;
using Microsoft.Extensions.Configuration;
using Novell.Directory.Ldap;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LdapConnection1 : ILdapConnection1
    {
        private readonly IConfiguration _config;

        public LdapConnection1(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string ldapHost = _config["LdapSettings:Server"];
            int ldapPort = int.Parse(_config["LdapSettings:Port"]);
            string domain = _config["LdapSettings:Domain"];
            string baseDn = _config["LdapSettings:BaseDn"];

            // Full login name
            string userDn = $"{username}";

            try
            {
                using var conn = new LdapConnection();

                await conn.ConnectAsync(ldapHost, ldapPort);
                await conn.BindAsync(userDn, password); // If fails → wrong password

                return true;
            }

            catch
            {
                return false; // Login failed
            }
        }


        public async Task<LdapEntry> GetUserInfoAsync(string username)
        {
            string server = _config["LdapSettings:Server"];
            int port = int.Parse(_config["LdapSettings:Port"]);
            string baseDn = _config["LdapSettings:BaseDn"];

            var conn = new LdapConnection();

            await conn.ConnectAsync(server, port);
            await conn.BindAsync(_config["LdapSettings:BindUserDn"], _config["LdapSettings:BindPassword"]);

            var result = await conn.SearchAsync(
                baseDn,
                LdapConnection.ScopeSub,
                $"(sAMAccountName={username})",
                null,
                false
            );

            var entry = await result.NextAsync();

            conn.Disconnect();

            return entry;
        }

        public async Task<List<LdapUserDto>> GetAllUsersAsync()
        {
            string server = _config["LdapSettings:Server"];
            int port = int.Parse(_config["LdapSettings:Port"]);
            string baseDn = _config["LdapSettings:BaseDn"];
            string bindUser = _config["LdapSettings:BindUserDn"];
            string bindPass = _config["LdapSettings:BindPassword"];

            var conn = new LdapConnection();
            var users = new List<LdapUserDto>();

            await conn.ConnectAsync(server, port);
            await conn.BindAsync(bindUser, bindPass);

            // Filter ONLY user accounts
            string filter = "(&(objectClass=user)(!(objectClass=computer)))";

            var constraints = new LdapSearchConstraints
            {
                ReferralFollowing = false
            };


            var result = await conn.SearchAsync(
                baseDn,
                LdapConnection.ScopeSub,
                filter,
                new[] { 
                    "sAMAccountName",
                    "userPrincipalName",
                    "displayName",
                    "description", //
                    "physicalDeliveryOfficeName", //
                    "telephoneNumber",
                    "department",
                    "title"
                },

                false,
                constraints
                
            );

            while (await result.HasMoreAsync())
            {
                var entry = await result.NextAsync();
                var attrs = entry.GetAttributeSet();

                foreach (LdapAttribute a in attrs)
                {
                    Console.WriteLine("ATTR => " + a.Name);
                }

                users.Add(new LdapUserDto
                {
                    Username = GetAttr1(attrs, "sAMAccountName"),
                    FirstName = "df",
                    LastName ="s",
                    DisplayName = GetAttr1(attrs, "displayName"),
                    Description = GetAttr1(attrs, "description"),
                    Office = GetAttr1(attrs, "physicalDeliveryOfficeName"),
                    Phone = GetAttr1(attrs, "telephoneNumber"),
                    Email = GetAttr1(attrs, "userPrincipalName"),

                    Department = GetAttr1(attrs, "department"),
                    Title = GetAttr1(attrs, "title")
                });
            }

            conn.Disconnect();
            return users;
        }

        private string GetAttr1(LdapAttributeSet attrs, params string[] names)
        {
            foreach (var name in names)
            {
                if (!attrs.ContainsKey(name))
                    continue;

                var attr = attrs.GetAttribute(name);
                if (attr == null)
                    continue;

                var val = attr.StringValue;
                if (!string.IsNullOrWhiteSpace(val))
                    return val;
            }

            return null;
        }
    }
}
