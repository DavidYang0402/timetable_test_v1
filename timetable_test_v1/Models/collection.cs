using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace timetable_test_v1.Models
{
    public partial class KioskItem
    {
        public string Source { get; set; }
        public ICommand Command { get; set; }
        public Uri ImageURI { get; set; }
    }
}
