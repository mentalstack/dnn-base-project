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
        public int Add(string name, string message)
        {
            return DataProvider.ExecuteScalar<int>("DNNBase_AddFoo", name, message);
        }

        public Foo GetBy(int id)
        {
            return CBO.FillObject<Foo>(DataProvider.ExecuteReader("DNNBase_GetById", id));
        }

        /// <summary>
        /// Gets activity by synonym and module ID.
        /// </summary>
        public Foo GetBy(string name)
        {
            return CBO.FillObject<Foo>(DataProvider.ExecuteReader("DNNBase_GetByName", name));
        }

        public List<Foo> GetAll()
        {
            return CBO.FillCollection<Foo>(DataProvider.ExecuteReader("DNNBase_GetAll"));
        }

        public List<Foo> GetAllView(int startIndex, int length, string orderBy, string orderDescription, out int total)
        {
            IDataReader reader = DataProvider.ExecuteReader("DNNBase_GetFooView", startIndex, length, orderBy, orderDescription);

            reader.Read();
            total = Utils.ConvertTo<int>(reader["TotalCount"]);
            reader.NextResult();

            return CBO.FillCollection<Foo>(reader);
        }

        public void Update(int id, string name, string message)
        {
            DataProvider.ExecuteNonQuery("DNNBase_UpdateFoo", id, name, message);
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooById", id);
        }

        public void Delete(string name)
        {
            DataProvider.ExecuteNonQuery("DNNBase_DeleteFooByName", name);
        }

        public FooRepository(DataProvider provider) : base(provider) { }
    }   
}