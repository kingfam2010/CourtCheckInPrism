using System;
using System.Collections.Generic;
using System.Text;

namespace CourtCheckInPrism
{
   public static class Constants
    {
        static readonly string tenantName = "peelpolicedemo";
        static readonly string tenantId = "peelpolicedemo.onmicrosoft.com";
        static readonly string clientId = "e29bdf1b-eebe-4368-8c06-bb5c1f2fc73e";
        static readonly string policySignin = "B2C_1_signupsignin";
        static readonly string policyPassword = "B2C_1_passwordreset1";
        static readonly string iosKeychainSecurityGroup = "com.xamarin.CourtCheckInPrism";

        // The following fields and properties should not need to be changed
        static readonly string[] scopes = { "" };
        static readonly string authorityBase = $"https://{tenantName}.b2clogin.com/tfp/{tenantId}/";

        public static string ClientId
        {
            get
            {
                return clientId;
            }
        }
        public static string AuthoritySignin
        {
            get
            {
                return $"{authorityBase}{policySignin}";
            }
        }
        public static string AuthorityPasswordReset
        {
            get
            {
                return $"{authorityBase}{policyPassword}";
            }
        }
        public static string[] Scopes
        {
            get
            {
                return scopes;
            }
        }
        public static string IosKeychainSecurityGroups
        {
            get
            {
                return iosKeychainSecurityGroup;
            }

        }

        }
}
