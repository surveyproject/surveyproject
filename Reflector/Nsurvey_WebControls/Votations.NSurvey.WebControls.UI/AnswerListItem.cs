namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    public class AnswerListItem : AnswerItem
    {
        private DropDownList _answerItemDropDownList = new DropDownList();
        private AnswerItemCollection _answerItems = new AnswerItemCollection();

        protected override void CreateChildControls()
        {
            foreach (AnswerSelectionItem item2 in this.AnswerItems)
            {
                if (item2.SelectionMode == AnswerSelectionMode.ListItem)
                {
                    ListItem item = new ListItem(item2.Text, item2.AnswerId.ToString());
                    if (item2.Selected)
                    {
                        this._answerItemDropDownList.ClearSelection();
                    }
                    item.Selected = item2.Selected;
                    this._answerItemDropDownList.Items.Add(item);
                }
            }
            this._answerItemDropDownList.Items.Insert(0, new ListItem(ResourceManager.GetString("ListSelection", base.LanguageCode), "-1"));
            this.Controls.Add(this._answerItemDropDownList);
        }

        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            if ((this._answerItemDropDownList.SelectedValue != null) && (this._answerItemDropDownList.SelectedValue != "-1"))
            {
                datas.Add(new PostedAnswerData(this.AnswerItems[this._answerItemDropDownList.SelectedIndex - 1], int.Parse(this._answerItemDropDownList.SelectedValue), base.SectionContainer.SectionNumber, null, AnswerTypeMode.Selection));
                return datas;
            }
            return null;
        }

        /// <summary>
        /// Collection of answeritem that will be listed 
        /// in the dropdownlist
        /// </summary>
        public virtual AnswerItemCollection AnswerItems
        {
            get
            {
                return this._answerItems;
            }
            set
            {
                this._answerItems = value;
            }
        }
    }
}

