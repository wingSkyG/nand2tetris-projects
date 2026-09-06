internal class Program
{
    private static void Main(string[] args)
    {
        // if (args.Length == 0)
        // {
        //     Console.WriteLine("Please provide the name of the assembly file to assemble.");
        //     return;
        // }

        // var fileName = args[0];

        var inputFile = @"C:\WorkStation\Study\Nand2Tetris\nand2tetris-projects\Assembler\Assembler\TestCases\pong\PongL.asm";
        var outputFile = @"Prog.hack";

        var assembler = new Assembler();
        var binaryCode = assembler.Assemble(inputFile);

        if (!File.Exists(outputFile))
        {
            File.Create(outputFile);
        }
        using (StreamWriter writer = new StreamWriter(outputFile, false))
        {
            writer.Write(binaryCode);
        }
    }
}