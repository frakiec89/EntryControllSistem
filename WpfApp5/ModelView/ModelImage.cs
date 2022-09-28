using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.ModelView
{

    /// <summary>
    /// Модель  картинки
    /// </summary>
    internal class ModelImage
    {
            /// <summary>
            /// название  - например  ivanov.png
            /// </summary>
            public string Name { get; set; } 

            /// <summary>
            /// Путь к картинке  
            /// </summary>
            public string Path { get; set; }
      
    }
}
