internal class Program
{
    private static void Main(string[] args)
    {
        // read file
        string asmFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Add.asm");
        if (!File.Exists(asmFile))
        {
            Console.WriteLine("File not found: " + asmFile);
            return;
        }
        string content = File.ReadAllText(asmFile);

        if(string.IsNullOrEmpty(content))
        {
            Console.WriteLine("File has 0 lines: " + asmFile);
            return;
        }

        // split into lines
        string[] lines = content.Split(new[] { '\r', '\n' });

        // process each line
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();

            // advance to the next line if the current line is empty or a comment
            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("//"))
            {
                continue;
            }

            // process the line (for demonstration, just print it)
            Console.WriteLine(trimmedLine);
        }
    }
}