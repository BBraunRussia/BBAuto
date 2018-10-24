using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace BBAuto.Domain.Common
{
    public class Ldap : IDisposable
    {
        private const string SERVER_IP_ADDRESS = "LDAP://bbmag.bbraun.com";

        private DirectoryEntry _rootDE;
        private DirectorySearcher _searcher;

        public Ldap()
        {
            _rootDE = new DirectoryEntry(SERVER_IP_ADDRESS);
            _searcher = new DirectorySearcher(_rootDE);
        }

        public string GetEmail(string login)
        {
            if (login == string.Empty)
                return string.Empty;

            _searcher.Filter = string.Format("(&(objectClass=user)(samAccountName={0}))", login);//strLogonName);
            //var queryFormat = "(&(objectClass=user)(objectCategory=person)(|(SAMAccountName=*{0}*)(cn=*{0}*)(gn=*{0}*)(sn=*{0}*)(email=*{0}*)))";

            _searcher.SearchScope = SearchScope.Subtree;

            SearchResult result = _searcher.FindOne();

            return result != null && result.Properties["mail"].Count > 0 ? result.Properties["mail"][0].ToString() : string.Empty;
        }

        public void Dispose()
        {
            _rootDE.Close();
        }
    }
}
