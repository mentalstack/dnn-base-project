namespace DNNBase.Components.Entities
{
    using DotNetNuke.Entities.Modules;

    using DNNBase.Components.Repositories;

    using System.Xml.Serialization;

    using System.Runtime.Serialization;

    using System;
    using System.Data;

    /// <summary>
    /// Base entity class.
    /// </summary>
    [DataContract, Serializable]
    public abstract class EntityBase : IHydratable
    {
        #region Private Fields

        /// <summary>
        /// Unit of work.
        /// </summary>
        private IUnitOfWork _uow = null;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get { return _uow ?? (_uow = new UnitOfWork()); }
        }

        #endregion

        #region Public Methods : Abstract

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public abstract void Fill(IDataReader r);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember, XmlIgnore]
        public abstract int KeyID { get; set; }

        #endregion
    }
}