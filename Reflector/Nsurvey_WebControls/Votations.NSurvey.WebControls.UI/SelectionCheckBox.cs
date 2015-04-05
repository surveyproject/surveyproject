namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    [ToolboxItem(false)]
    public class SelectionCheckBox : CheckBox, ISelectionControl
    {
        public bool Selected
        {
            get
            {
                return base.Checked;
            }
        }

        public override string UniqueID
        {
            get
            {
                return base.UniqueID;
            }
        }
    }
}

