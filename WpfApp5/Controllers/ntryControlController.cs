using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.Controllers
{
    public class EntryControlController
    {
        private DB.MyContext MyContext;
        private string meserror = "Ошибка при обращении к базе данных \n";
        public EntryControlController()
        {
            MyContext = new DB.MyContext();
        }

        internal List<ModelView.EntryControlView> GetEntryControlTheLastFiveDays()
        {
           var listResult =  new List<ModelView.EntryControlView>();
            try
            {
                List<DB.Acaunt> acaunts = MyContext.Acaunts.ToList(); // список пользователей  
                foreach (var item in acaunts)
                {
                    var  newModelAcaunting  = new ModelView.EntryControlView();
                    newModelAcaunting.NameEdnMessage = $"Пользователь: {item.Name} -->>> последний вход {GetLastEntry(item.AcauntId)}";
                    newModelAcaunting.MyPathImage = @"pack://application:,,,/AcauntImage/" + item.PathImage;

                    newModelAcaunting.ColorBorder = GetColorColorBorder(DateTime.Now , item.AcauntId);
                    newModelAcaunting.ColorBorder2 = GetColorColorBorder(DateTime.Now.AddDays(-1), item.AcauntId);
                    newModelAcaunting.ColorBorder3 = GetColorColorBorder(DateTime.Now.AddDays(-2), item.AcauntId);
                    newModelAcaunting.ColorBorder4 = GetColorColorBorder(DateTime.Now.AddDays(-3), item.AcauntId);
                    newModelAcaunting.ColorBorder5 = GetColorColorBorder(DateTime.Now.AddDays(-4), item.AcauntId);
                    
                    listResult.Add(newModelAcaunting);
                }

                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception( meserror + ex.Message );
            }
        }

        private string GetColorColorBorder(DateTime now, int acauntId)
        {
            var dataMin = DateTime.Parse($"{now.Year}.{now.Month}.{now.Day}");
            var dataMax = dataMin.AddDays(1);

            try
            {
               var flag =  MyContext.EntryControls.Any(x => x.AcauntId == acauntId && x.DateTimeEntryControl >= dataMin && x.DateTimeEntryControl < dataMax);
                if (flag)
                    return "Red";
                else return "Black";
            }
            catch (Exception ex)
            {
                throw new Exception(meserror + ex.Message);
            }
        }

     




        /// <summary>
        /// находит  последнию дату  входа у  пользователя 
        /// </summary>
        /// <param name="acauntId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string  GetLastEntry(int acauntId)
        {
            try
            {
               var  data  = MyContext.EntryControls.Where(x => x.AcauntId == acauntId).Max(x => x.DateTimeEntryControl);
                return data.ToLongDateString();
            }
            catch 
            {
                return "--No--";
            }

        }
    }
}
