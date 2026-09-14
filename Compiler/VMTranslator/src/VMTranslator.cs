
class VMTranslator
{
    private Parser parser = new();
    private CodeWriter codeWriter = new();

    /// <summary>
    /// 翻译VM文件为Assembly Code
    /// </summary>
    public string Translate(string inputFile)
    {
        var assemblyCode = "";

        using (StreamReader reader = new StreamReader(inputFile))
        {
            while (HasMoreLines(reader))
            {
                var curLine = GetCurrentLine(reader);
                var vmCommand = parser.ParseVMCommand(curLine);
                if (vmCommand == null)
                {
                    continue;
                }
                assemblyCode += codeWriter.TranslateVMCommand(vmCommand) + "\n";
            }
        }

        assemblyCode = assemblyCode.TrimEnd('\n');
        return assemblyCode;
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