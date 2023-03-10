using HanoiTower.GameElements;

namespace HanoiTower.Services
{
    public static class LogService
    {
        public static void ToFileShort(List<string> content, string role, int noDisks, int steps)
        {
            string logDirectory = Path.Combine(Environment.CurrentDirectory, "Logs");
            string logFilePath = Path.Combine(logDirectory, $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}_MovesLog_{role}.txt");

            // Create the Logs directory if it doesn't already exist
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            int noSteps = 1;
            // Write the content to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath))
            {
                foreach (string s in DesignCharConstants.Header)
                {
                    writer.WriteLine(s);
                }
                writer.WriteLine();
                writer.WriteLine(DateTime.Now);
                writer.WriteLine($"#{steps}   No. of disks: {noDisks}   Played by: {role}");
                writer.WriteLine(DesignCharConstants.LineBreak);
                writer.WriteLine();
                foreach (string line in content)
                {
                    if (line.Contains("->"))
                    {
                        writer.WriteLine($"{noSteps}) {line}");
                        noSteps++;
                    }
                    else
                    {
                        writer.WriteLine($"{line}");
                    }
                    
                }
            }
        }
    }
}
