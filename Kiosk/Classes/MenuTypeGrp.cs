using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    public class MenuTypeGrp
    {
        public string name;
        public string type;


        public class MenuTypeGrp1 : MenuTypeGrp
        {
            public MenuListType1[] menuGrpType1 = new MenuListType1[5];
        }

        public class MenuTypeGrp2 : MenuTypeGrp
        {
            public MenuListType2[] menuGrpType2 = new MenuListType2[5];
        }

        public class MenuTypeGrp3 : MenuTypeGrp
        {
            public MenuListType3[] menuGrpType3 = new MenuListType3[5];
        }

        public class MenuListType1
        {
            public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
            public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
            public List<string> type = new List<string>();
            public List<Label> menuTitle = new List<Label>();
            public List<Label> menuSingleTitle = new List<Label>();
            public List<Label> menuSetTitle = new List<Label>();
            public List<Label> menuSingleAmt = new List<Label>();
            public List<Label> menuSetAmt = new List<Label>();
            public List<string> menuMemo = new List<string>();
            public List<string> menuTCode = new List<string>();
            public List<bool> soldOut = new List<bool>();
        }

        public class MenuListType2
        {
            public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
            public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
            public List<Label> menuTitle = new List<Label>();
            public List<Label> menuSubTitle1 = new List<Label>();
            public List<Label> menuSubTitle2 = new List<Label>();
            public List<Label> menuSubTitle3 = new List<Label>();
            public List<Label> menuSubTitle4 = new List<Label>();
            public List<Label> menuAmt1 = new List<Label>();
            public List<Label> menuAmt2 = new List<Label>();
            public List<Label> menuAmt3 = new List<Label>();
            public List<Label> menuAmt4 = new List<Label>();
            public List<string> menuMemo = new List<string>();
            public List<string> menuTCode = new List<string>();
            public List<bool> soldOut = new List<bool>();
        }

        public class MenuListType3
        {
            public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
            public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
            public List<Label> menuTitle = new List<Label>();
            //public List<Label> menuSingelTitle = new List<Label>();
            public List<Label> menuAmt = new List<Label>();
            public List<string> menuMemo = new List<string>();
            public List<string> menuTCode = new List<string>();
            public List<bool> soldOut = new List<bool>();
        }

    }
}
