namespace DNNBase.Components.Repositories
{
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Data;

    using System;
    using System.Data;

    using System.Collections.Generic;

    using DNNBase.Components.Entities;

    /// <summary>
    /// Foo repository
    /// </summary>
    public class FooRepository : RepositoryBase
    {
        #region Public Add Method

        /// <summary>
        /// Add new foo
        /// </summary>
        public int Add(string name, string message)
        {
            return DataProvider.ExecuteScalar<int>("DNNBase_AddFoo", name, message);
        }

        #endregion

        #region Public Get Methods

        /// <summary>
        /// Gets foo by ID.
        /// </summary>
        public Foo GetBy(int id)
        {
            return CBO.FillObject<Foo>(DataProvider.ExecuteReader("DNNBase_GetById", id));
        }

        /// <summary>
        /// Gets foo by name.
        /// </summary>
        public Foo GetBy(string name)
        {
            return CBO.FillObject<Foo>(DataProvider.ExecuteReader("DNNBase_GetByName", name));
        }

        /// <summary>
        /// Get all foos
        /// </summary>
        public List<Foo> GetAll()
        {
            return CBO.FillCollection<Foo>(DataProvider.ExecuteReader("DNNBase_GetAll"));
        }

        /// <summary>
        /// Get all foos paged view
        /// </summary>
        public List<Foo> GetAllView(int startIndex, int length, string orderBy, string orderDescription, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNBase_GetFooView", startIndex, length, orderBy, orderDescription);

            reader.Read();
            total = Utils.ConvertTo<int>(reader["TotalCount"]);
            reader.NextResult();

            return CBO.FillCollection<Foo>(reader);
        }

        #endregion

        #region Public Update Method

        /// <summary>
        /// Update foo
        /// </summary>
        public void Update(int id, string name, string message)
        {
            DataProvider.ExecuteNonQuery("DNNBase_UpdateFoo", id, name, message);
        }

        #endregion

        #region Public Delete Methods

        /// <summary>
        /// Delete foo
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooById", id);
        }

        /// <summary>
        /// Delete foo
        /// </summary>
        public void Delete(string name)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooByName", name);
        }

        #endregion

        #region Public Contstructor with param

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public FooRepository(DataProvider provider) : base(provider) { }

        #endregion
    }
}