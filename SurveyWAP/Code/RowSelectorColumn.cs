//-----------------------------------------------------------------------
// <copyright file="RowSelectorColumn.cs" company="Fryslan Webservices">
//
// Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com) 
//
// NSurvey - The web survey and form engine
// Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// </copyright>
//-----------------------------------------------------------------------

namespace MetaBuilders.WebControls 
{
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;

  /// <summary>
  /// A <see cref="DataGridColumn"/> which shows a checkbox in each row to signal the selection of that row.
  /// </summary>
  /// <example>
  /// This example shows the use of this column in a simple <see cref="DataGrid"/>
  /// <code>
  /// <![CDATA[
  /// <%@ Import Namespace="System.Data" %>
  /// <%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.RowSelectorColumn" %>
  /// 
  /// <html>
  ///    <script language="C#" runat="server">
  ///  
  ///       ICollection CreateDataSource() 
  ///       {
  ///          DataTable dt = new DataTable();
  ///          DataRow dr;
  ///  
  ///          dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
  ///          dt.Columns.Add(new DataColumn("StringValue", typeof(string)));
  ///  
  ///          for (int i = 0; i < 5; i++) 
  ///          {
  ///             dr = dt.NewRow();
  ///  
  ///             dr[0] = i;
  ///             dr[1] = "Item " + i.ToString();
  ///  
  ///             dt.Rows.Add(dr);
  ///          }
  ///  
  ///          DataView dv = new DataView(dt);
  ///          return dv;
  ///       }
  ///  
  ///       void Page_Load(Object sender, EventArgs e) 
  ///       {
  ///  
  ///          if (!IsPostBack) 
  ///          {
  ///             // Load this data only once.
  ///             ItemsGrid.DataSource= CreateDataSource();
  ///             ItemsGrid.DataBind();
  ///          }
  ///       }
  ///       
  ///       protected void ShowSelections( Object sender, EventArgs e ) {
  ///     RowSelectorColumn rsc = ItemsGrid.Columns[0] as RowSelectorColumn;
  ///     Message.Text = "Total selected rows = " + rsc.SelectedIndexes.Length.ToString() + "<br />";
  ///     foreach( Int32 selectedIndex in rsc.SelectedIndexes ) {
  ///       Message.Text += selectedIndex.ToString() + "<br />";
  ///     }
  ///       }
  ///  
  ///    </script>
  ///  
  /// <body>
  ///  
  ///    <form runat=server>
  ///  
  ///       <h3>DataGrid Example</h3>
  ///  
  ///       <asp:DataGrid id="ItemsGrid"
  ///            BorderColor="black"
  ///            BorderWidth="1"
  ///            CellPadding="3"
  ///            AutoGenerateColumns="true"
  ///            runat="server">
  /// 
  ///          <HeaderStyle BackColor="darkblue" forecolor="white">
  ///          </HeaderStyle> 
  ///          <Columns>
  ///       <mbrsc:RowSelectorColumn allowselectall="true" />
  ///          </Columns>
  ///  
  ///       </asp:DataGrid>
  ///       
  ///       <asp:Button runat="server" onclick="ShowSelections" text="Show Selections" />
  ///       <br />
  ///       <asp:Label runat="server" id="Message" />
  ///  
  ///    </form>
  ///  
  /// </body>
  /// </html>
  /// ]]>
  /// </code>
  /// </example>
  public class RowSelectorColumn : DataGridColumn 
  {

    #region Constructors
        
    /// <summary>
    /// Creates a new instance of the RowSelectorColumn control.
    /// </summary>
    [Description("Creates a new instance of the RowSelectorColumn control.")]
    public RowSelectorColumn() : base() 
    {
    }
    #endregion
        
    #region FindColumns
    /// <summary>
    /// Finds the first <see cref="RowSelectorColumn"/> in the given <see cref="DataGrid"/>.
    /// </summary>
    /// <param name="grid">The <see cref="DataGrid"/> to search.</param>
    /// <returns>The <see cref="RowSelectorColumn"/> found, or null.</returns>
    public static RowSelectorColumn FindColumn( DataGrid grid ) 
    {
      RowSelectorColumn foundCol = null;
      foreach( DataGridColumn col in grid.Columns ) 
      {
        foundCol = col as RowSelectorColumn;
        if ( foundCol != null  ) 
        {
          return foundCol;
        }
      }
      return null;
    }
        
    /// <summary>
    /// Finds the first <see cref="RowSelectorColumn"/> in the given <see cref="DataGrid"/> after or at the given column index.
    /// </summary>
    /// <param name="grid">The <see cref="DataGrid"/> to search.</param>
    /// <param name="startIndex">The index of the column to start the search.</param>
    /// <returns>The <see cref="RowSelectorColumn"/> found, or null.</returns>
    public static RowSelectorColumn FindColumn( DataGrid grid, Int32 startIndex ) 
    {
      RowSelectorColumn foundCol = null;
      for( Int32 i = startIndex; i < grid.Columns.Count; i++ ) 
      {
        foundCol = grid.Columns[i] as RowSelectorColumn;
        if ( foundCol != null  ) 
        {
          return foundCol;
        }
      }
      return null;
    }

    #endregion

    #region Cell Creation
    /// <summary>
    /// This member overrides <see cref="DataGridColumn.InitializeCell"/>.
    /// </summary>
    public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType) 
    {
      base.InitializeCell(cell, columnIndex, itemType);

      switch (itemType) 
      {
        case ListItemType.EditItem:
        case ListItemType.Item:
        case ListItemType.AlternatingItem:
        case ListItemType.SelectedItem:

          if ( this.SelectionMode == ListSelectionMode.Multiple ) 
          {
            ParticipantCheckBox selector = new ParticipantCheckBox();
            selector.ID = "RowSelectorColumnSelector";
            selector.AutoPostBack = this.AutoPostBack;
            cell.Controls.Add( selector );
            if ( AllowSelectAll ) 
            {
              RegisterForSelectAll( selector );
            }
            selector.ServerChange += new EventHandler(selector_ServerChange);
          } 
          else 
          {
            ParticipantRadioButton selector = new ParticipantRadioButton();
            selector.Name = "RowSelectorColumnSelector";
            selector.ID = "RowSelectorColumnSelector";
            selector.AutoPostBack = this.AutoPostBack;
            cell.Controls.Add( selector );
            selector.DataBinding += new EventHandler( this.selectorDataBinding );
            selector.ServerChange += new EventHandler(selector_ServerChange);
          }
          break;
        case ListItemType.Header:
          if ( AllowSelectAll && this.SelectionMode == ListSelectionMode.Multiple ) 
          {
            selectAllControl = new SelectAllCheckBox();
            selectAllControl.ID = "RowSelectorColumnAllSelector";
            selectAllControl.AutoPostBack = this.AutoPostBack;
            RegisterSelectAllScript();
            
            String currentText = cell.Text;
            if ( currentText != "" ) 
            {
              cell.Text = "";
            }
            cell.Controls.Add( selectAllControl );
            if ( currentText != "" ) 
            {
              cell.Controls.Add( new LiteralControl(currentText) );
            }
            
          }
          break;
      }
      
    }

    
    /// <summary>
    /// Sets the value of the given selector to the row index.
    /// </summary>
    protected virtual void SetIndexValue( HtmlInputRadioButton radioSelector ) 
    {
      DataGridItem row = radioSelector.NamingContainer as DataGridItem;
      if ( row != null ) 
      {
        radioSelector.Value = row.ItemIndex.ToString();
      }
    }


    /// <summary>
    /// Gets the checkbox appearing in the header row which controls the other checkboxes.
    /// </summary>
    /// <remarks>The checkbox if <see cref="AllowSelectAll"/> is true, otherwise null.</remarks>
    [Description("Gets the checkbox appearing in the header row which controls the other checkboxes.")]
    protected virtual System.Web.UI.HtmlControls.HtmlInputCheckBox SelectAllControl 
    {
      get 
      {
        return this.selectAllControl;
      }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the behavior of including a checkbox in the Header area which selects all the row checkboxes based on its value.
    /// </summary>
    /// <remarks>This behavior will only exist on browsers supporting javascript and the W3C DOM.</remarks>
    [
    DefaultValue(false),
    Description("Gets or sets the behavior of including a checkbox in the Header area which selects all the row checkboxes based on its value.")
    ]
    public virtual Boolean AllowSelectAll 
    {
      get 
      {
        object savedState = this.ViewState["AllowSelectAll"];
        if ( savedState != null ) 
        {
          return (Boolean)savedState;
        }
        return false;
      }
      set 
      {
        this.ViewState["AllowSelectAll"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the selection mode of the <see cref="RowSelectorColumn"/>.
    /// </summary>
    /// <remarks>
    /// Use the SelectionMode property to specify the mode behavior of the <see cref="RowSelectorColumn"/>.
    /// Setting this property to ListSelectionMode.Single indicates only a single item can be selected, while ListSelectionMode.Multiple specifies multiple items can be selected.
    /// </remarks>
    [
    DefaultValue(ListSelectionMode.Multiple),
    Description("Gets or sets the selection mode of the RowSelectorColumn.")
    ]
    public virtual ListSelectionMode SelectionMode 
    {
      get 
      {
        object savedState = this.ViewState["SelectionMode"];
        if ( savedState != null ) 
        {
          return (ListSelectionMode)savedState;
        }
        return ListSelectionMode.Multiple;
      }
      set 
      {
        if ( !Enum.IsDefined(typeof(ListSelectionMode),value) ) 
        {
          throw new ArgumentOutOfRangeException("value");
        }
        this.ViewState["SelectionMode"] = value;
      }
    }

    /// <summary>
    /// Gets or sets if the column's controls will postback automaticly when the selection changes.
    /// </summary>
    [
    Description("Gets or sets if the column's controls will postback automaticly when the selection changes."),
    DefaultValue(false),
    ]
    public virtual Boolean AutoPostBack 
    {
      get 
      {
        object savedState = this.ViewState["AutoPostBack"];
        if ( savedState != null ) 
        {
          return (Boolean)savedState;
        }
        return false;
      }
      set 
      {
        this.ViewState["AutoPostBack"] = value;
      }
    }

    /// <summary>
    /// Gets or sets an array of the DataGridItem indexes which are marked as selected.
    /// </summary>
    /// <remarks>The index can be used to get, for example, in the DataKeys collection to get the keys for the selected rows.</remarks>
    [
    Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
    Description("Gets an array of the DataGridItem indexes which are marked as selected.")
    ]
    public virtual Int32[] SelectedIndexes 
    {
      get 
      {
        System.Collections.ArrayList selectedIndexList = new System.Collections.ArrayList();
        Int32 thisCellIndex = this.Owner.Columns.IndexOf(this);
        foreach( DataGridItem item in this.Owner.Items ) 
        {
          Control foundControl = item.FindControl("RowSelectorColumnSelector");
          System.Web.UI.HtmlControls.HtmlInputCheckBox Checkselector = foundControl as System.Web.UI.HtmlControls.HtmlInputCheckBox;
          System.Web.UI.HtmlControls.HtmlInputRadioButton radioselector = foundControl as System.Web.UI.HtmlControls.HtmlInputRadioButton;
          if ( Checkselector != null && Checkselector.Checked )  
          {
            selectedIndexList.Add( item.ItemIndex );
          } 
          else if ( radioselector != null && radioselector.Checked )  
          {
            selectedIndexList.Add( item.ItemIndex );
          }
        }
        return (Int32[])selectedIndexList.ToArray(typeof( System.Int32 ) );
      }
      set 
      {
        
        foreach( DataGridItem item in this.Owner.Items ) 
        {
          Boolean checkIt = false;
          for( Int32 i = 0; i < value.Length; i++ ) 
          {
            if ( value[i] == item.ItemIndex ) 
            {
              checkIt = true;
              break;
            }
          }

          Control foundControl = item.FindControl("RowSelectorColumnSelector");
          System.Web.UI.HtmlControls.HtmlInputCheckBox checkselector = foundControl as System.Web.UI.HtmlControls.HtmlInputCheckBox;
          System.Web.UI.HtmlControls.HtmlInputRadioButton radioselector = foundControl as System.Web.UI.HtmlControls.HtmlInputRadioButton;

          if ( checkselector != null )  
          {
            checkselector.Checked = checkIt;
          } 
          else if ( radioselector != null )  
          {
            radioselector.Checked = checkIt;
          }
        
          
        }
      }
    }

    #endregion

    #region Events
    /// <summary>
    /// The event that is raised when the selection has changed.
    /// </summary>
    public event EventHandler SelectionChanged;

    /// <summary>
    /// Raises the SelectionChanged event.
    /// </summary>
    protected virtual void OnSelectionChanged( EventArgs e ) 
    {
      if ( this.SelectionChanged != null ) 
      {
        this.SelectionChanged(this,e);
      }
    }
    private void selector_ServerChange(object sender, EventArgs e) 
    {
      this.OnSelectionChanged(EventArgs.Empty);
    }


    #endregion

    #region SelectAll Script

    /// <summary>
    /// Emits the script library neccessary for the SelectAll behavior.
    /// </summary>
    [Description("Emits the script library neccessary for the SelectAll behavior.")]
    protected virtual void RegisterSelectAllScript() 
    {
      if ( this.Owner.Page != null ) 
      {
        if ( !this.Owner.Page.ClientScript.IsClientScriptBlockRegistered("MetaBuilders_RowSelectorColumn Library") ) 
        {

            using (StreamReader reader = new StreamReader(typeof(RowSelectorColumn).Assembly.GetManifestResourceStream("Votations.NSurvey.WebAdmin.Code.RowSelectorColumnScript.js"))) 
          { 
            String script = "<script type='text/javascript' language='javascript'>\r\n<!--\r\n" + reader.ReadToEnd() + "\r\n//-->\r\n</script>";
            this.Owner.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MetaBuilders.WebControls.RowSelectorColumn", script);
          }
        }
      }
    }


    /// <summary>
    /// Registers the given checkbox as being bound to the SelectAll checkbox.
    /// </summary>
    /// <param name="selector">The checkbox being bound.</param>
    [Description("Registers the given checkbox as being bound to the SelectAll checkbox.")]
    protected virtual void RegisterForSelectAll( ParticipantCheckBox selector ) 
    {
      selector.Master = this.selectAllControl;
    }

    #endregion

    #region Private
    private void selectorDataBinding( Object sender, EventArgs e ) 
    {
      ParticipantRadioButton radio = sender as ParticipantRadioButton;
      if ( radio != null ) 
      {
        this.SetIndexValue( radio );
      }
    }

    private SelectAllCheckBox selectAllControl;
    #endregion

    #region Participant Controls

    /// <summary>
    /// The checkbox in the header for select-all.
    /// </summary>
    public class SelectAllCheckBox : System.Web.UI.HtmlControls.HtmlInputCheckBox 
    {

      /// <summary>
      /// Overrides <see cref="HtmlControl.RenderAttributes"/>
      /// </summary>
      protected override void RenderAttributes(HtmlTextWriter writer) 
      {
        String finalOnClick = this.Attributes["onclick"];
        if ( finalOnClick == null ) 
        {
          finalOnClick = "";
        }

        finalOnClick +=  " MetaBuilders_RowSelectorColumn_SelectAll( this );"; 
        if ( this.AutoPostBack ) 
        {
          finalOnClick += Page.ClientScript.GetPostBackEventReference(this, "");
        }
        this.Attributes["onclick"] = finalOnClick;

        base.RenderAttributes (writer);
      }

      /// <summary>
      /// Gets or sets if the control will postback after being changed.
      /// </summary>
      public Boolean AutoPostBack 
      {
        get 
        {
          Object savedState = this.ViewState["AutoPostBack"];
          if ( savedState != null ) 
          {
            return (Boolean)savedState;
          }
          return false;
        }
        set 
        {
          this.ViewState["AutoPostBack"] = value;
        }  
      }



    }

    /// <summary>
    /// The checkboxes which appear in each cell of a <see cref="RowSelectorColumn"/> when <see cref="RowSelectorColumn.SelectionMode"/> is set to <see cref="ListSelectionMode.Multiple"/>.
    /// </summary>
    public class ParticipantCheckBox : System.Web.UI.HtmlControls.HtmlInputCheckBox 
    {

      /// <summary>
      /// Gets or sets if the control will postback after being changed.
      /// </summary>
      public Boolean AutoPostBack 
      {
        get 
        {
          Object savedState = this.ViewState["AutoPostBack"];
          if ( savedState != null ) 
          {
            return (Boolean)savedState;
          }
          return false;
        }
        set 
        {
          this.ViewState["AutoPostBack"] = value;
        }  
      }


      /// <summary>
      /// Overrides <see cref="HtmlControl.Render"/>.
      /// </summary>
      protected override void Render( HtmlTextWriter writer ) 
      {
        base.Render(writer);
        if ( master != null ) 
        {
          LiteralControl script = new LiteralControl("\r\n<script type='text/javascript' language='javascript'>\r\nMetaBuilders_RowSelectorColumn_Register('" + master.ClientID + "', '" + this.ClientID + "')\r\n</script>");
          script.RenderControl(writer);
        }
      }

      /// <summary>
      /// Gets or sets the checkbox which controls the checked state of this checkbox.
      /// </summary>
      public Control Master 
      {
        get 
        {
          return this.master;
        }
        set 
        {
          this.master = value;
        }
      }

      private Control master;

    

      /// <summary>
      /// Overrides <see cref="HtmlControl.RenderAttributes"/>
      /// </summary>
      protected override void RenderAttributes(HtmlTextWriter writer) 
      {
        if ( master != null ) 
        {
          String originalOnClick = this.Attributes["onclick"];
          if ( originalOnClick != null ) 
          {
            this.Attributes.Remove("onclick");
          } 
          else 
          {
            originalOnClick = "";
          }
          this.Attributes["onclick"] = "MetaBuilders_RowSelectorColumn_CheckChildren( '" + this.Master.ClientID + "' ); " + originalOnClick;
        }

        if ( this.AutoPostBack ) 
        {
          String originalOnClick = this.Attributes["onclick"];
          if ( originalOnClick != null ) 
          {
            this.Attributes.Remove("onclick");
          } 
          else 
          {
            originalOnClick = "";
          }
          this.Attributes["onclick"] = originalOnClick + " " + Page.ClientScript.GetPostBackEventReference(this, "");
        }

        base.RenderAttributes (writer);
      }

    }

    /// <summary>
    /// The radiobutton which appears in each cell of a <see cref="RowSelectorColumn"/> when <see cref="RowSelectorColumn.SelectionMode"/> is set to <see cref="ListSelectionMode.Single"/>.
    /// </summary>
    public class ParticipantRadioButton : System.Web.UI.HtmlControls.HtmlInputRadioButton, IPostBackDataHandler 
    {

      #region IPostBackDataHandler Implementation
      /// <summary>
      /// This doesn't differ from the original implementaion... 
      /// except now i'm using my own RenderednameAttribute instead of the InputControl implementation.
      /// </summary>
      bool IPostBackDataHandler.LoadPostData( string postDataKey, NameValueCollection postCollection ) 
      {
        bool result = false;

        string postedValue = postCollection[this.RenderedNameAttribute];
        if (postedValue != null && postedValue == this.Value) 
        {
          if ( this.Checked) 
          {
            return result;
          }
          this.Checked = true;
          result = true;
        } 
        else if (this.Checked) 
        {
          this.Checked = false;
        }
        return result;
      }

      /// <summary>
      /// No change from the InputControl implementation
      /// </summary>
      void IPostBackDataHandler.RaisePostDataChangedEvent() 
      {
        this.OnServerChange(EventArgs.Empty);
      }
      #endregion

      /// <summary>
      /// Gets or sets if the control will postback after being changed.
      /// </summary>
      public Boolean AutoPostBack 
      {
        get 
        {
          Object savedState = this.ViewState["AutoPostBack"];
          if ( savedState != null ) 
          {
            return (Boolean)savedState;
          }
          return false;
        }
        set 
        {
          this.ViewState["AutoPostBack"] = value;
        }  
      }

      /// <summary>
      /// Overrides <see cref="HtmlInputRadioButton.RenderAttributes"/>.
      /// </summary>
      /// <remarks>Customized to use this implementation of RenderedNameAttribute</remarks>
      protected override void RenderAttributes(HtmlTextWriter writer ) 
      {
        writer.WriteAttribute("value", this.Value);
        this.Attributes.Remove("value");
        writer.WriteAttribute("name", this.RenderedNameAttribute);
        this.Attributes.Remove("name");
        if (this.ID != null) 
        {
          writer.WriteAttribute("id", this.ClientID);
        }
        if ( this.AutoPostBack ) 
        {
          String finalOnClick = this.Attributes["onclick"];
          if ( finalOnClick != null ) 
          {
            finalOnClick += " " + Page.ClientScript.GetPostBackEventReference(this, "");
            this.Attributes.Remove("onclick");
          } 
          else 
          {
            finalOnClick = Page.ClientScript.GetPostBackEventReference(this, "");
          }
          writer.WriteAttribute("onclick", finalOnClick );
        }

        this.Attributes.Render(writer);
        writer.Write(" /");

      }


      /// <summary>
      /// Gets the final rendering of the Name attribute.
      /// </summary>
      /// <remarks>
      /// Differs from the standard RenderedNameAttribute to use the column as the logical naming container instead of the row.
      /// </remarks>
      protected virtual String RenderedNameAttribute 
      {
        get 
        {
          this.DiscoverContainers();
          if ( parentCell == null || parentDataGridItem == null || parentDataGrid == null ) 
          {
            return this.Name;
          } 
          else 
          {
            System.Text.StringBuilder groupContainer = new System.Text.StringBuilder();
            groupContainer.Append( parentDataGrid.UniqueID );
            groupContainer.Append( ":Column" );
            groupContainer.Append( parentDataGridItem.Cells.GetCellIndex(parentCell).ToString() );
            groupContainer.Append( ":" );
            groupContainer.Append( this.Name );

            return groupContainer.ToString();
          }
        }
      }

      private TableCell parentCell;
      private DataGridItem parentDataGridItem;
      private DataGrid parentDataGrid;

      /// <summary>
      /// Looks up the control heirarchy to discover the container controls for this radiobutton
      /// </summary>
      protected virtual void DiscoverContainers() 
      {
        if ( parentCell == null || parentDataGridItem == null || parentDataGrid == null ) 
        {
          Control tempControl = this.NamingContainer;
          if ( tempControl is DataGridItem ) 
          {
            parentDataGridItem = (DataGridItem)tempControl;
            parentDataGrid = (DataGrid)parentDataGridItem.NamingContainer;
          }

          tempControl = this.Parent;
          while( tempControl.Parent.UniqueID != parentDataGridItem.UniqueID ) 
          {
            tempControl = tempControl.Parent;
          }
          if ( tempControl is TableCell ) 
          {
            parentCell = (TableCell)tempControl;
          }
        }
      }

      
    }
    #endregion

  }
}
