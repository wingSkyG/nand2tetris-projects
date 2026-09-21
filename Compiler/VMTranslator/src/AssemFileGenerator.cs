public enum TranslateType
{
    File,
    Directory
}

public static class AssemFileGenerator
{
    /// <summary>
    /// 生成输出文件
    /// </summary>
    public static void GenerateOuputFile(string inFile, string assemblyCode, TranslateType translateType)
    {

        switch (translateType)
        {
            case TranslateType.File:
                var outputFileDirectory = Path.GetDirectoryName(inFile) ?? Directory.GetCurrentDirectory();
                var outputFile = Path.Combine(outputFileDirectory, Path.GetFileNameWithoutExtension(inFile) + ".asm");
                File.WriteAllText(outputFile, assemblyCode);
                Console.WriteLine($"{Path.GetFileName(outputFile)} has been created. File has been placed in the same directory as the input file.");
                break;
            case TranslateType.Directory:
                var dirInfo = new DirectoryInfo(inFile);
                var outDir = dirInfo.FullName ?? Directory.GetCurrentDirectory();
                var outFile = Path.Combine(outDir, dirInfo.Name + ".asm");
                File.WriteAllText(outFile, assemblyCode);
                Console.WriteLine($"{Path.GetFileName(outFile)} has been created in the same directory.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(translateType), translateType, null);
        }
    }
}