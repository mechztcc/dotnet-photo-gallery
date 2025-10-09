using System;

namespace AppApi.Modules.Shared.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}