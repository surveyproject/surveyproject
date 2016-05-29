using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Web;

using Votations.NSurvey.Data;
using Votations.NSurvey.Resources;


namespace Votations.NSurvey.WebControls.UI
{
    /// <summary>
    /// Based on Google Maps Autocomplete for Adresses: 
    /// https://developers.google.com/maps/documentation/javascript/examples/place-details
    /// including Google Map showing location marker
    /// </summary>
    class AnswerFieldAddressItem : AnswerFieldItem
    {

        public HtmlInputText street_number = new HtmlInputText();

        /// <summary>
        /// Create the "layout" and adds the textbox control to the 
        /// control tree
        /// </summary>
        protected override void CreateChildControls()
        {
              
            //script 1.
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));


            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");

            if (Page.Header.FindControl("GoogleMaps") == null)
           { 
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("id", "GoogleMaps");
            //javascriptControl.Attributes.Add("src", "https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places&sensor=false&key=AIzaSyDwh2agwB_vfybUaiuLrl4Hopr9EDX6rUI&language=en");
            javascriptControl.Attributes.Add("src", "https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places&sensor=false&key=" + Resources.AddressManager.GetString("GoogleAddressKey") + "&language=en");

                Page.Header.Controls.Add(javascriptControl);
           }

            // test script 1.: " double check" 

           //String csname = "GoogleMapsScript";
           ////String csurl = "https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places&sensor=false&key=AIzaSyDwh2agwB_vfybUaiuLrl4Hopr9EDX6rUI&language=en";
           //String csurl = "~/Scripts/Javascript/google/Google.js?v=3.exp&libraries=places&sensor=false&key=AIzaSyDwh2agwB_vfybUaiuLrl4Hopr9EDX6rUI&language=en";
           //Type cstype = this.GetType();

           //// Get a ClientScriptManager reference from the Page class.
           //ClientScriptManager cs = Page.ClientScript;

           //// Check to see if the include script exists already.
           //if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
           //{
           //    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
           //}


            
           //script 2.
           
           Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           //HtmlGenericControl javascriptControl = new HtmlGenericControl("script");

           javascriptControl = new HtmlGenericControl("script");

           javascriptControl.Attributes.Add("type", "text/Javascript");
           javascriptControl.Attributes.Add("name", "SearchAddress");
           javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/Javascript/google/SearchAddress.js"));

           Page.Header.Controls.Add(javascriptControl);

           //script 4.: add onload to body tag of html page:
           //HtmlGenericControl body = (HtmlGenericControl)
           //Page.FindControl("TestBody");
           //body.Attributes.Add("onload", "initialize()");


           // add .css files:
           this.Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           HtmlGenericControl css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");

           css.Attributes.Add("href", ResolveUrl("~/Scripts/Javascript/google/SearchAddress.css"));

           Page.Header.Controls.Add(css);

            // add the html

           base.CreateChildControls();
           
           this.Controls.Add(new LiteralControl(textHtmlOne));

          //input field 'autocomplete' to enter address and select google result:

           HtmlInputText input = new HtmlInputText();
           input.Attributes["id"] = "autocomplete";
           input.Attributes["placeholder"] = "Please enter your address";
           input.Attributes["runat"] = "server";

           if (base._fieldTextBox.Text.Length != 0)
           {
               input.Value = string.Empty;
               input.Value = base._fieldTextBox.Text;
           }
           else
           {
               input.Value = this.DefaultText;
           }

           input.Size =  100;
           input.Attributes["type"] = "text";
           this.Controls.Add(input);


          //base._fieldTextBox set as hidden to save address answer results:

            if (input.Value.Length != 0)
            {
                base._fieldTextBox.Text = string.Empty;
                base._fieldTextBox.Text = input.Value;
            }
            else
            {
                base._fieldTextBox.Text = this.DefaultText;
            }

            base._fieldTextBox.Width = Unit.Percentage(100);
            base._fieldTextBox.Attributes["id"] = "hiddenFullAddress";
            base._fieldTextBox.Attributes["type"] = "hidden";
            
           this.Controls.Add(new LiteralControl(textHtmlThree));
           this.Controls.Add(new LiteralControl(textHtmlTwo));

           this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));

        }

        /// <summary>
        /// Returns	the default answer text that was 
        /// filled with the correct template value specified
        /// by the user
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection userAnswers = this.GetUserAnswers();
            this.OnAnswerPublished(new AnswerItemEventArgs(userAnswers));
            return userAnswers;
        }

        /// <summary>
        /// Returns the answeritem user's answers
        /// </summary>
        protected virtual PostedAnswerDataCollection GetUserAnswers()
        {
            PostedAnswerDataCollection datas = null;
            if (this._fieldTextBox.Text.Length != 0 )
            {
                datas = new PostedAnswerDataCollection();
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._fieldTextBox.Text , AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.Field));
            }
            return datas;
        }
        
        //html including address input fields and javascript functions:

        private string textHtmlOne = "<div id='locationField'>";

        private string textHtmlThree = "</div>";
        
        private string textHtmlTwo =
"<div id='panel'>" +
"<table id='address'><tr><td class='label'>Street address</td><td class='slimField'><input class='field' readonly='readonly' id='street_number' disabled='disabled' runat='server' /></td>" +
"<td class='wideField' colspan='2'><input class='field' id='route' readonly='readonly' disabled='disabled' /></td></tr>" +
"<tr><td class='label'>City</td><td class='wideField' colspan='3'><input class='field' readonly='readonly' id='locality' disabled='disabled' /></td></tr>" +
"<tr><td class='label'>State</td><td class='slimField'><input class='field' readonly='readonly' id='administrative_area_level_1' disabled='disabled' /></td>" +
"<td class='label'>Zip code</td><td class='wideField'><input class='field' readonly='readonly' id='postal_code' disabled='disabled' /></td></tr>" +
"<tr><td class='label'>Country</td><td class='wideField' colspan='3'><input class='field' readonly='readonly' id='country' disabled='disabled' /></td></tr></table><br />" +
"<input type='button' Class='btn btn-primary btn-xs bw' value='Show on Map' onclick='codeAddress()' /><input onclick='deleteMarkers(); initialize()' Class='btn btn-primary btn-xs bw' type='button' value='Clear All'/>" +
"<br /><br /><div id='map-canvas'></div></div>";

    }
}
