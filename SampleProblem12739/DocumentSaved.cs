using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProblem12739
{
    class DocumentSaved
    {
        public String DirName { get; set; }
        public String ScanNum { get; set; }
        public String LastChange { get; set; }

        public DocumentSaved(string DirName, string ScanNum, String LastChange)
        {
            this.DirName = DirName;
            this.ScanNum = ScanNum;
            this.LastChange = LastChange;
        }
    }
}
