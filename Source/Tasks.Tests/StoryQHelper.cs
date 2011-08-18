using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tasks.Tests
{
    public static class StoryQHelper
    {
        public static MethodBase GetTestMethod()
        {
            var trace = new StackTrace();
            for (int i = 0; i < trace.FrameCount; i++)
            {
                var methodBase = trace.GetFrame(i).GetMethod();
                bool isTestMethod = methodBase.GetCustomAttributes(true).Any(
                    attribute =>
                    {
                        var name = attribute.GetType().Name;
                        return name == "TestAttribute" || name == "TestMethodAttribute";
                    });

                if (isTestMethod) return methodBase;
            }
            return null;
        }

        public static string GetTestMethodName()
        {
            var method = GetTestMethod();
            if (method == null) return null;

            return method.Name;
        }

        public static string Uncamel(string methodName)
        {
            return Regex.Replace(methodName, "[A-Z_]", x => " " + x.Value.ToLowerInvariant()).Trim();
        }

        public static string ConvertTestMethodAsScenario()
        {
            var methodName = GetTestMethodName();
            
            if (methodName == null) 
                return "No scenario name!";

            return Uncamel(GetTestMethodName());
        }
    }
}