using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.Adapters;
using System.Web.UI;
using System.Design;
using System.Web.UI.WebControls;


namespace Votations.NSurvey.WebAdmin
{
    public class CheckBoxCtrlAdapter : ControlAdapter
    {

        protected override void Render(HtmlTextWriter writer)
        {

            // revive the control

            WebControl wctrlCheckBox = ((WebControl)Control);

            // revive the Tooltip
            string toolTip = wctrlCheckBox.ToolTip;

            // add title attribute to the control
            writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);

            // remove content of the tooltip, if it is different from empty string, asp.net will render
            // wrapping <span> with tooltip on it

            wctrlCheckBox.ToolTip = "";

            base.Render(writer);

        }

    }
}
