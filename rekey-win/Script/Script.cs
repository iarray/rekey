using System;
using System.Collections.Generic;
using System.Text;
namespace rekey.Script
{
    public abstract class Script:IExcuteScript
    {
        private readonly string key;
        private readonly ValuePair[] args;
        public Script(string key, ValuePair[] args) { this.key = key; this.args = args; }

        public string GetKey()
        {
            return key;
        }

        public ValuePair[] GetArgs()
        {
            return args;
        }

        public abstract void Excute();
    }
}
