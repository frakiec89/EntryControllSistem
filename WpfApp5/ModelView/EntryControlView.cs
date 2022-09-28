using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.ModelView
{

    /// <summary>
    /// модель  пользователя  
    /// </summary>
    internal class EntryControlView
    {
        /// <summary>
        /// имя пользователя - получим из бд 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ид  пользователя - нужен для идентефикации 
        /// </summary>
        public int IdAccaunt { get; set; }

        /// <summary>
        /// путь к  картинке 
        /// </summary>
        public  string MyPathImage { get; set; }

        /// <summary>
        /// информация  о  входе  - например  "пользователь  иван  ---> последний вход 28.09.2022
        /// </summary>
        public string NameEdnMessage { get; set; }
        public string ColorBorder { get; set; }
        public string ColorBorder2 { get; set; }
        public string ColorBorder3 { get; set; }
        public string ColorBorder4 { get; set; }
        public string ColorBorder5 { get; set; }
    }
}
