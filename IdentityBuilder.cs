using Microsoft.AspNetCore.Identity;

namespace AW.Test.Utilities.Builders;

public class IdentityBuilder
{
    private IdentityUser _user = new();

    public IdentityBuilder WithId(string id)
    {
        _user.Id = id;

        return this;
    }

    public IdentityUser Build() => _user;
}