#rekey 按键映射

支持自定义改键规则

解析设置key.ini文件: 

例如: 按下F1等于按下Ctrl+C设置如下

[F1]=[CTRL,0]+[C,0]+[CTRL,2]+[C,2]

其中0代表按下,2代表松开

按下F2运行QQ,并附带参数-a

[F2]=[D:\QQ.exe,a]

暂时支持单键按下映射,组合键映射其它按键并不支持
下面的设置暂不支持
[CTRL] + [F1]=[CTRL,0]+[C,0]+[CTRL,2]+[C,2]

[![程序下载地址](http://files.cnblogs.com/files/fornet/rekey.zip)]
#Developed By

* IARRAY
  

