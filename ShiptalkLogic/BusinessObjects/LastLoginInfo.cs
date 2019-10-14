using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkLogic.BusinessObjects
{
    public class LastLoginInfo
    {

        public int UserId { get; set; }

        public DateTime? FirstFailedLoginAttempt { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; } //added for 60 days password verification
        public short FailedLoginAttemptsCount { get; set; }
        public Guid? SessionToken { get; set; }

    }
}
