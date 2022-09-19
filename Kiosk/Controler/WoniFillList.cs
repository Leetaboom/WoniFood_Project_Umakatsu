using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Kiosk.Controler
{
    public partial class WoniFillList : Panel
    {
        public WoniFillList()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable | ControlStyles.FixedHeight, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            TabStop = false;
            BackColor = System.Drawing.Color.White;
            UpdateStyles();
            AutoScroll = true;
            HorizontalScroll.Visible = false;

            MinimumSize = new System.Drawing.Size(200, 200);
            Padding = new Padding(10);
        }

        private readonly WoniFillLayout _layoutEngin = new WoniFillLayout();

        public override LayoutEngine LayoutEngine
        {
            get
            {
                return _layoutEngin;
            }
        }

        private int _space = 0;

        public int Space
        {
            get { return _space; }
            set
            {
                _space = value;
                Invalidate();
            }
        }

        internal class WoniFillLayout : LayoutEngine
        {
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                var parent = container as WoniFillList;

                System.Drawing.Rectangle parentDisplayRectangle = parent.DisplayRectangle;
                System.Drawing.Point nextControlLocation = parentDisplayRectangle.Location;

                foreach (Control c in parent.Controls)
                {
                    if (!c.Visible)
                        continue;

                    c.Location = nextControlLocation;
                    c.Width = parentDisplayRectangle.Width - 10;
                    nextControlLocation.Offset(0, c.Height);
                }

                return false;
            }
        }

        internal class WoniVericalPanel : Panel
        {
            public WoniVericalPanel()
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.Selectable | ControlStyles.FixedHeight, false);
                SetStyle(ControlStyles.ResizeRedraw, true);
                TabStop = false;
                BackColor = System.Drawing.Color.Transparent;
                UpdateStyles();
            }

            private int space = 10;

            public int Space
            {
                get { return space; }
                set
                {
                    space = value;
                    LayoutControls();
                }
            }

            protected override void OnControlAdded(ControlEventArgs e)
            {
                base.OnControlAdded(e);
                LayoutControls();
            }

            protected override void OnControlRemoved(ControlEventArgs e)
            {
                base.OnControlRemoved(e);
                LayoutControls();
            }

            private void LayoutControls()
            {
                int height = space;

                foreach (Control c in base.Controls)
                    height += c.Height + space;

                base.AutoScrollMinSize = new System.Drawing.Size(0, height);

                int top = base.AutoScrollPosition.Y + space;
                int width = base.ClientSize.Width - (space * 2);

                foreach (Control c in base.Controls)
                {
                    c.SetBounds(space, top, width, c.Height);
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    top += c.Height + space;
                }
            }
        }
    }
}
