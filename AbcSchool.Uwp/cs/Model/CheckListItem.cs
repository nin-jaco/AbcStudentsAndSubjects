using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCSchool.Uwp.Model
{
    public class CheckListItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
