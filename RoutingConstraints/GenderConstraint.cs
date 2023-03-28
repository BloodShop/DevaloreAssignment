namespace DevaloreAssignment.RoutingConstraints
{
    public class GenderConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object? value))
            {
                if (value is string stringVal)
                {
                    var lowerStr = stringVal.ToLower();
                    return lowerStr == "male" || lowerStr == "female";
                }
            }

            return false;
        }
    }
}
