using System;

namespace DataLayer.Model
{
    public abstract class BaseEntity
    {
        public Guid? Id { get; set; }
        public override bool Equals(Object obj)
        {
     
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                BaseEntity entity = (BaseEntity)obj;
                return (Id == entity.Id);
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
