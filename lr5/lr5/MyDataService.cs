using Newtonsoft.Json;

namespace lr5
{
    public class MyDataService
    {
        private List<User> userList;
        private readonly string filePath = "Users.json";

        public MyDataService()
        {
            LoadData();
        }

        public List<User> GetUserList()
        {
            return userList;
        }

        public User GetUserById(string userId)
        {
            return userList.Find(u => u.id == userId);
        }

        public void AddUser(User newUser)
        {
            userList.Add(newUser);
            SaveData();
        }

        public void UpdateUser(string userId, User updatedUser)
        {
            var existingUser = userList.Find(u => u.id == userId);
            if (existingUser != null)
            {
                existingUser.name = updatedUser.name;
                existingUser.email = updatedUser.email;
                existingUser.phone = updatedUser.phone;
                SaveData();
            }
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                userList = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                userList = new List<User>();
            }
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(userList, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        ~MyDataService()
        {
            SaveData();
        }
    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
