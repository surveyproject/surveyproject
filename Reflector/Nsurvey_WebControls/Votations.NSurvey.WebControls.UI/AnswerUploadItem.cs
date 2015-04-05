namespace Votations.NSurvey.WebControls.UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Resources;
    using System.Collections.Generic;
    /// <summary>
    /// File upload answer item
    /// </summary>
    public class AnswerUploadItem : ExtendedAnswerItem, IMandatoryAnswer
    {

        private Table _adminTable;
        private TextBox _contentTypeAcceptTextBox;
        private TextBox _fileSizeTextBox;
        private Table _fileTable = new Table();
        private HtmlInputFile _fileUpload = new HtmlInputFile();
        private bool _mandatory = false;
        private DropDownList _maxUploadFilesDropDownList;
        private Button _uploadButton = new Button();

        public AnswerUploadItem()
        {
            var files = RestoreTable();
            foreach(var f in files)
                this._fileTable.Rows.Add(this.GetFileRow(f.fileId,f.groupGuid,f.fileSize,f.fileName,f.fileType));
        }
        private TableRow BuildOptionRow(string label, Control optionControl, string comment)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            TableCell cell2 = new TableCell();
            cell.Wrap = false;
            if (label != null)
            {
                Label child = new Label();
                child.ControlStyle.Font.Bold = true;
                child.Text = label;
                child.CssClass = "AnswerTextRender";//JJ
                cell.Controls.Add(child);
                if (comment != null)
                {
                    cell.Controls.Add(new LiteralControl("<br />" + comment));
                }
                row.Cells.Add(cell);
            }
            else
            {
                cell2.ColumnSpan = 2;
            }
            cell2.Controls.Add(optionControl);
            row.Cells.Add(cell2);
            return row;
        }


        private void StoreFileRow(FileInfo finf)
        {
            if (this.ViewState["FileTable"] == null) this.ViewState["FileTable"] = new List<FileInfo>();
            var f = this.ViewState["FileTable"] as List<FileInfo>;
            f.Add(finf);
            this.ViewState["FileTable"] = f;
        }

        private List<FileInfo> RestoreTable()
        {
            if (this.ViewState["FileTable"] == null) this.ViewState["FileTable"] = new List<FileInfo>();
            var f = this.ViewState["FileTable"] as List<FileInfo>;
            return f;
        }

        /// <summary>
        /// Creates the checkbox control "layout" and adds
        /// it to the overall control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            if (this.SetupFormEncoding())
            {
                Literal child = new Literal();
                child.EnableViewState = false;
                this.Controls.Add(child);

                if (this.ShowAnswerText)
                {
                    if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                    {
                        Image image = new Image();
                        image.ImageUrl = this.ImageUrl;
                        image.ImageAlign = ImageAlign.Middle;
                        image.ToolTip = this.Text;
                        this.Controls.Add(image);
                    }
                    else
                    {
                        Literal literal2 = new Literal();
                        literal2.Text = this.Text;
                        this.Controls.Add(literal2);

                        if (this.Mandatory)
                        {
                            Label label1 = new Label();
                            label1.Text = "&nbsp; * &nbsp;";
                            label1.ForeColor = System.Drawing.Color.Red;
                            this.Controls.Add(label1);
                        }
                    }
                    //this.Controls.Add(new LiteralControl("<br />"));
                }
                this._fileUpload.Disabled = this.RenderMode == ControlRenderMode.ReadOnly;
                this.Controls.Add(this._fileUpload);

                this._uploadButton.Click += new EventHandler(this.uploadButton_Click);
                this._uploadButton.Text = ResourceManager.GetString("UploadFileUploadButton", base.LanguageCode);
                this._uploadButton.CssClass = "btn btn-primary btn-xs bw fileuploadbuttonmargin";
                this._uploadButton.Enabled = this.RenderMode != ControlRenderMode.ReadOnly;
                this.Controls.Add(this._uploadButton);

                this.GenerateFileList();
                this.Controls.Add(this._fileTable);
            }
        }

        protected virtual void deleteButton_Command(object sender, CommandEventArgs e)
        {
            string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
            new Answer().DeleteAnswerFile(int.Parse(strArray[0]), strArray[1]);
            this.GenerateFileList();
            this.OnAnswerMessage(new AnswerItemMessageEventArgs(ResourceManager.GetString("UploadFileDeletedMessage", base.LanguageCode), AnswerMessageType.Information));
        }

        /// <summary>
        /// Sends the file to the client
        /// </summary>
        protected virtual void downloadButton_Command(object sender, CommandEventArgs e)
        {
            string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
            byte[] answerFileData = new Answers().GetAnswerFileData(int.Parse(strArray[0]), strArray[1]);
            this.Context.Response.Clear();
            this.Context.Response.ContentType = ((strArray[3] != null) && (strArray[3].Length > 0)) ? strArray[3] : "application/octet-stream";
            this.Context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + strArray[2] + "\"");
            this.Context.Response.BinaryWrite(answerFileData);
            this.Context.Response.End();
        }

        protected virtual void GenerateFileList()
        {
           // return;
            //JJ Return so the File is not displayed in other surveys ad 
            this._fileTable.ControlStyle.Font.CopyFrom(base.AnswerStyle.Font);
            this._fileTable.ControlStyle.CssClass = "lddl"; 
            this._fileTable.Rows.Clear();
            FileData guidFiles = new Answers().GetGuidFiles(this.GroupGuid);
            if (guidFiles.Files.Rows.Count > 0)
            {
                foreach (FileData.FilesRow row in guidFiles.Files)
                {
                    this._fileTable.Rows.Add(this.GetFileRow(row.FileId, row.GroupGuid, row.FileSize, row.FileName, row.FileType));
                }
            }
        }

        /// <summary>
        /// Generates the interface to set the 
        /// extended properties (file size, file number etc ...)
        /// </summary>
        /// <returns></returns>
        public override Control GeneratePropertiesUI()
        {
            this._adminTable = new Table();
            this._adminTable.CssClass = "rounded_corners";
            this._adminTable.Width = Unit.Percentage(100);
            new TableRow();
            
            this._maxUploadFilesDropDownList = new DropDownList();
            this._maxUploadFilesDropDownList.ID = "MaxUploadDropDownList";
            for (int i = 1; i <= 10; i++)
            {
                this._maxUploadFilesDropDownList.Items.Add(i.ToString());
            }
            this._maxUploadFilesDropDownList.Items.Add(new ListItem(ResourceManager.GetString("UnlimitedFileNumberOption", base.LanguageCode), "-1"));
            this._maxUploadFilesDropDownList.SelectedValue = this.MaxFileNumber.ToString();
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("MaxUploadFileNumberLabel", base.LanguageCode), this._maxUploadFilesDropDownList, null));
            
            this._fileSizeTextBox = new TextBox();
            this._fileSizeTextBox.ID = "FileSizeTextBox";
            this._fileSizeTextBox.Columns = 10;
            this._fileSizeTextBox.Text = this.MaxFileSize.ToString();
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("MaxUploadFileSizeLabel", base.LanguageCode), this._fileSizeTextBox, null));
            
            this._contentTypeAcceptTextBox = new TextBox();
            this._contentTypeAcceptTextBox.ID = "ContentTypeAcceptTextBox";
            this._contentTypeAcceptTextBox.Columns = 20;
            this._contentTypeAcceptTextBox.Text = this.AcceptedContentType.ToString();
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("AcceptedContentTypeLabel", base.LanguageCode), this._contentTypeAcceptTextBox, ResourceManager.GetString("AcceptedContentTypeExampleLabel")));
            
            Button optionControl = new Button();
            optionControl.ID = "ApplyChangeButton";
            optionControl.Text = ResourceManager.GetString("ApplyChangesButton", base.LanguageCode);
            optionControl.CssClass = "btn btn-primary btn-xs bw fileuploadbuttonmargin";
            optionControl.Click += new EventHandler(this.OnClick);
            this._adminTable.Rows.Add(this.BuildOptionRow(null, optionControl, null));

            return this._adminTable;
        }
        [Serializable]
        private class FileInfo
        {
            public int fileId { get; set; }
            public string groupGuid { get; set; }
            public int fileSize { get; set; }
            public string fileName { get; set; }
            public string fileType { get; set; }
        }

        protected virtual TableRow GetFileRow(int fileId, string groupGuid, int fileSize, string fileName, string fileType)
        {
            StoreFileRow(new FileInfo { fileId = fileId, fileName = fileName, fileSize = fileSize, fileType = fileType, groupGuid = groupGuid });
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = "&nbsp;";
            row.Cells.Add(cell);
            if (this.RenderMode == ControlRenderMode.Edit)
            {
                cell = new TableCell();
                cell.Text = fileName;
                row.Cells.Add(cell);

                cell = new TableCell();
                double num = Math.Round((double)((((double)fileSize) / 1048576.0) * 100000.0)) / 100000.0;
                cell.Text = num.ToString("0.##") + " " + ResourceManager.GetString("UploadFileSizeFormat", base.LanguageCode);
                row.Cells.Add(cell);

                cell = new TableCell();

                LinkButton button = new LinkButton();
                button.Text = ResourceManager.GetString("UploadFileDownloadButton", base.LanguageCode);
                button.CommandArgument = string.Format("{0},{1},{2},{3}", new object[] { fileId.ToString(), groupGuid, fileName, fileType });
                button.Command += new CommandEventHandler(this.downloadButton_Command);
                button.CssClass = "nsurveyUploadCommandClass";
                cell.Controls.Add(button);

                cell.Controls.Add(new LiteralControl("&nbsp;|"));
                row.Cells.Add(cell);
            }
            else
            {
                cell.Text = fileName;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = ((Math.Round((double)((((double)fileSize) / 1048576.0) * 100000.0)) / 100000.0)).ToString("0.##") + " " + ResourceManager.GetString("UploadFileSizeFormat", base.LanguageCode);
                row.Cells.Add(cell);
            }
            cell = new TableCell();
            LinkButton child = new LinkButton();
            child.ID = GlobalConfig.DeleteFileButtonName + fileId;
            child.CssClass = "nsurveyUploadCommandClass";
            child.Text = ResourceManager.GetString("UploadFileDeleteButton", base.LanguageCode);
            child.CommandArgument = string.Format("{0},{1},{2},{3}", new object[] { fileId.ToString(), groupGuid, fileName, fileType });
            child.Command += new CommandEventHandler(this.deleteButton_Command);
            cell.Controls.Add(child);
            row.Cells.Add(cell);

            return row;
        }

        /// <summary>
        /// Returns the check box status to the event subscribers 
        /// once the survey's page get posted
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            int answerFileCount = new Answers().GetAnswerFileCount(this.GroupGuid);
            if (this.Mandatory && (answerFileCount == 0))
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("UploadFileRequiredMessage", base.LanguageCode), this.Text)));
            }
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this.GroupGuid, AnswerTypeMode.ExtendedType | AnswerTypeMode.Upload | AnswerTypeMode.Mandatory));

            return datas;
        }

        protected virtual void OnClick(object sender, EventArgs e)
        {
            if (Information.IsNumeric(this._fileSizeTextBox.Text))
            {
                this.MaxFileSize = int.Parse(this._fileSizeTextBox.Text);
                this.MaxFileNumber = int.Parse(this._maxUploadFilesDropDownList.SelectedValue.ToString());
                if (this._contentTypeAcceptTextBox.Text.Length > 0)
                {
                    this.AcceptedContentType = this._contentTypeAcceptTextBox.Text;
                }
                this.PresistProperties();
            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text = ResourceManager.GetString("InvalidIPExpireTime", base.LanguageCode);
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);
                this._adminTable.Rows.AddAt(0, row);
            }
        }

        /// <summary>
        /// Upload field needs a special enctype 
        /// to work and allow multipart mimes
        /// to be able to receive the file
        /// </summary>
        /// <returns></returns>
        protected virtual bool SetupFormEncoding()
        {
            bool flag = false;
            Control parent = this.Parent;
            while ((parent != null) && (parent.GetType() != typeof(HtmlForm)))
            {
                parent = parent.Parent;
            }
            HtmlForm form = parent as HtmlForm;
            if (form != null)
            {
                form.Enctype = "multipart/form-data";
                flag = true;
            }
            return flag;
        }


        protected virtual void uploadButton_Click(object sender, EventArgs e)
        {
            if (((this._fileUpload.PostedFile != null) && (this._fileUpload.PostedFile.ContentLength > 0)) && this.ValidateUpload())
            {
                byte[] buffer = new byte[this._fileUpload.PostedFile.ContentLength];


                this._fileUpload.PostedFile.InputStream.Read(buffer, 0, this._fileUpload.PostedFile.ContentLength);
                int fileId = new Answer().StoreAnswerFile(this.GroupGuid, Path.GetFileName(this._fileUpload.PostedFile.FileName), this._fileUpload.PostedFile.ContentLength, this._fileUpload.PostedFile.ContentType, buffer, GlobalConfig.UploadedFileDeleteTimeOut, GlobalConfig.SessionUploadedFileDeleteTimeOut);
                this._fileTable.Rows.Add(this.GetFileRow(fileId, this.GroupGuid, this._fileUpload.PostedFile.ContentLength, Path.GetFileName(this._fileUpload.PostedFile.FileName), this._fileUpload.PostedFile.ContentType));
                this.OnAnswerMessage(new AnswerItemMessageEventArgs(ResourceManager.GetString("FileUploadedMessage", base.LanguageCode), AnswerMessageType.Information));
            }
        }

        /// <summary>
        /// Valdidates the upload against the answer item
        /// properties
        /// </summary>
        protected virtual bool ValidateUpload()
        {
            string message = null;
            if (this.RenderMode == ControlRenderMode.Edit)
            {
                return true;
            }
            if ((this._fileUpload.PostedFile != null) && (Path.GetFileName(this._fileUpload.PostedFile.FileName).Length > 0))
            {
                if (((this.AcceptedContentType != null) && (this.AcceptedContentType.Length > 0)) && (this.AcceptedContentType.ToLower().IndexOf(this._fileUpload.PostedFile.ContentType.ToLower()) < 0))
                {
                    message = string.Format(ResourceManager.GetString("ContentTypeNotAllowedMessage", base.LanguageCode), this.Text);
                }
                else if ((this.MaxFileNumber > 0) && (new Answers().GetAnswerFileCount(this.GroupGuid) >= this.MaxFileNumber))
                {
                    message = string.Format(ResourceManager.GetString("UploadFileLimitReachedMessage", base.LanguageCode), this.Text, this.MaxFileNumber);
                }
                else if ((this.MaxFileSize > 0) && (this._fileUpload.PostedFile.ContentLength > this.MaxFileSize))
                {
                    message = string.Format(ResourceManager.GetString("UploadFileToBigMessage", base.LanguageCode), this.Text, (Math.Round((double)((((double)this.MaxFileSize) / 1048576.0) * 100000.0)) / 100000.0).ToString("0.##"));
                }
            }
            if (message == null)
            {
                return true;
            }
            this.OnAnswerMessage(new AnswerItemMessageEventArgs(message, AnswerMessageType.Error));
            return false;
        }

        /// <summary>
        /// What files are accepted, list must be a comma separated list
        /// eg : application/pdf, image/gif
        /// etc ...
        /// </summary>
        public string AcceptedContentType
        {
            get
            {
                if (base.Properties.ContainsProperty("AcceptedContentType") && (base.Properties["AcceptedContentType"] != null))
                {
                    return base.Properties["AcceptedContentType"].ToString();
                }
                return string.Empty;
            }
            set
            {
                if (value == null)
                {
                    base.Properties.Remove("AcceptedContentType");
                }
                else
                {
                    base.Properties["AcceptedContentType"] = value;
                }
            }
        }

        /// <summary>
        /// Guid that allows to group several
        /// file uploads under the same answer and to retrieve
        /// already saved files in the database when a user
        /// resumes or does a page change or session resume
        /// </summary>
        public string GroupGuid
        {

            get
            {
                if (this.ViewState["GroupGuid"] == null)
                {
                    object obj2 = Guid.NewGuid().ToString();
                    this.ViewState["GroupGuid"] = (this.DefaultText == null) ? obj2 : this.DefaultText;
                }
                return this.ViewState["GroupGuid"].ToString();
            }
            set
            {
                this.ViewState["GroupGuid"] = value;
            }
        }

        /// <summary>
        /// Is a file upload mandatory ?
        /// </summary>
        public virtual bool Mandatory
        {
            get
            {
                return this._mandatory;
            }
            set
            {
                this._mandatory = value;
            }
        }

        /// <summary>
        /// Maximum of files that can be uploaded
        /// </summary>
        public int MaxFileNumber
        {
            get
            {
                if (!base.Properties.ContainsProperty("MaxUploadFiles"))
                {
                    return 1;
                }
                return int.Parse(base.Properties["MaxUploadFiles"].ToString());
            }
            set
            {
                base.Properties["MaxUploadFiles"] = value;
            }
        }



        /// <summary>
        /// Maximum of size an upload file can have
        /// </summary>
        public int MaxFileSize
        {
            get
            {
                if (!base.Properties.ContainsProperty("UploadFileSize"))
                {
                    return 0x100000;
                }
                return int.Parse(base.Properties["UploadFileSize"].ToString());
            }
            set
            {
                base.Properties["UploadFileSize"] = value;
            }
        }
    }
}

