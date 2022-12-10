// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace JustNote.Backend.Data
{
    // TODO-delete-later: gonna need some testing of this; as in, this is totally not tested, like, at all
    public class Export
    {
        public static void ExportJSON(Data data, string filepath)
        {
            using (StreamWriter sw = new StreamWriter(filepath + ".json"))
            {
                sw.WriteLine(JsonSerializer.Serialize(data));
            }
        }

        public static void ExportTXT(Data data, string filepath)
        {
            using (StreamWriter sw = new StreamWriter(filepath + ".txt"))
            {
                sw.WriteLine(data);
            }
        }

        public static void ExportPDF(Data data, string filepath)
        {
            FileStream fs = new FileStream(filepath + ".pdf", FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.AddAuthor("User");
            document.AddTitle(data.Title);
            document.AddCreationDate();

            document.Open();
            document.Add(new Paragraph(data.ToString()));
            document.Close();
            writer.Close();
            fs.Close();
        }
    }
}
