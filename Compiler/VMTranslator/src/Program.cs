using System.Text;

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

        if (File.Exists(inputFile))
        {
            var inputFileName = Path.GetFileNameWithoutExtension(inputFile);
            LanguageSpecificationData.FileName = inputFileName;

            var assemblyCode = TranslateSingleFile(inputFile);

            var outputFileDirectory = Path.GetDirectoryName(inputFile) ?? Directory.GetCurrentDirectory();
            var outputFile = Path.Combine(outputFileDirectory, inputFileName + ".asm");
            GenerateOuputFile(outputFile, assemblyCode);
            return;
        }

        if (Directory.Exists(inputFile))
        {
            var assemblyCode = TranslateDirectory(inputFile);


            var directory = new DirectoryInfo(inputFile);
            var outputFileName = directory.Name;
            var outputFileDirectory = directory.FullName ?? Directory.GetCurrentDirectory();
            var outputFile = Path.Combine(outputFileDirectory, outputFileName + ".asm");
            GenerateOuputFile(outputFile, assemblyCode);
            return;
        }

        Console.WriteLine("Input file or directory not found.");
        return;
    }

    /// <summary>
    /// 翻译目录下的所有VM文件为Assembly Code
    /// </summary>
    private static string TranslateDirectory(string directory)
    {
        var vmFiles = Directory.GetFiles(directory, "*.vm", SearchOption.AllDirectories);

        if (vmFiles.Length == 0)
        {
            Console.WriteLine($"No .vm files found in the directory: {directory}");
            return string.Empty;
        }

        var outputBuffer = new StringBuilder();

        // add bootstrap code
        outputBuffer.AppendLine($"/// Bootstrap");
        LanguageSpecificationData.Reset();
        var bootstrapWriter = new CodeWriter();
        var bootstrapCode = bootstrapWriter.GenerateBootstrapCode();
        outputBuffer.AppendLine(bootstrapCode);
        outputBuffer.AppendLine();

        var sortedFiles = vmFiles
                .OrderBy(f => Path.GetFileName(f) == "Sys.vm" ? 0 :
                            Path.GetFileName(f) == "Main.vm" ? 1 : 2)
                .ToList();

        foreach (var file in sortedFiles)
        {
            var fileName = Path.GetFileName(file);
            outputBuffer.AppendLine($"/// {fileName}");
            var assemblyCode = TranslateSingleFile(file);
            outputBuffer.AppendLine(assemblyCode);
            outputBuffer.AppendLine();
        }

        return outputBuffer.ToString();
    }

    /// <summary>
    /// 翻译单个VM文件为Assembly Code
    /// </summary>
    private static string TranslateSingleFile(string inputFile)
    {
        if (!inputFile.EndsWith(".vm"))
        {
            Console.WriteLine("Input file must be a VM file.");
            return string.Empty;
        }

        var inputFileName = Path.GetFileNameWithoutExtension(inputFile);
        LanguageSpecificationData.FileName = inputFileName;

        // translate
        var vmTranslator = new VMTranslator();
        var assemblyCode = vmTranslator.Translate(inputFile);

        Console.WriteLine($"Translate VM File {Path.GetFileName(inputFile)} completed. File has been placed in the same directory as the input file.");

        return assemblyCode;
    }

    /// <summary>
    /// 生成输出文件
    /// </summary>
    private static void GenerateOuputFile(string outputFile, string assemblyCode)
    {
        File.WriteAllText(outputFile, assemblyCode);
        Console.WriteLine($"All Translations completed. {Path.GetFileName(outputFile)} has been created. File has been placed in the same directory as the input file.");
    }
}