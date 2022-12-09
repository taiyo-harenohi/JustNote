
// Author: Lukáš Leták
// Date: 8/12/2022

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustNote.Backend.Data;

namespace JustNote.App.Services
{
    public class DataDiskService : DataServiceBase
    {   
        /*
        protected override Data GetDateDataAsync(DateTime date, string title)
        {
            //var dataFilePath = $@"Data/{date}.json";

            //if(!File.Exists(dataFilePath)) return string.Empty;

            //return await File.ReadAllTextAsync(dataFilePath);

            return Read(title);
        }
        */
    }
}
