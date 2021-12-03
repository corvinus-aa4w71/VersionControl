using MicroSimulation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroSimulation
{
    public partial class Form1 : Form
    {
        List<Person> Population = null;
        List<BirthProbability> BirthProbability = null;
        List<DeathProbability> DeathProbability = null;
        public Form1()
        {
            InitializeComponent();
            Population = GetPopupalation(@"C:\Users\vanst\AppData\Local\Temp\nép-teszt.csv");
        }

        public List<Person> GetPopupalation(string csvPatch)
        {
            List<Person> population = new List<Person>();
            using (var sr = new StreamReader(csvPatch, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new Person();
                    p.BirthYear = int.Parse(line[0]);
                    p.Gender = (Gender)Enum.Parse(typeof(Gender), line[1]);
                    p.NbrOfChildren = int.Parse(line[2]);
                    population.Add(p);
                }
            }

            return population;
        }
    }
}
