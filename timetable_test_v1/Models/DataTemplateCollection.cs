using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetable_test_v1.Models
{
    public partial class TimetableDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClassroomDontHaveClass { get; set; }
        public DataTemplate ClassroomHaveClass { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Timetable)item).Course == "Empty" ? ClassroomDontHaveClass : ClassroomHaveClass;
        }
    }
}
