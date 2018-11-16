using OnlineMovieStore.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Models.Abstract
{
    public class Entity : IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; } // ID vs GUID?

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
