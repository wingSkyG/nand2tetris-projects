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
// push constant 6
@6
D=A
@SP
A=M
M=D
@SP
M=M+1
// push constant 8
@8
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Class1.set 2
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
@2
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Class1.set
0;JMP   // goto f
(Sys.init$ret.0)    // retAddr
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
// push constant 23
@23
D=A
@SP
A=M
M=D
@SP
M=M+1
// push constant 15
@15
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Class2.set 2
@Sys.init$ret.1
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
@2
D=D-A
@ARG
M=D     // ARG = SP-5-numArgs
@SP
D=M
@LCL
M=D     // LCL = SP
@Class2.set
0;JMP   // goto f
(Sys.init$ret.1)    // retAddr
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
// call Class1.get 0
@Sys.init$ret.2
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
@Class1.get
0;JMP   // goto f
(Sys.init$ret.2)    // retAddr
// call Class2.get 0
@Sys.init$ret.3
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
@Class2.get
0;JMP   // goto f
(Sys.init$ret.3)    // retAddr
// label END
(END)
// goto END
@END
0;JMP
/// Class1.vm
// function Class1.set 0
(Class1.set)
@0
D=A
(Class1.set$LOOP)
@Class1.set$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Class1.set$LOOP
(Class1.set$ENDLOOP)
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
// pop static 0
@SP
M=M-1
A=M
D=M
@Class1.0
M=D
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
// pop static 1
@SP
M=M-1
A=M
D=M
@Class1.1
M=D
// push constant 0
@0
D=A
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
// function Class1.get 0
(Class1.get)
@0
D=A
(Class1.get$LOOP)
@Class1.get$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Class1.get$LOOP
(Class1.get$ENDLOOP)
// push static 0
@Class1.0
D=M
@SP
A=M
M=D
@SP
M=M+1
// push static 1
@Class1.1
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
/// Class2.vm
// function Class2.set 0
(Class2.set)
@0
D=A
(Class2.set$LOOP)
@Class2.set$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Class2.set$LOOP
(Class2.set$ENDLOOP)
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
// pop static 0
@SP
M=M-1
A=M
D=M
@Class2.0
M=D
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
// pop static 1
@SP
M=M-1
A=M
D=M
@Class2.1
M=D
// push constant 0
@0
D=A
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
// function Class2.get 0
(Class2.get)
@0
D=A
(Class2.get$LOOP)
@Class2.get$ENDLOOP
D;JEQ
@SP
A=M
M=0
@SP
M=M+1
D=D-1
@Class2.get$LOOP
(Class2.get$ENDLOOP)
// push static 0
@Class2.0
D=M
@SP
A=M
M=D
@SP
M=M+1
// push static 1
@Class2.1
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
