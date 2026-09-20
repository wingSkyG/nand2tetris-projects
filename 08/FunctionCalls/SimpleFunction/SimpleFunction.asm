// function f nVars
(SimpleFunction.test)
@2
D=A
(SimpleFunction.test$LOOP)
@SimpleFunction.test$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@SimpleFunction.test$LOOP
(SimpleFunction.test$ENDLOOP)
// push local 0
@LCL
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push local 1
@LCL
D=M
@1
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
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
// not
@SP
M=M-1
A=M
M=!M
@SP
M=M+1
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
// push argument 1
@ARG
D=M
@1
A=D+A
D=M
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