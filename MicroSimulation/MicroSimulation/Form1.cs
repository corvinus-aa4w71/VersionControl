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
        Random rng = new Random(1234);
        List<Person> Population = null;
        List<BirthProbability> BirthProbability = null;
        List<DeathProbability> DeathProbability = null;
        public Form1()
        {
            InitializeComponent();

            BirthProbability = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbability = GetDeathProbabilities(@"C:\Temp\halál.csv");
            
        }

        private void StartSimulation(int endyear, string csvPath)
        {
            Population = GetPopupalation(csvPath);
            for (int year = 2005; year <= endyear; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int NbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int NbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                txtMain.Text += (string.Format("Szimulációs év:{0}\n\tFiúk:{1}\n\tLányok:{2}\n\n", year, NbrOfMales, NbrOfFemales));
            }
        }

        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbability
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbability
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
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
                        Age = byte.Parse(line[0]),
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
                        Age = byte.Parse(line[1]),                        
                        P = double.Parse(line[2])
                    });
                }
            }

            return deathProbability;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartSimulation((int)numericUpDown1.Value, txtPath.Text);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName = txtPath.Text;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            txtPath.Text = ofd.FileName;
            
        }
    }
}
