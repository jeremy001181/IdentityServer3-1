﻿using System;
using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Connect.Models;
using Thinktecture.IdentityServer.Core.Services;

namespace Thinktecture.IdentityServer.Core.Connect
{
    public class ValidatedAuthorizeRequest
    {
        public string ResponseType { get; set; }
        public string ResponseMode { get; set; }
        public Flows Flow { get; set; }
        public ICoreSettings CoreSettings { get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }
        public Uri RedirectUri { get; set; }
        public List<string> Scopes { get; set; }
        public string State { get; set; }
        public string UiLocales { get; set; }
        public bool IsOpenIdRequest { get; set; }
        public bool IsResourceRequest { get; set; }
        
        public string Nonce { get; set; }
        public List<string> AuthenticationContextClasses { get; set; }
        public List<string> AuthenticationMethods { get; set; }
        public string DisplayMode { get; set; }
        public string PromptMode { get; set; }
        public int? MaxAge { get; set; }

        public bool AccessTokenRequested 
        { 
            get
            {
                return (ResponseType == Constants.ResponseTypes.IdTokenToken ||
                        ResponseType == Constants.ResponseTypes.Code);
            }
        }

        public ValidatedAuthorizeRequest()
        {
            Scopes = new List<string>();
            AuthenticationContextClasses = new List<string>();
            AuthenticationMethods = new List<string>();
        }
    }
}