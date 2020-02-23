using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("User")]
        internal Guid Id { get; set; }

        public virtual User User { get; set; }
    }
}