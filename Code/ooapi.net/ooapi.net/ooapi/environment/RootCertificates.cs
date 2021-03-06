/*
    Copyright 2010 DanID

    This file is part of OpenOcesAPI.

    OpenOcesAPI is free software; you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation; either version 2.1 of the License, or
    (at your option) any later version.

    OpenOcesAPI is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with OpenOcesAPI; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


    Note to developers:
    If you add code to this file, please take a minute to add an additional
    @author statement below.
*/
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using org.openoces.ooapi.certificate;

namespace org.openoces.ooapi.environment
{
    public class RootCertificates
    {
        static readonly Dictionary<OcesEnvironment, X509Certificate2> TheRootCertificates = LoadRootCertificates();

        const string ProductionCertificateOcesI =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIIFGTCCBAGgAwIBAgIEPki9xDANBgkqhkiG9w0BAQUFADAxMQswCQYDVQQGEwJE\n" +
          "SzEMMAoGA1UEChMDVERDMRQwEgYDVQQDEwtUREMgT0NFUyBDQTAeFw0wMzAyMTEw\n" +
          "ODM5MzBaFw0zNzAyMTEwOTA5MzBaMDExCzAJBgNVBAYTAkRLMQwwCgYDVQQKEwNU\n" +
          "REMxFDASBgNVBAMTC1REQyBPQ0VTIENBMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A\n" +
          "MIIBCgKCAQEArGL2YSCyz8DGhdfjeebM7fI5kqSXLmSjhFuHnEz9pPPEXyG9VhDr\n" +
          "2y5h7JNp46PMvZnDBfwGuMo2HP6QjklMxFaaL1a8z3sM8W9Hpg1DTeLpHTk0zY0s\n" +
          "2RKY+ePhwUp8hjjEqcRhiNJerxomTdXkoCJHhNlktxmW/OwZ5LKXJk5KTMuPJItU\n" +
          "GBxIYXvViGjaXbXqzRowwYCDdlCqT9HU3Tjw7xb04QxQBr/q+3pJoSgrHPb8FTKj\n" +
          "dGqPqcNiKXEx5TukYBdedObaE+3pHx8b0bJoc8YQNHVGEBDjkAB2QMuLt0MJIf+r\n" +
          "TpPGWOmlgtt3xDqZsXKVSQTwtyv6e1mO3QIDAQABo4ICNzCCAjMwDwYDVR0TAQH/\n" +
          "BAUwAwEB/zAOBgNVHQ8BAf8EBAMCAQYwgewGA1UdIASB5DCB4TCB3gYIKoFQgSkB\n" +
          "AQEwgdEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2VydGlmaWthdC5kay9yZXBv\n" +
          "c2l0b3J5MIGdBggrBgEFBQcCAjCBkDAKFgNUREMwAwIBARqBgUNlcnRpZmlrYXRl\n" +
          "ciBmcmEgZGVubmUgQ0EgdWRzdGVkZXMgdW5kZXIgT0lEIDEuMi4yMDguMTY5LjEu\n" +
          "MS4xLiBDZXJ0aWZpY2F0ZXMgZnJvbSB0aGlzIENBIGFyZSBpc3N1ZWQgdW5kZXIg\n" +
          "T0lEIDEuMi4yMDguMTY5LjEuMS4xLjARBglghkgBhvhCAQEEBAMCAAcwgYEGA1Ud\n" +
          "HwR6MHgwSKBGoESkQjBAMQswCQYDVQQGEwJESzEMMAoGA1UEChMDVERDMRQwEgYD\n" +
          "VQQDEwtUREMgT0NFUyBDQTENMAsGA1UEAxMEQ1JMMTAsoCqgKIYmaHR0cDovL2Ny\n" +
          "bC5vY2VzLmNlcnRpZmlrYXQuZGsvb2Nlcy5jcmwwKwYDVR0QBCQwIoAPMjAwMzAy\n" +
          "MTEwODM5MzBagQ8yMDM3MDIxMTA5MDkzMFowHwYDVR0jBBgwFoAUYLWF7FZkfhIZ\n" +
          "J2cdUBVLc647+RIwHQYDVR0OBBYEFGC1hexWZH4SGSdnHVAVS3OuO/kSMB0GCSqG\n" +
          "SIb2fQdBAAQQMA4bCFY2LjA6NC4wAwIEkDANBgkqhkiG9w0BAQUFAAOCAQEACrom\n" +
          "JkbTc6gJ82sLMJn9iuFXehHTuJTXCRBuo7E4A9G28kNBKWKnctj7fAXmMXAnVBhO\n" +
          "inxO5dHKjHiIzxvTkIvmI/gLDjNDfZziChmPyQE+dF10yYscA+UYyAFMP8uXBV2Y\n" +
          "caaYb7Z8vTd/vuGTJW1v8AqtFxjhA7wHKcitJuj4YfD9IQl+mo6paH1IYnK9AOoB\n" +
          "mbgGglGBTvH1tJFUuSN6AJqfXY3gPGS5GhKSKseCRHI53OI8xthV9RVOyAUO28bQ\n" +
          "YqbsFbS1AoLbrIyigfCbmTH1ICCoiGEKB5+U/NDXG8wuF/MEJ3Zn61SD/aSQfgY9\n" +
          "BKNDLdr8C2LqL19iUw==\n" +
          "-----END CERTIFICATE-----";

        const string ProductionCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
        "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\n" +
        "-----END CERTIFICATE-----"; 

        const string SystemTestCertificateOcesI =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIIEXTCCA8agAwIBAgIEQDYX/DANBgkqhkiG9w0BAQUFADA/MQswCQYDVQQGEwJE\n" +
          "SzEMMAoGA1UEChMDVERDMSIwIAYDVQQDExlUREMgT0NFUyBTeXN0ZW10ZXN0IENB\n" +
          "IElJMB4XDTA0MDIyMDEzNTE0OVoXDTM3MDYyMDE0MjE0OVowPzELMAkGA1UEBhMC\n" +
          "REsxDDAKBgNVBAoTA1REQzEiMCAGA1UEAxMZVERDIE9DRVMgU3lzdGVtdGVzdCBD\n" +
          "QSBJSTCBnzANBgkqhkiG9w0BAQEFAAOBjQAwgYkCgYEArawANI56sljDsnosDU+M\n" +
          "p4r+RKFys9c5qy8jWZyA+7PYFs4+IZcFxnbNuHi8aAcbSFOUJF0PGpNgPEtNc+XA\n" +
          "K7p16iawNTYpMkHm2VoInNfwWEj/wGmtb4rKDT2a7auGk76q+Xdqnno4PRO8e7AK\n" +
          "EHw7pN3kiHmZCI48PTRpRx8CAwEAAaOCAmQwggJgMA8GA1UdEwEB/wQFMAMBAf8w\n" +
          "DgYDVR0PAQH/BAQDAgEGMIIBAwYDVR0gBIH7MIH4MIH1BgkpAQEBAQEBAQEwgecw\n" +
          "LwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2VydGlmaWthdC5kay9yZXBvc2l0b3J5\n" +
          "MIGzBggrBgEFBQcCAjCBpjAKFgNUREMwAwIBARqBl1REQyBUZXN0IENlcnRpZmlr\n" +
          "YXRlciBmcmEgZGVubmUgQ0EgdWRzdGVkZXMgdW5kZXIgT0lEIDEuMS4xLjEuMS4x\n" +
          "LjEuMS4xLjEuIFREQyBUZXN0IENlcnRpZmljYXRlcyBmcm9tIHRoaXMgQ0EgYXJl\n" +
          "IGlzc3VlZCB1bmRlciBPSUQgMS4xLjEuMS4xLjEuMS4xLjEuMS4wEQYJYIZIAYb4\n" +
          "QgEBBAQDAgAHMIGWBgNVHR8EgY4wgYswVqBUoFKkUDBOMQswCQYDVQQGEwJESzEM\n" +
          "MAoGA1UEChMDVERDMSIwIAYDVQQDExlUREMgT0NFUyBTeXN0ZW10ZXN0IENBIElJ\n" +
          "MQ0wCwYDVQQDEwRDUkwxMDGgL6AthitodHRwOi8vdGVzdC5jcmwub2Nlcy5jZXJ0\n" +
          "aWZpa2F0LmRrL29jZXMuY3JsMCsGA1UdEAQkMCKADzIwMDQwMjIwMTM1MTQ5WoEP\n" +
          "MjAzNzA2MjAxNDIxNDlaMB8GA1UdIwQYMBaAFByYCUcaTDi5EMUEKVvx9E6Aasx+\n" +
          "MB0GA1UdDgQWBBQcmAlHGkw4uRDFBClb8fROgGrMfjAdBgkqhkiG9n0HQQAEEDAO\n" +
          "GwhWNi4wOjQuMAMCBJAwDQYJKoZIhvcNAQEFBQADgYEApyoAjiKq6WK5XaKWUpVs\n" +
          "kutzohv1VcCke/3JeUVtmB+byexJMC171s4RHoqcbufcI2ASVWwu84i45MaKg/nx\n" +
          "oqojMyY19/W2wbQFEdsxUCnLa9e9tlWj0xS/AaKeUhk2MBOqv+hMdc71jOqc5JN7\n" +
          "T2Ba6ZRIY5uXkO3IGZ3XUsw=\n" +
          "-----END CERTIFICATE-----";

        const string LocalhostTestingCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIICJDCCAY2gAwIBAgIBCjANBgkqhkiG9w0BAQUFADBFMQswCQYDVQQGEwJESzEO\n" +
          "MAwGA1UECgwFREFOSUQxJjAkBgNVBAsMHUxvY2FsaG9zdCBPQ0VTIGRldmVsb3Bt\n" +
          "ZW50IENBMB4XDTA5MDgyNDA4MzcwM1oXDTEwMDgyNTA4MzcwM1owRTELMAkGA1UE\n" +
          "BhMCREsxDjAMBgNVBAoMBURBTklEMSYwJAYDVQQLDB1Mb2NhbGhvc3QgT0NFUyBk\n" +
          "ZXZlbG9wbWVudCBDQTCBnTANBgkqhkiG9w0BAQEFAAOBiwAwgYcCgYEAslnS1uYn\n" +
          "p2jJS+NhZMLZ/HnZequSUxQOW/F3URl3Mdb3VA0lCee5/+4KcKbibVbpLS7df4Wr\n" +
          "qFYAtpCJ819r2/PCmOBYQlNdnwZOawORy30wbgotIMTftOe0mpZAveomwQrWnD8F\n" +
          "AHziUTzuRM/gGZjmK2w2N9P8A5EHmybuNtUCARGjJjAkMBIGA1UdEwEB/wQIMAYB\n" +
          "Af8CAQAwDgYDVR0PAQH/BAQDAgEGMA0GCSqGSIb3DQEBBQUAA4GBAI+HZqAoDtC+\n" +
          "v10Il+XtWVptKF5G3eIGGw24Jac3gDrD5+h8i4pM9Ohi/fcO6HmbhWP4bQ5uZhEX\n" +
          "I7cnEtBiNPXr+jfNW8LXE0CuFNxxhaju/O9xGzhIXd00yV7EkpTm9Dr8GGL93vQa\n" +
          "nuUv/mUKpaVqe9SMKyeirVMz9/Do7t0K\n" +
          "-----END CERTIFICATE-----";

        const string DevelopmentCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIIEPTCCAyWgAwIBAgIESdNTxzANBgkqhkiG9w0BAQUFADBJMQswCQYDVQQGEwJE\n" +
          "SzEOMAwGA1UEChMFRGFuSUQxKjAoBgNVBAMTIURhbklEIE9DRVMgRGV2ZWxvcG1l\n" +
          "bnQgUHJpbWFyeSBDQTAeFw0wOTA0MDExMTE1MTNaFw0yOTA0MDExMTQ1MTNaMEkx\n" +
          "CzAJBgNVBAYTAkRLMQ4wDAYDVQQKEwVEYW5JRDEqMCgGA1UEAxMhRGFuSUQgT0NF\n" +
          "UyBEZXZlbG9wbWVudCBQcmltYXJ5IENBMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A\n" +
          "MIIBCgKCAQEAqbO17+/DZWITNBPuG/PqzEA9fYdwCQLqN3zCiy8hrVZo9hP7KF2C\n" +
          "7ouvomRQf03zURbfii++uyKDwvjDCpUbVOZCmtwPWtEbgHghnbua3sLrvZjf2syW\n" +
          "T64rQ8tiDIEE1pSKLniLkQBVvFrNXLRNnRdaD8JRfXULH8fcpwxfQV1L4DkPLkHj\n" +
          "SICLgKRV76WG7VeS/cHqI5FclLNvFWl7YKDeSmQfND8HrjQTlsEiUdDNKem4T87r\n" +
          "BiaVuU7Iljjf7V7lRlvQkE/pM7jxYw/YchGqK2ucVzKDeMf3aIOjhT2YcsmPtK9+\n" +
          "TwVEAhuzHceWiQR/ek62GPHhTs3BRmneaQIDAQABo4IBKzCCAScwEQYJYIZIAYb4\n" +
          "QgEBBAQDAgAHMGsGA1UdHwRkMGIwYKBeoFykWjBYMQswCQYDVQQGEwJESzEOMAwG\n" +
          "A1UEChMFRGFuSUQxKjAoBgNVBAMTIURhbklEIE9DRVMgRGV2ZWxvcG1lbnQgUHJp\n" +
          "bWFyeSBDQTENMAsGA1UEAxMEQ1JMMTArBgNVHRAEJDAigA8yMDA5MDQwMTExMTUx\n" +
          "M1qBDzIwMjkwNDAxMTE0NTEzWjALBgNVHQ8EBAMCAQYwHwYDVR0jBBgwFoAUFoae\n" +
          "x+wDqlqYG3Js6faI063wUF0wHQYDVR0OBBYEFBaGnsfsA6pamBtybOn2iNOt8FBd\n" +
          "MAwGA1UdEwQFMAMBAf8wHQYJKoZIhvZ9B0EABBAwDhsIVjcuMTo0LjADAgSQMA0G\n" +
          "CSqGSIb3DQEBBQUAA4IBAQBJrBF1mszFMyYjjYB6h+eu8mOzSHlaANF5vOUxmDd/\n" +
          "8ujaf5ofV4V6bomybRPEtaYJYF50U7q8/YTCIzfavuXMNSoRJn7kUM27KNnexpE1\n" +
          "OAjeROlXFnHh1yOe0HPAkYes9bZqSeQSdtJ4nebzb4JX/1ljQ29L7RfhgaFEkiIK\n" +
          "r2uLJm81iIOIvD9TbOJ9Mp6WoDBzYNIJ9kPO4uxewLQB70YdgG9novZ+Bx52TEbL\n" +
          "FSuKJAcRftuHfpd9WMJKCUJe/GzfQNWtvV6c2k2oSjj10+WKtnFxF9JuR4BnNlKc\n" +
          "ybaqkOMtK1+PLInBaEpPp1Yq7CNGXId3CRKV92eX2bKC\n" +
          "-----END CERTIFICATE-----";

        const string OperationsTestCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIIGcDCCBFigAwIBAgIESuA7FTANBgkqhkiG9w0BAQsFADBNMQswCQYDVQQGEwJE\n" +
          "SzEOMAwGA1UEChMFRGFuSUQxLjAsBgNVBAMTJURhbklEIE9DRVMgT3BlcmF0aW9u\n" +
          "cy1UZXN0IFByaW1hcnkgQ0EwHhcNMDkxMDIyMTAyOTM3WhcNMzcxMDIyMTA1OTM3\n" +
          "WjBNMQswCQYDVQQGEwJESzEOMAwGA1UEChMFRGFuSUQxLjAsBgNVBAMTJURhbklE\n" +
          "IE9DRVMgT3BlcmF0aW9ucy1UZXN0IFByaW1hcnkgQ0EwggIiMA0GCSqGSIb3DQEB\n" +
          "AQUAA4ICDwAwggIKAoICAQCwdiqpbCDNgIk8xydCgJe8x8nMjTyfzvmgcaoL/wLJ\n" +
          "zy3F+2tPcUJHX59kQ0cA/lIWgmhTPDCR/e9w08cPZuSmnBdxjRg1lvlPGSHqO3K7\n" +
          "1swpzUprZVWmMNxN24gKp46V2EmtrHa456SdeRGT0JGzimusohG7uHGcbmywUyjB\n" +
          "ylj00A9Ps0941XSTwPBcgk8BLXFNV5PBZlsWQ68Hy50MYE6wm14nrp5bVWequQ9s\n" +
          "Y131mdM/O+9Q6LUa13Z+JK9saay/tQgm+OWg/SG/p/5zChGqanyOtZ7WChkL4K/e\n" +
          "lEfNMF48Of9OLiGQ1HeEIteT2oUzlCXlopV7aYovUaSuTurIlOQUQ68iaAOv3mDg\n" +
          "Lsw+vrOcv+LTFPbkllLF2eP0tA/LmjHAqybECKjmmjrSpa3asHAxQunwjhIw5XBt\n" +
          "qZplWjHnwFrUhPm2xOuq7h1Wc+/o4O8b1mb0FGUOg489OzZqcA+u5aePC3StHcMV\n" +
          "mqPe/BKqC8iu+/F9KXNeNAz0V26p4aZHUb6L+cFY+SMnsbyO/S9pCu6ubbuaaiJH\n" +
          "Zw+wTtn+dEOX4icDSgfCrKzVFJz1f1vR2bl9ZOmct+UKS7XJFp2BYpPmqIoVVYWd\n" +
          "WayRy8oxtqaUOkToTALnLaxLCqczQIkNDavZU4rFZRvVrbA0mTKYOXhD7/8IsWGs\n" +
          "BQIDAQABo4IBVjCCAVIwDwYDVR0TAQH/BAUwAwEB/zAOBgNVHQ8BAf8EBAMCAQYw\n" +
          "gcEGA1UdIAEB/wSBtjCBszCBsAYDuioqMIGoMDAGCCsGAQUFBwIBFiRodHRwOi8v\n" +
          "d3d3LmNlcnRpZmlrYXQuZGsvcmVwb3NpdG9yeSAwdAYIKwYBBQUHAgIwaDAMFgVE\n" +
          "YW5JRDADAgEBGlhEZXR0ZSBjZXJ0aWZpa2F0IGVyIHVkc3RlZHQgYWYgZXQgdGVz\n" +
          "dHN5c3RlbSBvZyBi+HIgdWRlbHVra2VuZGUgYW52ZW5kZXMgdGlsIHRlc3Rmb3Jt\n" +
          "5WwuMCsGA1UdEAQkMCKADzIwMDkxMDIyMTAyOTM3WoEPMjAzNzEwMjIxMDU5Mzda\n" +
          "MB8GA1UdIwQYMBaAFOQl5KGl5twmFkvWiv3sPFPUiN2mMB0GA1UdDgQWBBTkJeSh\n" +
          "pebcJhZL1or97DxT1IjdpjANBgkqhkiG9w0BAQsFAAOCAgEAO/38JlfwBHXh+Jno\n" +
          "AyUYVDSQ2BdokIugmUIXFDKQ12pWw4/F3AjaJAMkI68SHKzaGKQ3yBCNYc7pxKsR\n" +
          "fNN/CCwtvgtHHBOtv3GI+700X0EhQsqTcTNFn2BOfDHKrHjIf32UPTyJ9l1ByQP+\n" +
          "W56WyozGfOjCZ8Q+Slk2p2gim1PF3iiIiONK1p/kZ4E4Wvj6+wXcNjnDRIZX0M8e\n" +
          "H8HTh+CNuyUtOMLt7uobvfP6H4QXfOVCMjcH/z1nRP8ELfLO1/M7/xhmUItgmm0P\n" +
          "mDqDE2/N3PEJBEZu9Z2D46Yhb8+Wco2xjDk94kNkSjuz8zZNBBOPBG7m/mB+zT9g\n" +
          "lpBbPk3QRViZO/VuWvZTzt4G3T1DiAdlH/EOgxCOhJ3wFieBD9vjqcEhWnHykLPH\n" +
          "xaIzzuwydRGD3XY1BHshGFLRogsetmm4Tc5C9PMHayqXiEh0MlFOXpy8Q7KL5gpe\n" +
          "QDxfEpdDJs5tEj4rSdVO+/Wo1rXwxQEjvRGtpWNlS5u+uY1N4BXz4fSVVE56AzXg\n" +
          "1C/xwowZASe0lUVWegngX79In9uxUZZVHkteq+oRNx7p0YqoikNXfGEtQzUIi04k\n" +
          "OCGz53h3zcCETQzQxfMC3K9M889/7950z9I85noYcNSy+6xr4BL2wuW6H3zoBHVP\n" +
          "KThpLl/jhfYnpab54AQDIaHfnx0=\n" +
          "-----END CERTIFICATE-----";

        const string InternalTestCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
          "MIIGbDCCBFSgAwIBAgIESuGMBTANBgkqhkiG9w0BAQsFADBLMQswCQYDVQQGEwJE\n" +
          "SzEOMAwGA1UEChMFRGFuSUQxLDAqBgNVBAMTI0RhbklEIE9DRVMgSW50ZXJuYWwt\n" +
          "VGVzdCBQcmltYXJ5IENBMB4XDTA5MTAyMzEwMjcxMloXDTM3MTAyMzEwNTcxMlow\n" +
          "SzELMAkGA1UEBhMCREsxDjAMBgNVBAoTBURhbklEMSwwKgYDVQQDEyNEYW5JRCBP\n" +
          "Q0VTIEludGVybmFsLVRlc3QgUHJpbWFyeSBDQTCCAiIwDQYJKoZIhvcNAQEBBQAD\n" +
          "ggIPADCCAgoCggIBANr/S6cF0qBnB+IAJuDv+3VoxIObX0jqRXftayiKJgViZLhH\n" +
          "zUoPgNZloDUS4Y2odoVtFoUpNhW0FSnyqDoxJnae+UP58GMgf3xOyK9M1EA/V2em\n" +
          "kxbYqIw0ZepH37JsqblenEv5ZA3AhrJ4nG2KajzaYxicIUw2WxLiOg4/UgM5RH4j\n" +
          "ba41qcvvSZ9KMIAx9lVUfcST5hu/3UswdhLJtIu5ECNSb9JPfY15clBkB3Xhl20u\n" +
          "QuD9TJ0+SR+jSNlB9De+jbNjvVg8vyJfddO0tEZtEJ3TxYYVArKy+ldJc0vEre/x\n" +
          "kTRpOM4aOssM46ZF1K35Q7oqPbkCueRch5yxmkagRd3gB6sq3AuaCtG3zgA0rXo7\n" +
          "39LIosuny5OtpxJHzJnEBPTIJgjcCKZ21YGo2TGEMzsb4IY4axf3oMSwehddzRuo\n" +
          "4/i3P+ve/cho6D8hkFUEmuY2x1X9oiFdyF3Ppr91p9DM/Bg6ckEmRzvAZipbx9zT\n" +
          "kwAydqIwSrrErz7TLba88m/v2z6gPeK5oqxbQ6yqXtSRbjC32fba9z3o5wrB1mRe\n" +
          "3Sre/FlQbQ3hg7reGwS0RZ11jm1OcN9HMtXeJdjwZyYWXvE4DdyL5qlUjIb07axv\n" +
          "frWj44gqEmKEgycsiM1QVfJdD2fOiyPOCMdVQ2AGIpQnIRcLk1Mck6IcF0iRAgMB\n" +
          "AAGjggFWMIIBUjAPBgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB/wQEAwIBBjCBwQYD\n" +
          "VR0gAQH/BIG2MIGzMIGwBgO6KiowgagwMAYIKwYBBQUHAgEWJGh0dHA6Ly93d3cu\n" +
          "Y2VydGlmaWthdC5kay9yZXBvc2l0b3J5IDB0BggrBgEFBQcCAjBoMAwWBURhbklE\n" +
          "MAMCAQEaWERldHRlIGNlcnRpZmlrYXQgZXIgdWRzdGVkdCBhZiBldCB0ZXN0c3lz\n" +
          "dGVtIG9nIGL4ciB1ZGVsdWtrZW5kZSBhbnZlbmRlcyB0aWwgdGVzdGZvcm3lbC4w\n" +
          "KwYDVR0QBCQwIoAPMjAwOTEwMjMxMDI3MTJagQ8yMDM3MTAyMzEwNTcxMlowHwYD\n" +
          "VR0jBBgwFoAUSwMcS2qehQ/ZQUI0dorfLVjpowQwHQYDVR0OBBYEFEsDHEtqnoUP\n" +
          "2UFCNHaK3y1Y6aMEMA0GCSqGSIb3DQEBCwUAA4ICAQBuLHSzx/v1WhVyUxBmUAc/\n" +
          "Un+sc5yEO2tmVEEWCJX++kzIDEPH8Vsz4JreJVM6lgBbssDgPZv7Z8io7euhXaY9\n" +
          "kw5RYLKaNksxKXq5xNzAbcgFb+EuCm91lHe02mp8RAWvu1C8Ni4gq1gfz6B9LmOg\n" +
          "h3UZFWxOTk5ZrHj5ggx3EU09tDQhzZa9g6I2KLx4OoLfHDOi/xEIwrl1IW9kUnrB\n" +
          "kU9Ktj5/XZl9wMGqlzKaozwAArRh2FpGB1x9qEmOxIpgNaal6pveqVrm1/GZjSJo\n" +
          "VPaU7CesAKgYEqqRtQbU1Fa7et1D9q3KnZISscJP62IV9K9O1bINgzJGFKb7QJB+\n" +
          "dgJm5xFqCVsLKW84RVMzeAA114F3c0eqftPN4dZuRf5aTyxxCK6x4F9Yb7WiSzRV\n" +
          "FBXWyFzsICSMfD46ZUbxrCxo9k6XetOHKaJ6Fh/AZFA95mVO76NCo42FPosaZEBP\n" +
          "OFvymEQ6Fg55VJvQMaqR+WfMZpck7i0san410yU//ieeDR2GQFiKuTZV/5+Pw2Zr\n" +
          "FHPcbBSSGcwMsWuyxLg88p9f57jFZ6zGY0qobiPTMrPvjA3LPYrb1GDx7EtaoyYc\n" +
          "YkA4tAUUm0OpcQfMp7CJOc4+PGnTtkQKkEX9XjtYJZSX4UdIlAE4HZ8W7RQc+oEI\n" +
          "k1jr22Y4rAJM2TutoaXzWw==\n" +
          "-----END CERTIFICATE-----";

        const string DevelopmentTestCertificateOcesII =
            "-----BEGIN CERTIFICATE-----\n" +
            "MIIGSDCCBDCgAwIBAgIES8cPejANBgkqhkiG9w0BAQsFADBPMQswCQYDVQQGEwJE\n" +
            "SzESMBAGA1UEChMJVFJVU1QyNDA4MSwwKgYDVQQDEyNUUlVTVDI0MDggU3lzdGVt\n" +
            "dGVzdCBJSUkgUHJpbWFyeSBDQTAeFw0xMDA0MTUxMjM3MTBaFw0zNzAzMTUxMzA3\n" +
            "MTBaME8xCzAJBgNVBAYTAkRLMRIwEAYDVQQKEwlUUlVTVDI0MDgxLDAqBgNVBAMT\n" +
            "I1RSVVNUMjQwOCBTeXN0ZW10ZXN0IElJSSBQcmltYXJ5IENBMIICIjANBgkqhkiG\n" +
            "9w0BAQEFAAOCAg8AMIICCgKCAgEAoygbAGrDUZbHS8ciZMO7YfHh1KOBuiMiwiYu\n" +
            "umFYRSR0YUNejn2qDrh2k86QyBMBBVTUMhyojZwuqTbe/toTNCemBBYuffA14xDJ\n" +
            "V2AaRvL1F0B6oFKJ9OAZ24AwJXrASPqV+QjJkbMJHHYXEgxuJcYK9uSQkyyWQN7D\n" +
            "mtDz4kehd8YD9EwqQx5oN0aoWf5F031/NcMIafrD/qEvPBrNh3uujDUEyi35Qct1\n" +
            "geqPeuOWcPwLkMIK4iFBqauC6PbgD2QQtbtUwWjYMob7SPTJ31qaLMpz79dtd8aM\n" +
            "/u+AmA/eWDdUHG+QXyIPz6nKcYIB5FIJ0rGfuTcaxkbBBpcHXRoz/5zx2eR6LnMl\n" +
            "nLkv1sVjbPJd1uY7nExtnibNR+WbO5AC7DvfBtn+2LndSfBt5tsR/TUEeTQ8HDg8\n" +
            "5Z7X3H0LRovGE1meGmaJ/Gz3WldaH9Qo40Rh8o+mL+W8QioyZKLbkop2gyZvHZ1B\n" +
            "uaTixSeZjzShDJolLoPJ1zbyXvrm29Dy4hWLjRfVuMUcXsmjRN5YNJc1voU+h56h\n" +
            "tMGLeb9CbKNomYrhjVZ/B9HbnixD8fpvC0gxzXKMpeS1QPYEDDncAHmR4kHtUhNF\n" +
            "TdBsrE1+XMhN5T3O1oKb0vWGE5NRqoJcvRdxsW2ztqHIUVqBLMrPJnHEORbhKO6Y\n" +
            "eZbYT/ECAwEAAaOCASowggEmMA8GA1UdEwEB/wQFMAMBAf8wDgYDVR0PAQH/BAQD\n" +
            "AgEGMBEGA1UdIAQKMAgwBgYEVR0gADCBrwYDVR0fBIGnMIGkMDqgOKA2hjRodHRw\n" +
            "Oi8vY3JsLnN5c3RlbXRlc3QzLnRydXN0MjQwOC5jb20vc3lzdGVtdGVzdDMuY3Js\n" +
            "MGagZKBipGAwXjELMAkGA1UEBhMCREsxEjAQBgNVBAoTCVRSVVNUMjQwODEsMCoG\n" +
            "A1UEAxMjVFJVU1QyNDA4IFN5c3RlbXRlc3QgSUlJIFByaW1hcnkgQ0ExDTALBgNV\n" +
            "BAMTBENSTDEwHwYDVR0jBBgwFoAU8kbBLak+Oj9Af0QkMvwNXLmEDegwHQYDVR0O\n" +
            "BBYEFPJGwS2pPjo/QH9EJDL8DVy5hA3oMA0GCSqGSIb3DQEBCwUAA4ICAQAzXJB9\n" +
            "MDBBgimATJNe9vHnBcxE4ugKSK6A3OUUGjOXJawVXu3TdriX0So8RZwahCgPQ3Oo\n" +
            "wM3d4/SRcQEUiY3x6HPvdFGRR+Iwq9bOnzFjkQXVdgS18TcKWxhMuyC7NxWuAsRo\n" +
            "J26r/t7cMvL09rF+uMjJvmt1y3Kv9VezDI2D9g5euHS6VpzW4Y1gyMC7qwiLZOTm\n" +
            "xp+Ege0dI6xmehpRlkhtL3I0RktposDWCJNbEWKdMCbuYXW369baSlTu7n3gl5re\n" +
            "GyRQJ02eOaAXiAwhBmJ7wOYIvi0pD4gY6rcOV9RBPLo26CvHAMZLoBhqaQDddfev\n" +
            "Fmj2DFUmdoKufNPJ3nZa+HXG4Z69UIdGCyNyoh2SR4OSuYv1hpEzlrUR0CnPOWnG\n" +
            "xnzJLMVaoR9g3l7/DycdoM97fb/hH13OqRPVERBNb3O0/HIsK3KZnz9/2HsNykiC\n" +
            "qPZDJtnJ36HX39w97qpstzKTfCZChvg8h22Bhi6Z9EEeu0DxutL7MchyXWbZhK1r\n" +
            "siHETi9p6gIOniAaV7MlWUhJpPR9FsxGd8VHqjsc2jI98rpwf/IhYBmZuu7hcgLQ\n" +
            "EF8UpCqQElR2DDlT3VQyqdiaigXXm6zhY/1JalJvVJ2SGg38+qQaTvGv11nSZg0o\n" +
            "cIRwutKx2Pu5lCVfo/vAQtOT+tHTZCV27K/epA==\n" +
            "-----END CERTIFICATE-----";

        const string ExternalTestCertificateOcesII =
                   "-----BEGIN CERTIFICATE-----\n" +
            "MIIGRTCCBC2gAwIBAgIES8xKvDANBgkqhkiG9w0BAQsFADBOMQswCQYDVQQGEwJE\n" +
            "SzESMBAGA1UEChMJVFJVU1QyNDA4MSswKQYDVQQDEyJUUlVTVDI0MDggU3lzdGVt\n" +
            "dGVzdCBJViBQcmltYXJ5IENBMB4XDTEwMDQxOTExNTExOVoXDTM2MTIxOTEyMjEx\n" +
            "OVowTjELMAkGA1UEBhMCREsxEjAQBgNVBAoTCVRSVVNUMjQwODErMCkGA1UEAxMi\n" +
            "VFJVU1QyNDA4IFN5c3RlbXRlc3QgSVYgUHJpbWFyeSBDQTCCAiIwDQYJKoZIhvcN\n" +
            "AQEBBQADggIPADCCAgoCggIBAL9C15kRgzFppYLz7YA6FPp67L5MAjFn0qD7Xxc0\n" +
            "6b2Lho7X5u7Hx59n9qlL3iNUoBBaht9J7UmqYGf3MrdaxU3lUfnPGaboancNOF5a\n" +
            "UDGWfFwdv29KBWJY1BYyhiBpHcR12E5FYjuNuoU1090nYqmlAohVBqoam84vpsG/\n" +
            "uLVk+mnWyw/advs/C0LbZC2OYNHISbNoX34cwEXXozTaEBkwvcf/CmGRm1EeXmFz\n" +
            "PXNgcwijypuczEodSMLSSQbTukNOsSfRbvcvKTf3WQv5mtYllh9E20zCITwa7A4k\n" +
            "jS/uyLBjOy/TsRJ1lBpuFxQEIq89x2MNv8QgEXk46VHZ7pKmeuNog6/wlvAmuxQn\n" +
            "fQx1TJyWL7PIMvOxdN547rhM25/Q1xocSKG/a4rxpVH4edpUFfJQDxNASccgdRjj\n" +
            "DmsIL8oXEqHPS6g9C+mYg6HzmwkTLJFstcTqEwtwoxJfAstzfH9j3cuVHmng57gk\n" +
            "8FnSdo+z4SNdfyWQ9EoBHDEc3nVS5BkeQDuuI2F/t91QcwwO1d5EL65qly4EW+7o\n" +
            "A1KIQ+JvqyxEfoEVLEqekfFKso8WJyUZb86U2DU75v08clBLHsD/Kvj+ijTzngxH\n" +
            "KchenSTmARJclxeQu7GqJd3yRtSDh4+vR7oxT8bAPhww1IJnrq2Hqap5q+kQUwCK\n" +
            "JpdFAgMBAAGjggEpMIIBJTAPBgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB/wQEAwIB\n" +
            "BjARBgNVHSAECjAIMAYGBFUdIAAwga4GA1UdHwSBpjCBozA6oDigNoY0aHR0cDov\n" +
            "L2NybC5zeXN0ZW10ZXN0NC50cnVzdDI0MDguY29tL3N5c3RlbXRlc3Q0LmNybDBl\n" +
            "oGOgYaRfMF0xCzAJBgNVBAYTAkRLMRIwEAYDVQQKEwlUUlVTVDI0MDgxKzApBgNV\n" +
            "BAMTIlRSVVNUMjQwOCBTeXN0ZW10ZXN0IElWIFByaW1hcnkgQ0ExDTALBgNVBAMT\n" +
            "BENSTDEwHwYDVR0jBBgwFoAUScER+EKQbVZjiUMnmjMrRICiKr8wHQYDVR0OBBYE\n" +
            "FEnBEfhCkG1WY4lDJ5ozK0SAoiq/MA0GCSqGSIb3DQEBCwUAA4ICAQBwoCBT8f52\n" +
            "QyFNTTgxZsbWNq+iZKiQhIf4+9oOdvvIgrbosf1pgynxzozoTmtuKLvjA9GTgblX\n" +
            "KUHuz6hh0HgVXLaYwF0yzi6tYdwlRhQ+zpQvtXIR//P+tQyuM3Xbwgp3ybiX9Q/3\n" +
            "YJwwzwT+1EnVyEetCHz+n3jPUebMApuLP8CjDeaHph8CGC+NgjKMGjaSjQOVihdv\n" +
            "K8DRoMSUeP9rhmNYmvdIHBNUU3K6wAOOp8qDAe3mgRofosuY0PyYcT+s8/icj0UW\n" +
            "dVFiCvNEaen2Y0KYlSPe0I1PNXDTcwV1zFQH59DGkYHKlcyD1nelj5r6sdse4oDJ\n" +
            "Ey37yusCEe4t8nwlupFJNjrsSPF7kE/tpesJMt/CpPLDOARMe1xYqhls+kF3HWBZ\n" +
            "JH7CdCeHTRV+fqrz0na8engCcLbcLSj8Kj0TQEtOzx3tj6d2AnPHzTgeTNzVAFE3\n" +
            "k5FPoQj+e5EvZEeMr5YIAVmFIPDq2U3KPPGKkc6XGn69bx0IDKpqRo3203TYXNdK\n" +
            "HMGFoYx6KCubWa+OxNu4hyyEzRkZLuj3fsyTkOb7XxlAG5MSjypznOtNBJ5PGPje\n" +
            "sHqvUNcE8638WqPMwxp8Gfebv6bvfd3vRBfUWdRNbgybF7dVZy2rgx3337h7N+8c\n" +
            "LOmbRXQpkORycBIDkM7nJyFf2OaQazNVOQ==\n" +
            "-----END CERTIFICATE-----";

        const string IntegrationTestCertificateOcesII =
    "-----BEGIN CERTIFICATE-----\n" +
    "MIIGRTCCBC2gAwIBAgIETHO9tTANBgkqhkiG9w0BAQsFADBOMQswCQYDVQQGEwJE\n" +
    "SzESMBAGA1UEChMJVFJVU1QyNDA4MSswKQYDVQQDEyJUUlVTVDI0MDggU3lzdGVt\n" +
    "dGVzdCBJWCBQcmltYXJ5IENBMB4XDTEwMDgyNDEyMTAyM1oXDTM3MTIyNDEyNDAy\n" +
    "M1owTjELMAkGA1UEBhMCREsxEjAQBgNVBAoTCVRSVVNUMjQwODErMCkGA1UEAxMi\n" +
    "VFJVU1QyNDA4IFN5c3RlbXRlc3QgSVggUHJpbWFyeSBDQTCCAiIwDQYJKoZIhvcN\n" +
    "AQEBBQADggIPADCCAgoCggIBAMgd7UdslIik/4S2EF+i37FaxHOD+tvtJQgeMAei\n" +
    "0kOBFtCuu+tz6uJGWOVDRvh6SyTncdZGAlRKNZAK+ZULUnU1pdB2fbV9rhLF4q0M\n" +
    "BGSgjUd+DpQhUmLi2QLaZvfmmTz4melVewCtYjqCRzPULHetHQKCQIduIhMfR0EE\n" +
    "e38Ooy6PwLEUrYbKyq6rd0Xf2jcSV0srM3INfEULmeWld/kYPI8SH6M/XXiyvhFv\n" +
    "ymAYY3v9XlAWUtTSnJmqs1yU6xpQG1VwRsHQSDvyWmPluGKwELCLWKXK2sNco6Yy\n" +
    "RwNGcnhsjM2kPZ8nhgDJNVFFdd9AjD/qAeex54n+sJHMH/WtmOz9HWeQYrbGO+lW\n" +
    "W/ZXss8Z+KlMzje3pWgxYIhK8OZoRvoUKoLQ1JJH/KjgwcZxuxKzGm7uwoLGHUjg\n" +
    "Yr/1TzJT+sddLTK9TNL2SOwATbg+ueZ7kqIt7Uxih9203b4Y1x1rtIxa7zxtZ4Fc\n" +
    "MvOc8rVfEnanBdhC1nUCThPivf6HrsybD3FG/22FQdq/7ZmcOB2avn4Z1F983Wlc\n" +
    "o6etLHsHfqDy771bMO83aLp/bHBHqOUG7bnNaSegmK5blfEBmYkzAXFaxQnr02LK\n" +
    "7v54dCO8lzBya/06erErdTywSRGLN/+We/h2NVGDokv6remDdAC0XFIs4WrTSvYg\n" +
    "oiP/AgMBAAGjggEpMIIBJTAPBgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB/wQEAwIB\n" +
    "BjARBgNVHSAECjAIMAYGBFUdIAAwga4GA1UdHwSBpjCBozA6oDigNoY0aHR0cDov\n" +
    "L2NybC5zeXN0ZW10ZXN0OS50cnVzdDI0MDguY29tL3N5c3RlbXRlc3Q5LmNybDBl\n" +
    "oGOgYaRfMF0xCzAJBgNVBAYTAkRLMRIwEAYDVQQKEwlUUlVTVDI0MDgxKzApBgNV\n" +
    "BAMTIlRSVVNUMjQwOCBTeXN0ZW10ZXN0IElYIFByaW1hcnkgQ0ExDTALBgNVBAMT\n" +
    "BENSTDEwHwYDVR0jBBgwFoAUAMhRPjg1v23MAbpjBIk5L7AlcdowHQYDVR0OBBYE\n" +
    "FADIUT44Nb9tzAG6YwSJOS+wJXHaMA0GCSqGSIb3DQEBCwUAA4ICAQAk/ghXxPKM\n" +
    "5E/VwViE0UtJQKBzsaCT33Jzqx081Cmt8mfQTEhpVhiE3jMkYYj5kaN0qqHfuvip\n" +
    "mcpjs4qs38lpZGR13XeuHKY5QLEKo7L14DxhmJi3nfBIUMdcplQpvGZFr9zmyWZ3\n" +
    "DUXNdLfKLwXXZHJB5+N3TrOk/11yksibNLEDLpS/tCjYKZI3VKL/6QDdFbR1JjCy\n" +
    "t6hUeCG4Do2SIggst3oiKRcuPYkX6kukm1V5+vY8i0zRd48jKh3oPQFyi5StD1+o\n" +
    "uHYLHDr5UgueC77xJ3ZcVpyToxJjc2mxqovB5r2Zrfs9JdT/iLQDs5kvpkOuZL8F\n" +
    "4yPj3PgNvz1WZkQq/QwlO6EdwoAiLTzWxlnTSQ2XGYEjREkOglrLuRoBWz89ZgMC\n" +
    "xrMfPWbCRyTC6i5MRNmdRKUtqhe/KO2oSuO1RioIO0sTe2tnkiEmIN7kXD92R1KL\n" +
    "JCZB2NFaWOv+yU1GvpER2gXrlvq/yoFuU8g+72BT6UiaCsmr7L1iK7poJKDClS+A\n" +
    "t+5/+gvQRq9BjGtR/q4d3B8xL8Mg58rZbf6FHas6cb0c3e9iVtqSQviXO6VYPQch\n" +
    "X8rjBrXViDvlKXa3fwu6pzhJhJQnsM0jgSV7wEQfoRoTvkXPxwik1xyroV3qKIhx\n" +
    "y3pgq7fDfTxMgVDvMIhjU0+ZQ/DP4ska2g==\n" +
    "-----END CERTIFICATE-----";

         
        const string PreproductionCertificateOcesII =
          "-----BEGIN CERTIFICATE-----\n" +
            "MIIGSDCCBDCgAwIBAgIES+pulDANBgkqhkiG9w0BAQsFADBPMQswCQYDVQQGEwJE\n" +
            "SzESMBAGA1UEChMJVFJVU1QyNDA4MSwwKgYDVQQDEyNUUlVTVDI0MDggU3lzdGVt\n" +
            "dGVzdCBWSUkgUHJpbWFyeSBDQTAeFw0xMDA1MTIwODMyMTRaFw0zNzAxMTIwOTAy\n" +
            "MTRaME8xCzAJBgNVBAYTAkRLMRIwEAYDVQQKEwlUUlVTVDI0MDgxLDAqBgNVBAMT\n" +
            "I1RSVVNUMjQwOCBTeXN0ZW10ZXN0IFZJSSBQcmltYXJ5IENBMIICIjANBgkqhkiG\n" +
            "9w0BAQEFAAOCAg8AMIICCgKCAgEApuuMpdHu/lXhQ+9TyecthOxrg5hPgxlK1rpj\n" +
            "syBNDEmOEpmOlK8ghyZ7MnSF3ffsiY+0jA51p+AQfYYuarGgUQVO+VM6E3VUdDpg\n" +
            "WEksetCYY8L7UrpyDeYx9oywT7E+YXH0vCoug5F9vBPnky7PlfVNaXPfgjh1+66m\n" +
            "lUD9sV3fiTjDL12GkwOLt35S5BkcqAEYc37HT69N88QugxtaRl8eFBRumj1Mw0LB\n" +
            "xCwl21GdVY4EjqH1Us7YtRMRJ2nEFTCRWHzm2ryf7BGd80YmtJeL6RoiidwlIgzv\n" +
            "hoFhv4XdLHwzaQbdb9s141q2s9KDPZCGcgIgeXZdqY1Vz7UBCMiBDG7q2S2ni7wp\n" +
            "UMBye+iYVkvJD32srGCzpWqG7203cLyZCjq2oWuLkL807/Sk4sYleMA4YFqsazIf\n" +
            "V+M0OVrJCCCkPysS10n/+ioleM0hnoxQiupujIGPcJMA8anqWueGIaKNZFA/m1IK\n" +
            "wnn0CTkEm2aGTTEwpzb0+dCATlLyv6Ss3w+D7pqWCXsAVAZmD4pncX+/ASRZQd3o\n" +
            "SvNQxUQr8EoxEULxSae0CPRyGwQwswGpqmGm8kNPHjIC5ks2mzHZAMyTz3zoU3h/\n" +
            "QW2T2U2+pZjUeMjYhyrReWRbOIBCizoOaoaNcSnPGUEohGUyLPTbZLpWsm3vjbyk\n" +
            "7yvPqoUCAwEAAaOCASowggEmMA8GA1UdEwEB/wQFMAMBAf8wDgYDVR0PAQH/BAQD\n" +
            "AgEGMBEGA1UdIAQKMAgwBgYEVR0gADCBrwYDVR0fBIGnMIGkMDqgOKA2hjRodHRw\n" +
            "Oi8vY3JsLnN5c3RlbXRlc3Q3LnRydXN0MjQwOC5jb20vc3lzdGVtdGVzdDcuY3Js\n" +
            "MGagZKBipGAwXjELMAkGA1UEBhMCREsxEjAQBgNVBAoTCVRSVVNUMjQwODEsMCoG\n" +
            "A1UEAxMjVFJVU1QyNDA4IFN5c3RlbXRlc3QgVklJIFByaW1hcnkgQ0ExDTALBgNV\n" +
            "BAMTBENSTDEwHwYDVR0jBBgwFoAUI7pMMZDh08zTG7MbWrbIRc3Tg5cwHQYDVR0O\n" +
            "BBYEFCO6TDGQ4dPM0xuzG1q2yEXN04OXMA0GCSqGSIb3DQEBCwUAA4ICAQCRJ9TM\n" +
            "7sISJBHQwN8xdey4rxA0qT7NZdKICcIxyIC82HIOGAouKb3oHjIoMgxIUhA3xbU3\n" +
            "Putr4+Smnc1Ldrw8AofLGlFYG2ypg3cpF9pdHrVdh8QiERozLwfNPDgVeCAnjKPN\n" +
            "t8mu0FWBS32tiVM5DEOUwDpoDDRF27Ku9qTFH4IYg90wLHfLi+nqc2HwVBUgDt3t\n" +
            "XU6zK4pzM0CpbrbOXPJOYHMvaw/4Em2r0PZD+QOagcecxPMWI65t2h/USbyO/ah3\n" +
            "VKnBWDkPsMKjj5jEbBVRnGZdv5rcJb0cHqQ802eztziA4HTbSzBE4oRaVCrhXg/g\n" +
            "6Jj8/tZlgxRI0JGgAX2dvWQyP4xhbxLNCVXPdvRV0g0ehKvhom1FGjIz975/DMav\n" +
            "kybh0gzygq4sY9Fykl4oT4rDkDvZLYIxS4u1BrUJJJaDzHCeXmZqOhx8She+Fj9Y\n" +
            "wVVRGfxT4FL0Qd3WAtaCVyhSQ6SkZgrPvzAmxOUruI6XhEhYGlP5O8WFETiATxuZ\n" +
            "AJNuKMJtibfRhMNsQ+TVv/ZPr5Swe+3DIQtmt1MIlGlTn4k40z4s6gDGKiFwAYXj\n" +
            "d/kID32R/hJPE41o9+3nd8aHZhBy2lF0jKAmr5a6Lbhg2O7zjGq7mQ3MceNeebuW\n" +
            "XD44AxIinryzhqnEWI+BxdlFaia3U7o2+HYdHw==\n" +
            "-----END CERTIFICATE-----";

        const string ProductionCertificateCampusI =
            "-----BEGIN CERTIFICATE-----\n" +
            "MIIENzCCAx+gAwIBAgIEOwvg0zANBgkqhkiG9w0BAQUFADBHMQswCQYDVQQGEwJE\n" +
            "SzEVMBMGA1UEChMMVERDIEludGVybmV0MSEwHwYDVQQLExhUREMgSW50ZXJuZXQg\n" +
            "Q2xhc3MgSUkgQ0EwHhcNMDEwNTIzMTU0MDE5WhcNMjEwNTIzMTYxMDE5WjBHMQsw\n" +
            "CQYDVQQGEwJESzEVMBMGA1UEChMMVERDIEludGVybmV0MSEwHwYDVQQLExhUREMg\n" +
            "SW50ZXJuZXQgQ2xhc3MgSUkgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEK\n" +
            "AoIBAQD+fovcAqfX/8orBksCy9k/4azJN/KtqUadU016VFjAL6y9W8N9KVQY0aPH\n" +
            "5ZYZ8PrrjYf4P5Uzw4/VmR3fulOD0mKI28qxkdDD7BZ6wmbcFit0QNWvQ3sEBhDA\n" +
            "DItmR9Eh8PTsZkqcOD1I063iQJX/oLCW87U1kW0ViyAsi0+WVH7tHzGfW3KTetPR\n" +
            "kvq0H/xYQs1eztTJ/0Q2wi3Nuts6KH/uw4D610YZrjm2CWQdDTF3abZfalDkOGPh\n" +
            "RJFvtWekBjRp4PTBA4b0LKVahhBqDZdD4KDPvH/x8n/V0SG0GGs9RT/Sznt7FI94\n" +
            "WQwRuCyzqKelbsLEmNfkv9/TS/G5AgMBAAGjggEpMIIBJTARBglghkgBhvhCAQEE\n" +
            "BAMCAAcwaQYDVR0fBGIwYDBeoFygWqRYMFYxCzAJBgNVBAYTAkRLMRUwEwYDVQQK\n" +
            "EwxUREMgSW50ZXJuZXQxITAfBgNVBAsTGFREQyBJbnRlcm5ldCBDbGFzcyBJSSBD\n" +
            "QTENMAsGA1UEAxMEQ1JMMTArBgNVHRAEJDAigA8yMDAxMDUyMzE1NDAxOVqBDzIw\n" +
            "MjEwNTIzMTYxMDE5WjALBgNVHQ8EBAMCAQYwHwYDVR0jBBgwFoAU30ODHe5hxtzn\n" +
            "H+x7a8aNkI5viFkwHQYDVR0OBBYEFN9Dgx3uYcbc5x/se2vGjZCOb4hZMAwGA1Ud\n" +
            "EwQFMAMBAf8wHQYJKoZIhvZ9B0EABBAwDhsIVjUuMDo0LjADAgSQMA0GCSqGSIb3\n" +
            "DQEBBQUAA4IBAQChlJTg+NtSX+R8NCK1dwIcvUoiQbbILJj9J5bxDKeF47sLdhVJ\n" +
            "Ljd9Yv/yN2BFedEcm2xA5ayLJeNKbnrmph/79EWX/slF7KXN6+mws4IGCwozVOTV\n" +
            "tGZ021Cjx5VSz5zZb73qTdCEkE9NuYxJG3xFOpRWqZDVSx4vXLQfiM4SNqSk2MxK\n" +
            "yWmk9PUge5L//YxTpZ+ZG9KOCX1h3aKSYaqn8/sdOTtcvoFBD0Q75r0TM15IQHHV\n" +
            "kjU/g4eqaBJB8yP20HeCTwAPXw9NkEK87eWwJ0NARE43KlcAdoxEYwQap/tGdPUV\n" +
            "ZDxhW5RUKrYO3rYcNBsF052zhPmTUQ+KPsJs\n" +
            "-----END CERTIFICATE-----";

        const string DevelopmentCertificateOcesI =
            "-----BEGIN CERTIFICATE-----\n" +
            "MIIEHDCCAwSgAwIBAgIEPhrz/TANBgkqhkiG9w0BAQUFADA+MQswCQYDVQQGEwJE\n" +
            "SzEMMAoGA1UEChMDVERDMSEwHwYDVQQLExhUREMgT0NFUyBTeXN0ZW10ZXN0IENB\n" +
            "IEkwHhcNMDMwMTA3MTUwNjM4WhcNMzYwMTA3MTUzNjM4WjA+MQswCQYDVQQGEwJE\n" +
            "SzEMMAoGA1UEChMDVERDMSEwHwYDVQQLExhUREMgT0NFUyBTeXN0ZW10ZXN0IENB\n" +
            "IEkwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDPICGMNL7UiANFu6td\n" +
            "YXzWdMI47a7UNVpgeBjVWtsCD72AfG9kYWHxN6rBoil25lIiXpCRkdNJc7aicUi/\n" +
            "mIM2Ye6RnTFzSWKXt/GlnPBC0lSYEZxKGdmmZ10CUlx3FjioLP4hi41rnpWSSBLA\n" +
            "0y2JZADJnueI3Q4S5MsBLUgE9jB9V67BflpttaZmnMf/hkYOuXS3BZwfE3+frhW9\n" +
            "nDYUHuAi57iUptcIxYtAA6reXKe8MJdgqqTv9XaQcbCED5xE61B+M7RbZtFpvDpi\n" +
            "nnpPMQkmwHMr+w9zKaZDRf+rLKtxIb7BkmAUkJhVIoypJHsvvkS5z7a0CHmUzYxm\n" +
            "9gTzAgMBAAGjggEgMIIBHDARBglghkgBhvhCAQEEBAMCAAcwYAYDVR0fBFkwVzBV\n" +
            "oFOgUaRPME0xCzAJBgNVBAYTAkRLMQwwCgYDVQQKEwNUREMxITAfBgNVBAsTGFRE\n" +
            "QyBPQ0VTIFN5c3RlbXRlc3QgQ0EgSTENMAsGA1UEAxMEQ1JMMTArBgNVHRAEJDAi\n" +
            "gA8yMDAzMDEwNzE1MDYzOFqBDzIwMzYwMTA3MTUzNjM4WjALBgNVHQ8EBAMCAQYw\n" +
            "HwYDVR0jBBgwFoAUnVZtJPHUQT1IYYZNL50dWLvMUBwwHQYDVR0OBBYEFJ1WbSTx\n" +
            "1EE9SGGGTS+dHVi7zFAcMAwGA1UdEwQFMAMBAf8wHQYJKoZIhvZ9B0EABBAwDhsI\n" +
            "VjYuMDo0LjADAgSQMA0GCSqGSIb3DQEBBQUAA4IBAQBw6Ed4FO75vnLlZ7+Nx6r9\n" +
            "3pgPJRT7GDzH6pFJrgrALmxI6GhQrII9mtppm5J1mHFyVvMRdSyjutVBlp7JKjvS\n" +
            "h8z+Zg1iQv0l+xQcb6hyFrSfpGbJMg2AUbUf8CejAxc/Gt+b4cU4KTUtpTYXqg8/\n" +
            "Io+uj0483j1FMmB9Oly+Fx38ePTc0dTQPWe9qxftOAn8UEhy8BZVxDeZbQ/J4WO5\n" +
            "W2OA7FLj+H+w82guI8EnDY8RQlUxCLSwcQGWCdt3cwznebBNJw1E8Rxb25AuuATk\n" +
            "Fc7ZNP2Up58a4lIBrI/K2RJ368XehaWpjLqNLf3An7oDPv92uE5ggzReQFUnVODF\n" +
            "-----END CERTIFICATE-----";

        static Dictionary<OcesEnvironment, X509Certificate2> LoadRootCertificates()
        {
            var certificates = new Dictionary<OcesEnvironment, String>
                     {
                       {OcesEnvironment.OcesIDanidEnvProd, ProductionCertificateOcesI},
                       {OcesEnvironment.OcesIDanidEnvSystemtest, SystemTestCertificateOcesI},
                       {OcesEnvironment.OcesIDanidEnvDevelopment, DevelopmentCertificateOcesI},
                       {OcesEnvironment.OcesIiDanidEnvDevelopment, DevelopmentCertificateOcesII},
                       {OcesEnvironment.OcesIIDanidEnvLocalhostTesting, LocalhostTestingCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvInternaltest, InternalTestCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvOperationstest, OperationsTestCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvDevelopmenttest, DevelopmentTestCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvExternaltest, ExternalTestCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvPreprod, PreproductionCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvIgtest, IntegrationTestCertificateOcesII},
                       {OcesEnvironment.OcesIiDanidEnvProd, ProductionCertificateOcesII},
                       {OcesEnvironment.CampusIDanidEnvProd, ProductionCertificateCampusI}
                     };

            var result = new Dictionary<OcesEnvironment, X509Certificate2>();
            foreach (var environment in certificates.Keys)
            {
                result.Add(environment, GenerateCertificate(certificates[environment]));
            }
            return result;
        }

        static X509Certificate2 GenerateCertificate(string certificate)
        {
            var encoding = new ASCIIEncoding();
            return new X509Certificate2(encoding.GetBytes(certificate));
        }

        /// <summary>
        /// Gets root certificate of the given <code>Environment</code>
        /// </summary>
        public static X509Certificate2 LookupCertificate(OcesEnvironment environment)
        {
            if (!TheRootCertificates.ContainsKey(environment))
            {
                throw new ArgumentException("No certificate for: " + environment);
            }
            return TheRootCertificates[environment];
        }

        public static X509Certificate2 LookupCertificateBySubjectDn(X500DistinguishedName subjectDn)
        {
            foreach (var entry in TheRootCertificates)
            {
                if (entry.Value.SubjectName.Decode(X500DistinguishedNameFlags.None).ToLower() == subjectDn.Decode(X500DistinguishedNameFlags.None).ToLower())
                {
                    return entry.Value;
                }
            }
            throw new ArgumentException("No certificate for subjectDn: " + subjectDn.Format(false));
        }

        public static bool HasCertificate(OcesEnvironment environment)
        {
            return TheRootCertificates.ContainsKey(environment);
        }

        /// <summary>
        /// Gets <code>Environment</code> for given <code>CA</code>
        /// </summary>
        public static OcesEnvironment GetEnvironment(Ca ca)
        {
            if (ca == null)
            {
                throw new ArgumentException("Ca is null");
            }
            if (!ca.IsRoot)
            {
                return GetEnvironment(ca.IssuingCa);
            }
            foreach (var e in TheRootCertificates)
            {
                if (e.Value.Equals(ca.Certificate))
                {
                    return e.Key;
                }
            }
            throw new ArgumentException(ca + " is not a known root certificate");
        }
    }
}
