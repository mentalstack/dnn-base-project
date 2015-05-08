namespace DNNBase
{
    using DNNBase.Components.Repositories;

    using DotNetNuke.Entities.Modules;

    using DotNetNuke.Services;
    using DotNetNuke.Services.Exceptions;

    using DotNetNuke.Framework;
    using DotNetNuke.Framework.JavaScriptLibraries;

    using DotNetNuke.Web.Client.ClientResourceManagement;

    using System.Web.UI.WebControls;

    using System;

    /// <summary>
    /// Leaderboard module base.
    /// </summary>
    public class ModuleBase : PortalModuleBase
    {
        #region Private Fields

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private IUnitOfWork _uow = null;

        #endregion

        #region Protected Properties : Data Access

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get { return _uow ?? (_uow = new UnitOfWork()); }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// OnInit handler.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            try // try to handle OnInit
            {
                WebControl.DisabledCssClass = "aspNetDisabled disabled";

                ClientResourceManager.RegisterScript(Page, TemplateSourceDirectory + "/Scripts/utils.js");
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