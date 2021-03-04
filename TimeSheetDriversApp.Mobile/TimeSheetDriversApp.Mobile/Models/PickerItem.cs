using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheetDriversApp.Mobile.Models
{
    public class PickerItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
