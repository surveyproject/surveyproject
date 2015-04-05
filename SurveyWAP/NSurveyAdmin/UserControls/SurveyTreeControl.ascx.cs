using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//using Geekees.Common.Controls;
using Goldtect;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using  Votations.NSurvey.Resources;
namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class SurveyTree : System.Web.UI.UserControl
    {
        public delegate void SurveyTreeSelectedNodeDelegate(string selectedNodeValue);
         //JJ after Creating/Deleting a Survey We need to fully refresh
        public bool isTreeStale { get; set; }
        public event SurveyTreeSelectedNodeDelegate OnSelectedNodeChangeHandler;
        public int SurveyId
        {
            get { return ((PageBase)Page).getSurveyId(); }
            set { ((PageBase)Page).SurveyId = value; }
        }
     
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            HtmlGenericControl css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl("~/Scripts/astreeview/astreeview.css"));
            Page.Header.Controls.Add(css);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl("~/Scripts/astreeview/contextmenu.css"));
            Page.Header.Controls.Add(css);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl("~/Scripts/astreeview/themes/macOS/macOS.css"));
            Page.Header.Controls.Add(css);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", Page.ResolveUrl("~/Scripts/astreeview/astreeview.min.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", Page.ResolveUrl("~/Scripts/astreeview/contextmenu.min.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            ASTreeViewTheme macOS = new ASTreeViewTheme();
            macOS.BasePath = "~/Scripts/astreeview/themes/macOS/";
            macOS.CssFile = "macOS.css";
            this.astvMyTree.Theme = macOS;
            this.astvMyTree.EnableTreeLines = false;
            this.astvMyTree.EnableRightToLeftRender = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RebuildTree();
            
        }

        public void RebuildTree()
        {

            if (!IsPostBack || isTreeStale)
                GenerateTree();

            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin )
            {
                AddTreeMenus();
            }
        }
        private void GenerateTree()
        {

            if (((PageBase)Page).NSurveyUser.Identity.UserId == -1 ||
            !(((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveyList) ||((PageBase)Page).NSurveyUser.Identity.IsAdmin)  )
            { astvMyTree.Visible = false; return; }

            astvMyTree.Visible = true;
            FolderData folders;
            folders = new Folders().GetTreeNodes(((PageBase)Page).NSurveyUser.Identity.UserId);
            if (folders.TreeNodes.Count == 0) return;
            ASTreeViewDataTableColumnDescriptor descripter = new ASTreeViewDataTableColumnDescriptor("NodeName", "ItemId", "ParentFolderId");
            this.astvMyTree.DataTableRootNodeValue = null;
            this.astvMyTree.DataSourceDescriptor = descripter;
            this.astvMyTree.DataSource = folders.TreeNodes;
            this.astvMyTree.DataBind();
            string sfId = ((PageBase)Page).SelectedFolderId.HasValue ? "f" + ((PageBase)Page).SelectedFolderId.ToString() : "";
            System.Collections.Hashtable h = (System.Collections.Hashtable)Session["treeCtrl"];

            // set customized icons
            astvMyTree.EnableCustomizedNodeIcon = false;

            this.astvMyTree.TraverseTreeNode(this.astvMyTree.RootNode, delegate(ASTreeViewNode node)
            {
                node.Selected = false;
                if (node.NodeValue.StartsWith("f") && node.ParentNode ==this.astvMyTree.RootNode)
                {
                    node.AdditionalAttributes.Add(
                      new KeyValuePair<string, string>(
                          "disableDelFolder"
                             , "true"));
                }
                if (node.ChildNodes.Count == 0 && node.NodeValue.StartsWith("f"))
                {
                    // add empty node to show folder icon and set to closed state
                    node.AppendChild(new ASTreeViewNode(""));
                    node.EnableOpenClose = false;
                    node.OpenState = ASTreeViewNodeOpenState.Close;
                    if (h != null && h.Contains(node.NodeValue))
                        h.Remove(node.NodeValue);
                }
                else if (node.NodeValue.StartsWith("s"))
                {
                    node.AdditionalAttributes.Add(
                        new KeyValuePair<string, string>(
                            "disableAddFolder"
                            , "true"));
                    node.AdditionalAttributes.Add(
                        new KeyValuePair<string, string>(
                            "disableRenFolder"
                            , "true"));
                    //JJ If the Survey is current set it as selected node
                    if (((PageBase)Page).getSurveyId() == int.Parse(node.NodeValue.Substring(1)))
                        node.Selected = true;
                }

                string nv = string.IsNullOrEmpty(node.NodeValue) ? "" : node.NodeValue;

                //  node.EnableSelection = !string.IsNullOrEmpty(nv);

                //JJ Set Folder as selected only if there is no current survey.If there is current survey it is set as selected
                if (((PageBase)Page).getSurveyId() == -1)
                    if (!string.IsNullOrEmpty(nv) && nv == sfId)
                        node.Selected = true;
            
                if (h != null && h[node.NodeValue] != null)
                    node.OpenState = (ASTreeViewNodeOpenState)h[node.NodeValue];
                else node.OpenState = node.ParentNode == astvMyTree.RootNode ? ASTreeViewNodeOpenState.Open : ASTreeViewNodeOpenState.Close;
            });

            if (string.IsNullOrEmpty(sfId))
                this.astvMyTree.RootNode.ChildNodes[0].Selected = true;

            ASTreeViewNode rootNode = this.astvMyTree.RootNode.ChildNodes[0];
            rootNode.EnableDragDrop = false;

            /// if root node is empty we hide it
            if (rootNode.ChildNodes.Count == 1 && string.IsNullOrEmpty(rootNode.ChildNodes[0].NodeValue))
            {
                // add empty node to show folder icon and set to closed state
                rootNode.EnableOpenClose = false;
                rootNode.OpenState = ASTreeViewNodeOpenState.Close;
            }

            if (h == null) StoreOpenCloseState();



        }

        private void AddTreeMenus()
        {
            ASContextMenuItem item = new ASContextMenuItem(ResourceManager.GetString("TreeAddMenu"), "AddFolder(" + this.astvMyTree.ContextMenuClientID + ".getSelectedItem()" + "); return false;", "AddFolder");
            item.Href = "";
            this.astvMyTree.ContextMenu.MenuItems.Add(item);

            item = new ASContextMenuItem(ResourceManager.GetString("TreeDeleteMenu"), "DelFolder(" + this.astvMyTree.ContextMenuClientID + ".getSelectedItem()" + "); return false;", "DelFolder");
            item.Href = "";
            this.astvMyTree.ContextMenu.MenuItems.Add(item);

            item = new ASContextMenuItem(ResourceManager.GetString("TreeRenameMenu") , "RenFolder(" + this.astvMyTree.ContextMenuClientID + ".getSelectedItem()" + "); return false;", "RenFolder");
            item.Href = "";
            this.astvMyTree.ContextMenu.MenuItems.Add(item);

            this.astvMyTree.EnableNodeSelection = true;
            this.astvMyTree.EnableParentNodeExpand = false;

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }


        void OnSelectionChange(string id)
        {
            if (OnSelectedNodeChangeHandler != null)
            {
                OnSelectedNodeChangeHandler(id);
            }
        }


        public void OnSelectedNodeChange(object src, ASTreeViewNodeSelectedEventArgs e)
        {

            OnSelectionChange(e.NodeValue);

            if (e.NodeValue.StartsWith("f"))
            {
                if (astvMyTree.GetSelectedNode().ParentNode.NodeValue == "root")
                    ((PageBase)Page).SelectedFolderId = null;
                else
                    ((PageBase)Page).SelectedFolderId = int.Parse(e.NodeValue.Substring(1));
                //JJ Unset SurveyId if Folder is Selected
                ((PageBase)Page).SurveyId = -1;
                ((PageBase)Page).SurveyTitle = null;
            }
            else
            {
                ((PageBase)Page).SurveyId = int.Parse(e.NodeValue.Substring(1));

                ((PageBase)Page).SurveyTitle = e.NodeText;
                if (astvMyTree.GetSelectedNode().ParentNode.NodeValue == "root")
                    ((PageBase)Page).SelectedFolderId = null;
                else
                    ((PageBase)Page).SelectedFolderId = int.Parse(astvMyTree.GetSelectedNode().ParentNode.NodeValue.Substring(1));

            }

            StoreOpenCloseState();

            Page.Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.SurveyListHyperLink, ((PageBase)Page).getSurveyId(), (int)((PageBase)Page).MenuIndex));
        }

        private void StoreOpenCloseState()
        {
            System.Collections.Hashtable h = new System.Collections.Hashtable();

            this.astvMyTree.TraverseTreeNode(this.astvMyTree.RootNode, delegate(ASTreeViewNode node)
            {
                h[node.NodeValue] = node.OpenState;
            });

            Session["treeCtrl"] = h;
        }
    }
}