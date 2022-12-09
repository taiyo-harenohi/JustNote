
// Author: Lukáš Leták
// Date: 8/12/2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustNote.Backend.Data;

namespace JustNote.App.Services
{
    public abstract class DataServiceBase : IDataService
    {
        public Data GetDateData(DateTime date, string title)
        {
            if (title == null)
            {
                var new_page = new Data();
                new_page.Title = "New Title";
                return new_page;
            };

            //var DataOBJ = GetDateDataAsync(date,title);
            var Reader = new ReadingData();
            var keyword = title + date.ToString("dd/mm/yyyy");
            return Reader.Read(keyword);
        }

        //protected abstract Data GetDateDataAsync(DateTime date, string title);
    }
}
