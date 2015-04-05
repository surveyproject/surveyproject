namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Summary description for SelectionRadioButton.
    /// </summary>
    [ToolboxItem(false)]
    public class SelectionRadioButton : GlobalRadioButton, ISelectionControl
    {
        public bool Selected
        {
            get
            {
                return this.Checked;
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

