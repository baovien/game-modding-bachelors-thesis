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
        string source = File.ReadAllText(@"C:\Users\Kristoffer\Desktop\Skole\DAT304 - Bachelor\Spillmappe\Mods\Nibblet.cs");
        var assembly = Compile(source);

        var method = assembly.GetType("Nibblet").GetMethod("Start");
        var del = (Action)Delegate.CreateDelegate(typeof(Action), method);
        del.Invoke();
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