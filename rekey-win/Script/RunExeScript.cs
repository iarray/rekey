using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace rekey.Script
{
    public class RunExeScript:Script
    {
        public RunExeScript(string key, ValuePair[] args) : base(key, args) { }

        public override void Excute()
        {
            var args = GetArgs();
            foreach (var item in args)
            {
                try
                {
                    Process.Start(item.value, item.arg);
                }
                catch { }
            }

        }
    }
}
