using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SampleProblem12739
{
    class Scan
    {
        public ImageSource Image { get; set; }
        //Descrizione o importo
        public String Value { get; set; }
        public String Path { get; set; }
        public String FileName { get; set; }

        public Scan(ImageSource Image, string Value, string Path, string FileName)
        {
            this.Image = Image;
            this.Value = Value;
            this.Path = Path;
            this.FileName = FileName;
        }
    }
}
