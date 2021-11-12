using SantaFactory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFactory.Entitties
{
    class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            var imageFile = Image.FromFile(@"Images\car.png");
            g.DrawImage(imageFile, 0, 0, Width, Height);
        }
    }
}
