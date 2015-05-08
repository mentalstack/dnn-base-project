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

        FooRepository Foos { get; }

        #endregion
    }

    /// <summary>
    /// Unit of work implementation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private FooRepository _foos = null;

        #endregion

        #region Private Properties : Provider

        private DataProvider DataProvider { get; set; }

        #endregion

        #region Public Properties

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