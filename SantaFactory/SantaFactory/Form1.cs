using SantaFactory.Abstractions;
using SantaFactory.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SantaFactory
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();

        private IToyFactory _toyfactory;

        public IToyFactory ToyFactory
        {
            get { return _toyfactory; }
            set { _toyfactory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            ToyFactory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = ToyFactory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var lastPosition = 0;
            
            foreach (var item in _toys)
            {
                item.MoveToy();
                if(item.Left > lastPosition)
                {
                    lastPosition = item.Left;
                }
            }

            if (lastPosition >= 1000)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
        }
    }
}
