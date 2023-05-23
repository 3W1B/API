namespace RadonAPI.Utils;

public abstract class ParameterHandler
{
    public static bool IsNull(dynamic? value)
    {
        return value is null || value.ToString().Equals("") || value?.ToString().Equals("null");
    }
}