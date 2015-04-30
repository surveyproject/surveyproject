using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
//using Geekees.Common.Controls;
using Goldtect;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class AjaxWebMethodsProxy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]


        public static void TreeViewOpenClose(string FolderId, string State)
        {
            System.Collections.Hashtable h = (System.Collections.Hashtable) HttpContext.Current.Session["treeCtrl"];
            if (h == null) return;
            h[FolderId] =(ASTreeViewNodeOpenState) Enum.Parse(typeof(ASTreeViewNodeOpenState), State);

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]


        public static string TreeViewAddFolder(int SelectedFolderId, string name)
        {
            try
            {

                new Folders().AddFolder(SelectedFolderId, name);
                return null;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message == "DUPLICATEFOLDER") return ex.Message;
                throw;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static void TreeViewDelFolder(string SelectedFolderId)
        {
            int id = int.Parse(SelectedFolderId.Substring(1),System.Globalization.CultureInfo.InvariantCulture);

            if (SelectedFolderId.StartsWith("f"))
                new Folders().DeleteFolderById(id);
            else
            {

                new Survey().DeleteSurveyById(id);
                HttpContext.Current.Session[SessionParameters.SurveyId.ToString()] = -1;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static string TreeViewUpdateFolder(int? ParentFolderId, int SelectedFolderId, string name)
        {
            if (ParentFolderId == 0)
                ParentFolderId = null;
            try
            {
                new Folders().UpdateFolder(ParentFolderId, SelectedFolderId, name);
                return null;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message == "DUPLICATEFOLDER") return ex.Message;
                throw;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object TreeViewMoveItem(string ParentFolderId, string ItemId)
        {
            try
            {

                int id = int.Parse(ItemId.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
                int? pid = (string.IsNullOrEmpty(ParentFolderId)) ? (int?)null : int.Parse(ParentFolderId.Substring(1), System.Globalization.CultureInfo.InvariantCulture);

                // skip move to a list
                if (ParentFolderId.StartsWith("s"))
                    return new { Status = false, Message = "TREECANNOTMOVE" };

                if (ItemId.StartsWith("f"))
                {
                    FolderData fd = new Folders().GetFolderById(id);
                    FolderData.TreeNodesRow tn = (FolderData.TreeNodesRow)fd.TreeNodes.Rows[0];

                    if ((tn.ParentFolderId != null) && pid.HasValue)
                        new Folders().MoveFolder(pid, id);
                    else
                        return new { Status = false, Message = "TREECANNOTMOVE" };
                }
                else if (ItemId.StartsWith("s"))
                {
                    Surveys srv = new Surveys();

                    int sid;
                    if (int.TryParse(ItemId.Substring(1), out sid))
                    {
                        srv.SetFolderId(pid, sid);
                    }
                }
                return new { Status = true, Message = string.Empty };
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message == "DUPLICATEFOLDER")   return new { Status = false, Message = ex.Message };
                throw;
            }
       
        }
    }
}
