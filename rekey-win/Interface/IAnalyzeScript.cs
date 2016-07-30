using System;
using System.Collections.Generic;
using System.Text;

namespace rekey
{
    public interface IAnalyzeScript
    {
        IExcuteScript Analyze(string script);
    }
}
