using SOAP.Entities;
using SOAP.MNBServiceReference;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace SOAP
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            string result = response.GetCurrenciesResult;
            XmlDocument vxml = new XmlDocument();
            vxml.LoadXml(result);
            foreach (XmlElement item in vxml.DocumentElement.FirstChild.ChildNodes)
            {
                Currencies.Add(item.InnerText);
            }
            comboBox1.DataSource = Currencies;
            RefreshData();
        }

        private void RefreshData()
        {
            if (comboBox1.SelectedItem == null) return;
            Rates.Clear();
            string xmlstring = Consume();
            LoadXml(xmlstring);
            dataGridView1.DataSource = Rates;
            Charting();
        }

        private void Charting()
        {
            chartData.DataSource = Rates;
            var series = chartData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var chartArea = chartData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
            var legend = chartData.Legends[0];
            legend.Enabled = false;
        }

        string Consume()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = comboBox1.SelectedItem.ToString(); //"EUR";
            request.startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"); //"2020-01-01";
            request.endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd"); //"2020-06-30";
            var response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            return result;
        }

        void LoadXml(string input)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(input);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData rd = new RateData();
                rd.Date = DateTime.Parse(item.GetAttribute("date"));
                XmlElement child = (XmlElement)item.FirstChild;
                if (child == null) continue;
                rd.Currency = child.GetAttribute("curr");
                rd.Value = decimal.Parse(child.InnerText);
                int unit = int.Parse(child.GetAttribute("unit"));
                if (unit != 0) rd.Value = rd.Value / unit;
                Rates.Add(rd);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void filterChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
