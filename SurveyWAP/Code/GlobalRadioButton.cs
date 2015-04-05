//-----------------------------------------------------------------------
// <copyright file="GlobalRadioButton.cs" company="Fryslan Webservices">
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
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

  /// <summary>
  /// This radio button is much like the built in <see cref="RadioButton"/> control, but unlike it, has a <see cref="GroupName"/> that can be global, ignoring naming containers.
  /// </summary>
  /// <remarks>
  /// Be <b>very careful</b> when using this control, 
  /// as it is quite easy to break other controls on the page 
  /// because of the disrespect for naming containers in the "name" attribute of the rendered html input.
  /// </remarks>
  [
  DefaultEvent("CheckedChanged"),
  DefaultProperty("Text"),
  Description("A RadioButton with a global GroupName"),
  ]

  public class GlobalRadioButton : System.Web.UI.WebControls.WebControl, IPostBackDataHandler 
  {
    /// <summary>
    /// Creates a new instance of the GlobalRadioButton class.
    /// </summary>
    public GlobalRadioButton() : base(HtmlTextWriterTag.Input) 
    {
    }

    #region events
    /// <summary>
    /// EventCheckedChanged
    /// </summary>
    private static readonly Object EventCheckedChanged = new Object();

    /// <summary>
    /// Raises the <see cref="CheckedChanged"/> event.
    /// </summary>
    protected virtual void OnCheckedChanged(EventArgs e) 
    {
      EventHandler ecc = (EventHandler) this.Events[GlobalRadioButton.EventCheckedChanged];
      if (ecc != null)
        ecc.Invoke(this, e);
    }

    /// <summary>
    /// The event that occurs when the checked property has changed.
    /// </summary>
    public event EventHandler CheckedChanged 
    {
      add 
      {
        this.Events.AddHandler(GlobalRadioButton.EventCheckedChanged, value);
      }
      remove 
      {
        this.Events.AddHandler(GlobalRadioButton.EventCheckedChanged, value);
      }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value indicating whether the state automatically posts back to the server when clicked.
    /// </summary>
    [
    Description("Gets or sets a value indicating whether the state automatically posts back to the server when clicked."),
    Category("Behavior"),
    DefaultValue(false),
    ]
    public virtual Boolean AutoPostBack 
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
    /// Gets or sets a value indicating whether the control is checked.
    /// </summary>
    [
    Description("Gets or sets a value indicating whether the control is checked."),
    Category("Appearance"),
    DefaultValue(false),
    ]
    public virtual Boolean Checked 
    {
      get 
      {
        Object savedState = this.ViewState["Checked"];
        if ( savedState != null ) 
        {
          return (Boolean)savedState;
        }
        return false;
      }
      set 
      {
        this.ViewState["Checked"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the text label associated with the control.
    /// </summary>
    [
    Description("Gets or sets the text label associated with the control."),
    Category("Appearance"),
    DefaultValue(""),
    ]
    public virtual String Text 
    {
      get 
      {
        Object savedState = this.ViewState["Text"];
        if ( savedState != null ) 
        {
          return (String)savedState;
        }
        return String.Empty;
      }
      set 
      {
        this.ViewState["Text"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the alignment of the text label associated with the control.
    /// </summary>
    [
    Description("Gets or sets the alignment of the text label associated with the control."),
    Category("Appearance"),
    DefaultValue(TextAlign.Right),
    ]
    public virtual TextAlign TextAlign 
    {
      get 
      {
        Object savedState = this.ViewState["TextAlign"];
        if ( savedState != null ) 
        {
          return (TextAlign)savedState;
        }
        return TextAlign.Right;
      }
      set 
      {
        if ( !Enum.IsDefined(typeof(System.Web.UI.WebControls.TextAlign), value) ) 
        {
          throw new ArgumentOutOfRangeException("value");
        }
        this.ViewState["TextAlign"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the group that the radio button belongs to.
    /// </summary>
    [
    Description("Gets or sets the name of the group that the radio button belongs to."),
    Category("Behavior"),
    DefaultValue(""),
    ]
    public virtual String GroupName 
    {
      get 
      {
        Object savedState = this.ViewState["GroupName"];
        if ( savedState != null ) 
        {
          return (String)savedState;
        }
        return String.Empty;
      }
      set 
      {
        this.ViewState["GroupName"] = value;
      }
    }

    /// <summary>
    /// Gets or sets if the GroupName will span across naming containers on the page.
    /// </summary>
    /// <remarks>
    /// Set this property to true to enable the "global" functionality of the control.
    /// Set this property to false to make it behave like a normal RadioButton.
    /// </remarks>
    [
    Description("Gets or sets if the GroupName will span across naming containers on the page."),
    Category("Behavior"),
    DefaultValue(true),
    ]
    public virtual Boolean GlobalGroup 
    {
      get 
      {
        Object savedState = this.ViewState["GlobalGroup"];
        if ( savedState != null ) 
        {
          return (Boolean)savedState;
        }
        return true;
      }
      set 
      {
        this.ViewState["GlobalGroup"] = value;
      }
    }

    /// <summary>
    /// Gets the group name for the control as it will exist in the name attribute of the html.
    /// </summary>
    protected virtual String UniqueGroupName 
    {
      get 
      {
        String groupName = this.GroupName;
        if ( !this.GlobalGroup ) 
        {
          String uniqueID = this.UniqueID;
          Int32 indexOfColon = uniqueID.LastIndexOf(':');
          if (indexOfColon >= 0) 
          {
            groupName = uniqueID.Substring(0, indexOfColon + 1) + groupName;
          }
          
        }
        return groupName;
      }
    }

    /// <summary>
    /// Gets the content to be put in the html value attribute.
    /// </summary>
    protected virtual String ValueAttribute 
    {
      get 
      {
        String value = this.Attributes["value"];
        if (value == null) 
        {
          value = this.UniqueID;
        }
        return value;
      }
    }

    #endregion

    /// <summary>
    /// Overrides the OnPreRender method.
    /// </summary>
    protected override void OnPreRender(EventArgs e) 
    {
      // From CHeckBox
      if (this.Page != null && this.Enabled) 
      {
        this.Page.RegisterRequiresPostBack(this);
        if (this.AutoPostBack) 
        {
          this.Page.ClientScript.GetPostBackEventReference(this, "");
        }
      }

      // From RadioButton
      if (this.GroupName.Length == 0) 
      {
        this.GroupName = this.UniqueID;
      }

      base.OnPreRender(e);
    }


    /// <summary>
    /// Overrides the Render method.
    /// </summary>
    protected override void Render(HtmlTextWriter writer) 
    {
      
      if (this.Page != null) 
      {
        this.Page.VerifyRenderingInServerForm(this);
      }

      Boolean doCreateSpan = false;

      if (this.ControlStyleCreated) 
      {
        // Horrible Horrible HACK, IsEmpty should not be internal, dammit.
        Boolean styleIsEmpty = (Boolean)this.GetHiddenProperty(this.ControlStyle,typeof(Style),"IsEmpty");
        if (!(styleIsEmpty)) 
        {
          this.ControlStyle.AddAttributesToRender(writer, this);
          doCreateSpan = true;
        }
      }
      if (!(this.Enabled)) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
        doCreateSpan = true;
      }

      if (this.ToolTip.Length > 0) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
        doCreateSpan = true;
      }

      if (this.Attributes.Count != 0) 
      {
        String valueAttribute = this.Attributes["value"];
        
        if (valueAttribute != null) 
        {
          this.Attributes.Remove("value");
        }
        if (this.Attributes.Count != 0) 
        {
          this.Attributes.AddAttributes(writer);
          doCreateSpan = true;
        }
        if (valueAttribute != null) 
        {
          this.Attributes["value"] = valueAttribute;
        }
      }

      if (doCreateSpan) 
      {
        writer.RenderBeginTag(HtmlTextWriterTag.Span);
      }

      if (this.Text.Length != 0) 
      {
        if (this.TextAlign == System.Web.UI.WebControls.TextAlign.Left) 
        {
          this.RenderLabel(writer, this.Text, this.ClientID);
          this.RenderInputTag(writer, this.ClientID);
        } 
        else 
        {
          this.RenderInputTag(writer, this.ClientID);
          this.RenderLabel(writer, this.Text, this.ClientID);
        }
      } 
      else 
      {
        this.RenderInputTag(writer, this.ClientID);
      }

      if (doCreateSpan) 
      {
        writer.RenderEndTag();
      }
    }
    
    /// <summary>
    /// Renders the input tag portion of the control.
    /// </summary>
    protected virtual void RenderInputTag(HtmlTextWriter writer, String clientID) 
    {

      writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
      writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueGroupName);
      writer.AddAttribute(HtmlTextWriterAttribute.Value, this.ValueAttribute);
      
      if (this.Checked) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
      }

      if (this.AutoPostBack) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Onchange, this.Page.ClientScript.GetPostBackEventReference(this, ""));
        writer.AddAttribute("language", "javascript");
      }

      if (this.AccessKey.Length > 0) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
      }
      
      if (this.TabIndex != 0) 
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
      }

      writer.RenderBeginTag(HtmlTextWriterTag.Input);
      writer.RenderEndTag();

    }

    /// <summary>
    /// Renders the label portion of the control.
    /// </summary>
    protected virtual void RenderLabel(HtmlTextWriter writer, String text, String clientID) 
    {
      writer.AddAttribute(HtmlTextWriterAttribute.For, clientID);
      writer.RenderBeginTag(HtmlTextWriterTag.Label);
      writer.Write(text);
      writer.RenderEndTag();
    }

    #region Implementation of IPostBackDataHandler
    void IPostBackDataHandler.RaisePostDataChangedEvent() 
    {
      this.OnCheckedChanged(EventArgs.Empty);
    }

    Boolean IPostBackDataHandler.LoadPostData(String postDataKey, System.Collections.Specialized.NameValueCollection postCollection) 
    {

      String postedValue = postCollection[this.UniqueGroupName];
      Boolean meCheckedPosted = (postedValue != null && postedValue == this.ValueAttribute);
      Boolean checkChanged = false;
      
      if (meCheckedPosted) 
      {
        if (this.Checked) 
        {
          return checkChanged;
        } 
        else 
        {
          this.Checked = true;
          checkChanged = true;
        }
      } 
      else if (this.Checked) 
      {
        this.Checked = false;
      }
      
      return checkChanged;
    }

    private Object GetHiddenProperty(Object target, Type targetType, String propertyName ) 
    {
      PropertyInfo property = targetType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public );
      if ( property != null ) 
      {
        return property.GetValue(target, null);
      } 
      else 
      {
        return null;
      }
    }
    #endregion
  }
}
