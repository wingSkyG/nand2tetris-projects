internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the VM file to translate.");
            return;
        }

        var inFile = args[0];
        var vmTranslator = new VMTranslator();
        if (File.Exists(inFile))
        {
            var inputFileName = Path.GetFileNameWithoutExtension(inFile);

            vmTranslator.TranslateFile(inFile, (assemblyCode) =>
            {
                Console.WriteLine($"Translation completed. {Path.GetFileName(inFile)} has been created. File has been placed in the same directory as the input file.");
                AssemFileGenerator.GenerateOuputFile(inFile, assemblyCode, TranslateType.File);
            });
            return;
        }

        if (Directory.Exists(inFile))
        {
            vmTranslator.TranslateDirectory(inFile, (assemblyCode) =>
            {
                AssemFileGenerator.GenerateOuputFile(inFile, assemblyCode, TranslateType.Directory);
            });
            return;
        }

        Console.WriteLine("Input file or directory not found.");
        return;
    }
}