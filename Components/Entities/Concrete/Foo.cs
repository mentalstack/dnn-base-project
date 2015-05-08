namespace DNNBase.Components.Entities
{
    using System;

    using System.Data;

    using System.Runtime.Serialization;

    /// <summary>
    /// Foo entity class.
    /// </summary>
    [DataContract, Serializable]
    public class Foo : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [DataMember]
        public int FooId { get; set; }

        /// <summary>
        /// Gets or sets key ID.
        /// </summary>
        [IgnoreDataMember]
        public override int KeyID
        {
            get { return FooId; } set { FooId = value; }
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Provides hydration.
        /// </summary>
        public override void Fill(IDataReader r)
        {
            FooId       = Utils.ConvertTo<int>(r["FooId"]);

            Description = Utils.ConvertTo<string>(r["Description"]);
            Name        = Utils.ConvertTo<string>(r["Name"]);
        }

        #endregion
    }
}