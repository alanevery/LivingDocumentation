using System.Security.Claims;
using System.Security.Principal;

namespace AW.Test.Utilities.Builders
{
    public class ClaimsPrincipalBuilder
    {
        private string _authenticationType = "Test";
        private List<Claim> _claims = new List<Claim>();
        private string _identityName = "TestUserName";

        public ClaimsPrincipalBuilder WithClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));

            return this;
        }
              
        public ClaimsPrincipalBuilder WithNameClaim(string value)
        {
            return WithClaim(ClaimTypes.Name, value);
        }

        public ClaimsPrincipalBuilder WithIdentityName(string name)
        { 
            _identityName = name;
            _claims.Add(new Claim(ClaimTypes.NameIdentifier, name));

            return this;
        }

        public ClaimsPrincipalBuilder WithIdentityName<T>(T value)
        {
            return WithIdentityName(value?.ToString() ?? string.Empty);
        }

        public ClaimsPrincipal Build()
        {

            var identity = new GenericIdentity(_identityName);
            identity.AddClaims(_claims);
                                                                 
            return new ClaimsPrincipal(identity);
        }
    }
}
