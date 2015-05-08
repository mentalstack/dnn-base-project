namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.Web.UI.WebControls;

    using System.Collections;
    using System.Collections.Generic;

    using System;
    using System.Linq;
    using System.IO;

    /// <summary>
    /// FooView module settings.
    /// </summary>
    public partial class FooViewSettings : ModuleSettings
    {
        #region Defines

        /// <summary>
        /// Templates directory relative path.
        /// </summary>
        private const string TEMPLATES_DIRECTORY = "~\\DesktopModules\\DNNBase\\Templates\\View";

        #endregion

        #region Private Fields

        /// <summary>
        /// Module settings.
        /// </summary>
        private Infrastructure.FooModuleSettings _settings = null;

        #endregion

        #region Protected Methods : Event Handlers

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/FooViewSettings.resx";

                ClientResourceManager.RegisterStyleSheet(Page, TemplateSourceDirectory + "/Css/foo.settings.css", FileOrder.Css.DefaultPriority + 1);
                {
                    base.OnInit(e);
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// Page_Load hadler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {
                if (IsPostBack) return;

                IEnumerable<string> paths = null; // get directories list
                {
                    paths = Directory.EnumerateDirectories(Server.MapPath(TEMPLATES_DIRECTORY));
                }

                cbTemplateDirectory.DataSource = paths.Select(x => new DirectoryInfo(x));
                {
                    cbTemplateDirectory.DataBind(); cbTemplateDirectory.SelectedValue = _settings.TemplateDirectory;
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Load settings
        /// </summary>
        public override void LoadSettings()
        {
            try // try to handle LoadSettings
            {
                _settings = Infrastructure.FooModuleSettings.Load(TabModuleId);
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// Updates settings.
        /// </summary>
        public override void UpdateSettings()
        {
            try // try to handle UpdateSettings 
            {
                if (!Page.IsValid) return;

                _settings = Infrastructure.FooModuleSettings.Load(TabModuleId);
                {
                    _settings.TemplateDirectory = cbTemplateDirectory.SelectedValue;

                    _settings.Update(); // update settings
                }
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion
    }
}