namespace ChatApp.Api
{
    public class MessageService
    {
        public Dictionary<string, List<Tuple<string, string>>> Messages =
          new Dictionary<string, List<Tuple<string, string>>>()
            {
                {"Dotnet", new List<Tuple<string,string>>() },
                {"Php", new List<Tuple<string,string>>() },
                {"Java", new List<Tuple<string,string>>() }
            };
    }
}
