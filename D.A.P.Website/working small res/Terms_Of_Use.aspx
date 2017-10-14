<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms_Of_Use.aspx.cs" Inherits="LastOneFromScratch.Terms_Of_Use" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>
Terms and Conditions of Download</p>
        
        <asp:Button ID="BtnAgree" runat="server" Text="I agree"  onclick="BtnAgree_Click" />
        <asp:Button ID="BtnCancel" runat="server" Text="I disagree"  OnClientClick="self.close();" />
           </div>
    </form>
</body>
</html>
