namespace Votations.NSurvey.WebControls.UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.Globalization;
    using System.Web.UI.HtmlControls;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Votations.NSurvey;
    using Votations.NSurvey.Resources;

    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Web;
    using Votations.NSurvey.WebControls.UI;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// AnswerType example that inherits a textbox behaviors and adds a slider
    /// to it; jquery scripts used to add slider;
    /// </summary>
    public class AnswerFieldSliderItem : AnswerFieldItem
    {

        protected virtual void AddSliderScript()
        {
            // add original jquery .js slider script: http://jqueryui.com/slider/#default / http://api.jqueryui.com/slider/#entry-examples

            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            string SliderScript =
                "<script id='s2'>"
                + "$(function () { "

+ " function setValue(){ "

+ "var ftbvalue = String( $('#" + base._fieldTextBox.ClientID + "').val());"

+ " if(ftbvalue!='' && $('#" + base._fieldTextBox.ClientID + "').val() != " + answer.Answers[0].SliderValue + " )"
+ " {value = $('#" + base._fieldTextBox.ClientID + "').val(); }"
+ " else {var value = " + Convert.ToString(answer.Answers[0].SliderValue) + "; }"
//+ " alert(ftbvalue);"
+ " return value; } "

                + "$('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId) + "').slider({"
                + "  orientation: 'horizontal' "
                + ", animate:" + Convert.ToString(answer.Answers[0].SliderAnimate).ToLower()
                + ", range:'" + answer.Answers[0].SliderRange + "'"
                + ", value: setValue()"
                + ", min:" + Convert.ToString(answer.Answers[0].SliderMin)
                + ", max:" + Convert.ToString(answer.Answers[0].SliderMax)
                + ", step:" + Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { $('#" + base._fieldTextBox.ClientID + "').val('' + ui.value); }"
                + "  }); " 

                + "$('#" + base._fieldTextBox.ClientID + "').val('' + $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId) + "').slider('value')); " 

                + "});"
                + "</script>";

            this.Controls.Add(new LiteralControl(SliderScript));
        }


        protected virtual void AddSliderSecondScript()
        {
            //this._slider.SelectionChanged += new EventHandler(this._slider_SelectionChanged);

            // add original jquery .js slider script: http://jqueryui.com/slider/#default / http://api.jqueryui.com/slider/#entry-examples

            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            string SliderScript =
                "<script id='s2b'>" +
                "$(function () { $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider({ orientation: 'horizontal' "
                + ", animate:" + Convert.ToString(answer.Answers[0].SliderAnimate).ToLower()
                + ", range:'" + answer.Answers[0].SliderRange + "'"
                + ", value: $('#" + base._fieldTextBox.ClientID + "').val();"
                + ", min:" + Convert.ToString(answer.Answers[0].SliderMin)
                + ", max:" + Convert.ToString(answer.Answers[0].SliderMax)
                + ", step:" + Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { $('#" + base._fieldTextBox.ClientID + "').val('' + ui.value); }  });  "
                + "$('#" + base._fieldTextBox.ClientID + "').val('' + $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId) + "').slider('value')); });"
                + "</script>";

            this.Controls.Add(new LiteralControl(SliderScript));
        }



        protected virtual void AddSliderStateScript()
        {
            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            // second version of jquery .js slider script to correct override "default value" field and keep state:
            // http://stackoverflow.com/questions/16429787/jquery-slider-maintain-value-on-postback

            string SliderScriptState =
                "<script id='s3'>" +
                "$(function () { $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider({ orientation: 'horizontal' "
                + ", animate:" + Convert.ToString(answer.Answers[0].SliderAnimate).ToLower()
                + ", range:'" + answer.Answers[0].SliderRange + "'"
                + ", value:" + Convert.ToString(this.DefaultText)
                + ", min:" + Convert.ToString(answer.Answers[0].SliderMin)
                + ", max:" + Convert.ToString(answer.Answers[0].SliderMax)
                + ", step:" + Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { $('#" + base._fieldTextBox.ClientID + "').val('' + ui.value); }  });  "
                + "$('#" + base._fieldTextBox.ClientID + "').val('' + $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId) + "').slider('value')); });"
                + "</script>";
            this.Controls.Add(new LiteralControl(SliderScriptState));

        }

        protected virtual void AddSliderCssJsFiles()
        {

            // determine use on SP or DNN:
            string SiteName = string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["SiteName"]) ? "0" : System.Web.Configuration.WebConfigurationManager.AppSettings["SiteName"];

            // add secundary jquery scripts for non SP sites - DNN SurveyBox module:
            //if (SiteName != "Survey Project")
            //{
            //    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.core.js", ResolveUrl(GlobalConfig.ScriptFilesPath + "/Javascript/ui/jquery.ui.core.js"));
            //    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.widget.js", ResolveUrl(GlobalConfig.ScriptFilesPath + "/Javascript/ui/jquery.ui.widget.js"));
            //    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.mouse.js", ResolveUrl(GlobalConfig.ScriptFilesPath + "/Javascript/ui/jquery.ui.mouse.js"));
            //}

            // add .css files:
            this.Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            HtmlGenericControl css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");

            if (SiteName == "Survey Project")
            {
                css.Attributes.Add("href", ResolveUrl(GlobalConfig.ContentFilesPath + "themes/base/base.css"));
            }
            else
            {
                css.Attributes.Add("href", ResolveUrl(GlobalConfig.ContentFilesPath + "themes/base/base.css"));
               // css.Attributes.Add("href", ResolveUrl(GlobalConfig.ScriptFilesPath + "/Scripts/css/ui-core-base/jquery.ui.all.css"));
            }
            Page.Header.Controls.Add(css);

            css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            if (SiteName != "Survey Project")
            {
                css.Attributes.Add("href", ResolveUrl(GlobalConfig.ContentFilesPath + "/themes/base/slider.css"));
            }
            Page.Header.Controls.Add(css);


            // add .js scripts if survey not taken inside SP tool:
            if (this.Page.Request.Path.Contains("survey.aspx"))
            {
                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

                HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
                javascriptControl.Attributes.Add("type", "text/Javascript");

                if (SiteName != "Survey Project")
                {
                    javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/jquery-3.3.1.min.js"));
                }

                Page.Header.Controls.Add(javascriptControl);

                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

                javascriptControl = new HtmlGenericControl("script");
                javascriptControl.Attributes.Add("type", "text/Javascript");
                if (SiteName != "Survey Project")
                {
                    javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/jquery-ui-1.12.1.min.js"));
                }
                Page.Header.Controls.Add(javascriptControl);

                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            }

        }

        protected virtual void AddSliderScriptChoice()
        {
            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);
 
            // choose scriptversion depending on state and default value:
            if (!Page.IsPostBack || String.IsNullOrEmpty(base._fieldTextBox.Text))
            {
                //this.Controls.Add(new LiteralControl(SliderScript));
                this.AddSliderScript();
            }
            else if (!Page.IsPostBack && !String.IsNullOrEmpty(base._fieldTextBox.Text))
            {
                //this.Controls.Add(new LiteralControl(SliderScript));
                this.AddSliderSecondScript();
            }
            else if(!String.IsNullOrEmpty(DefaultText))
            {
                //this.Controls.Add(new LiteralControl(SliderScriptState));
                this.AddSliderStateScript();
            }

        }


        protected virtual void AddSliderDiv()
        {
            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            this.Controls.Add(new LiteralControl("<div id=\"slider-range-min-"));
            this.Controls.Add(new LiteralControl(Convert.ToString(answer.Answers[0].AnswerId)));
            this.Controls.Add(new LiteralControl("\"></div><br />"));

            base._fieldTextBox.TextMode = TextBoxMode.SingleLine;
           // base._fieldTextBox.Attributes.Add("value", this.DefaultText);
           //base._fieldTextBox.Attributes.Add("id", "amount" + Convert.ToString(answer.Answers[0].AnswerId));
            base._fieldTextBox.Attributes.Add("style", "border:0; color:#f6931f; font-weight:bold;");
            base._fieldTextBox.Attributes.Add("readonly", "true");

        }


        protected override void CreateChildControls()
        {                     

             // add html to add slideranswer to webpage:
            // this.Controls.Add(new LiteralControl("<br /><div class='demo'>"+ DefaultText + "</div>"));

            base.CreateChildControls();

            this.AddSliderScriptChoice();

            //this.AddSliderCssJsFiles();
            this.AddSliderDiv();

        }

    }
}

