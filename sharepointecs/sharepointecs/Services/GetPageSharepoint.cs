using sharepointecs.Models;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace sharepointecs.Services
{
    public class GetPageSharepoint : IGetPageSharepoint
    {
        private readonly IConfiguration _configuration;

        public GetPageSharepoint(IConfiguration configuration) { 
            _configuration = configuration;
        }

        async Task<SPModel> IGetPageSharepoint.MakeExtract(string requestPage)
        {
            var spmodel = await MakeExtractWithCertificate();
            return spmodel;
        }

        private async Task<SPModel> MakeExtractWithCertificate()
        {
            SPModel spmodel = new SPModel();

            var tenantName = _configuration.GetValue<string>("SharepointSettings:TenantName");
            var token = await GetAccessTokenWithCertificate();
            var siteUrl = $"https://{tenantName}.sharepoint.com/";

            using (var context = new ClientContext(siteUrl))
            {
                context.ExecutingWebRequest += (s, e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] =
                        "Bearer " + token;
                };

                var web = context.Web;
                context.Load(web);
                context.ExecuteQuery();
                Console.WriteLine(web.Title);
            }

            return spmodel;
        }

        public async Task<string> GetAccessTokenWithCertificate()
        {
            var tenantName = _configuration.GetValue<string>("SharepointSettings:TenantName");
            var clientId = _configuration.GetValue<string>("SharepointSettings:ClientID");
            var certName = _configuration.GetValue<string>("SharepointSettings:CertificateName");
            
            List<X509Certificate2> lstcertificates = new List<X509Certificate2>();
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly);
            
            foreach(X509Certificate2 foundcert in store.Certificates)
            {
                    if (foundcert.Issuer.Contains(certName))
                    {
                    lstcertificates.Add(foundcert);
                    }
            }

            var certificate = lstcertificates.FirstOrDefault();
            var authority = $"https://login.microsoftonline.com/{tenantName}.onmicrosoft.com/";
            var azureApp = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithAuthority(authority)
                .WithCertificate(certificate)
                .Build();

            var scopes = new string[] { $"https://{tenantName}.sharepoint.com/.default" };
            var authResult = await azureApp.AcquireTokenForClient(scopes).ExecuteAsync();
            return authResult.AccessToken; 
        }  
    }
}
