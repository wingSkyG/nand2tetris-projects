internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the assembly file to assemble.");
            return;
        }

        var inputFile = args[0];
        if (!File.Exists(inputFile))
        {
            Console.WriteLine("File not found: " + inputFile);
            return;
        }

        // assemble
        var assembler = new Assembler();
        var binaryCode = assembler.Assemble(inputFile);

        // output file
        var outputFileDirectory = Path.GetDirectoryName(inputFile) ?? Directory.GetCurrentDirectory();
        var outputFileName = Path.GetFileNameWithoutExtension(inputFile);
        var outputFileNameWithExtension = outputFileName + ".hack";
        var outputFilePath = Path.Combine(outputFileDirectory, outputFileNameWithExtension);
        File.WriteAllText(outputFilePath, binaryCode);
    }
}