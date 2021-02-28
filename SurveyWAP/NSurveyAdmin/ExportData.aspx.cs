/**************************************************************************************************
	Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Security;
using Microsoft.VisualBasic;
using System.Xml;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using Votations.NSurvey.Enums;
using System.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Export survey data
    /// </summary>
    public partial class ExportData : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Label CreationDateLabel;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.Button ExportDataButton;
        protected System.Web.UI.WebControls.TextBox StartDateTextBox;
        protected System.Web.UI.WebControls.TextBox EndDateTextBox;
        protected System.Web.UI.WebControls.TextBox TextDelimiterTextBox;
        protected System.Web.UI.WebControls.TextBox FieldDelimiterTextBox;
        protected System.Web.UI.WebControls.Label FieldDelimiterLabel;
        protected System.Web.UI.WebControls.Label TextDelimiterLabel;
        protected System.Web.UI.WebControls.Label ExportFromDateLabel;
        protected System.Web.UI.WebControls.Label ExportToDateLabel;
        protected System.Web.UI.WebControls.Literal SurveyExportTitle;
        protected System.Web.UI.WebControls.Button VoterExportXMLButton;
        protected System.Web.UI.WebControls.Label ExportTypeLabel;
        protected System.Web.UI.WebControls.DropDownList ExportDropDownList;
        protected System.Web.UI.WebControls.Label ReplaceCarriageLabel;
        protected System.Web.UI.WebControls.DropDownList CarriageReturnDropDownList;
        protected System.Web.UI.WebControls.PlaceHolder CSVOptionPlaceHolder;
        protected System.Web.UI.WebControls.TextBox CRCharTextbox;
        //protected SurveyListControl SurveyList;

        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.DataExport);

            SetupSecurity();
            LocalizePage();
            MessageLabel.Visible = false;
            if (!Page.IsPostBack)
            {
                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
                StartDateTextBox.Text = new DateTime(2003, 12, 31).ToShortDateString();
                EndDateTextBox.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessExport, true);
        }

        private void LocalizePage()
        {
            FieldDelimiterLabel.Text = GetPageResource("FieldDelimiterLabel");
            TextDelimiterLabel.Text = GetPageResource("TextDelimiterLabel");
            ExportFromDateLabel.Text = GetPageResource("ExportFromDateLabel");
            ExportToDateLabel.Text = GetPageResource("ExportToDateLabel");
            SurveyExportTitle.Text = GetPageResource("SurveyExportTitle");
            ExportDataButton.Text = GetPageResource("ExportDataButton");
            VoterExportXMLButton.Text = GetPageResource("VoterExportXMLButton");
            ReplaceCarriageLabel.Text = GetPageResource("ReplaceCarriageLabel");
            ExportTypeLabel.Text = GetPageResource("ExportTypeLabel");
            HeaderFieldLabel.Text = GetPageResource("HeaderFieldLabel");
            answerFieldLabel.Text = GetPageResource("AnswerFieldlabel");
            rdStyle1.Text = GetPageResource("CSVAnswerExportStyle1");
            rdStyle2.Text = GetPageResource("CSVAnswerExportStyle2");
            dataSelectionLabel.Text = GetPageResource("CSVEXPDataSelection");
            rdAllDates.Text = GetPageResource("CSVEXPGetAllDates");
            rdSelectedDates.Text = GetPageResource("CSVExpGetSelectedDates");
            info1Label.Text = GetPageResource("ExportInstructions1");
            layoutLabel.Text = GetPageResource("LayoutLabel");
            formatlabel.Text = GetPageResource("FormatLayout");
            MultiSeperatorLabel.Text = GetPageResource("MultiSeparator");

            TranslateListControl(ddlHeader);
            TranslateListControl(ddlAnswer);

            if (!Page.IsPostBack)
            {
                ExportDropDownList.Items.Add(new System.Web.UI.WebControls.ListItem(GetPageResource("CSVExportOption"), "0"));
                ExportDropDownList.Items.Add(new System.Web.UI.WebControls.ListItem(GetPageResource("XmlExportOption"), "1"));

                CarriageReturnDropDownList.Items.Add(new System.Web.UI.WebControls.ListItem(GetPageResource("KeepCarriageOption"), "0"));
                CarriageReturnDropDownList.Items.Add(new System.Web.UI.WebControls.ListItem(GetPageResource("OnlyReturnOption"), "1"));
                CarriageReturnDropDownList.Items.Add(new System.Web.UI.WebControls.ListItem(GetPageResource("UserDefinedCROption"), "2"));
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ExportDropDownList.SelectedIndexChanged += new System.EventHandler(this.ExportDropDownList_SelectedIndexChanged);
            this.CarriageReturnDropDownList.SelectedIndexChanged += new System.EventHandler(this.CarriageReturnDropDownList_SelectedIndexChanged);
            this.ExportDataButton.Click += new System.EventHandler(this.ExportCSVButton_Click);
            this.VoterExportXMLButton.Click += new System.EventHandler(this.VoterExportXMLButton_Click);

            //pdf
            this.ExportDataPdfButton.Click += new System.EventHandler(this.ExportPDFButton_Click);

            this.Load += new System.EventHandler(this.Page_Load);
            this.rdStyle1.CheckedChanged += new EventHandler(rdStyle1_CheckedChanged);
            this.rdStyle2.CheckedChanged += new EventHandler(rdStyle1_CheckedChanged);
            this.PreRender += new EventHandler(ExportData_PreRender);
        }

        void ExportData_PreRender(object sender, EventArgs e)
        {
            this.MultiSeparatorTextBox.Visible = this.MultiSeperatorLabel.Visible = rdStyle1.Checked;
        }


        #endregion
        void rdStyle1_CheckedChanged(object sender, EventArgs e)
        {
            plhDdls.Visible = rdStyle1.Checked;
        }

        /// <summary>
        /// Create and sends the CSV file to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportCSVButton_Click(object sender, System.EventArgs e)
        {
            string text = (rdStyle1.Checked ? ExportCSVStyleOne() : ExportCSVStyleTwo());
            if (string.IsNullOrEmpty(text)) return;
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"SP_VoterData.csv\"");

            // Writes the UTF 8 header
            byte[] BOM = new byte[] { 0xef, 0xbb, 0xbf };
            Response.BinaryWrite(BOM);

            // Export the data

            Response.Write(text);
            Response.End();
        }

        /// <summary>
        /// Create and sends the PDF file to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportPDFButton_Click(object sender, System.EventArgs e)
        {
            // Create a Document object
            var document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Create a new PdfWriter object, specifying the output stream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            string text = (rdStyle1.Checked ? ExportCSVStyleOne() : ExportCSVStyleTwo());
            if (string.IsNullOrEmpty(text)) return;


            document.Add(new Paragraph(text));

            document.Close();

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"SP_dataexport.pdf\"");
            //Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", txtOrderID.Text));

            // Writes the UTF 8 header
            byte[] BOM = new byte[] { 0xef, 0xbb, 0xbf };
            Response.BinaryWrite(BOM);

            // Export the data

            //Response.Write(text);
            //Response.End();

            Response.BinaryWrite(output.ToArray());


        }




        private void VoterExportXMLButton_Click(object sender, System.EventArgs e)
        {
            DateTime startDate;
            DateTime endDate;
            if (rdAllDates.Checked)
            {
                startDate = new DateTime(2004, 1, 1);
                endDate = DateTime.Today;
            }
            else
            {
                startDate = (StartDateTextBox.Text == null && !Information.IsDate(StartDateTextBox.Text)) ?
                     new DateTime(2004, 1, 1) : DateTime.Parse(StartDateTextBox.Text);
                endDate = (EndDateTextBox.Text == null && !Information.IsDate(EndDateTextBox.Text)) ?
                     new DateTime(2010, 1, 1) : DateTime.Parse(EndDateTextBox.Text);
            }

            Response.Charset = "utf-8";
            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"survey_results" + SurveyId + ".xml\"");

            NSurveyVoter voterAnswers = new Voters().GetForExport(SurveyId, startDate, endDate);


            WebSecurityAddInCollection securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);
            NameValueCollection addInVoterData;

            if (voterAnswers.Voter.Rows.Count > 0)
            {
                // Get security addins data if any available and get key name
                // to add it as new column to the voter table
                for (int i = 0; i < securityAddIns.Count; i++)
                {
                    addInVoterData = securityAddIns[i].GetAddInVoterData(voterAnswers.Voter[0].VoterID);
                    if (addInVoterData != null)
                    {
                        for (int j = 0; j < addInVoterData.Count; j++)
                        {
                            voterAnswers.Voter.Columns.Add(new DataColumn(addInVoterData.GetKey(j), typeof(string), null, System.Data.MappingType.Element));
                        }
                    }
                }

                for (int voterIndex = 0; voterIndex < voterAnswers.Voter.Rows.Count; voterIndex++)
                {
                    // Get security addins data if any available
                    for (int i = 0; i < securityAddIns.Count; i++)
                    {
                        addInVoterData =
                            securityAddIns[i].GetAddInVoterData(((NSurveyVoter.VoterRow)voterAnswers.Voter.Rows[voterIndex]).VoterID);
                        if (addInVoterData != null)
                        {
                            for (int j = 0; j < addInVoterData.Count; j++)
                            {
                                voterAnswers.Voter.Rows[voterIndex][addInVoterData.GetKey(j)] = addInVoterData[j];
                            }
                        }
                    }
                }
            }


            XmlReader reader = new XmlTextReader(voterAnswers.GetXml(), XmlNodeType.Document, null); ;
            XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8); ;

            writer.WriteStartDocument(true);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        writer.WriteStartElement(reader.Prefix,
                            reader.LocalName, reader.NamespaceURI);
                        if (reader.HasAttributes)
                        {
                            writer.WriteAttributes(reader, true);
                            reader.MoveToElement();
                        }
                        if (reader.IsEmptyElement)
                            writer.WriteEndElement();
                        break;
                    case XmlNodeType.EndElement:
                        writer.WriteEndElement();
                        break;
                    case XmlNodeType.Text:
                        // removes any invalid xml char
                        writer.WriteString(reader.Value.Replace(((char)12).ToString(), ""));
                        break;
                    case XmlNodeType.CDATA:
                        writer.WriteCData(reader.Value);
                        break;
                }
            }
            writer.WriteEndDocument();

            writer.Close();
            reader.Close();

            Response.End();
        }

        private string ExportCSVStyleTwo()
        {
            DateTime startDate;
            DateTime endDate;
            if (rdAllDates.Checked)
            {
                startDate = new DateTime(2004, 1, 1);
                endDate = DateTime.Today;
            }
            else
            {
                startDate = (StartDateTextBox.Text == null && !Information.IsDate(StartDateTextBox.Text)) ?
                     new DateTime(2004, 1, 1) : DateTime.Parse(StartDateTextBox.Text);
                endDate = (EndDateTextBox.Text == null && !Information.IsDate(EndDateTextBox.Text)) ?
                     new DateTime(2010, 1, 1) : DateTime.Parse(EndDateTextBox.Text);
            }

            System.Text.StringBuilder csvData = new System.Text.StringBuilder();
            bool scored = new Surveys().IsSurveyScored(SurveyId);

            // Get an instance of the voter DAL using the DALFactory
            CSVExportData csvExportData = new Voters().GetCSVExport(SurveyId, startDate, endDate);


            string textDelimiter = TextDelimiterTextBox.Text,
                fieldDelimiter = FieldDelimiterTextBox.Text;

            csvData.Append(textDelimiter + "VoterID" + textDelimiter);
            csvData.Append(fieldDelimiter + textDelimiter + "Section" + textDelimiter);
            csvData.Append(fieldDelimiter + textDelimiter + "Start date" + textDelimiter);
            csvData.Append(fieldDelimiter + textDelimiter + "End date" + textDelimiter);
            csvData.Append(fieldDelimiter + textDelimiter + "IP" + textDelimiter);
            if (scored)
            {
                csvData.Append(fieldDelimiter + textDelimiter + "Score" + textDelimiter);
            }
            csvData.Append(fieldDelimiter + textDelimiter + "Email" + textDelimiter);
            csvData.Append(fieldDelimiter + textDelimiter + "UserName" + textDelimiter);

            // Build CSV header
            foreach (CSVExportData.ExportAnswersRow headerColumn in csvExportData.ExportAnswers.Rows)
            {

                headerColumn.ColumnHeader = System.Text.RegularExpressions.Regex.Replace(GetHeader(headerColumn), "<[^>]*>", " ");
                csvData.Append(fieldDelimiter + textDelimiter + headerColumn.ColumnHeader.Replace("[[", "").Replace("]]", "").Replace("!", "").Replace(textDelimiter, textDelimiter + textDelimiter) + textDelimiter);
            }

            WebSecurityAddInCollection securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);
            NameValueCollection addInVoterData;

            if (csvExportData.Voters.Rows.Count > 0)
            {

                // Get security addins data if any available and get key name
                // to add to the export header
                for (int i = 0; i < securityAddIns.Count; i++)
                {
                    addInVoterData = securityAddIns[i].GetAddInVoterData(csvExportData.Voters[0].VoterID);
                    if (addInVoterData != null)
                    {
                        for (int j = 0; j < addInVoterData.Count; j++)
                        {
                            csvData.Append(string.Format("{0}{1}{2}{1}",
                                fieldDelimiter, textDelimiter, addInVoterData.GetKey(j).Replace(textDelimiter, textDelimiter + textDelimiter)));
                        }
                    }
                }
            }

            foreach (CSVExportData.VotersRow voter in csvExportData.Voters.Rows)
            {

                for (int voterSections = 0; voterSections <= GetVoterMaxSections(voter.VoterID, csvExportData); voterSections++)
                {
                    csvData.Append(System.Environment.NewLine);
                    csvData.Append(textDelimiter + voter.VoterID + textDelimiter);
                    csvData.Append(fieldDelimiter + textDelimiter + voterSections + textDelimiter);
                    csvData.Append(fieldDelimiter + textDelimiter + voter.StartDate + textDelimiter);
                    csvData.Append(fieldDelimiter + textDelimiter + voter.VoteDate + textDelimiter);
                    csvData.Append(fieldDelimiter + textDelimiter + voter.IPSource + textDelimiter);
                    if (scored)
                    {
                        csvData.Append(fieldDelimiter + textDelimiter + voter.Score + textDelimiter);
                    }
                    csvData.Append(fieldDelimiter + textDelimiter + voter.Email + textDelimiter);
                    csvData.Append(fieldDelimiter + textDelimiter + voter.UserName + textDelimiter);

                    // reparse all answers to see which answer was answered or not by the voter
                    foreach (CSVExportData.ExportAnswersRow headerColumn in csvExportData.ExportAnswers.Rows)
                    {
                        // Check if the voter has answered
                        CSVExportData.VoterAnswersRow[] answer = (CSVExportData.VoterAnswersRow[])csvExportData.VoterAnswers.Select(string.Format("VoterID={0} AND AnswerID = {1} AND SectionNumber={2}",
                            voter.VoterID, headerColumn.AnswerId, voterSections));
                        if (answer.Length > 0)
                        {
                            int i = answer[0].AnswerTypeId;

                            if (answer[0].IsAnswerTextNull())
                            {
                                csvData.Append(fieldDelimiter + textDelimiter + 1 + textDelimiter);
                            }
                            else
                            {
                                switch (CarriageReturnDropDownList.SelectedValue)
                                {
                                    case "1":
                                        {
                                            csvData.Append(fieldDelimiter + textDelimiter + GetDetail(answer[0], (AnswerTypeEnum)headerColumn.AnswerTypeId).Replace(textDelimiter, textDelimiter + textDelimiter).Replace(Environment.NewLine, ((char)10).ToString()) + textDelimiter);
                                            break;
                                        }
                                    case "2":
                                        {
                                            csvData.Append(fieldDelimiter + textDelimiter + GetDetail(answer[0], (AnswerTypeEnum)headerColumn.AnswerTypeId).Replace(textDelimiter, textDelimiter + textDelimiter).Replace(Environment.NewLine, CRCharTextbox.Text) + textDelimiter);
                                            break;
                                        }
                                    default:
                                        {
                                            csvData.Append(fieldDelimiter + textDelimiter + GetDetail(answer[0], (AnswerTypeEnum)headerColumn.AnswerTypeId).Replace(textDelimiter, textDelimiter + textDelimiter) + textDelimiter);
                                            break;
                                        }

                                }
                            }
                        }
                        else
                        {
                            csvData.Append(fieldDelimiter + textDelimiter + textDelimiter);
                        }
                    }

                    // Get security addins data if any available
                    for (int i = 0; i < securityAddIns.Count; i++)
                    {
                        addInVoterData = securityAddIns[i].GetAddInVoterData(voter.VoterID);
                        if (addInVoterData != null)
                        {
                            for (int j = 0; j < addInVoterData.Count; j++)
                            {
                                if (addInVoterData[j] != null)
                                {
                                    switch (CarriageReturnDropDownList.SelectedValue)
                                    {
                                        case "1":
                                            {
                                                csvData.Append(string.Format("{0}{1}{2}{1}",
                                                    fieldDelimiter, textDelimiter, addInVoterData[j].Replace(textDelimiter, textDelimiter + textDelimiter).Replace(Environment.NewLine, ((char)10).ToString())));
                                                break;
                                            }
                                        case "2":
                                            {
                                                csvData.Append(string.Format("{0}{1}{2}{1}",
                                                    fieldDelimiter, textDelimiter, addInVoterData[j].Replace(textDelimiter, textDelimiter + textDelimiter).Replace(Environment.NewLine, CRCharTextbox.Text)));
                                                break;
                                            }
                                        default:
                                            {
                                                csvData.Append(string.Format("{0}{1}{2}{1}",
                                                    fieldDelimiter, textDelimiter, addInVoterData[j].Replace(textDelimiter, textDelimiter + textDelimiter)));
                                                break;
                                            }

                                    }
                                }
                                else
                                {
                                    csvData.Append(fieldDelimiter + textDelimiter + textDelimiter);
                                }
                            }
                        }
                    }
                }
            }

            return csvData.ToString();
        }

        private string Delimit(string inStr)
        {
            string textDelimiter = TextDelimiterTextBox.Text, fieldDelimiter = FieldDelimiterTextBox.Text;
            return fieldDelimiter + textDelimiter + inStr + textDelimiter;
        }

        private string StripCharactesAndDelimit(string inStr)
        {
            string textDelimiter = TextDelimiterTextBox.Text, fieldDelimiter = FieldDelimiterTextBox.Text;
            inStr = System.Text.RegularExpressions.Regex.Replace(inStr, "<[^>]*>", " ");
            return Delimit(ReplaceDelimitersInText(inStr.Replace("[[", "").Replace("]]", "").Replace("!", ""), textDelimiter));

        }

        private string ReplaceDelimitersInText(string inStr, string textDelimiter)
        {
            return inStr.Replace(textDelimiter, textDelimiter + textDelimiter);
        }

        private string ReplaceCRAndDelimitAnswers(string answerText)
        {
            string textDelimiter = TextDelimiterTextBox.Text, fieldDelimiter = FieldDelimiterTextBox.Text;
            switch (CarriageReturnDropDownList.SelectedValue)
            {
                case "1": return Delimit(ReplaceDelimitersInText(answerText, textDelimiter).Replace(Environment.NewLine, ((char)10).ToString()));
                case "2": return Delimit(ReplaceDelimitersInText(answerText, textDelimiter).Replace(Environment.NewLine, CRCharTextbox.Text));
                default: return Delimit(ReplaceDelimitersInText(answerText, textDelimiter));
            }
        }

        public bool IsMatrixQuestion(int questionSelectionMode)
        {
            return (QuestionSelectionMode)questionSelectionMode == QuestionSelectionMode.Matrix ||
                 (QuestionSelectionMode)questionSelectionMode == QuestionSelectionMode.Matrix;
        }

        public bool isTextAnswer(int answerTypeId)
        {
            AnswerTypeEnum at = (AnswerTypeEnum)answerTypeId;
            return (at == AnswerTypeEnum.FieldBasicType ||
                    at == AnswerTypeEnum.ExtendedFreeTextBoxType ||
                    at == AnswerTypeEnum.BooleanType ||
                    at == AnswerTypeEnum.FieldCalendarType ||
                    at == AnswerTypeEnum.FieldEmailType ||
                    at == AnswerTypeEnum.FieldLargeType ||
                    at == AnswerTypeEnum.FieldPasswordType ||
                    at == AnswerTypeEnum.FieldRequiredType ||
                    at == AnswerTypeEnum.SubscriberXMLList ||
                    at == AnswerTypeEnum.XMLCountryList ||
                    at == AnswerTypeEnum.SelectionOtherType ||
                    at == AnswerTypeEnum.FieldHiddenType ||
                    at == AnswerTypeEnum.FieldAddressType ||
                    at == AnswerTypeEnum.FieldSliderType
                    );

        }

        public string TrimSplit(string x)
        {
            char splitChar = '|';
            return x.TrimEnd(new char[] { splitChar });
        }

        public string GetQuestionOnlyHeader(CSVExportData.ExportAnswersRow header)
        {
            string splitChar = "|";
            switch (ddlHeader.SelectedValue)
            {

                case "Question":
                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionTextNull() ? string.Empty : header.ParentQuestionText)
                            + splitChar + (header.IsQuestionTextNull() ? string.Empty : header.QuestionText));
                    else
                        return header.QuestionText;
                case "QuestionDisplayOrderNumber":
                    if (!header.IsRowOrderNull())
                        return TrimSplit(header.QuestionDisplayOrder.ToString() + splitChar + header.RowOrder);
                    else
                        return header.QuestionDisplayOrder.ToString();

                case "QuestionID": if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionIdTextNull() ? string.Empty :header.ParentQuestionIdText)+splitChar+
                            header.RowOrder.ToString() );
                    else
                        return TrimSplit(header.IsQuestionIdTextNull() ? header.QuestionText :
                             header.QuestionIdText.ToString());

                case "QuestionAlias":
                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionAliasTextNull() ? header.QuestionText : header.ParentQuestionAliasText) +
                            splitChar + header.RowOrder.ToString());
                    else
                        return header.IsQuestionAliasNull() ? header.QuestionText : header.QuestionAlias;

                case "QuestionIDAlias":
                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionIdTextNull() ? header.ParentQuestionText : header.ParentQuestionIdText) +
                      " " + (header.IsParentQuestionAliasTextNull() ? string.Empty : header.ParentQuestionAliasText) +
                      splitChar + header.RowOrder.ToString());
                    else
                        return TrimSplit(header.IsQuestionIdTextNull() ?
                        header.QuestionText : header.QuestionIdText + " " +
                        (header.IsQuestionAliasNull() ? string.Empty : header.QuestionAlias));

                //default: return "Invalid DDl Value";
                default: return (GetPageResource("DDlValue"));
            }
        }

        public string GetHeader(CSVExportData.ExportAnswersRow header)
        {
            string splitChar = "|";
            switch (ddlHeader.SelectedValue)
            {
                case "Question":

                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.ParentQuestionText + splitChar + header.QuestionText + splitChar + header.AnswerText));
                    else
                        return TrimSplit((header.QuestionText + splitChar + header.AnswerText));

                case "QuestionDisplayOrderNumber":
                    if (!header.IsRowOrderNull())
                        return TrimSplit(header.QuestionDisplayOrder.ToString() + splitChar + header.RowOrder.ToString() + splitChar + header.AnswerText);
                    else
                        return TrimSplit(header.QuestionDisplayOrder.ToString() + splitChar + header.AnswerDisplayOrder.ToString());

                case "QuestionID":
                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionIdTextNull() ? string.Empty : header.ParentQuestionIdText) +
                            splitChar + header.RowOrder.ToString() +
                            splitChar + (header.IsAnswerTextNull() ? string.Empty : header.AnswerText));
                    else
                        return TrimSplit((header.IsQuestionIdTextNull() ? header.QuestionText : header.QuestionIdText)
                            + splitChar + (header.IsAnswerIdTextNull() ? string.Empty : header.AnswerIdText));

                case "QuestionAlias":
                    if (!header.IsRowOrderNull())
                        return TrimSplit(header.ParentQuestionAliasText.ToString()
                            + splitChar + header.RowOrder.ToString() + splitChar + header.AnswerText);
                    else

                        return TrimSplit(header.IsQuestionAliasNull() ? header.QuestionText : header.QuestionAlias
                        + splitChar + (header.IsAnswerAliasNull() ?
                        header.IsAnswerTextNull() ? string.Empty : header.AnswerText : header.AnswerAlias));

                case "QuestionIDAlias":
                    if (!header.IsRowOrderNull())
                        return TrimSplit((header.IsParentQuestionIdTextNull() ? string.Empty :
                            header.ParentQuestionIdText) + " " +
                            (header.IsParentQuestionAliasTextNull() ? string.Empty :
                            header.ParentQuestionAliasText) + splitChar +
                            header.RowOrder.ToString() + splitChar + header.AnswerText);
                    else
                        return
                            TrimSplit(header.IsQuestionIdTextNull() ? string.Empty : header.QuestionIdText + " " +
                        (header.IsQuestionAliasNull() ? string.Empty : header.QuestionAlias) + "| " +
                       (header.IsAnswerIdTextNull() ? string.Empty : header.AnswerIdText) + " " +
                       (header.IsAnswerAliasNull() ? string.Empty : header.AnswerAlias));
                default: return TrimSplit("Invalid DDl Value");
            }
        }

        public string GetAnswerOnlyHeader(CSVExportData.ExportAnswersRow header)
        {
            switch (ddlAnswer.SelectedValue)
            {
                case "Answer": return header.IsAnswerTextNull() ? string.Empty : header.AnswerText;
                case "AnswerDisplayOrderNumber":
                    if (!header.IsRowOrderNull())
                        return TrimSplit(header.AnswerDisplayOrder.ToString());
                    else
                        return header.AnswerDisplayOrder.ToString();
                case "AnswerID": return header.IsAnswerIdTextNull() ? (header.IsAnswerTextNull() ? string.Empty : header.AnswerText) : header.AnswerIdText;
                case "AnswerAlias": return header.IsAnswerAliasNull() ? (header.IsAnswerTextNull() ? string.Empty : header.AnswerText) : header.AnswerAlias;
                case "AnswerIdAlias": return (header.IsAnswerIdTextNull()
                       ? (header.IsAnswerTextNull() ? string.Empty : header.AnswerText) : header.AnswerIdText) + " " +
                       (header.IsAnswerAliasNull()
                       ? (header.IsAnswerTextNull() ? string.Empty : header.AnswerText) : header.AnswerAlias);
                default: return "Invalid DDl Value";
            }
        }

        public string GetDetail(CSVExportData.VoterAnswersRow detail, AnswerTypeEnum answerType)
        {
            switch (ddlAnswer.SelectedValue)
            {
                 case "Answer": return answerType == AnswerTypeEnum.SelectionTextType ?
                    (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText) :
                    (detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText);

                case "AnswerDisplayOrderNumber":
                    if (isTextAnswer(detail.AnswerTypeId))
                        return detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText;
                    else
                        return answerType == AnswerTypeEnum.SelectionOtherType ?
                            (detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText) :
                            detail.AnswerDisplayOrder.ToString();

                case "AnswerID":
                    if (isTextAnswer(detail.IsAnswerTypeIdNull()?0:detail.AnswerTypeId))
                        return detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText;
                    else
                        return answerType == AnswerTypeEnum.SelectionTextType ?
                            (detail.IsAnswerIdAliasNull() ?
                            (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText) : detail.AnswerIdAlias) :
                            (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText);

                case "AnswerAlias":
                    if (isTextAnswer(detail.AnswerTypeId))
                        return detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText;
                    else
                        return answerType == AnswerTypeEnum.SelectionTextType ?
                            (detail.IsAnswerAliasNull() ? (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText)
                            : detail.AnswerAlias) :
                        (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText);

                case "AnswerIdAlias":
                    if (isTextAnswer(detail.AnswerTypeId))
                        return detail.IsAnswerTextNull() ? string.Empty : detail.AnswerText;
                    else
                        return answerType == AnswerTypeEnum.SelectionTextType ?
                                                (detail.IsAnswerIdAliasNull() ? detail.AnswerAnswerText : detail.AnswerIdAlias) + " " +
                                                (detail.IsAnswerAliasNull() ? string.Empty : detail.AnswerAlias) :
                      (detail.IsAnswerAnswerTextNull() ? string.Empty : detail.AnswerAnswerText);

                default: return "Invalid DDl Value";
            }
        }

        private string ExportCSVStyleOne()
        {
            try
            {
                DateTime startDate;
                DateTime endDate;
                if (rdAllDates.Checked)
                {
                    startDate = new DateTime(2004, 1, 1);
                    endDate = DateTime.Today;
                }
                else
                {
                    startDate = DateTime.Parse(StartDateTextBox.Text);
                    endDate =  DateTime.Parse(EndDateTextBox.Text);
                }

                System.Text.StringBuilder csvData = new System.Text.StringBuilder();
                bool scored = new Surveys().IsSurveyScored(SurveyId);

                // Get an instance of the voter DAL using the DALFactory
                CSVExportData csvExportData = new Voters().GetCSVExport(SurveyId, startDate, endDate);


                string textDelimiter = TextDelimiterTextBox.Text,
                    fieldDelimiter = FieldDelimiterTextBox.Text;
                csvData.Append(textDelimiter + "VoterID" + textDelimiter);
                csvData.Append(Delimit("Section"));
                csvData.Append(Delimit("Start date"));
                csvData.Append(Delimit("End date"));
                csvData.Append(Delimit("IP"));
                if (scored)
                {
                    csvData.Append(Delimit("Score"));
                }
                csvData.Append(Delimit("Email"));
                csvData.Append(Delimit("UserName"));

                // Build CSV header
                //
                //Whenever question Changes we need to detect
                int questionIdPrev = -1;
                foreach (CSVExportData.ExportAnswersRow headerColumn in csvExportData.ExportAnswers.Rows)
                {
                    QuestionSelectionMode selectionMode;
                    if (headerColumn.QuestionId != questionIdPrev) // Detect Question Change to emit Question Text alone for Certain conditions
                    {
                        selectionMode = (QuestionSelectionMode)headerColumn.SelectionModeId;
                        questionIdPrev = headerColumn.QuestionId;
                        if (csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev &&
                           ((AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionTextType)) != null)
                            csvData.Append(StripCharactesAndDelimit(GetQuestionOnlyHeader(headerColumn)));

                        // New requirement for Others we need a seperate column
                        var firstRec = csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev &&
                           ((AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionOtherType));
                        if (firstRec != null)
                            csvData.Append(StripCharactesAndDelimit(GetHeader(firstRec)));
                    }
                    if ((AnswerTypeEnum)headerColumn.AnswerTypeId != AnswerTypeEnum.SelectionTextType && //Selection Types go Under Question Header
                       (AnswerTypeEnum)headerColumn.AnswerTypeId != AnswerTypeEnum.SelectionOtherType)
                        csvData.Append(StripCharactesAndDelimit(GetHeader(headerColumn)));
                }

                WebSecurityAddInCollection securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);
                NameValueCollection addInVoterData;

                if (csvExportData.Voters.Rows.Count > 0)
                {

                    // Get security addins data if any available and get key name
                    // to add to the export header
                    for (int i = 0; i < securityAddIns.Count; i++)
                    {
                        addInVoterData = securityAddIns[i].GetAddInVoterData(csvExportData.Voters[0].VoterID);
                        if (addInVoterData != null)
                        {
                            for (int j = 0; j < addInVoterData.Count; j++)
                                csvData.Append(Delimit(ReplaceDelimitersInText(addInVoterData.GetKey(j), textDelimiter)));
                        }
                    }
                }

                foreach (CSVExportData.VotersRow voter in csvExportData.Voters.Rows)
                {

                    for (int voterSections = 0; voterSections <= GetVoterMaxSections(voter.VoterID, csvExportData); voterSections++)
                    {
                        csvData.Append(System.Environment.NewLine);

                        csvData.Append(textDelimiter + voter.VoterID + textDelimiter);
                        csvData.Append(fieldDelimiter + textDelimiter + voterSections + textDelimiter);
                        csvData.Append(fieldDelimiter + textDelimiter + voter.StartDate + textDelimiter);
                        csvData.Append(fieldDelimiter + textDelimiter + voter.VoteDate + textDelimiter);
                        csvData.Append(fieldDelimiter + textDelimiter + voter.IPSource + textDelimiter);
                        if (scored)
                        {
                            csvData.Append(fieldDelimiter + textDelimiter + voter.Score + textDelimiter);
                        }
                        csvData.Append(fieldDelimiter + textDelimiter + voter.Email + textDelimiter);
                        csvData.Append(fieldDelimiter + textDelimiter + voter.UserName + textDelimiter);


                        // For every new question concatenate with # all Select type Answers and emit as first row
                        // Thereafter look only at non select type answers
                        //Do the above only if the Voter has answered the question
                        //
                        questionIdPrev = -1;

                        foreach (CSVExportData.ExportAnswersRow headerColumn in csvExportData.ExportAnswers.Rows)
                        {
                            if (headerColumn.QuestionId != questionIdPrev) // Detect Question Change to emit Question Text alone for Certain conditions
                            {
                                questionIdPrev = headerColumn.QuestionId;
                                //if this question has any answers of type Select then we need to emit the Question Text
                                // Once that is done skip all Select type questions as they have only one answer and it goes under Question heading

                                string concatenatedOthers = string.Empty;
                                if (csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev && (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionOtherType) != null)
                                {
                                    csvExportData.VoterAnswers
                                        .Where(x => x.VoterID == voter.VoterID &&
                                               x.QuestionId == questionIdPrev &&
                                               x.SectionNumber == voterSections &&
                                               (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionOtherType)
                                       .ToList().ForEach(y => concatenatedOthers += GetDetail(y, AnswerTypeEnum.SelectionOtherType) +
                                           (MultiSeparatorTextBox.Text == string.Empty ? "#" : MultiSeparatorTextBox.Text));
                                    // csvData.Append(ReplaceCRAndDelimitAnswers(concatenatedOthers.TrimEnd('#')));
                                }

                                string concatenatedAnswers = string.Empty;
                                if (csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev &&
                                   (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionTextType) != null)
                                {
                                    csvExportData.VoterAnswers
                                        .Where(x => x.VoterID == voter.VoterID &&
                                               x.QuestionId == questionIdPrev &&
                                               x.SectionNumber == voterSections &&
                                               (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionTextType)
                                       .ToList().ForEach(y => concatenatedAnswers += GetDetail(y, AnswerTypeEnum.SelectionTextType) +
                                       (MultiSeparatorTextBox.Text == string.Empty ? "#" : MultiSeparatorTextBox.Text));



                                }

                                //Another change.If the there is Other types get the heading of the order and add it.
                                // if ((headerColumn.SelectionModeId == 1 || headerColumn.SelectionModeId == 2) && !string.IsNullOrEmpty(concatenatedOthers))//radiobutton selection

                                if (!string.IsNullOrEmpty(concatenatedOthers))//radiobutton selection
                                {
                                    var dat = csvExportData.ExportAnswers.Where(x => x.QuestionId == questionIdPrev &&
                                                   (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionOtherType).FirstOrDefault();
                                    if (dat != null)
                                    {
                                        concatenatedAnswers += GetAnswerOnlyHeader(dat);
                                    }

                                }
                                if (csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev &&
                                  (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionTextType) != null)
                                {
                                    csvData.Append(ReplaceCRAndDelimitAnswers(concatenatedAnswers.TrimEnd((MultiSeparatorTextBox.Text == string.Empty ? '#' : MultiSeparatorTextBox.Text[0]))));
                                }
                                if (csvExportData.ExportAnswers.FirstOrDefault(x => x.QuestionId == questionIdPrev && (AnswerTypeEnum)x.AnswerTypeId == AnswerTypeEnum.SelectionOtherType) != null)
                                {
                                    csvData.Append(ReplaceCRAndDelimitAnswers(concatenatedOthers.TrimEnd(MultiSeparatorTextBox.Text == string.Empty ? '#' : MultiSeparatorTextBox.Text[0])));
                                }

                            }

                            if (((AnswerTypeEnum)headerColumn.AnswerTypeId != AnswerTypeEnum.SelectionTextType &&
                                   (AnswerTypeEnum)headerColumn.AnswerTypeId != AnswerTypeEnum.SelectionOtherType))
                            {
                                // Check if the voter has answered
                                CSVExportData.VoterAnswersRow[] answer = (CSVExportData.VoterAnswersRow[])csvExportData.VoterAnswers.Select(string.Format("VoterID={0} AND AnswerID = {1} AND SectionNumber={2}",
                                    voter.VoterID, headerColumn.AnswerId, voterSections));
                                if (answer.Length > 0) csvData.Append(ReplaceCRAndDelimitAnswers(GetDetail(answer[0], (AnswerTypeEnum)headerColumn.AnswerTypeId)));
                                else csvData.Append(Delimit(string.Empty));
                            }
                        }

                        // Get security addins data if any available
                        for (int i = 0; i < securityAddIns.Count; i++)
                        {
                            addInVoterData = securityAddIns[i].GetAddInVoterData(voter.VoterID);
                            if (addInVoterData != null)
                            {
                                for (int j = 0; j < addInVoterData.Count; j++)
                                {
                                    if (addInVoterData[j] != null) csvData.Append(ReplaceCRAndDelimitAnswers(addInVoterData[j]));
                                    else csvData.Append(fieldDelimiter + textDelimiter + textDelimiter);

                                }
                            }
                        }
                    }
                }

                return csvData.ToString();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(MessageLabel, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the highest section created by the voter
        /// </summary>
        private int GetVoterMaxSections(int voterId, CSVExportData csvExportData)
        {
            int sectionCount = 0;
            CSVExportData.VoterAnswersRow[] voterSectionCount = (CSVExportData.VoterAnswersRow[])csvExportData.VoterAnswers.Select("VoterID=" + voterId, "SectionNumber DESC");
            if (voterSectionCount.Length > 0)
            {
                sectionCount = voterSectionCount[0].SectionNumber;
            }

            return sectionCount;
        }

        private void ExportDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Xml export selected
            if (ExportDropDownList.SelectedValue == "1")
            {
                CSVOptionPlaceHolder.Visible = false;
                plhCSVStyles.Visible = false;
                ExportDataButton.Visible = false;
                VoterExportXMLButton.Visible = true;
                //pdf button
                ExportDataPdfButton.Visible = false;
            }
            else
            {
                CSVOptionPlaceHolder.Visible = true;
                plhCSVStyles.Visible = true;
                ExportDataButton.Visible = true;
                VoterExportXMLButton.Visible = false;
                //pdf button
                ExportDataPdfButton.Visible = true;
            }
        }

        private void CarriageReturnDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CarriageReturnDropDownList.SelectedValue == "2")
            {
                CRCharTextbox.Visible = true;
            }
            else
            {
                CRCharTextbox.Visible = false;
            }
        }
    }
}
