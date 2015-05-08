﻿namespace DNNBase
{
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Web.Client;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    using DotNetNuke.Web.UI.WebControls;

    using System;

    /// <summary>
    /// FooSettings
    /// </summary>
    public partial class FooSettings : ModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// Interview settings.
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
                LocalResourceFile = TemplateSourceDirectory + "/App_LocalResources/FooSettings.resx";

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
        /// Page_Load handler.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try // try to handle Page_Load
            {
                if (IsPostBack) return; 
            }
            catch (Exception ex) // catch exceptions
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads settings.
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
                    _settings.Update();
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