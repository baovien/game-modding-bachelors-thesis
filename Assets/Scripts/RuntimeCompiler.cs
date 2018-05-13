using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

public class RuntimeCompiler : MonoBehaviour
{
    public List<string> navn = new List<string>();

    private string folderPath;
    
    void Start()
    {
        folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BareKoding/";
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Created folder.");
            }
        }
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }

        DirectoryInfo di = new DirectoryInfo(folderPath);
        FileInfo[] files = di.GetFiles("*.cs");
        foreach (FileInfo file in files)
        {
            navn.Add(file.Name);
        }

        foreach (var fileName in navn)
        {
            
            string text = File.ReadAllText(folderPath + fileName);
            var fileName2 = fileName.Substring(0, fileName.Length - ".cs".Length);
            var assembly = Compile(text);
            var method = assembly.GetType(fileName2).GetMethod("InstantiateMe");
            
            if (method != null)
            {
                var del = (Action) Delegate.CreateDelegate(typeof(Action), method);
                del.Invoke();
            }
        }
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