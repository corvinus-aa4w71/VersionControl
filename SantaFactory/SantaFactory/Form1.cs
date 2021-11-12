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
        Toy _nextToy;

        public IToyFactory ToyFactory
        {
            get { return _toyfactory; }
            set { 
                _toyfactory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            ToyFactory = new CarFactory();
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

        private void buttonBall_Click(object sender, EventArgs e)
        {
            ToyFactory = new BallFactory()
            {
                BallColor = btnColor.BackColor
            };

        }

        private void buttonCar_Click(object sender, EventArgs e)
        {
            ToyFactory = new CarFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
            {
                this.Controls.Remove(_nextToy);
            }
            _nextToy = ToyFactory.CreateNew();
            _nextToy.Left = lblNext.Left + lblNext.Width;
            _nextToy.Top = lblNext.Top;
            this.Controls.Add(_nextToy);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cd = new ColorDialog();
            cd.Color = button.BackColor;
            if (cd.ShowDialog() != DialogResult.OK) return;
            button.BackColor = cd.Color;

        }

        private void btnPresent_Click(object sender, EventArgs e)
        {
            ToyFactory = new PresentFactory()
            {
                Boxcolor = btnBox.BackColor,
                Ribboncolor = btnRibbon.BackColor
            };
        }
    }
}
