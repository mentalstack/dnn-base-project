﻿namespace DNNBase
{
    using DotNetNuke.Entities.Modules;

    using DotNetNuke.Services;
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Framework;
    using DotNetNuke.Framework.JavaScriptLibraries;

    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Web.UI.WebControls;

    using System;

    /// <summary>
    /// Module settings base.
    /// </summary>
    public class ModuleSettings : ModuleSettingsBase
    {
        #region Protected Methods

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                WebControl.DisabledCssClass = "aspNetDisabled disabled";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/auxiliary.js");
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
        /// OnLoad handler.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try // try to handle OnLoad
            {
                JavaScript.RequestRegistration("jQuery");
                JavaScript.RequestRegistration("jQuery-UI");

                ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                {
                    base.OnLoad(e);
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