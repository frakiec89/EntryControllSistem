using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace WpfApp5.DB
{
    public class EntryControl
    {
        [Key]
        public int EntryControlId { get; set; }

        public DateTime DateTimeEntryControl
        { get; set; }

        public int AcauntId { get; set; }
        public Acaunt Acaunt { get; set; }

    }
}