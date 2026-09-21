/// Bootstrap Code
// SP = 256
@256
D=A
@SP
M=D
// call Sys.init 0
@Bootstrap$ret.0
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
@0
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Sys.init
0;JMP   // goto f
(Bootstrap$ret.0)    // retAddr

/// Sys.vm
// function Sys.init 0
(Sys.init)
@0
D=A
(Sys.init$LOOP)
@Sys.init$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Sys.init$LOOP
(Sys.init$ENDLOOP)
// push constant 4000
@4000
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 0
@SP
M=M-1
A=M
D=M
@THIS
M=D
// push constant 5000
@5000
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 1
@SP
M=M-1
A=M
D=M
@THAT
M=D
// call Sys.main 0
@Sys.init$ret.0
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
@0
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Sys.main
0;JMP   // goto f
(Sys.init$ret.0)    // retAddr
// pop temp 1
@5
D=A
@1
D=D+A
@R13
M=D
@SP
M=M-1
A=M
D=M
@R13
A=M
M=D
// label LOOP
(LOOP)
// goto LOOP
@LOOP
0;JMP
// function Sys.main 5
(Sys.main)
@5
D=A
(Sys.main$LOOP)
@Sys.main$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Sys.main$LOOP
(Sys.main$ENDLOOP)
// push constant 4001
@4001
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 0
@SP
M=M-1
A=M
D=M
@THIS
M=D
// push constant 5001
@5001
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 1
@SP
M=M-1
A=M
D=M
@THAT
M=D
// push constant 200
@200
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop local 1
@LCL
D=M
@1
D=D+A
@R13
M=D
@SP
M=M-1
A=M
D=M
@R13
A=M
M=D
// push constant 40
@40
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop local 2
@LCL
D=M
@2
D=D+A
@R13
M=D
@SP
M=M-1
A=M
D=M
@R13
A=M
M=D
// push constant 6
@6
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop local 3
@LCL
D=M
@3
D=D+A
@R13
M=D
@SP
M=M-1
A=M
D=M
@R13
A=M
M=D
// push constant 123
@123
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Sys.add12 1
@Sys.main$ret.0
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
@Sys.add12
0;JMP   // goto f
(Sys.main$ret.0)    // retAddr
// pop temp 0
@5
D=A
@0
D=D+A
@R13
M=D
@SP
M=M-1
A=M
D=M
@R13
A=M
M=D
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
// push local 2
@LCL
D=M
@2
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push local 3
@LCL
D=M
@3
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
// push local 4
@LCL
D=M
@4
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
// function Sys.add12 0
(Sys.add12)
@0
D=A
(Sys.add12$LOOP)
@Sys.add12$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Sys.add12$LOOP
(Sys.add12$ENDLOOP)
// push constant 4002
@4002
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 0
@SP
M=M-1
A=M
D=M
@THIS
M=D
// push constant 5002
@5002
D=A
@SP
A=M
M=D
@SP
M=M+1
// pop pointer 1
@SP
M=M-1
A=M
D=M
@THAT
M=D
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
// push constant 12
@12
D=A
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
