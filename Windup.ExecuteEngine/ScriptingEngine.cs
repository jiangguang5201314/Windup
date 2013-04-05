using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;

namespace Windup.ExecuteEngine
{
    public class ScriptingEngine
    {
        public string Name;
        ScriptEngine eng;
        ScriptSource source;
        ScriptScope scope;
        ObjectOperations ops;
        object pyClass;
        object classObj;
        object method;
        public ScriptingEngine(string scriptFileName)
        {
            if (string.IsNullOrEmpty(scriptFileName)) 
                throw new ArgumentNullException("script filename is null...");
           
            eng = Python.CreateEngine();
            source = eng.CreateScriptSourceFromFile(scriptFileName);
            scope = eng.CreateScope();
            ops = eng.CreateOperations();
            source.Execute(scope);
            Name = scope.GetVariable("__caption__");
        }

        public void InitializationMethod(string clasz, string method)
        {
            pyClass = scope.GetVariable(clasz);
            classObj = ops.Invoke(pyClass);
            method = ops.GetMember(classObj, method);
        }

        public string Invoke(string parameter)
        {
           return (string)ops.Invoke(method, parameter);
        }
    }
}
