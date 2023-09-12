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

using Amazon.CertificateManager;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Security.Cryptography;
using System.IO.Pipelines;
using Amazon.CertificateManager.Model;
using Amazon.Runtime;
using Amazon;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace sharepointecs.Services
{
    public class SharepointServices : ISharepointServices
    {
        private readonly IConfiguration _configuration;

        private static readonly RegionEndpoint ACMRegion = RegionEndpoint.SAEast1;
        
        public SharepointServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken()
        {
            var _client = new Amazon.CertificateManager.AmazonCertificateManagerClient(ACMRegion);

            string pass = "teste";

            byte[] byteArray = Encoding.ASCII.GetBytes(pass);
            MemoryStream stream = new MemoryStream(byteArray);

            var request = new ExportCertificateRequest();
            request.CertificateArn = "";
            request.Passphrase = stream;

            var response = await _client.ExportCertificateAsync(request);

            X509Certificate2 certificate = new X509Certificate2(response.Certificate, response.PrivateKey);

            var teste = "";

            return teste;
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

                }

                return spmodel;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetTokenFromLocal()
        {
            try
            {
                //var spcredentials = Environment.GetEnvironmentVariable("SpCredentials").Split("|");
                var certificateName = _configuration.GetValue<string>("SharepointSettings:CertificateName");
                var clientID = _configuration.GetValue<string>("SharepointSettings:ClientID");
                var tenantID = _configuration.GetValue<string>("SharepointSettings:TenantID");
                var tenantName = _configuration.GetValue<string>("SharepointSettings:TenantName");

                //Path
                string certpath = @"C:\certs\cert.pfx";
                string certkey = @"C:\certs\cert.key";

                // Save the certificate and private key content to files
                string crt = System.IO.File.ReadAllText(@"C:\certs\cert.crt");
                string key = System.IO.File.ReadAllText(@"C:\certs\cert.key");

                var certENV = Environment.GetEnvironmentVariable("certificate");
                var keyENV = Environment.GetEnvironmentVariable("key");
                byte[] certbyte = ReadFile(certpath);
                byte[] keybyte = Convert.FromBase64String(keyENV);

                X509Certificate2 certificate = new X509Certificate2(certbyte);

                byte[] privateKeyBytes = Convert.FromBase64String(keyENV);
                var privateKey = RSA.Create();
                privateKey.ImportRSAPrivateKey(new ReadOnlySpan<byte>(privateKeyBytes), out _);
                var certWithPrivateKey = certificate.CopyWithPrivateKey(privateKey);


                //List<X509Certificate2> lstcertificates = new List<X509Certificate2>();
                //X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                //store.Open(OpenFlags.OpenExistingOnly);

                //foreach (X509Certificate2 foundcert in store.Certificates)
                //{
                //    if (foundcert.Issuer.Contains(certificateName))
                //    {
                //        lstcertificates.Add(foundcert);
                //        break;
                //    }
                //}

                //var certfromstore = lstcertificates.FirstOrDefault();
                var authority = $"https://login.microsoftonline.com/{tenantName}.onmicrosoft.com/";
                var azureApp = ConfidentialClientApplicationBuilder.Create(clientID)
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

        internal static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
    }
}