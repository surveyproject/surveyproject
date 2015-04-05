namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;

    /// <summary>
    /// Generates a matrix style layout
    /// </summary>
    public class MatrixSection : Section
    {
        private MatrixChildCollection _childQuestions = new MatrixChildCollection();
        private Style _matrixAlternatingItemStyle;
        private Style _matrixHeaderStyle;
        private Style _matrixItemStyle;
        private Style _matrixStyle;

        /// <summary>
        /// Build a selection layout with
        /// the question's answeritem child controls
        /// </summary>
        protected virtual Table BuildMatrixLayout()
        {
            Table table = new Table();//JJ;
            TableRow row = new TableRow();
            TableRow row2 = new TableRow();
            bool flag = true;
            table.ControlStyle.CopyFrom(this.MatrixStyle);
            TableCell cell2 = new TableCell();
            row2.Cells.Add(cell2);
            int num = 1;
            foreach (MatrixChildQuestion question in this.ChildQuestions)
            {
                row = new TableRow();
                TableCell cell3 = new TableCell();
                cell3.VerticalAlign = VerticalAlign.Top;
                cell3.Text = question.Text;
                row.Cells.Add(cell3);
                foreach (AnswerItem item in question.Answers)
                {
                    if (flag)
                    {
                        cell2 = new TableCell();
                        cell2.VerticalAlign = VerticalAlign.Top;
                        cell2.HorizontalAlign = HorizontalAlign.Center;
                        cell2.Text = item.Text;
                        row2.Cells.Add(cell2);
                        row2.ControlStyle.CopyFrom(this.MatrixHeaderStyle);
                    }
                    TableCell cell = new TableCell();
                    cell.VerticalAlign = VerticalAlign.Top;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Controls.Add(item);
                    row.Cells.Add(cell);
                }
                if (flag)
                {
                    table.Rows.Add(row2);
                    flag = false;
                }
                table.Rows.Add(row);
                if ((num % 2) == 0)
                {
                    row.ControlStyle.CopyFrom(this.MatrixAlternatingItemStyle);
                }
                else
                {
                    row.ControlStyle.CopyFrom(this.MatrixItemStyle);
                }
                num++;
            }
            table.CellSpacing = 2;
            table.CellPadding = 2;
            return table;
        }

        private Control EncloseInScrollableDiv(Control ctl)
        {
          
            Panel p = new Panel();
            p.CssClass = "ScrollableDiv";
            p.Controls.Add(ctl);
            return p;

        }
        protected override void CreateChildControls()
        {
            Table t = this.BuildMatrixLayout();
            t.Width = new Unit(100.0, UnitType.Percentage);
            this.Controls.Add(EncloseInScrollableDiv( t));
        }

        /// <summary>
        /// Parse the answer collection and generates 
        /// the question client side validation script for the answers / fields
        /// of sections that require it.
        /// </summary>
        protected override string GenerateClientSideValidationCode()
        {
            if (base.EnableClientSideValidation)
            {
                bool flag = false;
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("<script type=\"text/javascript\" language=\"javascript\"><!--{0}function {1}{2}(){{/*alert('start validation');*/", Environment.NewLine, GlobalConfig.QuestionValidationFunction, this.UniqueID.Replace(":", "_")));
                for (int i = 0; i < this.ChildQuestions.Count; i++)
                {
                    foreach (AnswerItem item in this.ChildQuestions[i].Answers)
                    {
                        if (item is IClientScriptValidator)
                        {
                            IClientScriptValidator validator = (IClientScriptValidator) item;
                            if ((validator.EnableValidation && (validator.JavascriptFunctionName != null)) && (validator.JavascriptFunctionName.Length != 0))
                            {
                                builder.Append(string.Format("if ((document.getElementsByName('{1}').item(0) != null  && !{0}(document.getElementsByName('{1}').item(0))) || (document.getElementById('{1}') != null  && !{0}(document.getElementById('{1}'))) ){{alert('{2} : {3}');return false;}}", new object[] { validator.JavascriptFunctionName, validator.GetControlIdToValidate(), item.Text, validator.JavascriptErrorMessage }));
                                flag = true;
                            }
                        }
                    }
                }
                builder.Append("return true;}//--></script>");
                if (flag)
                {
                    return builder.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// Does the section generates any client side validation script ?
        /// </summary>
        /// <returns></returns>
        public override bool GeneratesClientSideScript()
        {
            bool flag = false;
            if (base.EnableClientSideValidation)
            {
                for (int i = 0; i < this.ChildQuestions.Count; i++)
                {
                    foreach (AnswerItem item in this.ChildQuestions[i].Answers)
                    {
                        if (item is IClientScriptValidator)
                        {
                            IClientScriptValidator validator = (IClientScriptValidator) item;
                            if ((validator.EnableValidation && (validator.JavascriptFunctionName != null)) && (validator.JavascriptFunctionName.Length != 0))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        public MatrixChildCollection ChildQuestions
        {
            get
            {
                return this._childQuestions;
            }
            set
            {
                this._childQuestions = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string) null), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style MatrixAlternatingItemStyle
        {
            get
            {
                if (this._matrixAlternatingItemStyle != null)
                {
                    return this._matrixAlternatingItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixAlternatingItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix header
        /// </summary>
        [Category("Styles"), DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style MatrixHeaderStyle
        {
            get
            {
                if (this._matrixHeaderStyle != null)
                {
                    return this._matrixHeaderStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixHeaderStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [DefaultValue((string) null), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style MatrixItemStyle
        {
            get
            {
                if (this._matrixItemStyle != null)
                {
                    return this._matrixItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix table 
        /// </summary>
        [DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Category("Styles")]
        public Style MatrixStyle
        {
            get
            {
                if (this._matrixStyle != null)
                {
                    return this._matrixStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixStyle = value;
            }
        }
    }
}

