namespace DNNBase.WebControls
{
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    using DotNetNuke.Instrumentation;

    using DotNetNuke.Security.Permissions;

    using DotNetNuke.Services.FileSystem;

    using FileInfo = DotNetNuke.Services.FileSystem.FileInfo;

    using DotNetNuke.Web.UI;
    using DotNetNuke.Web.UI.WebControls;

    using System;
    using System.IO;

    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using Telerik.Web.UI;

    /// <summary>
    /// Dnn file picker.
    /// </summary>
    public class DnnFileSelector : CompositeControl, ILocalizable
    {
        #region Protected Enums

        /// <summary>
        /// File control modes.
        /// </summary>
        protected enum FileControlMode
        {
            Normal, UploadFile, Preview
        }

        #endregion

        #region Private Static Fields

        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(DnnFileSelector));

        #endregion

        #region Private Fields

        private HiddenField _hfFileId;

        private Panel _pnlButtons;
        private Panel _pnlRightDiv;
        private Panel _pnlLeftDiv;
        private Panel _pnlContainer;
        private Panel _pnlFolder;
        private Panel _pnlMessage;
        private Panel _pnlUpload;
        private Panel _pnlFile;

        private HtmlInputFile _txtFile;

        private DnnComboBox _cboFolders;
        private DnnComboBox _cboFiles;

        private Label _lblFolder;
        private Label _lblMessage;
        private Label _lblFile;

        private LinkButton _cmdCancel;
        private LinkButton _cmdSave;
        private LinkButton _cmdUpload;

        private Image _imgPreview;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets mode.
        /// </summary>
        protected FileControlMode Mode
        {
            get { return ViewState["Mode"] == null ? FileControlMode.Normal : (FileControlMode)ViewState["Mode"]; }

            set { ViewState["Mode"] = value; }
        }

        /// <summary>
        /// Gets parent folder path.
        /// </summary>
        protected string ParentFolder
        {
            get { return PortalSettings.HomeDirectoryMapPath; }
        }

        /// <summary>
        /// Gets portal id.
        /// </summary>
        protected int PortalId
        {
            get
            {
                if ((Page.Request.QueryString["pid"] != null) && (Globals.IsHostTab(PortalSettings.ActiveTab.TabID) || UserController.Instance.GetCurrentUserInfo().IsSuperUser))
                {
                    return Int32.Parse(Page.Request.QueryString["pid"]);
                }

                return PortalSettings.PortalId;
            }
        }

        /// <summary>
        /// Gets portal settings.
        /// </summary>
        protected PortalSettings PortalSettings
        {
            get { return PortalController.Instance.GetCurrentPortalSettings(); }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets resource.
        /// </summary>
        public string LocalResourceFile
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets need localize.
        /// </summary>
        public bool Localize
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets command CSS class.
        /// </summary>
        public string CommandCssClass
        {
            get
            {
                var cssClass = Utils.ConvertTo<string>(ViewState["CommandCssClass"]);
                {
                    return String.IsNullOrEmpty(cssClass) ? "dnnSecondaryAction" : cssClass;
                }
            }
            set
            {
                ViewState["CommandCssClass"] = value;
            }
        }

        /// <summary>
        /// Gets or sets file filter.
        /// </summary>
        public string FileFilter
        {
            get
            {
                return ViewState["FileFilter"] != null ? (string)ViewState["FileFilter"] : String.Empty;
            }
            set
            {
                ViewState["FileFilter"] = value;
            }
        }

        /// <summary>
        /// Gets or sets file id.
        /// </summary>
        public int FileId
        {
            get
            {
                EnsureChildControls();
                {
                    return Utils.ConvertTo<int>(ViewState["FileId"]);
                }
            }
            set
            {
                EnsureChildControls();

                IFileInfo fileInfo = null; ViewState["FileId"] = value;

                if (String.IsNullOrEmpty(FilePath) && (fileInfo = FileManager.Instance.GetFile(value)) != null)
                {
                    SetFilePath(fileInfo.Folder, fileInfo.FileName);
                }
            }
        }

        /// <summary>
        /// Gets or sets file path.
        /// </summary>
        public string FilePath
        {
            get { return Utils.ConvertTo<string>(ViewState["FilePath"]); }

            set { ViewState["FilePath"] = value; }
        }

        /// <summary>
        /// Gets or sets label CSS class.
        /// </summary>
        public string LabelCssClass
        {
            get
            {
                var cssClass = Utils.ConvertTo<string>(ViewState["LabelCssClass"]);
                {
                    return String.IsNullOrEmpty(cssClass) ? String.Empty : cssClass; // resolve string
                }
            }
            set
            {
                ViewState["LabelCssClass"] = value;
            }
        }

        /// <summary>
        /// Gets or sets required.
        /// </summary>
        public bool Required
        {
            get { return ViewState["Required"] != null && Utils.ConvertTo<bool>(ViewState["Required"]); }

            set { ViewState["Required"] = value; }
        }

        /// <summary>
        /// Gets or sets show file folders.
        /// </summary>
        public bool ShowFolders
        {
            get { return ViewState["ShowFolders"] == null || Utils.ConvertTo<bool>(ViewState["ShowFolders"]); }

            set { ViewState["ShowFolders"] = value; }
        }

        /// <summary>
        /// Gets or sets sow upload.
        /// </summary>
        public bool ShowUpload
        {
            get { return ViewState["ShowUpload"] == null || Utils.ConvertTo<bool>(ViewState["ShowUpload"]); }

            set { ViewState["ShowUpload"] = value; }
        }

        #endregion

        #region Public Methods : ILocalizable

        /// <summary>
        /// Localizes strings.
        /// </summary>
        public virtual void LocalizeStrings() { }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds new button.
        /// </summary>
        private void AddButton(string buttonId, ref LinkButton button)
        {
            button = new LinkButton { ID = buttonId, EnableViewState = false, ClientIDMode = ClientIDMode.AutoID };
            {
                button.ControlStyle.CssClass = CommandCssClass;
                {
                    button.Visible = false;
                }
            }

            _pnlButtons.Controls.Add(button);
        }

        /// <summary>
        /// Adds button area.
        /// </summary>
        private void AddButtonArea()
        {
            _pnlButtons = new Panel { ID = "pnlButtons", Visible = false, CssClass = "dnnFormItem wthButtons" };

            AddButton("btnUpload", ref _cmdUpload); _cmdUpload.Click += UploadFile;
            AddButton("btnSave",   ref _cmdSave);     _cmdSave.Click += SaveFile;
            AddButton("btnCancel", ref _cmdCancel); _cmdCancel.Click += CancelUpload;

            _pnlLeftDiv.Controls.Add(_pnlButtons);
        }

        /// <summary>
        /// Adds file and upload area.
        /// </summary>
        private void AddFileAndUploadArea()
        {
            _pnlFile = new Panel { ID = "pnlFile", CssClass = "dnnFormItem" };

            _lblFile = new Label { ID = "lblFile", Visible = true };
            {
                _pnlFile.Controls.Add(_lblFile);
            }

            _cboFiles = new DnnComboBox { ID = "cboFile", CssClass = "wthCombobox", DataTextField = "Text", DataValueField = "Value", AutoPostBack = true };
            {
                _cboFiles.SelectedIndexChanged += FileChanged;
            }

            _pnlFile.Controls.Add(_cboFiles); _pnlLeftDiv.Controls.Add(_pnlFile);

            _pnlUpload = new Panel { ID = "pnlUpload", CssClass = "dnnFormItem", Visible = true };

            _txtFile = new HtmlInputFile();
            {
                _txtFile.Attributes.Add("size", "13"); _pnlUpload.Controls.Add(_txtFile);
            }

            _pnlLeftDiv.Controls.Add(_pnlUpload);
        }

        /// <summary>
        /// Adds folder area.
        /// </summary>
        private void AddFolderArea()
        {
            _pnlFolder = new Panel { ID = "pnlFolder", CssClass = "dnnFormItem", Visible = true };

            _lblFolder = new Label { ID = "lblFolder", Visible = true };
            {
                _pnlFolder.Controls.Add(_lblFolder);
            }

            _cboFolders = new DnnComboBox { ID = "cboFolder", CssClass = "wthCombobox", AutoPostBack = true };
            {
                _cboFolders.SelectedIndexChanged += FolderChanged;
            }

            _pnlFolder.Controls.Add(_cboFolders); _pnlLeftDiv.Controls.Add(_pnlFolder);

            LoadFolders();
        }

        /// <summary>
        /// Adds preview area.
        /// </summary>
        private void AddPreviewImage()
        {
            _imgPreview = new Image { ID = "imgPreview", CssClass = "wthPreview" };
            {
                _pnlRightDiv.Controls.Add(_imgPreview);
            }
        }

        /// <summary>
        /// Loads files.
        /// </summary>
        private void LoadFiles()
        {
            int effectivePortalId = PortalId;

            _cboFiles.DataSource = Globals.GetFileList(effectivePortalId, FileFilter, !Required, _cboFolders.SelectedItem.Value);
            {
                _cboFiles.DataBind();
            }
        }

        /// <summary>
        /// Loads folders.
        /// </summary>
        private void LoadFolders()
        {
            if (_cboFolders.DataSource != null) _cboFolders.DataSource = null;

            foreach (FolderInfo folder in FolderManager.Instance.GetFolders(PortalId))
            {
                string text = (folder.FolderPath == Null.NullString) ? Utilities.GetLocalizedString("PortalRoot") : folder.DisplayPath;

                var folderItem = new RadComboBoxItem { Text = text, Value = folder.FolderPath };
                {
                    _cboFolders.Items.Add(folderItem);
                }
            }
        }

        /// <summary>
        /// Sets file path.
        /// </summary>
        private void SetFilePath(string folderName, string fileName)
        {
            if (!String.IsNullOrEmpty(folderName)) { FilePath = folderName + fileName; return; }
            {
                FilePath = _cboFolders.SelectedItem.Value + fileName;
            }
        }

        /// <summary>
        /// Sets file path.
        /// </summary>
        private void SetFilePath()
        {
            SetFilePath(null, _cboFiles.SelectedItem.Text);
        }

        /// <summary>
        /// Shows button.
        /// </summary>
        private void ShowButton(LinkButton button, string command)
        {
            button.Visible = true;

            if (!String.IsNullOrEmpty(command))
            {
                button.Text = Utilities.GetLocalizedString(command);
            }

            _pnlButtons.Visible = true;
        }

        /// <summary>
        /// Shows image.
        /// </summary>
        private void ShowImage()
        {
            var image = (FileInfo)FileManager.Instance.GetFile(FileId);

            if (image != null)
            {
                _imgPreview.Visible = true;
                {
                    _imgPreview.ImageUrl = FileManager.Instance.GetUrl(image);
                }

                try
                {
                    Utilities.CreateThumbnail(image, _imgPreview, 90, 90);
                }
                catch (Exception)
                {
                    Logger.WarnFormat("Unable to create thumbnail for {0}", image.PhysicalPath);
                    {
                        _pnlRightDiv.Visible = false;
                    }
                }
            }
            else
            {
                _imgPreview.Visible = false;

                var emptyPreview = new Panel { CssClass = "wthEmptyPreview" };

                _pnlRightDiv.Controls.Add(emptyPreview);
                {
                    _pnlRightDiv.Visible = true;
                }
            }
        }

        #endregion

        #region Private Event Handlers

        /// <summary>
        /// Cancel upload handler.
        /// </summary>
        private void CancelUpload(object sender, EventArgs e)
        {
            Mode = FileControlMode.Normal;
        }

        /// <summary>
        /// Specified file changed handler.
        /// </summary>
        private void FileChanged(object sender, EventArgs e)
        {
            int fileId = Null.NullInteger;

            if (_cboFiles.SelectedItem != null)
            {
                fileId = Int32.Parse(_cboFiles.SelectedItem.Value);
                {
                    ViewState["FileId"] = fileId;
                }
            }

            SetFilePath();
        }

        /// <summary>
        /// Folder changed handler.
        /// </summary>
        private void FolderChanged(object sender, EventArgs e)
        {
            LoadFiles(); SetFilePath();
        }

        /// <summary>
        /// Save file handler.
        /// </summary>
        private void SaveFile(object sender, EventArgs e)
        {
            Mode = FileControlMode.Normal;

            if (!String.IsNullOrEmpty(_txtFile.PostedFile.FileName))
            {
                string extension = Path.GetExtension(_txtFile.PostedFile.FileName).Replace(".", String.Empty);

                if (String.IsNullOrEmpty(FileFilter) || (!String.IsNullOrEmpty(FileFilter) && FileFilter.ToLower().Contains(extension.ToLower())))
                {
                    IFolderInfo folder = null;

                    IFolderManager folderManager = FolderManager.Instance;

                    string folderPath = PathUtils.Instance.GetRelativePath(PortalId, ParentFolder) + _cboFolders.SelectedItem.Value;
                    {
                        folder = folderManager.GetFolder(PortalId, folderPath);
                    }

                    string fileName = Path.GetFileName(_txtFile.PostedFile.FileName);

                    try
                    {
                        FileManager.Instance.AddFile(folder, fileName, _txtFile.PostedFile.InputStream, true);

                        var result = _txtFile.PostedFile.FileName.Substring(_txtFile.PostedFile.FileName.LastIndexOf("\\") + 1);
                        {
                            SetFilePath(null, result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex); // log error
                    }
                }
            }
        }

        /// <summary>
        /// Upload file handler.
        /// </summary>
        private void UploadFile(object sender, EventArgs e)
        {
            Mode = FileControlMode.UploadFile;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            _pnlContainer = new Panel // define container
            {
                ID = "pnlFileSelector", CssClass = "wthFileSelector"
            };

            _hfFileId = new HiddenField() { ID = "hfFileId" };
            {
                _pnlContainer.Controls.Add(_hfFileId); // add value hidden
            }

            _pnlLeftDiv = new Panel { ID = "pnlLeftDiv", CssClass = "dnnLeft", Visible = true };
            {
                AddFolderArea(); AddFileAndUploadArea(); AddButtonArea();
                {
                    _pnlContainer.Controls.Add(_pnlLeftDiv);
                }
            }
            _pnlRightDiv = new Panel { ID = "pnlRightDiv", CssClass = "dnnLeft", Visible = true };
            {
                AddPreviewImage(); _pnlContainer.Controls.Add(_pnlRightDiv);
            }

            Controls.Add(_pnlContainer); Controls.GetType();
            {
                base.CreateChildControls();
            }
        }

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); LocalResourceFile = Utilities.GetLocalResourceFile(this);
            {
                EnsureChildControls();
            }
        }

        /// <summary>
        /// OnPreRender handler.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            _lblFolder.Text     = Utilities.GetLocalizedString("Folder");
            _lblFile.Text       = Utilities.GetLocalizedString("File");

            _lblFolder.CssClass = LabelCssClass;
            _lblFile.CssClass   = LabelCssClass;

            string fileName = null; string folderPath = null;

            if (!String.IsNullOrEmpty(FilePath))
            {
                fileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1);
                {
                    folderPath = String.IsNullOrEmpty(fileName) ? FilePath : FilePath.Replace(fileName, String.Empty);
                }
            }
            else
            {
                fileName = FilePath; folderPath = String.Empty;
            }

            if (_cboFolders.Items.FindItemByValue(folderPath) != null)
            {
                _cboFolders.SelectedIndex = -1;
                {
                    _cboFolders.Items.FindItemByValue(folderPath).Selected = true;
                }
            }

            LoadFiles(); _pnlFolder.Visible = (_cboFolders.Items.Count > 1 && ShowFolders);

            if (_cboFiles.Items.FindItemByText(fileName) != null)
            {
                _cboFiles.Items.FindItemByText(fileName).Selected = true;
            }

            if (_cboFiles.SelectedItem == null || String.IsNullOrEmpty(_cboFiles.SelectedItem.Value))
            {
                FileId = -1;
            }
            else
            {
                FileId = Int32.Parse(_cboFiles.SelectedItem.Value);
            }

            _hfFileId.Value = FileId.ToString();

            switch (Mode)
            {
                case FileControlMode.Normal:
                    {
                        _pnlFile.Visible = true; _pnlUpload.Visible = false; _pnlRightDiv.Visible = true;
                        {
                            ShowImage(); // show file selector preview
                        }

                        if ((FolderPermissionController.HasFolderPermission(PortalId, _cboFolders.SelectedItem.Value, "ADD")) && ShowUpload)
                        {
                            ShowButton(_cmdUpload, "Upload");
                        }

                        break;
                    }
                case FileControlMode.UploadFile:
                    {
                        ShowButton(_cmdSave, "Save"); ShowButton(_cmdCancel, "Cancel");
                        {
                            _pnlFile.Visible = false; _pnlRightDiv.Visible = false; _pnlUpload.Visible = true;
                        }

                        RadAjaxPanel panel = (Parent as RadAjaxPanel);
                        {
                            panel.ClientEvents.OnRequestStart = @"auxiliary.postback";
                        }

                        break;
                    }
            }
        }

        #endregion
    }
}