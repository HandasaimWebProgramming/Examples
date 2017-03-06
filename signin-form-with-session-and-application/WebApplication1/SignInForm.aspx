<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignInForm.aspx.cs" Inherits="WebApplication1.SignInForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Sign in Form!</h1>
    <form id="form1" runat="server" action="HandleSignIn.aspx">
    <div>
        <input type="email" name="email" placeholder="E-mail" />
    </div>
    <div>
        <input type="password" name="password" placeholder="Password" />
    </div>
    <div>
        <input type="submit" />
    </div>
    </form>
</body>
</html>
