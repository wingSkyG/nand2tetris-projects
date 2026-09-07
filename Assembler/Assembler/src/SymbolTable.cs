public static class SymbolTable
{
    public static Dictionary<string, int> SYMBOL_TABLE_DICT = [];

    static SymbolTable()
    {
        InitSymbolTable();
    }

    public static void Reset()
    {
        SYMBOL_TABLE_DICT.Clear();
        InitSymbolTable();
    }

    /// <summary>
    /// Get the address associated with the specified symbol
    /// </summary>
    public static void AddEntry(string symbol, int address)
    {
        if (SYMBOL_TABLE_DICT.ContainsKey(symbol))
        {
            Console.WriteLine($"Symbol {symbol} already exists in the symbol table.");
            return;
        }

        SYMBOL_TABLE_DICT.Add(symbol, address);
    }

    /// <summary>
    /// Check if the symbol table contains the specified symbol
    /// </summary>
    public static bool Contains(string symbol)
    {
        return SYMBOL_TABLE_DICT.ContainsKey(symbol);
    }

    /// <summary>
    /// Get the address associated with the specified symbol
    /// </summary>
    public static int GetAddress(string symbol)
    {
        if (!SYMBOL_TABLE_DICT.TryGetValue(symbol, out int address))
        {
            Console.WriteLine($"Symbol {symbol} not found in the symbol table.");
            return -1;
        }

        return address;
    }

    /// <summary>
    /// Initialize the symbol table with predefined symbols
    /// </summary>
    private static void InitSymbolTable()
    {
        var predifinedSymbols = new List<KeyValuePair<string, int>>
        {
            new KeyValuePair<string, int>("R0", 0),
            new KeyValuePair<string, int>("R1", 1),
            new KeyValuePair<string, int>("R2", 2),
            new KeyValuePair<string, int>("R3", 3),
            new KeyValuePair<string, int>("R4", 4),
            new KeyValuePair<string, int>("R5", 5),
            new KeyValuePair<string, int>("R6", 6),
            new KeyValuePair<string, int>("R7", 7),
            new KeyValuePair<string, int>("R8", 8),
            new KeyValuePair<string, int>("R9", 9),
            new KeyValuePair<string, int>("R10", 10),
            new KeyValuePair<string, int>("R11", 11),
            new KeyValuePair<string, int>("R12", 12),
            new KeyValuePair<string, int>("R13", 13),
            new KeyValuePair<string, int>("R14", 14),
            new KeyValuePair<string, int>("R15", 15),
            new KeyValuePair<string, int>("SCREEN", 16384),
            new KeyValuePair<string, int>("KBD", 24576),
            new KeyValuePair<string, int>("SP", 0),
            new KeyValuePair<string, int>("LCL", 1),
            new KeyValuePair<string, int>("ARG", 2),
            new KeyValuePair<string, int>("THIS", 3),
            new KeyValuePair<string, int>("THAT", 4)
        };

        foreach (var symbol in predifinedSymbols)
        {
            SYMBOL_TABLE_DICT.Add(symbol.Key, symbol.Value);
        }
    }
}