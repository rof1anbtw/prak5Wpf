using System;
using System.Collections.Generic;
using System.Data;
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

namespace wpf5Pr
{
    /// <summary>
    /// Логика взаимодействия для Pagee.xaml
    /// </summary>
    public partial class Pagee : Page
    {
        public Pagee()
        {
            InitializeComponent();
        }
        Frame frame = new Frame();
        public Pagee(string date, Frame frame)
        {
            InitializeComponent();
            DatePicker.Text = date;
            this.frame = frame;
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.Text = MyDate.NextDate(DatePicker.Text);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.Text = MyDate.BackDate(DatePicker.Text);
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] splitDate = MyDate.splitDate(DatePicker.Text);

            var listJson = Json.Deserialize();
            List<DaysView> listDays = new List<DaysView>();

            for (int i = 1; i <= DateTime.DaysInMonth(Convert.ToInt32(splitDate[2]), Convert.ToInt32(splitDate[1])); i++)
            {
                string path = null;
                foreach (var element in listJson)
                {
                    if (element.date == MyDate.SetDays(MyDate.unsplitDate(splitDate), i))
                    {
                        path = element.pathToImg;
                    }
                }
                listDays.Add(new DaysView(i, path));
            }

            DaysPanel.ItemsSource = listDays;
        }
        private void DaysPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DaysPanel.SelectedIndex != -1)
            {
                frame.Content = new SecondPage(MyDate.SetDays(DatePicker.Text, (int)(DaysPanel.SelectedItem as DaysView).LabelNumber.Content), frame, DaysPanel.SelectedItem as DaysView);
            }
        }
    }
}
