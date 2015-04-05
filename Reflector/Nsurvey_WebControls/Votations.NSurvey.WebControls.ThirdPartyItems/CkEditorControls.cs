using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CKEditor;
using Votations.NSurvey.Data;
using Votations.NSurvey.Resources;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.WebControls.ThirdPartyItems;

namespace Votations.NSurvey.WebControls.ThirdPartyItems
{

    public class CkEditorControls
    {
        // CKeditor Skin options:

        public enum CkeToolbarStyleConfiguration
        {
            Moono = 0,
            MoonoColor = 1,
            Kama = 2,

        }

        public enum CkeToolbarLayoutConfiguration
        {
            Full = 0,
            Basic = 1,

        }

        public enum CkeBreakModeConfiguration
        {
            P = 1,
            BR = 2,
            DIV = 3,
        }


    }
}
