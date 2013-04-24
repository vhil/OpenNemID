﻿<%-- 
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
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckRid.aspx.cs" Inherits="dk.certifikat.extras.CheckRid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="da" lang="da">
    <head>
        <title>TU-interaktionsdesign - Check RID</title>
        <!--#include virtual="/include/common-head.inc" -->
    </head>
    <body id="demosite">

        <div id="skipToContent" class="accessibility">
            <dl>
                <dt>Genveje:</dt>
                <dd><a href="#content" title="Gå direkte til hovedindhold" accesskey="2">Hovedindhold</a></dd>
            </dl>
        </div>

        <!-- container for header section -->
        <div id="headerWrapper">
            <div id="header">
                <div id="logo">
                    <img src="/resources/images/logo.png" alt="Demologo" title="Kun til demobrug" />
                </div>
                <ul id="menu">
                    <li><a href="#.#" title="Simuleret menu kun til demobrug">Menu 01</a></li>
                    <li><a href="#.#" title="Simuleret menu kun til demobrug">Menu 02</a></li>
                    <li><a href="#.#" title="Simuleret menu kun til demobrug">Menu 03</a></li>
                    <li><a href="#.#" title="Simuleret menu kun til demobrug">Menu 04</a></li>
                    <li><a href="#.#" title="Simuleret menu kun til demobrug">Menu 05</a></li>
                </ul>
                <div class="button-wrapper">
                        <a href="/" title="Klik her for at komme tilbage til forsiden">Til forsiden</a>
                        <a href="Default.aspx" title="Klik her for at komme til ekstra funktioner">Ekstra funktioner</a>
                        
                    </div>
            </div>
        </div>

        <div id="contentWrapper">
            <div id="content">
                <div class="content-block">
                 <h2>RID-opslag</h2>
                    <p>Indtast et RID-nummer tilhørende det CPR du gerne vil hente. Kald servicen og check om de to passer sammen.</p>
                    <form method="post" action="CheckRid.aspx" id="checkRid" name="checkRid" runat="server">
                        <p><label for="rid">RID-nummer:(format CVR:xxx-RID:xxx)</label> <asp:TextBox id="ridTxt" runat="server"/></p>
                        <asp:Button ID="validateRidButton" runat="server" onClick="validateRid" Text="CheckRid" />
                    </form>                    
                    <%if(Session["errorText"]!=null && ! "".Equals((String)Session["errorText"])) {%>
                        <h1><%=Session["errorText"] %></h1>
                    <%}
                       String cpr = (String)Session["cpr"];
                       if (cpr!=null && ! "".Equals(cpr.Trim()))
                         { %>
			               <h1>RID matcher et CPR!</h1>
			               <p>til RID: <asp:Label ID="ridLabel" runat="server" /> matcher cpr: <asp:Label ID="cprLabel" runat="server" /></p>
                      <% }%>
                            
                    <p>Hvis du klikker på "Ekstra funktioner", kommer du tilbage til siden hvor du kan ændre standardindstillingerne til login og signering.</p>             
                    <p>Hvis du klikker på "Til forsiden", kommer du tilbage til indeks-siden og kan afprøve en af de forskellige varianter af interaktionsdesignet.</p>
                    
                </div>
                <div class="image-block">
                    <img src="/resources/images/01.jpg" alt="Demobillede" />
                    <img src="/resources/images/02.jpg" alt="Demobillede" />
                </div>
                 
            </div>
        </div>
    </body>
</html>