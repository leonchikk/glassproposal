using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.ReponseModels
{
    public class SignInResponseModel
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public int SecurityLevel { get; set; }
    }
}
