using System.Diagnostics;
using System.Reflection;

partial class Program
{
    static Random random = new Random();
    static private bool on = true;

    static void Main(string[] args)
    {
        // Create Options dictionary (flexible updates itself based on 'opgaver' folder
        var options = new Dictionary<string, Action>();

        string[] scriptFiles = Directory.GetFiles("Opgaver", "*.cs");
        foreach (string scriptFile in scriptFiles)
        {
            string name = Path.GetFileNameWithoutExtension(scriptFile);
            MethodInfo method = typeof(Program).GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null && method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
            {
                var action = (Action)Delegate.CreateDelegate(typeof(Action), method);

                options.Add(name, action);
            } else
            {
                throw new InvalidOperationException($"{name} is not setup correctly");
            }
        }

        // Create Window
        WindowMenu Menu = new WindowMenu(options, "Opgaver");
        Menu.Start();
        Console.Clear();
    }
}
