<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Hello!</h1>
    <u><%= s %></u>
    <form id="form1" runat="server" action="WebForm2.aspx">
    <div>
        <input type="text" placeholder="Enter some text" name="something" />
        <br />
        <input type="submit" />
    </div>
    </form>
</body>
</html>
