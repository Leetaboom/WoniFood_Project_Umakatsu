using DevComponents.WinForms.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Kiosk
{
    public partial class DblBuffPanel : Panel
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        //[Browsable(false)]
        //public  BorderStyle BorderStyle { get; set; }

        [Browsable(true)]
        [Category("Border")]
        public BorderType Border 
        { 
            get { return border; } 
            set 
            { 
                border = value;
                if (border == BorderType.RoundedRectangle)
                    radius = 1;
                else
                    radius = 0;
                this.Invalidate();
                this.PerformLayout();
            } 
        }

        [Browsable(true)]
        [Category("BorderRadius")]
        public int BorderRadius {
            get { return radius;  }
            set 
            {
                if (value <= 0)
                    radius = 1;
                else
                    radius = value;
                this.RecreateRegion();
                this.PerformLayout();
            }
        }

        [Browsable(true)]
        [Category("BorderWidth")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set 
            {
                if (value <= 0)
                    borderWidth = 0;
                else
                    borderWidth = value;
                this.Invalidate();
                this.PerformLayout();
            }
        }

        [Browsable(true)]
        [Category("BorderColor")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
                this.PerformLayout();
            }
        }

        private BorderType border = BorderType.Rectangle;
        private int radius = 10;
        private int borderWidth = 0;
        private Color borderColor = Color.Black;

        public DblBuffPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable | ControlStyles.FixedHeight, false);
            TabStop = false;
            BackColor = Color.White;
            UpdateStyles();
        }

        private GraphicsPath GetRectangle(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(bounds.Left + borderWidth / 2, bounds.Top + borderWidth / 2, bounds.Right - borderWidth / 2, bounds.Top + borderWidth / 2);
            path.AddLine(bounds.Right - borderWidth / 2, bounds.Top + borderWidth / 2 , bounds.Right - borderWidth / 2, bounds.Bottom - borderWidth / 2);
            path.AddLine(bounds.Right - borderWidth / 2 , bounds.Bottom - borderWidth / 2 , bounds.Left + borderWidth / 2, bounds.Bottom - borderWidth / 2);
            path.AddLine(bounds.Left + borderWidth / 2, bounds.Bottom - borderWidth / 2, bounds.Left + borderWidth / 2, bounds.Top + borderWidth / 2);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath GetRoundRectagle(Rectangle bounds, int radius)
        {
            float r = radius;
            float left = bounds.Left + (borderWidth / 2);
            float top = bounds.Top + (borderWidth / 2);
            float right = bounds.Right - r - (borderWidth / 2) - 2;
            float bottom = bounds.Bottom - r - (borderWidth / 2) - 2;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(left, top, r, r, 180, 90);
            path.AddArc(right, top, r, r, 270, 90);
            path.AddArc(right, bottom, r, r, 0, 90);
            path.AddArc(left, bottom, r, r, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void RecreateRegion()
        {
            var bounds = this.DisplayRectangle;
            if (this.Border == BorderType.RoundedRectangle)
                this.Region = Region.FromHrgn(CreateRoundRectRgn(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, radius, radius));
            else if (this.Border == BorderType.Rectangle)
                this.Region = Region.FromHrgn(CreateRectRgn(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom));
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (borderWidth > 0)
            {
                Pen pen = new Pen(borderColor, borderWidth);
                if (this.Border == BorderType.RoundedRectangle)
                    e.Graphics.DrawPath(pen, GetRoundRectagle(this.ClientRectangle, radius));
                else if (this.Border == BorderType.Rectangle)
                    e.Graphics.DrawRectangle(pen, this.Bounds);
            }
            else
                base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }
    }
}
