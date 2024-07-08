using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AW.Test.Utilities.Builders;

public class HttpContextBuilder
{
    public const string TEST_USERNAME = "test.user";    

    private DefaultHttpContext _context = new DefaultHttpContext();
    private string? _username;
    private bool _isAuthenticated;
    private ISession? _session;    

    public HttpContextBuilder WithTestValues()
    {
        _username = TEST_USERNAME;

        return this;
    }

    public HttpContextBuilder WithUserName(string username)
    {
        _username = username;

        return this;
    }

    public HttpContextBuilder IsUserAuthenticated(bool isAuthenticated)
    {
        _isAuthenticated = isAuthenticated;

        return this;
    }

    public HttpContextBuilder WithRequestScheme(string scheme)
    {
        _context.Request.Scheme = scheme;

        return this;
    }

    public HttpContextBuilder WithSession(ISession session)
    {
        _context.Session = session;
        
        return this;
    }
   
    public DefaultHttpContext Build()
    {
        List<Claim> claims = new();        

        if (!string.IsNullOrEmpty(_username))
        {
            claims.Add(new Claim(ClaimTypes.Name, _username));            
        }
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, _isAuthenticated ? "Custom": null));

        _context.User = user;
       
        return _context;
    }   
}
