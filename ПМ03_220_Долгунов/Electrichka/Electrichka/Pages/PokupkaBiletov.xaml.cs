using Electrichka.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Electrichka.Model;

namespace Electrichka.Pages
{
    public partial class PokupkaBiletov : Page
    {
        private int stationFromNum;
        private int stationToNum;
        private double priceForStation;
        public PokupkaBiletov()
        {
            InitializeComponent();
            DateTickets.Text = DateTime.Now.ToString("dd MMMMMM ");
            TimeTickets.Text = DateTime.Now.ToString("HH mm");
            NapravCB.ItemsSource = ModelClasses.db.Directions.ToList();
            NapravCB.DisplayMemberPath = "name";
            KlassCB.ItemsSource = ModelClasses.db.Categories.ToList();
            KlassCB.DisplayMemberPath = "name";

        }

        private void BuyTickets_Click(object sender, RoutedEventArgs e)
        {
            Model.Tickets tickets = new Model.Tickets
            {
                date = DateTime.Now,
                time = DateTime.Now.TimeOfDay,
                categoryId = (KlassCB.SelectedItem as Categories).id,
                directionId = (NapravCB.SelectedItem as Directions).id,
                stationFromId = (StanOtravCB.SelectedItem as Stations).id,
                stationToId = (StitionOutCB.SelectedItem as Stations).id,
                cost = Convert.ToDecimal(CenaBileta.Text)

            };

            ModelClasses.db.Tickets.Add(tickets);
            ModelClasses.db.SaveChanges();

            MessageBox.Show("Вы успешно зарегестрировались");
        }

        private void NapravCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ((sender as ComboBox).SelectedItem as Directions).id;
            StanOtravCB.ItemsSource = ModelClasses.db.Stations.Where(x => x.directionId == index).ToList();
            StanOtravCB.DisplayMemberPath = "name";

            StitionOutCB.ItemsSource = ModelClasses.db.Stations.Where(x => x.directionId == index).ToList();
            StitionOutCB.DisplayMemberPath = "name";
        }
        private double CalculatePrice(int firstnum, int lastnum, double oneprice)
        {
            return Convert.ToDouble(Math.Abs(firstnum - lastnum))*oneprice;
        }

        private void StanOtravCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            this.stationFromNum = (StanOtravCB.SelectedItem as Stations).number;
            CenaBileta.Text = Convert.ToString(CalculatePrice(this.stationFromNum,this.stationToNum,this.priceForStation));
        }

        private void StitionOutCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.stationToNum = (StitionOutCB.SelectedItem as Stations).number;
            CenaBileta.Text = Convert.ToString(CalculatePrice(this.stationFromNum, this.stationToNum, this.priceForStation));
        }

        private void KlassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.priceForStation = Convert.ToDouble((KlassCB.SelectedItem as Categories).tariffPerZone);
            CenaBileta.Text = Convert.ToString(CalculatePrice(this.stationFromNum, this.stationToNum, this.priceForStation));
        }
    }
}
