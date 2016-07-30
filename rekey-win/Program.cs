using Hook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using rekey.Script;
using rekey;
using System.Threading;

namespace rekey_win
{
    static class Program
    {
        static Mutex mutex;
        static Dictionary<string, IExcuteScript> excutor = new Dictionary<string, IExcuteScript>();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (checkIsRun())
            {
                MessageBox.Show("已经启动了");
                Application.Exit();
                return;
            }
            InitScriptConfig();
            var hook = new KeyBoardHook();
            hook.KeyDown += hook_KeyDown;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();

        }
         

        static bool hook_KeyDown(object sender, KeyBoardHookEventArgs e)
        {
            string keyName = e.Key.ToString();
            if (excutor.ContainsKey(keyName))
            {
                excutor[keyName].Excute();
                return true;
            }
            return false;
        }

        static bool checkIsRun()
        {
            string mutexName = "SingleInstance";
            bool createNew;
            mutex = new Mutex(true, mutexName, out createNew);
            if (!createNew)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void InitScriptConfig()
        {
            string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configPath = Path.Combine(dirPath, "key.ini");
            if (File.Exists(configPath) == false)
            {
                MessageBox.Show("缺少配置文件key.ini");
                Application.Exit();
            }

            using (StreamReader sr = new StreamReader(configPath, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string lineScript = sr.ReadLine();
                    var s = ScriptFactory.Create(lineScript);
                    if (s == null)
                        continue;
                    if (excutor.ContainsKey(s.GetKey()))
                        excutor[s.GetKey()] = s;
                    else
                        excutor.Add(s.GetKey(), s);
                }
            }

        }
    }
}
