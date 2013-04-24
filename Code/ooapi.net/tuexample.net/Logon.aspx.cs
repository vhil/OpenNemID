﻿/**
 * Copyright (c) 2010, DanID A/S
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 *  - Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *  - Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 *  - Neither the name of the DanID A/S nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Web;

namespace dk.certifikat
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = Request["ReturnUrl"];
            if (returnUrl != null && returnUrl.StartsWith("/variant1/restricted/"))
            {
                var loginType = FetchLoginTypeForVariant1(Request);
                Response.Redirect("variant1/" + loginType + "?" + Request.QueryString);
            }
            else if (returnUrl != null && returnUrl.StartsWith("/variant2/restricted/"))
            {
                var loginType = FetchLoginTypeForVariant2Or3(Request);
                Response.Redirect("variant2/" + loginType + "?" + Request.QueryString);
            }
            else if (returnUrl != null && returnUrl.StartsWith("/variant3/restricted/"))
            {
                var loginType = FetchLoginTypeForVariant2Or3(Request);
                Response.Redirect("variant3/" + loginType + "?" + Request.QueryString);
            }
        }

        private static string FetchLoginTypeForVariant1(HttpRequest request)
        {
            var rememberedLoginCookie = request.Cookies["preferredLogin"];
            var rememberedLoginType = rememberedLoginCookie != null ? rememberedLoginCookie.Value : null;

            if ("DigitalSignatur".Equals(rememberedLoginType))
            {
                return "log-ind-med-digital-signatur.aspx";
            }
            return "log-ind-med-engangsnoegle.aspx";
        }

        private static string FetchLoginTypeForVariant2Or3(HttpRequest request)
        {
            var rememberedLoginCookie = request.Cookies["preferredLogin"];
            var rememberedLoginType = rememberedLoginCookie != null ? rememberedLoginCookie.Value : null;
            
            if ("DigitalSignatur".Equals(rememberedLoginType))
            {
                return "log-ind-med-digital-signatur.aspx";
            }
            if ("Software".Equals(rememberedLoginType))
            {
                return "log-ind-uden-engangsnoegle.aspx";
            }
            return "log-ind-med-engangsnoegle.aspx";
        }
    }
}
