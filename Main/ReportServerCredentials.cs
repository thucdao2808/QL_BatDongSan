using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;

namespace Main
{
    public class ReportServerCredentials : IReportServerCredentials
    {
        private string _username;
        private string _password;
        private string _domain;

        public ReportServerCredentials(string username, string password, string domain)
        {
            _username = username;
            _password = password;
            _domain = domain;
        }

        public WindowsIdentity ImpersonationUser => null;

        public ICredentials NetworkCredentials => new NetworkCredential(_username, _password, _domain);

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}