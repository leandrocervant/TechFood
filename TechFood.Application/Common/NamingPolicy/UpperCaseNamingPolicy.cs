using System.Text.Json;

namespace TechFood.Application.Common.NamingPolicy
{
    public class UpperCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToUpper();
    }
}
