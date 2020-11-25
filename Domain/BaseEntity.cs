using System;

namespace Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
        public BaseEntity()
        {
            DateAdded = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
