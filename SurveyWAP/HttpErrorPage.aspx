<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HttpErrorPage.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.HttpErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey&trade; Project Error Page</title>
</head>
<body>
<form id="form1" runat="server">


  <div style=" font-family:Tahoma; 
      font-size:small; 
      width:1020px; 
      background-color: #e2e2e2; 
      padding:35px; 
      -webkit-border-radius: 7px;
      -moz-border-radius: 7px;
      border-radius: 7px;
  ">

            <div class="topCell"
            style=" left:-4px;
                    top:-3px;
                    position: relative;
                    padding:0px 13px 2px 0px; 
                    border: 0px; 
                    border-top-style: none;
                    border-left-style: none;
                    border-bottom-style: none;
                    border-right-style: none;
                    border-color: #ffffff;
                    
          
            "> 
            
                <a href="<%= Page.ResolveUrl("~")%>default.aspx" title="Survey&#8482; Project Homepage" target="_self">
                 <img src="<%= Page.ResolveUrl("~")%>Images/logotest.jpg" alt="logo" border="0" />
                </a>

            </div>  
  
  <br /><br />
    <h2>Survey&trade; Project Error Page</h2>
    <br />An invalid request was made.
    For safety reasons the request has been redirected to this error page.<br /><br />
    To try again please return to the <a href='Default.aspx'>Default Page</a>
    <br /><br /><br /><br /><br />
    <hr style="color:#e2e2e2;"/><br /><br />&copy; Fryslan Webservices&trade; 2013
  </div>
  </form>
</body>
</html>
