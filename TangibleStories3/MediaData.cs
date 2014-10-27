using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TangibleStories3
{
    public class MediaData
    {
        public string Source { get; set; }
        public string Caption { get; set; }
        public string Story { get; set; }
        public string LinkedVid { get; set; }

        public MediaData(string source, string caption, string story, string linkedVid)
        {
            this.Source = source;
            this.Caption = caption;
            this.Story = story;
            this.LinkedVid = linkedVid;
        }

        public System.Windows.Visibility Visibility { get; set; }

        public bool Checked { get; set; }
    }
}
