using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Linq;
using System.Threading;
using static Helpers.MailSubjectEnum;

namespace Helpers
{
    public class MailRepository 
    {
        private readonly string mailServer, login, password;
        private readonly int port;
        private readonly bool ssl;

        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }

        public string GetUnreadMails(Subject subject)
        {
            string content="" ;
            int retryCounter = 0;
            int maxRetries = 300;
            string sub = "";
            switch(subject)
            {
                case Subject.ForgotPassword:
                    sub = "account password reset";
                    break;
                case Subject.Register:
                    sub = "RCS account activation";
                    break;
                case Subject.RegistrationAccepted:
                    sub = "Registration Request Acceptance";
                    break;
                case Subject.RegistrationRejected:
                    sub = "Registration Request Rejection";
                    break;
                case Subject.ApplicationApproval:
                    sub = "Application Approval";
                    break;

                default:
                    break;
            }
            using (var client = new ImapClient())
            {
                client.Connect(mailServer, port, ssl);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);
                bool found = false;
                var inbox = client.Inbox;
                while (retryCounter < maxRetries)
                {

                    try
                    {
                        inbox.Open(FolderAccess.ReadWrite);
                        var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen).And(SearchQuery.SubjectContains(sub)));
                        var message = inbox.GetMessage(results.UniqueIds.Max());
                        content = message.HtmlBody;
                        found = true;
                        inbox.AddFlags(results.UniqueIds, MessageFlags.Deleted, true);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Thread.Sleep(2000);
                    retryCounter += 1;
                    if (found == true)
                        break;
                }
                client.Disconnect(true);
            }

            return content;
        }

    }
}
