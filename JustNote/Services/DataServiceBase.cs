
// Author: Lukáš Leták

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustNote.Backend.Data;
using JustNote.Backend;

namespace JustNote.App.Services
{
    public abstract class DataServiceBase : IDataService
    {
        public Data GetDateData(DateTime date, string? title)
        {
            if (title == null)
            {
                var new_page = new Data();
                new_page.Date = date;
                new_page.Title = "New Title";
                return new_page;
            };

            //var DataOBJ = GetDateDataAsync(date,title);
            var Reader = new ReadingData();
            return Reader.Read(title, date.ToString("dd/MM/yyyy"));
        }

        public void SaveDateData(Data data)
        {
            var saver = new SavingData();
            saver.Save(data);
        }

        public List<string> GetFilenamesInDate(DateTime date)
        {
            var Reader = new SearchingData();
            return Reader.SearchData(date.ToString("dd/MM/yyyy"));
        }

        public void DeleteDateData(Data data)
        {
            var Remover = new DeletingData();
            Remover.Delete(data);
        }

        // Author: Nikola Machálková
        public List<string> GetFilenamesKeyword(string keyword, DateTime date)
        {
            var Reader = new FindingKeyword();
            return Reader.FindKeyword(keyword, date.ToString("dd/MM/yyyy"));
        }

        //protected abstract Data GetDateDataAsync(DateTime date, string title);
    }
}
