// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.

// RAM[screen_size] = 8192
// LOOP:
//     if (key pressed) goto BLACKEN
//     goto WHITEN
//     goto LOOP

    // RAM[screen_size] = 8192
    @8192
    D=A
    @screen_size
    M=D

// main loop
(LOOP)
    // if (key pressed) goto BLACKEN else goto WHITEN
    @KBD
    D=M

    @BLACKEN
    D;JNE

    @WHITEN
    0;JMP

// fill all pixels with black
(BLACKEN)
    // initialize i=0
    @i
    M=0

    // BLACKENLOOP:
    //     if (i>=screen_size) goto lOOP
    //     RAM[SCREEN+i] = -1
    //     i++
    //     goto BLACKENLOOP
    (BLACKENLOOP)
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
        A=D
        M=-1

        @i
        M=M+1

        @BLACKENLOOP
        0;JMP

    @LOOP
    0;JMP

// fill all pixels with white
(WHITEN)
    // initialize j=0
    @j
    M=0

    // WHITENLOOP:
    //     if (j>=screen_size) goto lOOP
    //     RAM[SCREEN+j] = 0
    //     j++
    //     goto WHITENLOOP
    (WHITENLOOP)
        @j
        D=M
        @screen_size
        D=D-M
        @LOOP
        D;JGE

        @SCREEN
        D=A
        @j
        D=D+M
        A=D
        M=0

        @j
        M=M+1

        @WHITENLOOP
        0;JMP

    @LOOP
    0;JMP