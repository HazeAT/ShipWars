
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;



class Program
{
    static async Task Main()
    {
        //await PostUserAsync("WernerBerner","tst@gmail.com","123");
        //await GetAllUsersAsync();
        //await GetUserByNameAsync("HazeAT");
        //await GetUserByEmailAsync("haze.at@example.com");
        //await CheckPasswordAsync("65ead91a8546b33aa0c4d428", "securePassword123");
        //await CheckPasswordAsync("65ead91a8546b33aa0c4d428", "1");
        //await AddFriendAsync("65ead91a8546b33aa0c4d428", "65ead9238546b33aa0c4d429");
        await GetAllShipsAsync();
    }

    static async Task PostUserAsync(string uname, string mail, string pword)
    {
        try
        {
            string apiUrl = "http://10.10.3.152:8080/users"; // Replace with your server's URL
            User tmpU = await GetUserByNameAsync(uname);
            User tmpE = await GetUserByEmailAsync(mail);

            if(tmpU == null && tmpE==null) {
                var user = new
                {
                    username = uname,
                    email = mail,
                    password = pword
                    // Add other properties as needed
                };

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("User created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create user. Status code: {response.StatusCode}");
                    }
                }
            } else
            {
                Console.WriteLine("User Already Exists");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task GetAllUsersAsync()
    {
        string baseUrl = "http://10.10.3.152:8080"; // Replace with your actual server address

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/users");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<User> users = JsonSerializer.Deserialize<List<User>>(content);
                // Now 'users' contains the list of users from the server
                foreach (var user in users)
                {
                    Console.WriteLine($"Role: {user.role},Username: {user.username}, Email: {user.email}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
    static async Task<User> GetUserByNameAsync(string username)
    {
        string baseUrl = "http://10.10.3.152:8080";  // Replace with your actual base URL

        using (HttpClient client = new HttpClient())
        {
            string endpoint = $"/{username}";
            string requestUrl = $"{baseUrl}/users/name{endpoint}";

            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    User user = JsonSerializer.Deserialize<User>(content);
                    return user;
                } catch(JsonException ex)
                {
                    Console.WriteLine("Username does not exist");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }
    }

    static async Task<User> GetUserByEmailAsync(string email)
    {
        string baseUrl = "http://10.10.3.152:8080";  // Replace with your actual base URL

        using (HttpClient client = new HttpClient())
        {
            string endpoint = $"/{email}";
            string requestUrl = $"{baseUrl}/users/email{endpoint}";

            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    User user = JsonSerializer.Deserialize<User>(content);
                    return user;
                } catch( JsonException ex)
                {
                    Console.WriteLine("Email does not exist");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }
    }

    static async Task CheckPasswordAsync(string id, string password)
    {
        string baseUrl = "http://10.10.3.152:8080";  // Replace with your actual base URL

        using (HttpClient client = new HttpClient())
        {
            string endpoint = $"/password/{id}/{password}";
            string requestUrl = $"{baseUrl}/users{endpoint}";

            // Add password as a query parameter

            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    bool passwordCorrect = JsonSerializer.Deserialize<bool>(content);

                    if (passwordCorrect)
                    {
                        Console.WriteLine("Password is correct");
                    }
                    else
                    {
                        Console.WriteLine("Password is incorrect");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error parsing response");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    static async Task AddFriendAsync(string id, string friendId)
    {
        string baseUrl = "http://10.10.3.152:8080";  // Replace with your actual base URL

        using (HttpClient client = new HttpClient())
        {
            string endpoint = $"/friend/{id}";
            string requestUrl = $"{baseUrl}/users{endpoint}";

            // Create a UserID_DTO object
            UserID_DTO uid = new UserID_DTO { Id = friendId };

            // Serialize the object to JSON
            string jsonBody = JsonSerializer.Serialize(uid);

            // Create a StringContent object with the JSON payload
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Send the PUT request with the serialized JSON payload in the request body
            HttpResponseMessage response = await client.PutAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);  // You might want to do something with the result
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error parsing response");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    static async Task GetAllShipsAsync()
    {
        string baseUrl = "http://10.10.3.152:8080";

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/ships");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Ship> ships = JsonSerializer.Deserialize<List<Ship>>(content);
                // Now 'users' contains the list of users from the server
                foreach (var ship in ships)
                {
                    Console.WriteLine($"Model: {ship.cost_in_credits}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
    public class Ship
    {
        public string model { get; set; }
        public string starshipClass { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
        public string length { get; set; }
        public string crew { get; set; }
        public string passengers { get; set; }
        public string maxAtmospheringSpeed { get; set; }
        public string hyperdriveRating { get; set; }
        public string MGLT { get; set; }
        public string cargoCapacity { get; set; }
        public string consumables { get; set; }
        public string name { get; set; }
    }


    public class User
    {
        //public string Id { get; set; }
        public string username { get; set; }
        public int level { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        //public string password { get; set; }
        public int credits { get; set; }
        public List<UserID_DTO> friends { get; set; }
        public List<ShipID_DTO> ownedships { get; set; }
        public List<ShipID_DTO> rentedships { get; set; }
    }

    public class UserID_DTO
    {
        public string Id { get; set; }
    }

    public class ShipID_DTO
    {
        public string Id { get; set; }
        public int Amount { get; set; }
    }
}

