namespace DNNBase.Components.Repositories
{
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;

    using DotNetNuke.Data;

    using DNNBase.Components;
    using DNNBase.Components.Entities;

    using System.Collections;
    using System.Collections.Generic;

    using System.Data;

    /// <summary>
    /// Foo repository.
    /// </summary>
    public class FooRepository : RepositoryBase
    {
        #region Public Methods : Insert

        /// <summary>
        /// Adds new foo.
        /// </summary>
        public int Add(string name, string message)
        {
            return DataProvider.ExecuteScalar<int>("DNNBase_AddFoo", name, message);
        }

        #endregion

        #region Public Methods : Select

        /// <summary>
        /// Gets foo by id.
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
        /// Gets all foos.
        /// </summary>
        public List<Foo> GetAll()
        {
            return CBO.FillCollection<Foo>(DataProvider.ExecuteReader("DNNBase_GetAll"));
        }

        /// <summary>
        /// Gets all foos paged view.
        /// </summary>
        public List<Foo> GetAllView(int startIndex, int length, string orderBy, string orderDescription, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNBase_GetFooView", startIndex, length, orderBy, orderDescription);

            reader.Read(); total = Utils.ConvertTo<int>(reader["TotalCount"]); reader.NextResult();
            {
                return CBO.FillCollection<Foo>(reader);
            }
        }

        #endregion

        #region Public Methods : Update

        /// <summary>
        /// Updates foo.
        /// </summary>
        public void Update(int id, string name, string message)
        {
            DataProvider.ExecuteNonQuery("DNNBase_UpdateFoo", id, name, message);
        }

        #endregion

        #region Public Methods : Delete

        /// <summary>
        /// Deletes foo by name.
        /// </summary>
        public void Delete(string name)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooByName", name);
        }

        /// <summary>
        /// Deletes foo by id.
        /// </summary>
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooById", id);
        }

        #endregion

        #region Contstructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public FooRepository(DataProvider provider) : base(provider) { }

        #endregion
    }
}