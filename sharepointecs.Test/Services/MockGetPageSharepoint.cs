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
using sharepointecs.Services;

namespace sharepointecs.Test.Services
{
    public class GetPageSharepoint : IGetPageSharepoint
    {
        private readonly IConfiguration _configuration;

        public GetPageSharepoint(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        async Task<string[]> IGetPageSharepoint.MakeExtract(string requestPage)
        {
            //var teste = _configuration.GetValue<string>("SharepointSettings:SitePage");
            var spmodel = await MakeExtractSharepointUserPass(requestPage);
            return spmodel;
        }

        public async static Task<string[]> MakeExtractSharepointUserPass(string requestPage)
        {
            string[] spmodel = new string[20];
            try
            {
                Uri site = new Uri("https://3scyh4.sharepoint.com/");
                string user = "guilherme.oliveira@3scyh4.onmicrosoft.com";
                SecureString ss = new NetworkCredential("", "KnighTM@re07Keny").SecurePassword;

                using (var authenticationManager = new sharepointecs.Security.AuthenticationManager())
                using (var context = authenticationManager.GetContext(site, user, ss))
                {
                    var listTitle = "Site Pages";
                    var list = context.Web.Lists.GetByTitle(listTitle);
                    var items = list.GetItems(CamlQuery.CreateAllItemsQuery());
                    context.Load(items);

                    context.ExecuteQuery();
                    foreach (var item in items)
                    {
                        string itemname = item["FileRef"].ToString();
                        if (itemname.Contains(requestPage))
                        {
                            spmodel[0] = "";
                            spmodel[1] = item["FileRef"].ToString();
                            spmodel[2] = item["GUID"].ToString();
                            spmodel[3] = item["UniqueId"].ToString();
                            spmodel[4] = item["WikiField"].ToString();
                            spmodel[5] = item["Created"].ToString();
                            spmodel[6] = item["LayoutWebpartsContent"].ToString();
                            spmodel[7] = item["CanvasContent1"].ToString();
                            spmodel[8] = item["ContentType"].ToString();
                        }
                    }
                    return spmodel;
                }
            }
            catch (Exception ex)
            {
                spmodel[0] = "Não foi possível buscar informação: " + ex.Message;
                return spmodel;
            }
        }

        private async static Task MakeExtractWithCertificate()
        {
            var tenantName = "";
            var token = await GetAccessTokenWithCertificate();
            var siteUrl = $"https://{tenantName}.sharepoint.com/sites/demo";

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
        }

        public async static Task<string> GetAccessTokenWithCertificate()
        {
            var tenantName = "";
            var clientId = "";
            var certPath = "";

            var certFileName = certPath;  //Cert path
            var certPassword = "";
            var certificate = new X509Certificate2(certFileName, certPassword,
                    X509KeyStorageFlags.MachineKeySet);

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
