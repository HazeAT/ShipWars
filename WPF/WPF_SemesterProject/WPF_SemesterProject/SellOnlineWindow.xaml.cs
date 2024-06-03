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
    public partial class SellOnlineWindow : Window
    {
        public Action ClosingActionSell { get; set; }
        private Ship_DTO ship;
        private string username;
        public SellOnlineWindow(object item, string username)
        {
            InitializeComponent();
            ship = (Ship_DTO)item;
           this.username = username;
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Title = "Sell " + ship.model;
            modelNameLabel.Content = "Sell " + ship.model;
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            ClosingActionSell?.Invoke();
        }

        private async void SellButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long price = (long)Convert.ToDouble(TextBoxSell.Text);
                string id = await ServerCon.INSTANCE.GetIdByUsernameAsync(username);
                if (id != null)
                {
                    string response = await ServerCon.INSTANCE.RemoveOwnedShipByIdAsync(id, ship.id);
                    if(response == "true")
                    {
                        bool res = await ServerCon.INSTANCE.PostOnlineSell(ship, id, price.ToString());
                        if (res)
                        {
                            MessageBox.Show("Ship Posted Sucessfully");

                            Close();
                        }
                    }   
                }

            } catch(Exception ex)
            {
                TextBoxSell.Text = "wrong format!";
            }
        }
    }
}