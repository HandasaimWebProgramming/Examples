<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Welcome to my site!</h1>
    <h2><%= helloMessage %></h2>
    <h3>We have <%= Application["EntryCount"] %> entries</h3>
    <a href="SignInForm.aspx">Sign In</a>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
