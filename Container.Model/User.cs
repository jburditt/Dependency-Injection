namespace Inject.Model
{
    public class User : IUser
    {
        public string Name { get; set; }

        public User()
        {
            Name = "Test";
        }
    }

    public interface IUser
    {
        string Name { get; set; }
    }
}
