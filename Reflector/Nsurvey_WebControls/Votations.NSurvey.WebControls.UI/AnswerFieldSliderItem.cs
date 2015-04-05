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
        protected override void CreateChildControls()
        {
            // add original jquery .js slider script:

            AnswerData answer = new Answers().GetAnswerById(AnswerId, LanguageCode);

            string SliderScript = ""; 
            SliderScript = 
                "<script type='text/javascript'>"+
                "$(function () { $('#slider-range-min-"+Convert.ToString(answer.Answers[0].AnswerId)
                +"').slider({ orientation: 'horizontal', " 
                + "animate:"+ Convert.ToString(answer.Answers[0].SliderAnimate).ToLower() 
                + ", range:'" +answer.Answers[0].SliderRange+ "', value:"
                + Convert.ToString(answer.Answers[0].SliderValue)
                + ", min:"+Convert.ToString(answer.Answers[0].SliderMin) 
                + ", max:"+Convert.ToString(answer.Answers[0].SliderMax) 
                + ", step:"+Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { $('#amount"+Convert.ToString(answer.Answers[0].AnswerId)
                + "').val('' + ui.value); }  });  $('#amount"+Convert.ToString(answer.Answers[0].AnswerId)
                +"').val('' + $('#slider-range-min-"+Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider('value')); });"
                + "</script>";

            // second version of jquery .js slider script to correct override "default value" field and keep state:

            string SliderScriptState = "";
            SliderScriptState =
                "<script type='text/javascript'>" +
                "$(function () { $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider({ orientation: 'horizontal', "
                + "animate:" + Convert.ToString(answer.Answers[0].SliderAnimate).ToLower()
                + ", range:'" + answer.Answers[0].SliderRange + "', value:"
                + this.DefaultText
                + ", min:" + Convert.ToString(answer.Answers[0].SliderMin)
                + ", max:" + Convert.ToString(answer.Answers[0].SliderMax)
                + ", step:" + Convert.ToString(answer.Answers[0].SliderStep)
                + ", slide: function (event, ui) { $('#amount" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').val('' + ui.value); }  });  $('#amount" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').val('' + $('#slider-range-min-" + Convert.ToString(answer.Answers[0].AnswerId)
                + "').slider('value')); });"
                + "</script>";
            
            // add secundary jquery scripts:
           this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.core.js", ResolveUrl("~/Scripts/Javascript/ui/jquery.ui.core.js") );
           this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.widget.js", ResolveUrl("~/Scripts/Javascript/ui/jquery.ui.widget.js"));
           this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquery.ui.mouse.js", ResolveUrl("~/Scripts/Javascript/ui/jquery.ui.mouse.js"));


            // add .css files:
           this.Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           HtmlGenericControl css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           css.Attributes.Add("href", ResolveUrl("~/Scripts/css/ui-core-base/jquery.ui.all.css"));
           Page.Header.Controls.Add(css);

           css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           //css.Attributes.Add("href", ResolveUrl("~/Scripts/css/custom-theme/jquery-ui-1.8.12.custom.css"));
           css.Attributes.Add("href", ResolveUrl("~/Scripts/css/base/jquery.ui.all.css"));
           Page.Header.Controls.Add(css);

           css = new HtmlGenericControl("link");
           css.Attributes.Add("rel", "stylesheet");
           css.Attributes.Add("type", "text/css");
           css.Attributes.Add("href", ResolveUrl("~/Scripts/javascript/slider/slider.css"));
           Page.Header.Controls.Add(css);


            // add .js scripts if survey not taken inside SP tool:
           if (this.Page.Request.Path.Contains("survey.aspx"))
            {
           Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
           javascriptControl.Attributes.Add("type", "text/Javascript");
           javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/JavaScript/01_jquery/jquery-1.11.1.js"));
           Page.Header.Controls.Add(javascriptControl);

           Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           javascriptControl = new HtmlGenericControl("script");
           javascriptControl.Attributes.Add("type", "text/Javascript");
           javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/JavaScript/ui/jquery-ui-1.10.4.js"));
           Page.Header.Controls.Add(javascriptControl);

           Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            }

           //if (this.Page.Request.Path.Contains("surveymobile.aspx"))
           //{
           //    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

           //    HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
           //    javascriptControl.Attributes.Add("type", "text/Javascript");
           //    javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/JavaScript/ui/jquery-ui-1.10.4.js"));
           //    Page.Header.Controls.Add(javascriptControl);

           //    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
           //}


            // choose scriptversion depending on state and default value:
            if(this.DefaultText == answer.Answers[0].DefaultText )
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

            //this.Controls.Add(new LiteralControl("<div id=\"slider-range-min\"></div></div><br />"));                 
            this.Controls.Add(new LiteralControl("<div id=\"slider-range-min-"));
            this.Controls.Add(new LiteralControl(Convert.ToString(answer.Answers[0].AnswerId)));
            this.Controls.Add(new LiteralControl("\"></div></div><br />"));

        }
        
    }
}

