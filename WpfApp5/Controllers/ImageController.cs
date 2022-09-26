using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfApp5.ModelView;

namespace WpfApp5.Controllers
{
    internal class ImageController
    {
        public static List<ModelImage> GetImage()
        {
            List<ModelImage> acaunts = new List<ModelImage>();

            Assembly assembly = Assembly.GetExecutingAssembly();

            string ddlStart = assembly.Location; // полный путь   к  dll 

            string dirImage = System.IO.Path.GetDirectoryName(ddlStart);


            dirImage = dirImage + "/AcauntImage";
            var files = Directory.GetFiles(dirImage);

            var absolutPathFele = files.Where(
                x => x.ToLower().EndsWith(".png")
                || x.ToLower().EndsWith(".jpeg")
                 || x.ToLower().EndsWith(".jpg")
                ).ToArray();

            foreach (var file in absolutPathFele)
            {
                var name = System.IO.Path.GetFileName(file);
                var path = @"pack://application:,,,/AcauntImage/" + name;
                var newImage = new ModelImage() { Name = name, Path = path };
                acaunts.Add(newImage);
            }
            return acaunts;

        }
    }
}
