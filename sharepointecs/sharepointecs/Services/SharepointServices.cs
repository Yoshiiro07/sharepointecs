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
using Newtonsoft.Json.Linq;

namespace sharepointecs.Services
{
    public class SharepointServices : ISharepointServices
    {
        private readonly IConfiguration _configuration;

        public SharepointServices(IConfiguration configuration) { 
            _configuration = configuration;
        }

        public SPModel ExtractPage(string token, string requestPage)
        {
            try
            {
                SPModel spmodel = new SPModel();

                var spcredentials = Environment.GetEnvironmentVariable("SpCredentials").Split("|");
                var tenantName = spcredentials[1];
                var siteUrl = $"https://{tenantName}.sharepoint.com/";
                var subpage = requestPage.Remove(0, requestPage.LastIndexOf("/"));

                using (var context = new ClientContext(siteUrl))
                {
                    context.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.RequestHeaders["Authorization"] =
                            "Bearer " + token;
                    };

                    var listTitle = "Site Pages";
                    var list = context.Web.Lists.GetByTitle(listTitle);
                    var items = list.GetItems(CamlQuery.CreateAllItemsQuery());
                    context.Load(items);

                    context.ExecuteQuery();
                    foreach (var item in items)
                    {
                        string itemName = item["FileRef"].ToString();
                        if (itemName.Contains(subpage))
                        {
                            spmodel.Title = Convert.ToString(item["Title"]);
                            spmodel.GUID = Convert.ToString(item["GUID"]);
                            spmodel.FileLeafRef = Convert.ToString(item["FileRef"]);
                            spmodel.WikiField = Convert.ToString(item["WikiField"]);
                            spmodel.CanvasContent1 = Convert.ToString(item["CanvasContent1"]);
                            spmodel.LayoutWebpartsContent = Convert.ToString(item["LayoutWebpartsContent"]);
                            spmodel.Modified = Convert.ToString(item["Modified"]);
                            spmodel.Created = Convert.ToString(item["Created"]);
                            spmodel.UniqueId = Convert.ToString(item["UniqueId"]);
                        }
                    }
                }

                return spmodel;
            }

            catch(Exception ex)
            {
                return null;
            }
        }

        public string GetAccessToken()
        {
            try
            {
                var spcredentials = Environment.GetEnvironmentVariable("SpCredentials").Split("|");
                var tenantName = spcredentials[1];
                var clientId = spcredentials[2];
                var certName = spcredentials[3];

                List<X509Certificate2> lstcertificates = new List<X509Certificate2>();
                X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                store.Open(OpenFlags.OpenExistingOnly);

                foreach (X509Certificate2 foundcert in store.Certificates)
                {
                    if (foundcert.Issuer.Contains(certName))
                    {
                        lstcertificates.Add(foundcert);
                        break;
                    }
                }

                var certificate = lstcertificates.FirstOrDefault();
                var authority = $"https://login.microsoftonline.com/{tenantName}.onmicrosoft.com/";
                var azureApp = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithAuthority(authority)
                    .WithCertificate(certificate)
                    .Build();

                var scopes = new string[] { $"https://{tenantName}.sharepoint.com/.default" };
                var authResult = azureApp.AcquireTokenForClient(scopes).ExecuteAsync().Result;
                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                return "Não foi possível gerar token";
            }
        }
    }
}
