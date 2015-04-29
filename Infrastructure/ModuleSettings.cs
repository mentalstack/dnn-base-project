namespace DNNBase.Infrastructure
{
    using DotNetNuke.Entities.Modules;

    using System;
    using System.Collections;

    /// <summary>
    /// Foo module settings.
    /// </summary>
    public class FooModuleSettings
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private int _moduleId = -1;

        /// <summary>
        /// 
        /// </summary>
        private string _defaultMessage = "Default message";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string DefaultMessage
        {
            get { return _defaultMessage; } set { _defaultMessage = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads module settings.
        /// </summary>
        public static FooModuleSettings Load(int moduleId)
        {
            var controller = new ModuleController();
            {
                return new FooModuleSettings(moduleId, controller.GetTabModule(moduleId).TabModuleSettings);
            }
        }

        /// <summary>
        /// Updates module settings.
        /// </summary>
        public void Update()
        {
            var controller = new ModuleController();
            
            if (!String.IsNullOrEmpty(DefaultMessage))
            {
                controller.UpdateTabModuleSetting(_moduleId, "DefaultMessage", DefaultMessage);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        protected FooModuleSettings(int moduleId, Hashtable settings)
        {
            _moduleId = moduleId;

            if (settings.ContainsKey("DefaultMessage")) 
            {
                DefaultMessage = settings["DefaultMessage"].ToString();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FooModuleSettings() { }

        #endregion
    }
}