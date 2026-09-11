
class VMTranslator
{
    private Parser parser = new();
    private CodeWriter codeWriter = new();

    public string Translate(string inputFile)
    {
        using (StreamReader reader = new StreamReader(inputFile))
        {
            var curVMCommand = "";
            var lineNumber = 0;

            while (HasMoreLines(reader))
            {
                var curLine = GetCurrentLine(reader);

                if (parser.ParseInstructionType(curVMCommand) != InstructionType.L_INSTRUCTION)
                {
                    lineNumber++;
                    continue;
                }

                var symbol = parser.ParseSymbol(InstructionType.L_INSTRUCTION, curVMCommand);
                SymbolTable.AddEntry(symbol, lineNumber);
            }
        }

        return "";
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