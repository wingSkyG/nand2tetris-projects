using static LanguageSpecificationData;

class Parser
{
    private int varStartAddress = 16; // Starting address for variable symbols

    public void ResetVarStartAddress()
    {
        varStartAddress = 0;
    }

    /// <summary>
    /// 将instruction解析fields
    /// </summary>
    public Dictionary<string, string> ParseInstruction(string instruction)
    {
        var fields = new Dictionary<string, string>();

        var instructionType = ParseInstructionType(instruction);
        fields.Add("instructionType", instructionType.ToString());

        switch (instructionType)
        {
            case InstructionType.A_INSTRUCTION:
                var reminType = ParseAInstruction(instruction, out var value);
                fields.Add(reminType, value);
                break;
            case InstructionType.C_INSTRUCTION:
                var innerFields = ParseCInstruction(instruction);
                foreach (var kvp in innerFields)
                {
                    fields.Add(kvp.Key, kvp.Value);
                }
                break;
            default:
                Console.WriteLine($"Parse error: Unrecognized instruction type for instruction '{instruction}'.");
                break;
        }

        return fields;
    }

    /// <summary>
    /// 解析InstructionType
    /// </summary>
    public InstructionType ParseInstructionType(string instruction)
    {
        if (instruction.StartsWith('@'))
        {
            return InstructionType.A_INSTRUCTION;
        }

        if (instruction.StartsWith('('))
        {
            return InstructionType.L_INSTRUCTION;
        }

        return InstructionType.C_INSTRUCTION;
    }

    /// <summary>
    /// parse A-instruction
    /// </summary>
    /// <param name="instruction">instruction</param>
    /// <param name="value">instruction value</param>
    /// <returns>instruction type</returns>
    private string ParseAInstruction(string instruction, out string value)
    {
        var atIndex = instruction.IndexOf('@');
        var remain = instruction.Substring(atIndex + 1);

        if (IsSymbol(remain))
        {
            if (SymbolTable.Contains(remain))
            {
                value = remain;
                return "symbol";
            }

            // If the symbol is not in the symbol table, add it
            SymbolTable.AddEntry(remain, varStartAddress);
            varStartAddress++;
            value = varStartAddress.ToString();
            return "decimal";
        }

        // If it's a decimal value, return it as is
        value = remain;
        return "decimal";
    }

    /// <summary>
    /// Check if the string is a symbol (starts with a letter)
    /// </summary>
    private bool IsSymbol(string str)
    {
        var isLetter = char.IsLetter(str[0]);
        return isLetter;
    }

    /// <summary>
    /// 解析C-instruction
    /// </summary>
    private Dictionary<string, string> ParseCInstruction(string instruction)
    {
        var fields = new Dictionary<string, string>();

        var dest = ParseDest(InstructionType.C_INSTRUCTION, instruction);
        var comp = ParseComp(InstructionType.C_INSTRUCTION, instruction);
        var jump = ParseJump(InstructionType.C_INSTRUCTION, instruction);
        fields.Add("dest", dest);
        fields.Add("comp", comp);
        fields.Add("jump", jump);

        return fields;
    }

    /// <summary>
    /// 解析Symbol
    /// </summary>
    public string ParseSymbol(InstructionType type, string instruction)
    {
        var symbol = "";

        if (type == InstructionType.A_INSTRUCTION)
        {
            var startIndex = instruction.IndexOf('@') + 1;
            var result = instruction.Substring(startIndex);
            symbol = result;
        }
        if (type == InstructionType.L_INSTRUCTION)
        {
            var startIndex = instruction.IndexOf('(') + 1;
            var endIndex = instruction.IndexOf(')');
            var length = endIndex - startIndex;

            var result = instruction.Substring(startIndex, length);
            symbol = result;
        }

        return symbol;
    }

    /// <summary>
    /// 解析Jump
    /// </summary>
    private string ParseJump(InstructionType type, string instruction)
    {
        // if(type != InstructionType.C_INSTRUCTION)
        // {
        //     return string.Empty;
        // }

        if (!instruction.Contains(';'))
        {
            return "null";
        }

        var semicolonIndex = instruction.IndexOf(';');
        var jump = instruction.Substring(semicolonIndex + 1);
        return jump;
    }

    /// <summary>
    /// 解析Comp
    /// </summary>
    private string ParseComp(InstructionType type, string instruction)
    {
        // if(type != InstructionType.C_INSTRUCTION)
        // {
        //     return string.Empty;
        // }

        // parse "dest=comp;jump"
        if (instruction.Contains('=') && instruction.Contains(';'))
        {
            var equalIndex = instruction.IndexOf('=');
            var semicolonIndex = instruction.IndexOf(';');
            var length = semicolonIndex - equalIndex - 1;
            var comp = instruction.Substring(equalIndex + 1, length);
            return comp;
        }

        // parse "dest=comp"
        if(instruction.Contains('='))
        {
            var equalIndex = instruction.IndexOf('=');
            var comp = instruction.Substring(equalIndex + 1);
            return comp;
        }

        // parse "comp;jump"
        if (instruction.Contains(';'))
        {
            var semicolonIndex = instruction.IndexOf(';');
            var comp = instruction.Substring(0, semicolonIndex);
            return comp;
        }

        // parse "comp"
        return instruction;
    }

    /// <summary>
    /// 解析Dest
    /// </summary>
    private string ParseDest(InstructionType type, string instruction)
    {
        // if(type != InstructionType.C_INSTRUCTION)
        // {
        //     return string.Empty;
        // }

        // parse "comp;jump" or "comp"
        if (!instruction.Contains('='))
        {
            return "null";
        }

        // parse "dest=comp;jump" or "dest=comp"
        var equalIndex = instruction.IndexOf('=');
        var leftPart = instruction.Substring(0, equalIndex);
        return leftPart;
    }
}