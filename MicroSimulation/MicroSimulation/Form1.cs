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
            BirthProbability = GetBirthProbabilities(@"C:\Users\vanst\AppData\Local\Temp\születés.csv");
            DeathProbability = GetDeathProbabilities(@"C:\Users\vanst\AppData\Local\Temp\halál.csv");
        }

        public List<Person> GetPopupalation(string csvPatch)
        {
            List<Person> population = new List<Person>();
            using (var sr = new StreamReader(csvPatch, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');                
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvPatch)
        {
            List<BirthProbability> birthProbability = new List<BirthProbability>();
            using (var sr = new StreamReader(csvPatch, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbability.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return birthProbability;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvPatch)
        {
            List<DeathProbability> deathProbability = new List<DeathProbability>();
            using (var sr = new StreamReader(csvPatch, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbability.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),                        
                        P = double.Parse(line[2])
                    });
                }
            }

            return deathProbability;
        }
    }
}
