using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using System.IO;

public class RunetimeCompile : MonoBehaviour
{
    public List<string> navn = new List<string>();
    void Start()
    {
        
        DirectoryInfo di = new DirectoryInfo(@"D:\Users\BaoVien\Documents\Mods");
        FileInfo[] files = di.GetFiles("*.cs");
        foreach (FileInfo file in files)
        {
            navn.Add(file.Name);

        }

        foreach (var filename in navn)
        {
            string text = File.ReadAllText(@"D:\Users\BaoVien\Documents\Mods\" + filename);
            Debug.Log(text);

            var fileName2 = filename.Substring(0, filename.Length - ".cs".Length);
            var assembly = Compile(text);
            
            
            var runtimeType = assembly.GetType(fileName2);
            //var method = runtimeType.GetMethod("AddYourselfTo");
            MethodInfo[] methodList = runtimeType.GetMethods();
            
            
            foreach (var method in methodList)
            {
                Debug.Log(method);
                var del = (Func<GameObject, MonoBehaviour>) 
                    Delegate.CreateDelegate(
                    typeof(Func<GameObject, MonoBehaviour>),
                    method
                );
                
                /*
                Debug.Log(del);
                // We ask the compiled method to add its component to this.gameObject
                var addedComponent = del.Invoke(gameObject);
                */
            }
        }

        /*
        string text = File.ReadAllText(@"D:\Users\BaoVien\Documents\Mods\Test.cs");
        Debug.Log(text);

        var assembly = Compile(text);

        var runtimeType = assembly.GetType("Test");
        var method = runtimeType.GetMethod("AddYourselfTo");
        var del = (Func<GameObject, MonoBehaviour>)
                      Delegate.CreateDelegate(
                          typeof(Func<GameObject, MonoBehaviour>),
                          method
                  );

        // We ask the compiled method to add its component to this.gameObject
        var addedComponent = del.Invoke(gameObject);
        */
    }

    public static Assembly Compile(string source)
    {
        var provider = new CSharpCodeProvider();
        var param = new CompilerParameters();

        // Add ALL of the assembly references
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            param.ReferencedAssemblies.Add(assembly.Location);
        }

        // Add specific assembly references
        //param.ReferencedAssemblies.Add("System.dll");
        //param.ReferencedAssemblies.Add("CSharp.dll");
        //param.ReferencedAssemblies.Add("UnityEngines.dll");

        // Generate a dll in memory
        param.GenerateExecutable = false;
        param.GenerateInMemory = true;

        // Compile the source
        var result = provider.CompileAssemblyFromSource(param, source);

        if (result.Errors.Count > 0)
        {
            var msg = new StringBuilder();
            foreach (CompilerError error in result.Errors)
            {
                msg.AppendFormat("Error ({0}): {1}\n",
                    error.ErrorNumber, error.ErrorText);
            }
            throw new Exception(msg.ToString());
        }

        // Return the assembly
        return result.CompiledAssembly;
    }
}