using static LanguageSpecificationData;

/// <summary>
/// 函数指令翻译器
/// </summary>
class FunctionWriter
{

    /// <summary>
    /// 翻译函数调用指令为Assembly Code
    /// </summary>
    public string WriteFunctionCommand(FunctionCommandBase command)
    {
        switch (command.FunctionType)
        {
            case FunctionType.Function:
                var functionCommand = command as FunctionCommand
                    ?? throw new System.InvalidOperationException("Expected a FunctionCommand.");
                return WriteFunction(functionCommand.FunctionName, functionCommand.VariableCount);
            case FunctionType.Call:
                var callCommand = command as CallCommand
                    ?? throw new System.InvalidOperationException("Expected a CallCommand.");
                return WriteCall(callCommand.FunctionName, callCommand.ArgumentCount);
            case FunctionType.Return:
                var returnCommand = command as ReturnCommand
                    ?? throw new System.InvalidOperationException("Expected a ReturnCommand.");
                return WriteReturn();
        }

        return string.Empty;
    }

    /// <summary>
    /// 翻译function指令为Assembly Code
    /// </summary>
    private string WriteFunction(string functionName, int numVars)
    {
        var assemblyCode = string.Empty;

        var loopSymbol = $"{functionName}$LOOP";
        var endloopSymbol = $"{functionName}$ENDLOOP";
        assemblyCode = $"""
            // function f nVars
            ({functionName})
            @{numVars}
            D=A
            ({loopSymbol})
            @{endloopSymbol}
            D;JEQ
            @SP
            A=M
            M=0
            @SP
            M=M+1
            D=D-1
            @{loopSymbol}
            ({endloopSymbol})
            """;

        return assemblyCode;
    }

    /// <summary>
    /// 翻译call指令为Assembly Code
    /// </summary>
    private string WriteCall(string functionName, int numArgs)
    {
        return string.Empty;
    }

    /// <summary>
    /// 翻译return指令为Assembly Code
    /// </summary>
    private string WriteReturn()
    {
        var assemblyCode = string.Empty;

        assemblyCode = $"""
            // return
            @LCL
            D=M
            @R13
            M=D // frame = LCL

            @5
            A=D-A
            D=M
            @R14
            M=D // retAddr = *(frame-5)

            @SP
            M=M-1
            A=M
            D=M
            @ARG
            A=M
            M=D // *ARG = pop()
        
            @ARG
            D=M
            @SP
            M=D+1   // SP = ARG+1
            
            @R13
            A=M-1
            D=M
            @THAT
            M=D // THAT = *(frame-1)

            @R13
            A=M-1
            A=A-1
            D=M
            @THIS
            M=D // THIS = *(frame-2)

            @R13
            A=M-1
            A=A-1
            A=A-1
            D=M
            @ARG
            M=D // ARG = *(frame-3)

            @R13
            A=M-1
            A=A-1
            A=A-1
            A=A-1
            D=M
            @LCL
            M=D // LCL = *(frame-4)

            @R14
            A=M
            0;JMP // goto retAddr
            """;

        return assemblyCode;
    }
}