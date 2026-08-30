// RAM[screen_size] = 8192
// RAM[COLOR] = 0
// LOOP:
//     if (key pressed) goto BLACKEN
//     goto WHITEN
//     goto LOOP
// 
// BLACKEN:
//     RAM[COLOR] = -1
//     goto REFRESH
//
// WHITEN:
//     RAM[COLOR] = 0
//     goto REFRESH
//
// REFRESH:
//     for(i=0; i<screen_size; i++) {
//         RAM[SCREEN+i] = RAM[COLOR]
//     }
//     goto LOOP

    // RAM[screen_size] = 8192
    @8192
    D=A
    @screen_size
    M=D

    // RAM[COLOR] = 0
    @0
    D=A
    @COLOR
    M=D

(LOOP)
    // if (key pressed) goto BLACKEN else goto WHITEN
    @KBD
    D=M

    @BLACKEN
    D;JNE

    @WHITEN
    0;JMP

(BLACKEN)
    // RAM[COLOR] = -1
    @COLOR
    M=-1

    @REFRESH
    0;JMP

(WHITEN)
    // RAM[COLOR] = 0
    @COLOR
    M=0

    @REFRESH
    0;JMP

(REFRESH)
    // initialize i=0
    @i
    M=0

    // REFRESHLOOP:
    //     if (i>=screen_size) goto LOOP
    //     RAM[SCREEN+i] = RAM[COLOR]
    //     i++
    //     goto REFRESHLOOP
    (REFRESHLOOP)
        @i
        D=M
        @screen_size
        D=D-M
        @LOOP
        D;JGE

        @SCREEN
        D=A
        @i
        D=D+M
        @pointer
        M=D
        @COLOR
        D=M
        @pointer
        A=M
        M=D

        @i
        M=M+1

        @REFRESHLOOP
        0;JMP

    @LOOP
    0;JMP