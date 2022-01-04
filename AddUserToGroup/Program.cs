using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUserToGroup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryEntry userGroup = null;
            string groupName = "Administrators";
            string[] userNames = { "Abbasi", "1100" };
            foreach (string userName in userNames)
                try
                {
                    string groupPath = String.Format(CultureInfo.CurrentUICulture, "WinNT://{0}/{1},group", Environment.MachineName, groupName);
                    userGroup = new DirectoryEntry(groupPath);

                    if ((null == userGroup) || (true == String.IsNullOrEmpty(userGroup.SchemaClassName)) || (0 != String.Compare(userGroup.SchemaClassName, "group", true, CultureInfo.CurrentUICulture)))
                    { }

                    string userPath = String.Format(CultureInfo.CurrentUICulture, "WinNT://{0},user", userName);
                    userGroup.Invoke("Add", new object[] { userPath });
                    userGroup.CommitChanges();


                }
                catch (Exception)
                {

                }
                finally
                {
                    if (null != userGroup) userGroup.Dispose();
                }
        }
    }
}
