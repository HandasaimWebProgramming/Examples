using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class index : System.Web.UI.Page
    {
        public string helloMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserFullName"] == null)
                helloMessage = "Hello Guest!";
            else
                helloMessage = "Hello " + Session["UserFullName"] + "!";
            if (Application["EntryCount"] == null)
                Application["EntryCount"] = 1;
            else
                Application["EntryCount"] =  (int)Application["EntryCount"] + 1;
        }
    }
}