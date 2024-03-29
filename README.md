# EFrt Script

A Forth language implementation with scripting languages twist.

See the [documentation](docs/index.md) for more info.

## Examples

NOTE: These examples are (mostly) not tested/implemented yet.

```
( Hello world! )
: hello S" Hello, world!" S. CR ;

( A simplified hello-world! )
: greet ." Hello, I speak Forth!" CR ;

( Large letter F )
: STAR 42 EMIT ;
: STARS 0 DO  STAR  LOOP ;
: MARGIN CR 30 SPACES ;
: BLIP MARGIN STAR ;
: BAR MARGIN 5 STARS ;
: F BAR BLIP BAR BLIP BLIP CR ;

( do-loop, that runs 5 times )
: doit 5 0 DO ." hello" CR LOOP ;

( do-loop, that runs 5 times and shows the current I value ) 
: doit 5 0 DO ." hello" 1 SPACES I . CR LOOP ;

( do-loop, that breaks after I > 4 ) 
: doit 10 0 DO ." hello" 1 SPACES I DUP . CR 4 > IF LEAVE THEN LOOP ;
  
( begin-until loop, that prins even numbers from 10 to 0 )
: doit 10 BEGIN DUP . CR 2 - DUP 0< UNTIL ;

( infinite loop )
: doit BEGIN ." hello" CR AGAIN ;

( loop writes "hello" 10 times )
: doit 10 BEGIN DUP 0> WHILE ." hello " 1 - DUP . CR REPEAT ;

( Messing with programmers... )
: 1 2 ;
1 1 + . CR  \ What result are you expecting here? :-)

( Constants )
123 CONSTANT C1  \ Defines a constant C1.
C1 . CR          \ Prints the value of the constant - 123.

( Variables )
VARIABLE A   \ Defines a single cell variable A.
123 A !      \ Stores 123 into the variable A.
A @ . CR     \ Fetches and prints out the value of the variable A.
A ? CR       \ The same thing - "?" is a shortcut for "@ .". 

2VARIABLE B  \ Defines a double cell variable B.
1.5 B 2!     \ Stores 1.5 float (a double cell value) into the variable B.
B 2@ F.      \ Fetches and prints out the double cell (float) value of the variable B.

( 100 cells long array )
VARIABLE arr            \ Variable for storring the "address" of the first cell of the new array.
HERE arr !              \ Getting the first cell address.
100 ALLOT               \ Allocation of the array.
HERE . CR               \ Will print out the index ("address") of the last cell of the new array.
123 arr !               \ Stores 123 to the first cell of the array arr.
456 arr 1 CELLS + !     \ Stores 456 to the second cell of the array arr.
arr @ . CR              \ Gets and prints out the contents of the first cell of the array arr (123).
arr ? CR                \ Shorter version of the previous example.
arr 1 CELLS + ? CR      \ Gets and prints out the contents of the second cell of the array arr (456).

( Storing a number on the heap and printing it out )
123 , HERE 1 CELLS - @ . CR

( Factorial of N - without RECURSE )
: factorial DUP 0= IF DROP 1 ELSE DUP 1- FACTORIAL * THEN ;

( Factorial of N - with RECURSE )
: factorial DUP 0= IF DROP 1 ELSE DUP 1- RECURSE * THEN ;

5 fatorial . CR  \ 120

( Indexed array )
( Source: http://www.forth.org/svfig/Len/definwds.htm )
: indexed-array (n -- ) (i -- a)
     CREATE CELLS ALLOT
     DOES> SWAP CELLS + ;

20 indexed-array foo  \ Make a 1-dimensional array with 20 cells
 3 foo                \ Put addr of fourth element on the stack

( 80 cells long buffer/array )
CREATE buffer 80 CELLS ALLOT

( ' and EXECUTE )
: goodbye ." Goodbye" CR ;
: hello ." Hello" CR ;
VARIABLE a
: greet a @ execute ;   \ Expects an execution token (xt) of a word.
' hello a !             \ Set the variable a to the xt of the word hello.
greet                   \ greet will say "Hello".
' goodbye a !           \ Set the variable a to the xt of the word goodbye.
greet                   \ greet will say "Goodbye".

( ' ['] and EXECUTE )
( Source: https://www.forth.com/starting-forth/9-forth-execution/ ) 
( 1 ) : hello    ." Hello " ;
( 2 ) : goodbye  ." Goodbye " ;
( 3 ) VARIABLE 'aloha  ' hello 'aloha !
( 4 ) : aloha    'aloha @ EXECUTE ;

aloha                   \ Prints out "Hello".
' goodbye 'aloha !      \ Sets the 'aloha variable to xt of the goodbye word.
aloha                   \ Prints out "Goodbye".

: say  ' 'aloha ! ;

say hello
aloha                   \ Prints out "Hello".
say goodbye
aloha                   \ Prints out "Goodbye".

: coming   ['] hello   'aloha ! ;
: going    ['] goodbye 'aloha ! ;

coming                  \ Sets 'aloha to the xt of the word hello.
aloha                   \ Prints out "Hello".
going                   \ Sets 'aloha to the xt of the word goodbye.
aloha                   \ Prints out "Goodbye".

( Forvard declaration )
( Source: And so Forth..., J.L. Bezemer )
-1 VALUE (step2)            \ A place to store a reference (execution token) to the word step2.
: step2 (step2) EXECUTE ;   \ The step2 word. Without a body for now.
: step1 1+ DUP . CR step2 ; \ The step1 word, calling the step2 word.
:noname 1+ DUP . CR step1 ; \ The body of the step2 word. :NONAME leaves an execution token of the created word on the stack.
TO (step2)                  \ Sets the body of the word step2 (using the value of the VALUE (step2)).
1 step1                     \ Executes the step1 word, that in turn calls the step2 word, which calls the step1 word...

( Unloop )
: unloop-test 10 1 DO I DUP . CR 5 > IF ." Exiting..." CR UNLOOP EXIT THEN LOOP ." Never printer out..." ;

( Abort with a message )
: abort-with-message 10 1 DO I DUP . CR 5 > ABORT" Too big!" LOOP ." Never printer out..." ;

( Exceptions )
: th THROW ." Thrown" CR ;
: ca ." pre-t" CR ['] th CATCH ." post-t" CR ;
0 ca   \ No exception thrown.
-1 ca  \ Like ABORT.
-2 ca  \ Like ABORT" mesg".
1 ca   \ User exception.

( Is a word defined? )
: is-word-defined? ['] ' CATCH 0= IF ." Defined" ELSE ." Undefined" THEN  DROP ( Drop the product of the ' word.) ;
: w? is-word-defined? CR ;  \ Just a shortcut for the is-word-defined? word.
w? bla  \ Undefined
w? IF   \ Defined

( How the word . "dot" can be implemented... )
( n -- )                  \ Display n.
: . DUP ABS 0             \ Prepare.
   <# #S  ROT SIGN #>     \ Convert to string.
   TYPE SPACE ;           \ Output the created string.
   
( Prints a counted string )
( c-addr -- )
(c-addr --)
: printc    ( c-addr )
  DUP       ( c-addr c-addr )
  C@        ( c-addr count )
  1+        ( c-addr count )
  1         ( c-addr count start )
  DO        ( c-addr )
  DUP       ( c-addr c-addr )
  I         ( c-addr c-addr I )
  CHARS     ( c-addr c-addr bytes )
  +         ( c-addr c-addr+index )
  C@        ( c-addr char )
  EMIT      ( c-addr )
  LOOP ;
  
```

### Roman numerals for two bytes chars

Source: Thinking Forth, Leo Brodie

```
Create romans      ( ones) char I c, char V c,
                   ( tens) char X c, char L c,
               ( hundreds) char C c, char D c,
              ( thousands) char M c,

Variable column# ( current_offset)

: ones 0 column# ! ;
: tens 4 column# ! ;
: hundreds 8 column# ! ;
: thousands 12 column# ! ;

: column ( -- address-of-column ) romans column# @ + ;

: .symbol ( offset -- ) column + c@ emit ;

: oner 0 .symbol ;
: fiver 2 .symbol ;
: tener 4 .symbol ;

: oners ( #-of-oners -- )
    ?dup IF 0 DO oner LOOP THEN ;

: almost ( quotient-of-5/ -- )
    oner IF tener ELSE fiver THEN ;

: digit ( digit -- )
    5 /mod over 4 = IF almost drop ELSE IF fiver THEN
    oners THEN ;

: roman ( number -- ) 
    1000 /mod thousands digit
     100 /mod  hundreds digit
      10 /mod      tens digit
                   ones digit
  SPACE ;
```

## Links

### Forth related links

- https://www.root.cz/serialy/programovaci-jazyk-forth/
- https://www.forth.com/starting-forth/
- https://en.wikipedia.org/wiki/Forth_(programming_language)
- https://www.fourmilab.ch/atlast/atlast.html
- http://users.ece.cmu.edu/~koopman/stack_computers/

#### CREATE or BUILDS?

- http://amforth.sourceforge.net/TG/recipes/Builds.html
- http://www.forth.org/svfig/Len/definwds.htm

### General links

- https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type-similar-to-c-unions-
