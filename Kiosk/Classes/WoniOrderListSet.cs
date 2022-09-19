using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    public class WoniOrderListSet
    {
        public List<DoubleBuffer_Panel> panelMain = new List<DoubleBuffer_Panel>();
        public List<DoubleBuffer_Panel> panelTitle = new List<DoubleBuffer_Panel>();
        public List<PictureBox> picBoxBtnPlus = new List<PictureBox>();
        public List<PictureBox> picBoxBtnMinus = new List<PictureBox>();
        public List<Label> labelTitle = new List<Label>();
        public List<Label> labelMemo = new List<Label>();
        public List<Label> labelQuan = new List<Label>();
        public List<Label> labelAmt = new List<Label>();
        public List<decimal> danga = new List<decimal>();
    }
}
