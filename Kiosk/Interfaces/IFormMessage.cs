using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    interface IFormMessage
    {
        void Receive(Form frm, string msg);
    }
}
