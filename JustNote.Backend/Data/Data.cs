// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend.Data
{
    // structure of a one single page:
    // Title -- the name of the page
    // Notes -- every note on the page
    // DateTime -- when was this page created (how the heck should this stay the same? Because this should only happen once, right)

    public class Data
    {
        public string Title { get; set; }
        public List<Note> Notes { get; set; }

        public DateTime Date { get; set; }

        public Data()
        {

        }
        public Data(string title, List<Note> notes, DateTime date)
        {
            Title = title;
            Notes = notes;
            Date = date;
        }
    }
}
