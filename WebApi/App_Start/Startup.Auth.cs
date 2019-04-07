using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApi.Providers;
using WebApi.Models;
using Microsoft.Owin.Security.Facebook;
using WebApi.Facebook;


namespace WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                //AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

			// Uncomment the following lines to enable logging in with third party login providers
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "",
			//    clientSecret: "");

			//app.UseTwitterAuthentication(
			//    consumerKey: "",
			//    consumerSecret: "");

			//app.UseFacebookAuthentication(
			//    appId: "",
			//    appSecret: "");

			var facebookOptions = new FacebookAuthenticationOptions()
			{
				AppId = "322063095286295",
				AppSecret = "48170f69cccd4207ace7810f634b7e67",
				BackchannelHttpHandler = new FbBackChannelHandler(),
				UserInformationEndpoint = "https://graph.facebook.com/v3.2/me?fields=id,email"
			};
			facebookOptions.Scope.Add("email");
			app.UseFacebookAuthentication(facebookOptions);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "960028872055-c9oe2ova4j9mdsdikel0udel8n94bkog.apps.googleusercontent.com",
                ClientSecret = "xoU1AR6NZ3hhvP19KPhI1S1B"
            });
        }
    }
}
