<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms_Of_Use.aspx.cs" Inherits="LastOneFromScratch.Terms_Of_Use" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>Terms of Service [ToS]</title>
</head>
<body style="background-color:#EBE8E5;">
    <form id="form1" runat="server">
    <div>
      <h2 style=" text-align:center; color:#3269B4;">Terms of Service [ToS]</h2>
    <p style=" text-align: justify;  padding:10px;
line-height: 1.5em;
font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif, Tahoma, Calibri, Verdana,"Bitstream Vera Sans";
font-size: 0.85em;
color: #52463F;">
  

By using proprietary experimental data, supporting documentation or any other information (hereinafter the "Data") provided to you by a body, institute or laboratory within the project "SEISMIC ENGINEERING RESEARCH INFRASTRUCTURES FOR EUROPEAN SYNERGIES" (hereinafter "SERIES"), you agree to be bound by the following terms and conditions, and any policies or amendments thereto that may be subsequently introduced.
All intellectual property rights in the data including, but not limited to, copyright and database rights are vested in their respective right holders (hereinafter the "Providers"). You are authorised - on a non-exclusive basis - to access, extract, reproduce, store, create derivative works and publish the Data on all media without alteration and subject to the provision of the following acknowledgment and disclaimer in all publications containing the Data:
(Acknowledgment)  "The authors would like to thank the data providers and the SERIES Project (funded by the European Community's Seventh Framework Programme [FP7/2007-2013] under grant agreement n° 227887) for giving access to the Data."
(Disclaimer)  "The views expressed herein are those of the author(s) and do not necessarily reflect the official position or interpretation of the data providers. All rights in the data are the property of the respective owners."
THE DATA IS PROVIDED TO THE HIGHEST POSSIBLE QUALITY AVAILABLE ACCORDING TO THE BEST PRACTICE AVAILABLE AT THE TIME OF ITS GENERATION. HOWEVER, YOU EXPRESSLY AGREE THAT THE USE OF THE DATA IS AT YOUR OWN RISK. TO THE MAXIMUM PERMITTED BY LAW, THE PROVIDERS EXPRESSLY DISCLAIM ALL WARRANTIES AND CONDITIONS OF ANY KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO, ANY IMPLIED WARRANTY OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THE ENTIRE RISK AS TO THE USE, QUALITY AND SUITABILITY OF THE DATA REMAINS WITH YOU. THE PROVIDERS WILL NOT BE LIABLE FOR ANY INCIDENTAL, CONSEQUENTIAL, DIRECT OR INDIRECT DAMAGES INCLUDING, BUT NOT LIMITED TO, THE LOSS OF DATA, LOSS OF PROFITS, OR ANY OTHER FINANCIAL LOSS ARISING FROM THE USE OF THE DATA EVEN IF THE POSSIBILITY OF SUCH DAMAGES WERE FORESEEN, FORESEEABLE OR KNOWN BY THE PROVIDERS OR IF THE PROVIDERS WERE ADVISED OF SUCH RISK IN ADVANCE.
ANY REPRODUCTION OR DUPLICATION OF ALL OR ANY PART OF THE SERIES DATABASE IS PROHIBITED. ALL RIGHTS RESERVED. 
</p>
        
        <asp:Button ID="BtnAgree" runat="server" Text="I agree"  onclick="BtnAgree_Click" />
        <asp:Button ID="BtnCancel" runat="server" Text="I disagree"  OnClientClick="self.close();" />
           </div>
    </form>
</body>
</html>
