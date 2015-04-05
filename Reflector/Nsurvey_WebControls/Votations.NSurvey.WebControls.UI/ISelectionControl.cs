namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Internal ... Used by selection item to allow 
    /// the parent control to get selected state of the
    /// control that implements it
    /// </summary>
    public interface ISelectionControl
    {
        bool Selected { get; }

        string UniqueID { get; }
    }
}

