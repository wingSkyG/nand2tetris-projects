// function f nVars
(Main.fibonacci)
@0
D=A
(Main.fibonacci$LOOP)
@Main.fibonacci$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Main.fibonacci$LOOP
(Main.fibonacci$ENDLOOP)
// push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// lt
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
D=M-D
@LT_TRUE_0
D;JLT
@SP
A=M
M=0
@LT_END_0
0;JMP
(LT_TRUE_0)
@SP
A=M
M=-1
(LT_END_0)
@SP
M=M+1
// if-goto N_LT_2
@SP
M=M-1
A=M
D=M
@N_LT_2
D;JGT
// goto N_GE_2
@N_GE_2
0;JMP
// label N_LT_2
(N_LT_2)
// push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
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
M=D+1 // SP = ARG+1     
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
// label N_GE_2
(N_GE_2)
// push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
M=M-D
@SP
M=M+1
// call f nArgs
@Main.fibonacci$ret.0
D=A
@SP
A=M
M=D
@SP
M=M+1   // push retAddr
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1   // push LCL
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1   // push ARG
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1   // push THIS
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1   // push THAT
@SP
D=M
@5
D=D-A
@1
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Main.fibonacci
0;JMP   // goto f
(Main.fibonacci$ret.0)    // retAddr
// push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
M=M-D
@SP
M=M+1
// call f nArgs
@Main.fibonacci$ret.1
D=A
@SP
A=M
M=D
@SP
M=M+1   // push retAddr
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1   // push LCL
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1   // push ARG
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1   // push THIS
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1   // push THAT
@SP
D=M
@5
D=D-A
@1
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Main.fibonacci
0;JMP   // goto f
(Main.fibonacci$ret.1)    // retAddr
// add
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
M=D+M
@SP
M=M+1
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
M=D+1 // SP = ARG+1     
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