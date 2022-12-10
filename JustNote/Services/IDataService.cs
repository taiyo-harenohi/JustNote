
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
    public interface IDataService
    {
        Data GetDateData(DateTime date, string title);

        void SaveDateData(Data data);
    }
}
