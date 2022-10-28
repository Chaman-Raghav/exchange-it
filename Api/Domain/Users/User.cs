using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Users
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}