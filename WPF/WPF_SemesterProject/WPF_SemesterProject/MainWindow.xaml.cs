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

namespace WPF_SemesterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int windowCount = 0;
        private int windowCountSell = 0;
        private int windowCountTravel = 0;
        private string currentMode = "Market";
        private List<Ship> shipList = new List<Ship>();
        private bool allowReload = false;
        private Job currentJob = null;
        User user = null;
        public MainWindow()
        {
            InitializeComponent();
            ServerCon s = new ServerCon();

            System.Timers.Timer timer = new System.Timers.Timer(2500);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            allowReload = true;
        }

        private void ReloadButtonPressed(object sender, RoutedEventArgs e)
        {
            if(currentMode == "Job" && allowReload)
            {
                this.JobListViewUpdate();
                allowReload = false;
            } else if(currentMode == "OnlineMarket" && allowReload)
            {
                this.OnlineMarketListViewUpdate();
                allowReload = false;
            }
        }
        private void Button_Market_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode != "Market")
            {
                currentMode = "Market";
                MarketListView.Visibility = Visibility.Visible;
                OnlineMarketListView.Visibility = Visibility.Hidden;
                JobListView.Visibility = Visibility.Hidden;
                ReloadButton.Visibility = Visibility.Hidden;
                ExploreListView.Visibility = Visibility.Hidden;
                TravelPlanetButton.Visibility = Visibility.Hidden;
                MyShipsListView.Visibility = Visibility.Hidden;
                activateMarketButtons(true);
                BuyOnlineButton.Visibility = Visibility.Hidden;
            }
        }
        private void activateMarketButtons(bool status)
        {
            if (status)
            {
                BuyButton.Visibility = Visibility.Visible;
                RentButton.Visibility = Visibility.Visible;
                AcceptJobButton.Visibility = Visibility.Hidden;
                TravelPlanetButton.Visibility = Visibility.Hidden;
                SellToMarketButton.Visibility = Visibility.Hidden;
                SellToOnlineMarketButton.Visibility = Visibility.Hidden;
            } else
            {
                BuyButton.Visibility = Visibility.Hidden;
                RentButton.Visibility = Visibility.Hidden;
                SellToMarketButton.Visibility = Visibility.Hidden;
                SellToOnlineMarketButton.Visibility = Visibility.Hidden;
            }
        }

        private async void Button_Online_Market_Click(object sender, RoutedEventArgs e)
        {
            if(currentMode != "OnlineMarket")
            {
                currentMode = "OnlineMarket";
                OnlineMarketListView.Visibility = Visibility.Visible;
                MarketListView.Visibility = Visibility.Hidden;
                JobListView.Visibility = Visibility.Hidden;
                ExploreListView.Visibility = Visibility.Hidden;
                ReloadButton.Visibility = Visibility.Visible;
                MyShipsListView.Visibility = Visibility.Hidden;
                activateMarketButtons(true);
                BuyButton.Visibility= Visibility.Hidden;
                RentButton.Visibility= Visibility.Hidden;
                BuyOnlineButton.Visibility= Visibility.Visible;

                OnlineMarketListViewUpdate();
            }
        }
        private async void OnlineMarketListViewUpdate()
        {
            List<ShipOnSale> sosl = await ServerCon.INSTANCE.GetAllSaleShipsAsync();
            OnlineMarketListView.Items.Clear();
            foreach (var ship in sosl)
            {
                OnlineMarketListView.Items.Add(ship);
            }
        } 

        private async void Button_Jobs_Click(object sender, RoutedEventArgs e)
        {
            if(currentMode != "Job")
            {
                currentMode = "Job";
                activateMarketButtons(false);
                JobListView.Visibility = Visibility.Visible;
                ReloadButton.Visibility = Visibility.Visible;
                MarketListView.Visibility= Visibility.Hidden;
                OnlineMarketListView.Visibility= Visibility.Hidden;
                ExploreListView.Visibility = Visibility.Hidden;
                AcceptJobButton.Visibility = Visibility.Visible;
                TravelPlanetButton.Visibility = Visibility.Hidden;
                MyShipsListView.Visibility = Visibility.Hidden;

                JobListViewUpdate();
            }
        }

        private async void JobListViewUpdate()
        {
            await ServerCon.INSTANCE.GetAllJobsAsync();
            JobListView.Items.Clear();
            foreach (Job j in ServerCon.INSTANCE.jobList)
            {
                if (j.legalStatus.Equals("Legal"))
                {
                    j.legalStatus = "YES";
                }
                else if (j.legalStatus.Equals("Illegal"))
                {
                    j.legalStatus = "NO";
                }
                Planet p = await ServerCon.INSTANCE.GetPlanetByIdAsync(j.planetId);
                j.planetName = p.name;
                JobListView.Items.Add(j);
            }
        }
        private void Button_Explore_Click(object sender, RoutedEventArgs e)
        {
            if(currentMode != "Explore")
            {
                currentMode = "Explore";
                activateMarketButtons(false);
                ExploreListView.Visibility = Visibility.Visible;
                JobListView.Visibility= Visibility.Hidden;
                ReloadButton.Visibility = Visibility.Hidden;
                MarketListView.Visibility=Visibility.Hidden;
                OnlineMarketListView.Visibility = Visibility.Hidden;
                AcceptJobButton.Visibility = Visibility.Hidden;
                MyShipsListView.Visibility= Visibility.Hidden;
                TravelPlanetButton.Visibility = Visibility.Visible;
            }
        }

        private async void Button_My_Ships_Click(object sender, RoutedEventArgs e)
        {
            if(currentMode != "MyShips")
            {
                currentMode = "MyShips";
                activateMarketButtons(false);
                MyShipsListView.Visibility = Visibility.Visible;
                ExploreListView.Visibility= Visibility.Hidden;
                JobListView.Visibility= Visibility.Hidden;
                MarketListView.Visibility = Visibility.Hidden;
                OnlineMarketListView.Visibility = Visibility.Hidden;
                AcceptJobButton.Visibility = Visibility.Hidden;
                TravelPlanetButton.Visibility = Visibility.Hidden;
                SellToMarketButton.Visibility = Visibility.Visible;
                SellToOnlineMarketButton.Visibility = Visibility.Visible;

                LoadMyShipList();
            }
        }

        async private void LoadMyShipList()
        {
            List<Ship_DTO> list = await ServerCon.INSTANCE.GetOwnedByIDAsync(user.username);
            MyShipsListView.Items.Clear();
            if (list.Count > 0)
            {
                MyShipsListView.Items.Clear();
                foreach (Ship_DTO item in list)
                {
                    MyShipsListView.Items.Add(item);
                }
            }
        }

        async private void LoginClick(object sender, RoutedEventArgs e)
        {
            user = await ServerCon.INSTANCE.GetUserByEmailAsync(EmailLogin.Text);
            bool passwordOK = false;
            if (user != null)
            {
                labelCredits.Content = "Credits: " + user.credits.ToString("#,##0");
                passwordOK = await ServerCon.INSTANCE.CheckPasswordAsync(EmailLogin.Text, PasswordLogin.Password.ToString());
                if(passwordOK)
                {
                    Application.Current.MainWindow.Height = 780;
                    Application.Current.MainWindow.Width = 950;
                    Application.Current.MainWindow.Title = "ShipWars";
                    MainGame.Visibility = Visibility.Visible;
                    LoginGrid.Visibility = Visibility.Hidden;
                    ButtonUser.Content = user.username;
                    ButtonUserRole.Content = user.role;

                    await ServerCon.INSTANCE.GetAllShipsAsync();
                    foreach (var ships in ServerCon.INSTANCE.shipList)
                    {
                        MarketListView.Items.Add(ships);
                    }
                    await ServerCon.INSTANCE.GetAllPlanetsAsync();
                    foreach (Planet p in ServerCon.INSTANCE.planetList)
                    {
                        ExploreListView.Items.Add(p);
                    }
                }
            }
        }

        private void RegisterHereClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Height = 330;
            Application.Current.MainWindow.Width = 400;
            Application.Current.MainWindow.Title = "Register";
            RegisterGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Hidden;
        }

        private async void RegisterClick(object sender, RoutedEventArgs e)
        {
            if(PasswordBoxRegister.Password.Length < 8) {
                ErrorTextRegister.Visibility = Visibility.Visible;
                ErrorTextRegister.Content = "Password needs to be longer then 8!";
                return;
            }
            if (UserNameTextBoxRegister.Text.Length > 0 && UserNameTextBoxRegister.Text.Length < 8)
            {
                bool userposted = await ServerCon.INSTANCE.PostUserAsync(UserNameTextBoxRegister.Text, EmailTextBoxRegister.Text, PasswordBoxRegister.Password.ToString());

                if (userposted)
                {
                    ErrorTextRegister.Visibility = Visibility.Hidden;
                    MessageBox.Show("User Created Successfully!", "Message Box Title", MessageBoxButton.OK, MessageBoxImage.Information);
                    goToLogin();
                }
                else
                {
                    ErrorTextRegister.Visibility = Visibility.Visible;
                    ErrorTextRegister.Content = "Username or Email Already Exists!";
                }
            } else
            {
                ErrorTextRegister.Visibility = Visibility.Visible;
                ErrorTextRegister.Content = "Username needs to be longer then 0 and shorter then 8!";
            }
        }
        private void LoginHereClick(object sender, RoutedEventArgs e)
        {
            goToLogin();
        }

        private void goToLogin()
        {
            Application.Current.MainWindow.Height = 330;
            Application.Current.MainWindow.Width = 430;
            Application.Current.MainWindow.Title = "Login";
            RegisterGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            DetailViewWindow detailViewWindow = null;
            if (currentMode.Equals("Market"))
            {
                if (MarketListView.SelectedItem != null)
                {
                    Ship selected = MarketListView.SelectedItem as Ship;
                    detailViewWindow = new DetailViewWindow("Market", selected);
                }
            } else if(currentMode.Equals("OnlineMarket"))
            {
                if (OnlineMarketListView.SelectedItem != null)
                {
                    ShipOnSale selected = OnlineMarketListView.SelectedItem as ShipOnSale;
                    detailViewWindow = new DetailViewWindow("OnlineMarket", selected);
                }
            }
            else if (currentMode.Equals("Job"))
            {
                if (JobListView.SelectedItem != null)
                {
                    Job selected = JobListView.SelectedItem as Job;
                    detailViewWindow = new DetailViewWindow("Job", selected);
                }
            }
            else if (currentMode.Equals("Explore"))
            {
                if (ExploreListView.SelectedItem != null)
                {
                    Planet selected = ExploreListView.SelectedItem as Planet;
                    detailViewWindow = new DetailViewWindow("Explore", selected);
                }
            }
            else
            {
                if (MyShipsListView.SelectedItem != null)
                {
                    Ship_DTO selected = MyShipsListView.SelectedItem as Ship_DTO;
                    detailViewWindow = new DetailViewWindow("MyShips", selected);
                }
            }

            if (detailViewWindow != null && windowCount <3)
            {
                detailViewWindow.ClosingAction = () => {
                    windowCount--;
                };
                detailViewWindow.Show();
                windowCount++;
            }
        }

        private async void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode == "Market") {
                Ship ship =  (Ship)MarketListView.SelectedItem;
                if (user != null && ship != null)
                {
                    if (user.credits >= (long)Convert.ToDouble(ship.cost_in_credits))
                    {
                        bool status = await ServerCon.INSTANCE.AddOwnedShipAsync(user.username, ship.id, false);
                        if (status)
                        {
                            await ServerCon.INSTANCE.UpdateShipCountAsync(await ServerCon.INSTANCE.getShipByID(ship.id));
                            user.credits = user.credits - (long)Convert.ToDouble(ship.cost_in_credits);
                            labelCredits.Content = "Credits: " + user.credits.ToString("#,##0");
                            bool moneystatus = await ServerCon.INSTANCE.UpdateUserAsync(user.username, user);
                            await ServerCon.INSTANCE.UpdateUserLevelAsync(user);
                            user = await ServerCon.INSTANCE.GetUserByNameAsync(user.username);
                            ButtonUserRole.Content = user.role;
                            MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private async void RentButton_Click(object sender, RoutedEventArgs e)
        {
            Ship ship = (Ship)MarketListView.SelectedItem;
            long price = (long)Convert.ToDouble(ship.cost_in_credits) / 150;
            ship.cost_in_credits = price.ToString();


            if (user.credits >= (long)Convert.ToDouble(ship.cost_in_credits))
            {
                MessageBoxResult result = MessageBox.Show("One time rent cost: " + price,"", MessageBoxButton.OK);
                if(result == MessageBoxResult.OK)
                {
                    bool status = await ServerCon.INSTANCE.AddOwnedShipAsync(user.username, ship.id, true);
                    if (status) {
                        user.credits = user.credits - price;
                        await ServerCon.INSTANCE.UpdateUserAsync(user.username, user);
                    }
                }
            }
        }
        private async void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode == "MyShips")
            {
                Ship_DTO ship = (Ship_DTO)MyShipsListView.SelectedItem;

                if(ship.value == "0")
                {
                    MessageBox.Show("Hey there! Cant sell an Rented ship!.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (user != null && ship != null)
                {
                    string id = await ServerCon.INSTANCE.GetIdByUsernameAsync(user.username);
                    string status = await ServerCon.INSTANCE.RemoveOwnedShipByIdAsync(id,ship.id);
                    if (status == "true")
                    {
                        user.credits = user.credits + (long)Convert.ToDouble(ship.value);
                        labelCredits.Content = "Credits: " + user.credits.ToString("#,##0");
                        bool moneystatus = await ServerCon.INSTANCE.UpdateUserAsync(user.username, user);
                        MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadMyShipList();
                    }
                }
            }
        }
        private void SellOnlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyShipsListView.SelectedItem != null) {
                SellOnlineWindow sellOnlineWindow = new SellOnlineWindow(MyShipsListView.SelectedItem, user.username);
                if (sellOnlineWindow != null && windowCountSell < 1)
                {
                    sellOnlineWindow.ClosingActionSell = () => {
                        windowCountSell--;
                        LoadMyShipList();
                    };
                    sellOnlineWindow.Show();
                    windowCountSell++;
                }
            }
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = JobListView.SelectedItem as Job;
            string available = await ServerCon.INSTANCE.CheckJobAvailability(job.id);        
            
            if(available == "true") {
                user.currentJob = job;
                await ServerCon.INSTANCE.RemoveJobAsync(job.id);
                MessageBox.Show("You Accepted the Job! Now go to: " + job.planetName, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Job was taken already.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private async void BuyOnlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode == "OnlineMarket")
            {
                ShipOnSale s = (ShipOnSale)OnlineMarketListView.SelectedItem;
                Ship ship = await ServerCon.INSTANCE.getShipByID(s.ship.id);
                if (user != null && ship != null)
                {
                    if (user.credits >= (long)Convert.ToDouble(s.price))
                    {
                        bool status = await ServerCon.INSTANCE.AddOwnedShipAsync(user.username, ship.id, false);
                        if (status)
                        {
                            User selluser = await ServerCon.INSTANCE.GetUserByNameAsync(s.userID);
                            selluser.credits += (long)Convert.ToDouble(s.price);
                            await ServerCon.INSTANCE.UpdateUserAsync(selluser.username, selluser);
                            await ServerCon.INSTANCE.RemovePostOnlineSellAsync(s.id);

                            User u = await ServerCon.INSTANCE.GetUserByNameAsync(user.username);
                            u.credits = u.credits - (long)Convert.ToDouble(s.price);

                            labelCredits.Content = "Credits: " + user.credits.ToString("#,##0");
                            bool moneystatus = await ServerCon.INSTANCE.UpdateUserAsync(user.username, user);
                            MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private async void TravelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(user);
            TravelWindow travelWindow = new TravelWindow(user, ExploreListView.SelectedItem as Planet);
            if (travelWindow != null && windowCountTravel < 1)
            {
                travelWindow.ClosingActionTravel = async () => {
                    windowCountSell--;
                    currentJob = null;
                    labelCredits.Content = "Credits: " + user.credits.ToString("#,##0");
                    await ServerCon.INSTANCE.UpdatePlanetVisitedAsync(ExploreListView.SelectedItem as Planet);
                };
                travelWindow.Show();
                windowCountSell++;
            }
        }
    }
}