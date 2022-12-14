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
        /*  Key - ID of a single note on the page
            Text - text of a single note
            Position - current position of a single note
        */
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
