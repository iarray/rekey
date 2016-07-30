using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace rekey.Script
{
    //键盘事件常量
    public enum KeyEventFlag : int
    {
        Down = 0x0000,
        Up = 0x0002,
    }

    public class ChangeKeyScript:Script
    {
        private Dictionary<string, byte> keyCodeDict = null;
             
        //键盘事件函数
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, Byte bScan, KeyEventFlag dwFlags, Int32 dwExtraInfo);

        public ChangeKeyScript(string key, ValuePair[] args) : base(key, args) { }

        public void keyEvent(byte code,int flag)
        {
            keybd_event(code, 0, (KeyEventFlag)flag, 0);
        }

        public override void Excute()
        {
            var args = GetArgs(); 
            if (keyCodeDict == null)
            {
                keyCodeDict = new Dictionary<string, byte>(); 
                for (int i = 0; i < args.Length; i++)
                {
                    string keyStr = args[i].value;
                    if (keyCodeDict.ContainsKey(keyStr) == false)
                    {
                        byte code = (byte)(int)Enum.Parse(typeof(Hook.Keys), args[i].value);
                        keyCodeDict.Add(keyStr, code);
                    }

                }
            }

            foreach (var item in args)
            {
                int flag = 0x0000;
                if (!string.IsNullOrEmpty(item.arg))
                    int.TryParse(item.arg, out flag);

                keyEvent(keyCodeDict[item.value], flag);
            }
        }
    }
}
