class VMTranslator
{
    private Parser parser = new();
    private CodeWriter codeWriter = new();

    /// <summary>
    /// 翻译单个VM文件为Assembly Code
    /// </summary>
    public void TranslateFile(string inFile, Action<string>? onTranslateComplete = null)
    {
        if (!inFile.EndsWith(".vm"))
        {
            Console.WriteLine("Input file must be a VM file.");
            return;
        }

        var fileName = Path.GetFileNameWithoutExtension(inFile);
        codeWriter.SetFileName(fileName);

        using (StreamReader reader = new(inFile))
        {
            while (HasMoreLines(reader))
            {
                var curLine = GetCurrentLine(reader);
                var vmCommand = parser.ParseVMCommand(curLine);
                if (vmCommand == null)
                {
                    continue;
                }
                codeWriter.TranslateVMCommand(vmCommand);
            }
        }

        System.Console.WriteLine($"Translation of {Path.GetFileName(inFile)} completed.");
        onTranslateComplete?.Invoke(codeWriter.GetAssemblyCode());
    }

    /// <summary>
    /// 翻译目录下的所有VM文件为Assembly Code
    /// </summary>
    public void TranslateDirectory(string directory, Action<string> onTranslateComplete)
    {
        var vmFiles = Directory.GetFiles(directory, "*.vm", SearchOption.AllDirectories);

        if (vmFiles.Length == 0)
        {
            Console.WriteLine($"No .vm files found in the directory: {directory}");
            return;
        }

        var sortedFiles = vmFiles
                .OrderBy(f => Path.GetFileName(f) == "Sys.vm" ? 0 :
                            Path.GetFileName(f) == "Main.vm" ? 1 : 2)
                .ToList();

        foreach (var file in sortedFiles)
        {
            System.Console.WriteLine($"Translating {Path.GetFileName(file)}...");
            TranslateFile(file);
        }

        onTranslateComplete?.Invoke(codeWriter.GetAssemblyCode());
    }

    /// <summary>
    /// 获取当前行
    /// </summary>
    private string GetCurrentLine(StreamReader reader)
    {
        return reader.ReadLine()?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// 是否有更多的行
    /// </summary>
    private bool HasMoreLines(StreamReader reader)
    {
        return reader.Peek() >= 0;
    }
}