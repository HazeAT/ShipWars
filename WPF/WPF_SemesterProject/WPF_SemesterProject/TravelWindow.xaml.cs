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
    public partial class TravelWindow : Window
    {
        private User user = null;
        private Planet planet = null;
        public Action ClosingActionTravel;
        public TravelWindow(User user, Planet planet)
        {
            InitializeComponent();
            this.user = user;
            this.planet = planet;
            LabelTravelTitle.Content = "Travel - " + planet.name;

            this.loadListView();
        }

        async void loadListView()
        {
            List<Ship_DTO> s_list = await ServerCon.INSTANCE.GetOwnedByIDAsync(user.username);
            foreach (Ship_DTO s in s_list)
            {
                MyShipsListViewTravel.Items.Add(s);
            }
        }


        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            ClosingActionTravel?.Invoke();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            long money = 0;
            if (MyShipsListViewTravel.SelectedItem != null)
            {
                Ship_DTO sdto = (Ship_DTO)MyShipsListViewTravel.SelectedItem;
                Ship s = await ServerCon.INSTANCE.getShipByID(sdto.id);
                if (user.currentJob != null)
                {
                    Job job = user.currentJob;
                    if (GetRandomBasedOnPercentage(job.real_success) && job.planetName.Equals(planet.name))
                    {
                        money += job.salary;
                        MessageBox.Show("Job successfully done!\nEarned Money: " + job.salary);
                    } else if(job.planetName.Equals(planet.name))
                    {
                        money -= job.salary / 2;
                        MessageBox.Show("Job failed!\nLost Money: -" + (job.salary / 2));
                    }
                }
                Random random = new Random();
                long num = (long)Convert.ToDouble(s.cargo_capacity);
                long adjustedCapacity = AdjustCapacity(num, sdto);
                long randomNumber = random.NextInt64(0, adjustedCapacity);

                // Generate a random multiplier between 0.10 and 100, scaled by the random number
                double multiplier = GetRandomMultiplier(random);

                // Calculate the result by multiplying the random number by the random multiplier
                double result = randomNumber * multiplier;
                money += (int)result;
                MessageBox.Show("Goods Found:" + adjustedCapacity + "\nValue: " + (int)result);

                user.credits += money;
                await ServerCon.INSTANCE.UpdateUserAsync(user.username, user);
                await ServerCon.INSTANCE.UpdateShipHealth(user, sdto);
                this.Close();

            }
            else
            {
                MessageBox.Show("Select A Ship First!");
            }
        }
        static bool GetRandomBasedOnPercentage(int percentage)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if (randomNumber <= percentage)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        static double GetRandomMultiplier(Random random)
        {
            double minMultiplier = 0.10;
            double maxMultiplier = 100.0;
            return random.NextDouble() * (maxMultiplier - minMultiplier) + minMultiplier;
        }

        static long AdjustCapacity(long capacity, Ship_DTO s)
        {
            if (capacity > 1000000)
            {
                Random random = new Random();
                double percentage = random.NextDouble(); // generates a value between 0.0 and 1.0
                if (s.value != "0")
                {

                    if (capacity < 1000000000)
                    {
                        return (long)((capacity / 95) * percentage);
                    }
                    else
                    {
                        return (long)((capacity / 50000) * percentage);
                    }
                } else
                {
                    if (capacity < 1000000000)
                    {
                        return (long)((capacity / 195) * percentage);
                    } else
                    {
                        return (long)((capacity / 100000) * percentage);
                    }
                }
            }
            else
            {
                if (capacity > 10000)
                {
                    return capacity / 90; // use the full capacity if it's 1,000,000 or less
                } else
                {
                    return capacity;
                }
            }
        }
    }
}