using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeoSprint.UI.Extentions
{
    public static class ControlExtentions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate)action);
        }
    }
}
