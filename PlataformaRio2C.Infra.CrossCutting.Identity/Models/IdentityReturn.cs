using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class IdentityReturn
    {
        public IdentityReturn(IdentityResult result)
        {
            this.Succeeded = result.Succeeded;
            this.Errors = result.Errors;
        }

        public bool Succeeded { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
    }
}
