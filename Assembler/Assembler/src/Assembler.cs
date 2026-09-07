using static LanguageSpecificationData;

public class Assembler
{
    private Parser parser = new Parser();
    private Coder coder = new Coder();

    public string Assemble(string inputFile)
    {
        if(string.IsNullOrEmpty(inputFile))
        {
            Console.WriteLine($"Inputfile is null or empty, please provide a valid assembly file name.");
            return string.Empty;
        }

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("File not found: " + inputFile);
            return string.Empty;
        }


        FirstPass(inputFile);
        SecondPass(inputFile, out string? binaryCode);

        return binaryCode;
    }

    /// <summary>
    /// Perform the second pass to parse and translate each instruction
    /// </summary>
    private void SecondPass(string inputFile, out string binaryCode)
    {
        binaryCode = "";

        using (StreamReader reader = new StreamReader(inputFile))
        {
            var curInstruction = "";
            var fields = new Dictionary<string, string>();

            while (HasMoreLines(reader))
            {
                curInstruction = GetCurrentLine(reader);
                if (Advance(curInstruction))
                {
                    continue;
                }

                if(parser.ParseInstructionType(curInstruction) == InstructionType.L_INSTRUCTION)
                {
                    continue;
                }

                binaryCode += AssembleCurrentInstruction(curInstruction);

                if (HasMoreLines(reader))
                {
                    binaryCode += "\r\n";
                }
            }

            // Console.WriteLine(binaryCode);
        }
    }

    /// <summary>
    /// Perform the first pass to populate the symbol table with label symbols (L-instructions)
    /// </summary>
    private void FirstPass(string inputFile)
    {
        using (StreamReader reader = new StreamReader(inputFile))
        {
            var curInstruction = "";
            var lineNumber = 0;

            while (HasMoreLines(reader))
            {
                curInstruction = GetCurrentLine(reader);
                if (Advance(curInstruction))
                {
                    continue;
                }

                if (parser.ParseInstructionType(curInstruction) != InstructionType.L_INSTRUCTION)
                {
                    lineNumber++;
                    continue;
                }

                var symbol = parser.ParseSymbol(InstructionType.L_INSTRUCTION, curInstruction);
                SymbolTable.AddEntry(symbol, lineNumber);
            }
        }
    }

    /// <summary>
    /// Assemble the current instruction into binary code
    /// </summary>
    private string AssembleCurrentInstruction(string currentInstruction)
    {
        var fields = parser.ParseInstruction(currentInstruction);
        var code = coder.CodeInstruction(fields);
        return code;
    }

    /// <summary>
    /// advance to the next line if the current line is empty or a comment
    /// </summary>
    private bool Advance(string currentLine)
    {
        return string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//");
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