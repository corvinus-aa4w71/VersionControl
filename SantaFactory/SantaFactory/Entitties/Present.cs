using SantaFactory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFactory.Entitties
{
    public class Present : Toy
    {
        public SolidBrush Boxcolor { get; private set; }
        public SolidBrush Ribboncolor { get; private set; }
        public Present(Color boxcolor, Color ribboncolor)
        {
            Boxcolor = new SolidBrush(boxcolor);
            Ribboncolor = new SolidBrush(ribboncolor);
        }


        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(Boxcolor, 0, 0, Width, Height);
            g.FillRectangle(
                Ribboncolor,
                0,
                Height * 0.4f, //vagy (float)(Height * 0.4)
                Width,
                Height * 0.2f
                );
            g.FillRectangle(
                Ribboncolor,
                Width * 0.4f,
                0,
                Height * 0.2f,
                Height
                );
        }
    }
}
