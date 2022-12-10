﻿
// Author: Lukáš Leták

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
                new_page.Date = date;
                new_page.Title = "New Title";
                return new_page;
            };

            //var DataOBJ = GetDateDataAsync(date,title);
            var Reader = new ReadingData();
            return Reader.Read(title, date.ToString("dd/mm/yyyy"));
        }

        public void SaveDateData(Data data)
        {
            var saver = new SavingData();
            saver.Save(data);
        }

        //protected abstract Data GetDateDataAsync(DateTime date, string title);
    }
}