internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the VM file to translate.");
            return;
        }

        var inputFile = args[0];
        if (!File.Exists(inputFile))
        {
            Console.WriteLine("File not found: " + inputFile);
            return;
        }

        if (!inputFile.EndsWith(".vm"))
        {
            Console.WriteLine("Input file must be a VM file.");
            return;
        }

        var inputFileName = Path.GetFileNameWithoutExtension(inputFile);
        LanguageSpecificationData.FileName = inputFileName;

        // translate
        var vmTranslator = new VMTranslator();
        var assemblyCode = vmTranslator.Translate(inputFile);

        // output file
        var outputFileDirectory = Path.GetDirectoryName(inputFile) ?? Directory.GetCurrentDirectory();
        var outputFileName = inputFileName;
        var outputFileNameWithExtension = outputFileName + ".asm";
        var outputFilePath = Path.Combine(outputFileDirectory, outputFileNameWithExtension);
        File.WriteAllText(outputFilePath, assemblyCode);
    }
}