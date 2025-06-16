using System;
using System.Collections.Generic;

namespace Integration.Dtos.UserAccount
{
    public class UserAccountResponse
    {
        public long Id { get; set; }
        public long UserName { get; set; }
        public long Email { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }

        public IList<UserAccountFlatResponse> Trainers { get; set; } = null!;
        public IList<UserAccountFlatResponse> Trainees { get; set; } = null!;
    }
}
