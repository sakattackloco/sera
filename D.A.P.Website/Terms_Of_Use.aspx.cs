using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace LastOneFromScratch
{
    public partial class Terms_Of_Use : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnAgree_Click(object sender, EventArgs e)
        {
            string id = Request.Params["localid"];
            string cat = Request.Params["category"];
            string dom = Request.Params["lab"];
            string par = Request.Params["status"];
            string ip = Request.Params["ip"];
            string port = Request.Params["port"];
            string downloadComponentName = Request.Params["downloadComponentName"];
            string secured = Request.Params["secured"];

            string rqd = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
            UTF8Encoding ByteConverter = new UTF8Encoding();
            // Create the data to be signed
            string DataToBeSigned = rqd + id + cat + dom + par;
            byte[] dataBytes = ByteConverter.GetBytes(DataToBeSigned);
            //data to be signed
            byte[] signedData = null;


            //string DigitalCertificateName = "E=ioannid@ceid.upatras.gr, CN=150.140.188.205, OU=Human Computer Interaction, O=University Of Patras, L=Patra, S=Akhaia, C=GR";
            // string DigitalCertificateName = "E=ioannid@ceid.upatras.gr, CN=150.140.188.205, OU=Upatras, O=HCI Group, L=Patra, S=Achaia, C=GR";
            string DigitalCertificateName = "CN=Central_Site_Download";

            //load the certificate
            //X509Certificate2 CurentCert = new X509Certificate2//(@"C:\code\SignData.pfx");//X509Store(StoreName.My, StoreLocation.LocalMachine);
            //string DigitalCertificateName = "E=ioannid@ceid.upatras.gr, CN=150.140.188.205, OU=Upatras, O=Upatras, L=patra, S=patra, C=gr";


            X509Store my = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            RSACryptoServiceProvider csp = null;
            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.Subject.Contains(DigitalCertificateName))
                {
                    // We found it. 
                    // Get its associated CSP and private key
                    csp = (RSACryptoServiceProvider)cert.PrivateKey;
                }
            }
            if (csp == null)
            {
                throw new Exception("No valid cert was found");
            }


            //load the private key
            //csp = (RSACryptoServiceProvider)cert.PrivateKey;
            //sign the data with private key
            RSACryptoServiceProvider RSA = csp;
            //sign the data with private key
            signedData = RSA.SignData(dataBytes, new SHA1CryptoServiceProvider());
           
            //convert to base64
            string signature = System.Convert.ToBase64String(signedData);
            //fix it so as to be ok for web transimition
            signature = System.Web.HttpUtility.UrlEncode(signature);

            string urlPart = @""+ secured + "://" + ip + ":"+port+"/" + HttpUtility.UrlEncode(downloadComponentName) + "/Download?";
            string FinalUrl = urlPart + "rqd=" + HttpUtility.UrlEncode(rqd) + "&id=" + HttpUtility.UrlEncode(id) + "&cat=" + HttpUtility.UrlEncode(cat) + "&dom=" + HttpUtility.UrlEncode(dom) + "&par=" + HttpUtility.UrlEncode(par) + "&signature=" + signature;
            //FinalUrl = FinalUrl.ToLower();

           //Response.Redirect(FinalUrl);
            //Response.Write("<script type='text/javascript'>detailedresults='self.close';</script>");

            Response.Write("<script type='text/javascript'>detailedresults=window.open('" + FinalUrl + "');parent.close();</script>");


        }
    }
}
