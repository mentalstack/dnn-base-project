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

        

        #endregion
    }

    /// <summary>
    /// Unit of work implementation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        

        #endregion

        #region Private Properties : Provider

        /// <summary>
        /// Gets or sets data provider.
        /// </summary>
        private DataProvider DataProvider { get; set; }

        #endregion

        #region Public Properties

        

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