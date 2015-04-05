<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyTreeControl.ascx.cs"
    Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.SurveyTree" %>
<%@ Register Assembly="Goldtect.ASTreeView" Namespace="Goldtect" TagPrefix="ct" %>
<script type="text/javascript">

    function GetFolderId(val) {
        var id;

        if (val) {
            var prefix = val.substring(0, 1);
            id = val.substring(1);
        }

        return id;
    }

    function IsFolder(val) {
        if (val) {
            var prefix = val.substring(0, 1);
            if (prefix != "s")
                return true;
        }

        return false;
    }

    function DelFolder(element) {

        var id = element.parentNode.getAttribute('treeNodeValue');

        var pos = $("#" + element.id).offset();
        var p = [pos.left, pos.top];
        var title = '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeDeleteTitle") %>';
        if (IsFolder(id))
            $('#delText').text('<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeDeleteFolderWarn") %>'.replace("@REPLACE@", $("#" + element.id).text()));
        else {
            title = '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeDeleteSurvey") %>';
            $('#delText').text('<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeDeleteSurveyWarn") %>'.replace("@REPLACE@", $("#" + element.id).text()));
        }

        $('#DelFolderDlg').dialog({
            height: 180,
            width: 200,
            modal: true,
            position: p,
            autoOpen: true,
            title: title,
            buttons: {
                '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeDeleteMenu") %>': function () {
                    $(this).dialog("close");
                    DeleleteFolder(id);
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }

    function AddFolder(element) {
        var id;

        if (IsFolder(element.parentNode.getAttribute('treeNodeValue'))) {
            id = GetFolderId(element.parentNode.getAttribute('treeNodeValue'));
        }
        else {
            id = GetFolderId(element.parentNode.parentNode.getAttribute('treeNodeValue'));
        }

        var name = "";
        var pos = $("#" + element.id).offset();
        var p = [pos.left, pos.top];

        $('#AddFolderDlg').dialog({
            height: 170,
            width: 200,
            modal: true,
            position: p,
            autoOpen: true,
            title: '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeAddTitle") %>',
            buttons: {
                '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeOkMenu") %>': function () {
                    $(this).dialog("close");
                    name = $("#folderName").val();
                    $("#folderName").val("");
                    SaveNewFolder(id, name);
                },
                '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeCancelMenu") %>': function () {
                    $("#folderName").val("");
                    $(this).dialog("close");
                }
            }
        });
    }

    function SaveNewFolder(id, name) {
        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/NSurveyAdmin/AjaxWebMethodsProxy.aspx/TreeViewAddFolder") %>',
            data:
             "{SelectedFolderId:" + id +
              ", name:'" + name +
             "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "DUPLICATEFOLDER") { alert('<%= Votations.NSurvey.Resources.ResourceManager.GetString("DUPLICATEFOLDER") %>'); return; }
                window.location.replace(window.location.href);
            }
        });
    }

    function DeleleteFolder(id) {

        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/NSurveyAdmin/AjaxWebMethodsProxy.aspx/TreeViewDelFolder") %>',
            data:
             "{SelectedFolderId:'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                window.location.replace(window.location.href);
            }
        });
    }

    function UpdateFolder(id, pid, name) {
        if (!pid)
            pid = 0;

        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/NSurveyAdmin/AjaxWebMethodsProxy.aspx/TreeViewUpdateFolder") %>',
            data:
             "{ParentFolderId:" + pid + "," +
             "SelectedFolderId:" + id + "," +
             "name:'" + name + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == "DUPLICATEFOLDER") { alert('<%= Votations.NSurvey.Resources.ResourceManager.GetString("DUPLICATEFOLDER") %>'); return; }
                window.location.replace(window.location.href);
            }
        });
    }

    function RenFolder(element) {
        if (!IsFolder(element.parentNode.getAttribute('treeNodeValue'))) {
            alert("It's not a folder!");
            return;
        }

        var id = GetFolderId(element.parentNode.getAttribute('treeNodeValue'));
        var pid = GetFolderId(element.parentNode.parentNode.parentNode.getAttribute('treeNodeValue'));
        var name;
        var pos = $("#" + element.id).offset();
        var p = [pos.left, pos.top];

        $('#RenFolderDlg').dialog({
            height: 170,
            width: 200,
            modal: true,
            position: p,
            autoOpen: true,
            title: '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeRenameTitle") %>',
            buttons: {
                '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeRenameMenu") %>': function () {
                    name = $("#newFolderName").val();
                    $(this).dialog("close");
                    UpdateFolder(id, pid, name);
                },
                '<%= Votations.NSurvey.Resources.ResourceManager.GetString("TreeCancelMenu") %>': function () {
                    $(this).dialog("close");
                }
            }
        });
    }

    function MoveItem(elem, id, parentId) {
        parentId = parentId || "";

        var s = "{ParentFolderId:'" + parentId + "', ItemId:'" + id + "'}";

        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/NSurveyAdmin/AjaxWebMethodsProxy.aspx/TreeViewMoveItem") %>',
            data: s,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.Status == false) {
                    if (msg.d.Message == "DUPLICATEFOLDER")
                        alert('<%= Votations.NSurvey.Resources.ResourceManager.GetString("DUPLICATETREEITEM") %>');
                    if (msg.d.Message == "TREECANNOTMOVE")
                        alert('<%= Votations.NSurvey.Resources.ResourceManager.GetString("TREECANNOTMOVE") %>');
                }
                window.location.replace(window.location.href);
            }
        });
    }

    //parameter must be "elem", "newParent"
    function dndCompletedHandler(elem, newParent) {
        var startParentId = elem.parentNode.parentNode.getAttribute('treeNodeValue');
        var startId = elem.getAttribute("treeNodeValue");
        var endId = newParent.getAttribute("treeNodeValue");
        MoveItem(elem, startId, endId);
    }

    //parameter must be "elem", "newParent"
    function openCloseHandler(elem) {
        var startId = elem.getAttribute("treeNodeValue");
        var state = elem.getAttribute("OpenState");
        var s = "{FolderId:'" + startId + "', State:'" + state + "'}";
        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/NSurveyAdmin/AjaxWebMethodsProxy.aspx/TreeViewOpenClose") %>',
            data: s,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                }
        });
    }
</script>
<div id="AddFolderDlg" style="display: none">
    <input type="text" id="folderName" />
</div>
<div id="DelFolderDlg" style="display: none">
    <span id="delText"></span>
</div>
<div id="RenFolderDlg" style="display: none">
    <input type="text" id="newFolderName" />
</div>
<div>
    <ct:ASTreeView ID="astvMyTree" runat="server" 
        BasePath="~/Scripts/astreeview/themes/macOS"
        DataTableRootNodeValue="0" 
        EnableRoot="false" 
        EnableNodeSelection="true" 
        EnableCheckbox="false"
        EnableDragDrop="true" 
        EnableTreeLines="false" 
        EnableNodeIcon="true" 
        EnableCustomizedNodeIcon="false"
        DefaultFolderIcon="~/Scripts/astreeview/themes/macOS/images/astreeview-folder.gif"
        DefaultFolderOpenIcon="~/Scripts/astreeview/themes/macOS/images/astreeview-folder-open.gif"
        EnableContextMenu="true" 
        EnableContextMenuAdd="false" 
        EnableAjaxOnEditDelete="false"
        AutoPostBack="true" 
        EnableContextMenuDelete="false" 
        EnableContextMenuEdit="false"
        EnableMultiLineEdit="false" 
        EnableDebugMode="false" 
        OnOnSelectedNodeChanged="OnSelectedNodeChange"
        OnNodeDragAndDropCompletedScript="dndCompletedHandler( elem, newParent )" 
        EnableEscapeInput="false"
        OnNodeOpenedAndClosedScript="openCloseHandler(elem)"
        Height="110px" />
</div>
