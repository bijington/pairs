namespace Pairs.Models
{
    public class Speaker
    {
        public string Name { get; }

        public string ProfileImageUrl { get; }

        public Speaker(string name, string profileImageUrl)
        {
            Name = name;
            ProfileImageUrl = profileImageUrl;
        }
    }
}
