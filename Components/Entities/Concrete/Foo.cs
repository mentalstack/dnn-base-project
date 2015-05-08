namespace DNNBase.Components.Entities
{
    using System;

    using System.Data;

    using System.Runtime.Serialization;

    /// <summary>
    /// Foo class
    /// </summary>
    [DataContract, Serializable]
    public class Foo: EntityBase
    {
        [DataMember]
        public int FooId { get; set; }

        [IgnoreDataMember]
        public override int KeyID
        {
            get { return FooId; }
            set { FooId = value; }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public override void Fill(IDataReader r)
        {
            FooId = Utils.ConvertTo<int>(r["FooId"]);
            Name = Utils.ConvertTo<string>(r["Name"]);
            Description = Utils.ConvertTo<string>(r["Description"]);
        }
    }
}