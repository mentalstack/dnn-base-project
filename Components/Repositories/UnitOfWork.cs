namespace DNNBase.Components.Repositories
{
    using DotNetNuke.Data;

    using System;
    using System.Data.Common;

    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets foo repository.
        /// </summary>
        FooRepository Foos { get; }

        #endregion
    }

    /// <summary>
    /// Unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        /// Foo repository.
        /// </summary>
        private FooRepository _foos = null;

        #endregion

        #region Private Properties : Provider

        /// <summary>
        /// Data provider instance.
        /// </summary>
        private DataProvider DataProvider { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets foo repository.
        /// </summary>
        public FooRepository Foos
        {
            get { return _foos ?? (_foos = new FooRepository(DataProvider)); }
        }

        #endregion

        #region Public Methods : IDisposable

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnitOfWork()
        {
            DataProvider = DataProvider.Instance();
        }

        #endregion
    }
}