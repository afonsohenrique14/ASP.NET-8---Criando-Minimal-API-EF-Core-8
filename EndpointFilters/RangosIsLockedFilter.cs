namespace RangoAgil.API.EndpointFilters;

public class RangosIsLockedFilter : IEndpointFilter
{

    public readonly int _LockedRangoID;

    public RangosIsLockedFilter(int lockedRangoID)
    {
        _LockedRangoID = lockedRangoID;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int rangoId;
       
        if (context.HttpContext.Request.Method == "PUT")
        {
            rangoId =context.GetArgument<int>(3);

        }else if (context.HttpContext.Request.Method == "DELETE")
        {
            rangoId =context.GetArgument<int>(1);
        }
        else
        {
            throw new NotSupportedException("This filter is not supported for this scenario.");
        }
                

        if (rangoId == _LockedRangoID)
        {
            return TypedResults.Problem(new()
            {
                Status= 400,
                Title = "Rango já é Perfeito, você não precisa modificar ou deletar nada aqui.",
                Detail = "Você não pode modificar ou deletar essa Receita"

            });
        }

        var  result = await next.Invoke(context);
        return result;
            
    }
}
