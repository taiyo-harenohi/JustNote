
// Author: Lukáš Leták

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
        Data GetDateData(DateTime date, string? title);

        void SaveDateData(Data data);

        void DeleteDateData(Data data);

        void ExportDataTXT(Data data);

        List<string> GetFilenamesInDate(DateTime date);

        List<string> GetFilenamesKeyword(string keyword, DateTime date);
    }
}
