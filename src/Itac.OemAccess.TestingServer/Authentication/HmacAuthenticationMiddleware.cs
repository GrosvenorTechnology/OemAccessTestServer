using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Itac.OemAccess.TestingServer.BuisnessLogic;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Unity;

namespace Itac.OemAccess.TestingServer.Authentication
{
    public class HmacAuthenticationOptions : AuthenticationOptions
    {
        public Devices Devices { get; set; }
        public NetworkNotificationManager NotificationManager { get; set; }
        public HmacAuthenticationOptions(IUnityContainer contianer) : base("Hmac")
        {
            Devices = contianer.Resolve<Devices>();
            NotificationManager = contianer.Resolve<NetworkNotificationManager>();
        }
    }
    class HmacAuthenticationMiddleware : AuthenticationMiddleware<HmacAuthenticationOptions>
    {
        public HmacAuthenticationMiddleware(OwinMiddleware next, HmacAuthenticationOptions options) : base(next, options)
        {
        }

        protected override AuthenticationHandler<HmacAuthenticationOptions> CreateHandler()
        {
            return new HmacAuthenticationHandler();
        }
    }

    internal class HmacAuthenticationHandler : AuthenticationHandler<HmacAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            try
            {
                var uri = Context.Request.Uri.AbsoluteUri;
                if (!uri.Contains("grosvenor-oem"))
                    return Task.FromResult<AuthenticationTicket>(new AuthenticationTicket(new ClaimsIdentity(),
                        new AuthenticationProperties()));

                var headers = Context.Environment["owin.RequestHeaders"] as IDictionary<string, string[]>;
                var authHeader = headers?.FirstOrDefault(h => h.Key.Equals("Authorization"));

                var ms = new MemoryStream();
                Context.Request.Body.CopyTo(ms);
                ms.Position = 0;
                Context.Request.Body = ms;
                var contentLength = ms.Length;

                var contentType = Context.Request.ContentType ?? "no-content";
                var verb = Context.Request.Method;

                if (authHeader?.Value != null && authHeader.Value.Value.Length > 0)
                {
                    if (authHeader.Value.Value.First().Split(' ')[0].Equals("amx"))
                    {
                        var amxHeaderValue = authHeader.Value.Value.First().Split(' ')[1];
                        //Hmac info -> [0] Device Name | [1] Signature | [2] nonce | [3] Guid
                        var hmacInfo = amxHeaderValue.Split(':');

                        //Submit Network Tracking Event
                        Options.NotificationManager.SubmitNetworkEvent(Context.Request.Uri.AbsoluteUri,hmacInfo[0]);

                        string sharedKey;
                        try
                        {
                            sharedKey = Options.Devices[hmacInfo[0]].SharedKey;
                        }
                        catch (Exception)
                        {
                            Log.Error($"AUTHORIZATION FAILURE Device Not Found {hmacInfo[0]} ");
                            return Task.FromResult<AuthenticationTicket>(null);
                        }
                        if (hmacInfo[1] == GetSignature(sharedKey, verb, uri, contentLength, contentType,
                                hmacInfo[3], hmacInfo[2]))
                        {
                            return Task.FromResult(GetAuthenticationTicket(hmacInfo[0]));
                        }
                        Log.Error($"AUTHORIZATION FAILURE :: Signatures do not match {hmacInfo[1]} != {GetSignature(sharedKey, verb, uri, contentLength, contentType, hmacInfo[3], hmacInfo[2])}");
                    }
                    else
                    {
                        Log.Error($"AUTHORIZATION FAILURE :: Header does not start with 'amx'");
                    }
                }
                else
                {
                    Log.Error($"AUTHORIZATION FAILURE :: Authorization header leangth is 0");
                }
                
   
                return Task.FromResult<AuthenticationTicket>(null);

            }
            catch (Exception exception)
            {
                Log.Error($"AUTHORIZATION FAILURE :: {exception}");
                return Task.FromResult<AuthenticationTicket>(null);
            }

        }

        private string GetSignature(string secret, string verb, string uri, long contentLength, string contentType, string timestamp, string nonce)
        {
            var stringToSign = $"{verb}{contentLength}{contentType}{nonce}{timestamp}{uri}";
            var key = Convert.FromBase64String(secret);

            var encoder = new UTF8Encoding();
            var hasher = new HMACSHA256(key);
            var signature = hasher.ComputeHash(encoder.GetBytes(stringToSign));

            return Convert.ToBase64String(signature);
        }
        private AuthenticationTicket GetAuthenticationTicket(string sender = null)
        {
            var identity = new ClaimsIdentity(Options.AuthenticationType);

            if (sender != null)
            {
                var claims = GetHmacClaims(sender);
                identity.AddClaims(claims);
            }

            return new AuthenticationTicket(identity, new AuthenticationProperties());
        }
        private static IEnumerable<Claim> GetHmacClaims(string deviceNumber)
        {
            return new List<Claim>
            {
                new Claim(CustomClaimTypes.DeviceNumber, deviceNumber)
            };
        }
        public struct CustomClaimTypes
        {
            public static string DeviceNumber = "http://www.gtl.biz/2018/02/identity/claims/devicenumber";
        }
    }
}
