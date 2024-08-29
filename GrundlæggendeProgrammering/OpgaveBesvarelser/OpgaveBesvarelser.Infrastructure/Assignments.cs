using System.Reflection;
using OpgaveBesvarelser;

namespace OpgaveBesvarelser.Infrastructure;

public static class Assignments
{
    public static Dictionary<string, Action> GetAssignments()
    {
        string[] scriptFiles = Directory.GetFiles("Opgaver", "*.cs");
        foreach (string scriptFile in scriptFiles)
        {
            string name = Path.GetFileNameWithoutExtension(scriptFile);
            MethodInfo method = typeof(Program).GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null && method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
            {
                var action = (Action)Delegate.CreateDelegate(typeof(Action), method);

                options.Add(name, action);
            }
            else
            {
                throw new InvalidOperationException($"{name} is not setup correctly");
            }
        }


    }
}
