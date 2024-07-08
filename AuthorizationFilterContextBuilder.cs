using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AW.Test.Utilities.Builders;

public class AuthorizationFilterContextBuilder
{
    private DefaultHttpContext _httpContext;
    private List<FilterDescriptor> filterDescriptors = new List<FilterDescriptor>();

    public AuthorizationFilterContextBuilder(DefaultHttpContext httpContext)
    {
        _httpContext = httpContext;
    }
    
    public AuthorizationFilterContextBuilder AddActionDescriptorFilterDescriptor(FilterDescriptor filterDescriptor)
    {
        filterDescriptors.Add(filterDescriptor);

        return this;
    }
       
    public AuthorizationFilterContext Build()
    {
        var actionContext = new ActionContext(_httpContext, new(), new(), new());

        var authorisationFilterContext = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());

        if(filterDescriptors.Any())
        {
            authorisationFilterContext.ActionDescriptor.FilterDescriptors = filterDescriptors;
        }

        var thing = new List<FilterDescriptor>() {
            new FilterDescriptor(new AllowAnonymousFilter(), 0)
        };
      
        return authorisationFilterContext;        
    }   
}
