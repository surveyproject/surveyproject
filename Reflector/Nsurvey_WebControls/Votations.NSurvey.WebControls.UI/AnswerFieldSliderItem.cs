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
        private string _slider;

        //add to viewstate test
        protected virtual void _slider_SelectionChanged(object sender, EventArgs e)
        {
            base._fieldTextBox.Text = this.DefaultText;
        }

        protected virtual void AddSlider()
        {
            //this._slider.SelectionChanged += new EventHandler(this._slider_SelectionChanged);
            //this.Controls.Add(this._slider);
        }



        protected override void CreateChildControls()
        {
            // add original jquery .js slider script: http://jqueryui.com/slider/#default / http://api.jqueryui.com/slider/#entry-examples

            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            string SliderScript = "";
            SliderScript =
                "<script>" +
                "$(function () { "
                                //+ "sessionStorage.sessionValue = '20'; "
                                + "var sv =" + answer.Answers[0].SliderValue +";"
                + "$('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider({ orientation: 'horizontal', " 
                + "animate:"+ Convert.ToString(answer.Answers[0].SliderAnimate).ToLower() 
                + ", range:'" +answer.Answers[0].SliderRange+ "',"

                + "value: " + Convert.ToString(answer.Answers[0].SliderValue)

                //+ " if(sv !== null) { value:" + Convert.ToString(answer.Answers[0].SliderValue) +"}"
                //+" else {value: Number(sessionStorage.sessionValue) }" 

                + ", min:"+Convert.ToString(answer.Answers[0].SliderMin) 
                + ", max:"+Convert.ToString(answer.Answers[0].SliderMax) 
                + ", step:"+Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { "
                + "$('#amount" + Convert.ToString(answer.Answers[0].AnswerId) + "').val('' + ui.value); "

                + "sessionStorage.sessionValue = $('#test').val(ui.value); "
                + " }  });  $('#amount" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').val('' + $('#slider-range-min-"+Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider('value')); });"
                + "</script>";

        // second version of jquery .js slider script to correct override "default value" field and keep state:
        // http://stackoverflow.com/questions/16429787/jquery-slider-maintain-value-on-postback

            string SliderScriptState = "";
            SliderScriptState =
                "<script type='text/javascript'>" +
                "$(function () { "
                + "var setViewState = " + this.DefaultText + ";"
                + "$('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider({ orientation: 'horizontal', "
                + "animate:" + Convert.ToString(answer.Answers[0].SliderAnimate).ToLower()
                + ", range:'" + answer.Answers[0].SliderRange + "', value:"
                // + this.DefaultText
                + "setViewState"
                + ", min:" + Convert.ToString(answer.Answers[0].SliderMin)
                + ", max:" + Convert.ToString(answer.Answers[0].SliderMax)
                + ", step:" + Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { "
                + "$('#amount" + Convert.ToString(answer.Answers[0].AnswerId) + "').val('' + ui.value); "
                //+ "$('# " + this.DefaultText + "').val(ui.value); "
                +" }  });  $('#amount" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').val('' + $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider('value')); });"
                + "</script>";


            // determine use on SP or DNN:
            string SiteName = string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["SiteName"]) ? "0" : System.Web.Configuration.WebConfigurationManager.AppSettings["SiteName"];

            // add secundary jquery scripts for non SP sites - DNN SurveyBox module:
            if (SiteName != "Survey Project")
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.core.js", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/Javascript/ui/jquery.ui.core.js"));
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.widget.js", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/Javascript/ui/jquery.ui.widget.js"));
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.mouse.js", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/Javascript/ui/jquery.ui.mouse.js"));
            }

            // add .css files:
           this.Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           HtmlGenericControl css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           if (SiteName == "Survey Project")
           {
                css.Attributes.Add("href", ResolveUrl("~/Content/themes/base/jquery-ui.min.css"));
           }
           else
           {
               css.Attributes.Add("href", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/css/ui-core-base/jquery.ui.all.css"));
           }
           Page.Header.Controls.Add(css);

           css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           if (SiteName == "Survey Project")
           {
                css.Attributes.Add("href", ResolveUrl("~/Content/themes/base/jquery-ui.min.css"));
           }
           else
           {
               css.Attributes.Add("href", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/css/base/jquery.ui.all.css"));
           }
           Page.Header.Controls.Add(css);

           css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           if (SiteName != "Survey Project")
           {
               css.Attributes.Add("href", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/javascript/slider/slider.css"));
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
                    javascriptControl.Attributes.Add("src", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/JavaScript/01_jquery/jquery-1.11.1.js"));
                }

                Page.Header.Controls.Add(javascriptControl);

                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

                javascriptControl = new HtmlGenericControl("script");
                javascriptControl.Attributes.Add("type", "text/Javascript");
                if (SiteName != "Survey Project")
                {
                    javascriptControl.Attributes.Add("src", ResolveUrl("~/Desktopmodules/SurveyBox/Scripts/JavaScript/ui/jquery-ui-1.10.4.js"));
                }
                Page.Header.Controls.Add(javascriptControl);

                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            }

            // choose scriptversion depending on state and default value:
            if (this.DefaultText == answer.Answers[0].DefaultText || !Page.IsPostBack)
            {
            this.Controls.Add(new LiteralControl(SliderScript));
            }
            else if (this.DefaultText == null)
            {
                this.Controls.Add(new LiteralControl(SliderScript));
            }
            else
            {
                this.Controls.Add(new LiteralControl(SliderScriptState));
            }

            // add html to add slideranswer to webpage:
            this.Controls.Add(new LiteralControl("<br /><div class='demo'>"));

            base.CreateChildControls();

            base._fieldTextBox.TextMode = TextBoxMode.SingleLine;
            base._fieldTextBox.Attributes.Add("value", this.DefaultText);
            base._fieldTextBox.Attributes.Add("id", "amount"+Convert.ToString(answer.Answers[0].AnswerId));
            base._fieldTextBox.Attributes.Add("style", "border:0; color:#f6931f; font-weight:bold;");

        //DevTest - check if slidervalue has changed
        //answer.Answers[0].SliderValue. += new EventHandler(this._slider_SelectionChanged);
        //http://stackoverflow.com/questions/25102541/how-to-determine-if-the-slider-is-movedor-its-value-has-changed

            //this.Controls.Add(new LiteralControl("<div id=\"slider-range-min\"></div></div><br />"));                 
            this.Controls.Add(new LiteralControl("<div id=\"slider-range-min-"));
            this.Controls.Add(new LiteralControl(Convert.ToString(answer.Answers[0].AnswerId)));
            this.Controls.Add(new LiteralControl("\"></div></div><br />"));

        }

    }
}

