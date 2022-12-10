// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend.Data
{
    public class Note
    {
        public List<int> Key { get; set; }
        public List<string> Text { get; set; }
        public List<int[]> Position { get; set; }

        public Note(List<int> key, List<string> text, List<int[]> position)
        {
            Key = key;
            Text = text;
            Position = position;
        }
    }
}
