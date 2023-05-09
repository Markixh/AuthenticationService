namespace AuthenticationService
{
    public class Logger : ILogger
    {
        private readonly string errorsFile;
        private readonly string eventsFile;

        public Logger()
        {
            var curentDirectory = Directory.CreateDirectory(Directory.GetCurrentDirectory());
            var nameLogDirectory = Guid.NewGuid().ToString();
            var logDirectory = string.Concat(curentDirectory, "/Logs/", nameLogDirectory);
            Directory.CreateDirectory(logDirectory);
            errorsFile = string.Concat(logDirectory, "/errors.txt");
            eventsFile = string.Concat(logDirectory, "/events.txt");
        }

        public void WriteEvent(string eventMessage)
        {
            Console.WriteLine(eventMessage);
            WriteFile(eventsFile, eventMessage);
        }

        public void WriteError(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            WriteFile(errorsFile, errorMessage);
        }

        private static void WriteFile(string path, string message)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
