using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Timers;
using System.Windows.Shapes;
using System.Security.Policy;
using System.IO;
using System.ComponentModel;

namespace WPF_SemesterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DetailViewWindow : Window
    {
        public Action ClosingAction { get; set; }
        string currentMode = string.Empty;
        object item = null;
        public DetailViewWindow(string mode, object item)
        {
            currentMode = mode;
            this.item = item;
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Title = mode + " Details";

            printInfos();
            ListViewInfo.Items.Add(item.ToString());
        }
        private async void printInfos()
        {
            if (item.GetType() == typeof(Ship))
            {
                Ship s = (Ship)item;
                LabelInfoTitle.Content = "Info: " + s.model;

                string test = await ServerCon.INSTANCE.GetShipNameByModelAsync(s.model);
                string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Ships", test + ".jpg");
                var uri = new Uri(path);
                var bitmap = new BitmapImage(uri);
                ImageInfo.Source = bitmap;

            }
            else if (item.GetType() == typeof(ShipOnSale))
            {
                ShipOnSale s = (ShipOnSale)item;
                LabelInfoTitle.Content = "Info: " + s.ship.model;

                string test = await ServerCon.INSTANCE.GetShipNameByModelAsync(s.ship.model);
                string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Ships", test + ".jpg");
                var uri = new Uri(path);
                var bitmap = new BitmapImage(uri);
                ImageInfo.Source = bitmap;
            }
            else if (item.GetType() == typeof(Job))
            {
                Job job = (Job)item;
                LabelInfoTitle.Content = "Info: " + job.job;
                TextLabelInfo.Text = job.description;
                TextLabelInfo.Visibility = Visibility.Visible;
            }
            else if (item.GetType() == typeof(Planet))
            {
                Planet p = (Planet)item;
                LabelInfoTitle.Content = "Info: " + p.name;
            }
            else if (item.GetType() == typeof(Ship_DTO))
            {

            }
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            TextLabelInfo.Visibility = Visibility.Hidden;
            ClosingAction?.Invoke();
        }
    }
}