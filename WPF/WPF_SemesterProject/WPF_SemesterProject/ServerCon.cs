using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class ServerCon
    {
        public static ServerCon INSTANCE;
        public List<Ship> shipList = new List<Ship>();
        public List<Job> jobList = new List<Job>();
        public List<Planet> planetList = new List<Planet>();

        string baseUrl = "http://127.0.0.1:8080";
        public ServerCon()
        {
            INSTANCE = this;
        }
        public async Task<bool> PostUserAsync(string uname, string mail, string pword)
        {
            string apiUrl = baseUrl + "/users";
            try
            {
                User tmpU = await GetUserByNameAsync(uname);
                User tmpE = await GetUserByEmailAsync(mail);

                if (tmpU == null && tmpE == null)
                {
                    var user = new
                    {
                        username = uname,
                        email = mail,
                        password = pword
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<User> GetUserByNameAsync(string username)
        {
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
                    }
                    catch (JsonException ex)
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
        public async Task<User> GetUserByIDAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/{id}";
                string requestUrl = $"{baseUrl}/users/id{endpoint}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        User user = JsonSerializer.Deserialize<User>(content);
                        return user;
                    }
                    catch (JsonException ex)
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

        public async Task<User> GetUserByEmailAsync(string email)
        {

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
                    }
                    catch (JsonException ex)
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

        public async Task<string> GetIdByUsernameAsync(string username)
        {

            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{baseUrl}/users/unametoid/{username}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    catch (JsonException ex)
                    {
                        return "";
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return "";
                }
            }
        }

        public async Task<List<Ship_DTO>> GetOwnedByIDAsync(string username)
        {

            using (HttpClient client = new HttpClient())
            {
                string id = await GetIdByUsernameAsync(username);
                string requestUrl = $"{baseUrl}/users/owned/get/{id}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        List<Ship_DTO> list = JsonSerializer.Deserialize <List<Ship_DTO>>(content);

                        return list;
                    }
                    catch (JsonException ex)
                    {
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

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/password/{email}/{password}";
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
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Password is incorrect");
                            return false;
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine("Error parsing response");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }
            }
        }

        public async Task<Ship> getShipByID(string id)
        {
            
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/{id}";
                string requestUrl = $"{baseUrl}/ships{endpoint}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Ship ship = JsonSerializer.Deserialize<Ship>(content);
                        return ship;
                    }
                    catch (JsonException ex)
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

        public async Task<bool> AddOwnedShipAsync(string username, string ship_id, bool r)
        {
            string apiUrl = baseUrl + "/users/addowned/" + await GetIdByUsernameAsync(username);
            try
            {
                var ship_dto = new
                {
                    id = ship_id,
                    rented = r.ToString()
                };

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ship_dto), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Ship added successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add ship. Status code: {response.StatusCode}");
                    }
                }
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateUserAsync(string username, User user)
        {
            string apiUrl = baseUrl + "/users/" + await GetIdByUsernameAsync(username);
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("User updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to update user. Status code: {response.StatusCode}");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task GetAllShipsAsync()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/ships");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    shipList = JsonSerializer.Deserialize<List<Ship>>(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                foreach(Ship s in shipList)
                {
                    if (s.cost_in_credits != "unknown")
                    {
                        long l = (long)Convert.ToDouble(s.cost_in_credits);
                        s.cost_in_credits = l.ToString("#,##0");
                    }
                }
            }
        }

        public async Task<List<ShipOnSale>> GetAllSaleShipsAsync()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/sales");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ShipOnSale> sosl = JsonSerializer.Deserialize<List<ShipOnSale>>(content);

                    foreach(ShipOnSale s in sosl)
                    {
                        Ship ship = await ServerCon.INSTANCE.getShipByID(s.ship.id);
                        User u = await GetUserByIDAsync(s.userID);
                        s.ship.model = ship.model;
                        s.userID = u.username; 
                        long d = (long)Convert.ToDouble(s.price);
                        s.price = d.ToString("#,##0");
                    }
                    return sosl;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }
        public async Task<string?> GetShipNameByModelAsync(string model)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/nameof/{model}";
                string requestUrl = $"{baseUrl}/ships{endpoint}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    catch (JsonException ex)
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
        public async Task<string> RemoveOwnedShipByIdAsync(string uid, string sid)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/remove/{uid}/{sid}";
                string requestUrl = $"{baseUrl}/users/owned{endpoint}";

                HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    catch (JsonException ex)
                    {
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

        public async Task GetAllJobsAsync()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/jobs");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    jobList = JsonSerializer.Deserialize<List<Job>>(content);

                    foreach(Job j in jobList)
                    {
                        j.job = j.job.Replace("_", " ");
                        j.risk = j.risk.Replace("_", " ");
                        j.success_string = j.min_success.ToString() + "% - " + j.max_success.ToString() + "%";
                    }
                    return;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return;
                }
            }
        }

        public async Task RemoveJobAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{baseUrl}/jobs/{id}";

                HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return;
                    }
                    catch (JsonException ex)
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return;
                }
            }
        } 
        public async Task<Planet?> GetPlanetByIdAsync(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/{id}";
                string requestUrl = $"{baseUrl}/planets{endpoint}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Planet planet = JsonSerializer.Deserialize<Planet>(content);
                        return planet;
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine("Planet does not exist");
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
        public async Task GetAllPlanetsAsync()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/planets");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    planetList = JsonSerializer.Deserialize<List<Planet>>(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }

        public async Task<bool> PostOnlineSell(Ship_DTO s, string uid, string pr)
        {
            string apiUrl = baseUrl + "/sales";
            try
            {
                var add = new
                {
                    ship = s,
                    userID = uid,
                    price = pr,
                    health = s.health
                };

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(add), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Ship Added successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add Ship. Status code: {response.StatusCode}");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task RemovePostOnlineSellAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{baseUrl}/sales/{id}";

                HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return;
                    }
                    catch (JsonException ex)
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return;
                }
            }
        }

        public async Task<string> DeleteJobByID(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"id";
                string requestUrl = $"{baseUrl}/jobs/{endpoint}";

                HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    catch (JsonException ex)
                    {
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
        public async Task<string?> CheckJobAvailability(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"/checkAvailability/{id}";
                string requestUrl = $"{baseUrl}/jobs{endpoint}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    catch (JsonException ex)
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public async Task<bool> UpdateShipCountAsync(Ship ship)
        {
            string apiUrl = baseUrl + "/ships/count";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ship), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatePlanetVisitedAsync(Planet p)
        {
            string apiUrl = baseUrl + "/planets/visited";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserLevelAsync(User p)
        {
            string apiUrl = baseUrl + "/users/update/level";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateShipHealth(User p, Ship_DTO s)
        {
            string apiUrl = baseUrl + "/users/update/ship/health/" + p.username;
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(apiUrl, content);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

    }
}
