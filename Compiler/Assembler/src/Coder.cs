using static LanguageSpecificationData;

public class Coder
{
    /// <summary>
    /// 翻译Instruction
    /// </summary>
    public string CodeInstruction(Dictionary<string, string> fields)
    {
        var binaryCode = "";

        var instructionType = fields.GetValueOrDefault("instructionType", "");

        switch (instructionType)
        {
            case "A_INSTRUCTION":
                binaryCode = CodeAInstruction(fields);
                break;
            case "C_INSTRUCTION":
                binaryCode = CodeCInstruction(fields);
                break;
            case "L_INSTRUCTION":
                // L-instruction does not produce binary code
                break;
            default:
                Console.WriteLine($"Translate error: Unrecognized instruction type '{instructionType}'.");
                break;
        }

        return binaryCode;
    }

    /// <summary>
    /// 翻译A-instruction
    /// </summary>
    private string CodeAInstruction(Dictionary<string, string> aFields)
    {
        var typeField = aFields.GetValueOrDefault("instructionType", "");

        // get subtype of A-instruction
        var decimalValue = "";
        var type = aFields.Last().Key;
        var value = aFields.Last().Value;
        switch (type)
        {
            case "symbol":
                decimalValue = SymbolTable.GetAddress(value).ToString();
                break;
            case "decimal":
                decimalValue = value;
                break;
            default:
                Console.WriteLine("the A-instruction type is wrong");
                break;
        }

        var typeCode = CodeField(typeField, INSTRUCTIONTYPE_TABLE, "instructionTypeTable");
        var valueCode = CodeDecimalValue(decimalValue);

        return typeCode + valueCode;
    }

    /// <summary>
    /// 翻译C-instruction
    /// </summary>
    private string CodeCInstruction(Dictionary<string, string> cFields)
    {
        var typeField = cFields.GetValueOrDefault("instructionType", "");
        var compField = cFields.GetValueOrDefault("comp", "");
        var destField = cFields.GetValueOrDefault("dest", "");
        var jumpField = cFields.GetValueOrDefault("jump", "");

        var typeCode = CodeField(typeField, INSTRUCTIONTYPE_TABLE, "instructionTypeTable");
        var compCode = CodeField(compField, COMP_TABLE, "compTable");
        var destCode = CodeField(destField, DEST_TABLE, "destTable");
        var jumpCode = CodeField(jumpField, JUMP_TABLE, "jumpTable");

        return typeCode + compCode + destCode + jumpCode;
    }

    /// <summary>
    /// 翻译field
    /// </summary>
    private string CodeField(string field, Dictionary<string, string> specificationTable, string tableName = "")
    {
        var binaryCode = specificationTable.GetValueOrDefault(field, "");
        if(binaryCode == string.Empty)
        {
            Console.WriteLine($"Code error: Field '{field}' not found in {tableName} specification table.");
            return string.Empty;
        }

        return binaryCode;
    }

    /// <summary>
    /// 翻译decimalValue
    /// </summary>
    /// <returns></returns>
    private string CodeDecimalValue(string decimalValue)
    {
        var binaryCode = Convert.ToString(int.Parse(decimalValue), 2).PadLeft(15, '0');
        return binaryCode;
    }
}