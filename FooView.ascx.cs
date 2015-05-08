namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Collections.Generic;

    using System;
    using System.IO;

    using Telerik.Web.UI;

    using DNNBase.Models;

    /// <summary>
    /// FooView Module
    /// </summary>
    public partial class FooView : ModuleBase
    {
        #region Defines

        public const string TEMPLATES_PATH = "~\\DesktopModules\\DNNBase\\Templates";

        #endregion

        #region Private Fields

        /// <summary>
        /// Rad AJAX manager.
        /// </summary>
        private RadAjaxManager _ajaxManager = null;

        /// <summary>
        /// Template evaluator.
        /// </summary>
        private RazorEvaluator<FooViewModel> _razor = null;

        /// <summary>
        /// Module settings
        /// </summary>
        private Infrastructure.FooModuleSettings _settings = null;

        /// <summary>
        /// HTML templates.
        /// </summary>
        private Dictionary<string, string> _templates = null;

        /// <summary>
        /// Model for Razor template
        /// </summary>
        private FooViewModel _model = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts path to relative url.
        /// </summary>
        public string ToRelativeUrl(FileInfo source)
        {
            string result = source.FullName.Replace(Server.MapPath("~\\"), null);
            {
                return result.Replace(@"\", "/").Insert(0, "~/");
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Evaluates item template.
        /// </summary>
        protected string EvaluateItem()
        {
            return _razor.Evaluate(_model, _templates["Item"]);
        }

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                string[] patterns = { "*.js", "*.css", "*.cshtml" };

                _settings = Infrastructure.FooModuleSettings.Load(TabModuleId);
                {
                    LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/FooView.resx";
                }

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/foo.view.js", FileOrder.Js.DefaultPriority + 1);
                {
                    ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/foo.view.css", FileOrder.Css.DefaultPriority + 1);
                }

                _razor = new RazorEvaluator<FooViewModel>(ModuleContext, LocalResourceFile);

                DirectoryInfo rootDirectory = null; _templates = new Dictionary<string, string>();

                string path = String.Format("{0}\\View\\{1}", TEMPLATES_PATH, _settings.TemplateDirectory);
                {
                    rootDirectory = new DirectoryInfo(Server.MapPath(path));
                }

                foreach (var file in rootDirectory.EnumerateFiles(patterns[0], SearchOption.AllDirectories))
                {
                    ClientResourceManager.RegisterScript(Page, ToRelativeUrl(file), FileOrder.Js.DefaultPriority + 2);
                }
                foreach (var file in rootDirectory.EnumerateFiles(patterns[1], SearchOption.AllDirectories))
                {
                    ClientResourceManager.RegisterStyleSheet(Page, ToRelativeUrl(file), FileOrder.Css.DefaultPriority + 2);
                }

                foreach (var file in rootDirectory.EnumerateFiles(patterns[2], SearchOption.AllDirectories))
                {
                    _templates.Add(Path.GetFileNameWithoutExtension(file.Name), ToRelativeUrl(file));
                }

                base.OnInit(e);
            }
            catch (Exception exc) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Page_Load hadler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {
                _ajaxManager = null;

                if ((_ajaxManager = RadAjaxManager.GetCurrent(Page)) == null)
                {
                    _ajaxManager = new RadAjaxManager() { ID = "amMain", EnableAJAX = true };
                    {
                        phAjaxManager.Controls.Add(_ajaxManager);
                    }
                }

                var apMainSetting = new AjaxSetting("apMain");
                {
                    apMainSetting.UpdatedControls.Add(new AjaxUpdatedControl("apMain", "alpMain"));
                    {
                        _ajaxManager.AjaxSettings.Add(apMainSetting);
                    }
                }

                if (IsPostBack) return;
                {
                    _model = new FooViewModel() { Foos = UnitOfWork.Foos.GetAll() };
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Constructors

        #endregion
    }
}