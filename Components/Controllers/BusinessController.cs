namespace DNNBase.Components.Controllers
{
    using DotNetNuke.Entities.Modules;

    using System;

    /// <summary>
    /// Business controller.
    /// </summary>
    public class BusinessController : IUpgradeable
    {
        #region Public Methods

        /// <summary>
        /// UpgradeModule handler.
        /// </summary>
        public string UpgradeModule(string version)
        {
            try // try to upgrade if requried
            {
                return "Success";
            }
            catch (Exception ex) // return failed message
            {
                return "Failed: " + ex.Message;
            }
        }

        #endregion
    }
}
