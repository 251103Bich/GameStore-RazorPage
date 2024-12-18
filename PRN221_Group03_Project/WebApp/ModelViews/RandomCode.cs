using System.Text;

namespace WebApp.ModelViews
{
    public class RandomCode
    {
        private static readonly Random random = new Random();
        private const string chars = "0123456789";

        public string GenerateRandomString(int length)
        {
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}
