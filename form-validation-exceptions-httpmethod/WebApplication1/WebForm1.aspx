<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function validate() {
            var name = document.getElementById("name").value;
            if (name.length < 8) {
                alert("Error");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
<p><%= message %></p>
    <form id="form1" runat="server" action="WebForm1.aspx" onsubmit="return validate();">
    <div>
    <input type="text" name="name" id="name" />
    </div>
    <div>
    <input type="submit" />
    </div>
    </form>
</body>
</html>
