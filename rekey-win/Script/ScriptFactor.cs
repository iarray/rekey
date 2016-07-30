using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rekey.Script
{
    public enum ScriptType
    {
        NONE = 0,
        KEY = 1,
        EXE = 2
    }

    public struct ValuePair
    {
        public string value;
        public string arg;
    }

    public struct KeyParams
    {
        public string key;
        public ValuePair[] value;
        public ScriptType type;
    }

    public class ScriptFactory
    {
        public static Script Create(string script)
        {
           var kp = GetKeyAndParams(script);
           if (kp == null)
               return null;

           switch (kp.Value.type)
           {
               case ScriptType.KEY:
                   return new ChangeKeyScript(kp.Value.key,kp.Value.value);
               case ScriptType.EXE:
                   return new RunExeScript(kp.Value.key, kp.Value.value);
               default:
                   return null;
           }
        }

        public static KeyParams? GetKeyAndParams(string script)
        {
            var arg = script.Split('=');
            if (arg.Length == 2)
            {
                //去除左右中括号[x],得到x
                string key = getRealValue(arg[0]);
                if (string.IsNullOrEmpty(key) || !IsKeyCode(key))
                    return null;

                var param = arg[1].Split('+');
                if(param.Length <=0 )
                    return null;

                ValuePair[] vps = new ValuePair[param.Length]; 
                ScriptType type = ScriptType.NONE;
                for (int i = 0; i < param.Length; i++)
			    {
                    var vp = GetValuePair(param[i]);

                    if (vp == null )
                        break;
                    
                    vps[i] = vp.Value;
                    if ((type = GetScriptType(vps[i].value)) != ScriptType.KEY)
                        break;
			    }
                if (type == ScriptType.NONE)
                    return null;

                return new KeyParams { key = key, value = vps, type = type };
            }
            return null;
        }

        public static ScriptType GetScriptType(string s)
        {
            ScriptType type = ScriptType.NONE;
            if (IsKeyCode(s))
            {
                type = ScriptType.KEY;
            }
            else if (IsFilePath(s))
            {
                type = ScriptType.EXE;
            }
            else
            {
                type = ScriptType.NONE; 
            }
            return type;
        }

        public static bool IsFilePath(string p)
        {  
            return File.Exists(p);
        }

        public static bool IsKeyCode(string keyCode)
        {
            try
            {
                Enum.Parse(typeof(Hook.Keys), keyCode, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static string getRealValue(string valueWithBrackets)
        {
            string key = "";
            if (valueWithBrackets.Length > 2 && valueWithBrackets.StartsWith("[") && valueWithBrackets.EndsWith("]"))
            {
                key = valueWithBrackets.Substring(1, valueWithBrackets.Length - 2).Trim();
            }
            return key;
        }

        private static ValuePair? GetValuePair(string par)
        {
            ValuePair res = new ValuePair();
            var arr = getRealValue(par).Split(',');
            if (arr.Length > 0)
            {
                res.value = arr[0];
            }
            else
                return null;

            res.arg = arr.Length >= 2 ? arr[1] : "";
            return res;
        }

    }
}
