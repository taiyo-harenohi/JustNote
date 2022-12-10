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
        public int Key { get; set; }
        public string Text { get; set; }
        public int[] Position { get; set; }

        public Note(int key, string text, int[] position)
        {
            Key = key;
            Text = text;
            Position = position;
        }
    }
}
