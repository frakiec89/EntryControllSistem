using System.ComponentModel.DataAnnotations;

namespace WpfApp5.DB
{
    public class Acaunt
    {
        [Key]
        public int AcauntId { get; set; }
        public string Name { get; set; }

        public string PathImage { get; set; }


        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}