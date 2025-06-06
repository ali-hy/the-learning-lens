using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Dtos.UserAccount
{
    public class UserAccountResponse
    {
        public long Id { get; set; }
        public long UserName { get; set; }
        public long Email { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;

        public DateOnly DOB { get; set; }

        public IList<UserAccountFlatResponse> Trainers { get; set; } = null!;
        public IList<UserAccountFlatResponse> Trainees { get; set; } = null!;
    }
}
