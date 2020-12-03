using System;

namespace Shinetech.Common.Helper
{
    public class EnvironmentHelper
    {
        public static string GetEnvironmentVariable(string variableName)
        {
            var result = "";
            result = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(result))
            {
                result = Environment.GetEnvironmentVariable(variableName);
            }

            if (string.IsNullOrEmpty(result))
            {
                throw new Exception(variableName + " get environment variable failed");
            }
            return result;
        }

    }
}
