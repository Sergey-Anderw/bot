using Shared.Interfaces;

namespace Shared.Infrastructure
{
    public  class EntityBase : IDomainEntity
    {
	    /*private Guid _id;
        public EntityBase()
        {
          _id = Guid.NewGuid();   
        }

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }*/
        public int Id { get; set; }
    }

    //todo defines a public T Id property
}
