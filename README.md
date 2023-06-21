# EFrt Script

A Forth language implementation with scripting languages twist.

## Forth differences

- Selected words are implemented only.
- Words cannot be redefined.
- Stack, return stack and the heap contains value objects (integers, strings etc.).
- No "direct" memory access.
- No low level stuff.

## Stacks

- Data stack: Main stack for user data. Holds all values.
- Return stack: Stack for (mostly) interpreter internal use. Holds values. Mostly integers.
- Exception stack: Not accessible for users. Its used internally by THROW and CATCH words.
- Input source stack: Stack for keeping the inputs. Used by the EVALUATE word.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes, S = suspended compilation only (not available in I, C or IC).
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack - and description of the word itself.

## Strings

Words like `S"` and `."` reads strings from the input stream and push them to the stack. Strings can contain special characters like `\n` and `\t`.

The table below shows all special characters.

| Character | Description |
|-----------|-------------|
| \a        | BEL (alert, ASCII 7) |
| \b        | BS (backspace, ASCII 8) |
| \e        | ESC (escape, ASCII 27) |
| \f        | FF (form feed, ASCII 12) |
| \l        | LF (line feed, ASCII 10) |
| \m        | CR/LF pair (ASCII 13, 10) |
| \n        | newline (implementation dependent , e.g., CR/LF, CR, LF, LF/CR) |
| \q or \\" | double-quote (ASCII 34) |
| \r        | CR (carriage return, ASCII 13) |
| \t        | HT (horizontal tab, ASCII 9) |
| \v        | VT (vertical tab, ASCII 11) |
| \z or \0  | NUL (no character, ASCII 0) |
| \\\\      | backslash itself (ASCII 92) |
| \'        | double-quote (ASCII 39) |
| \uHHHH    | A sequence of 4 hex characters. |
| \x or \X  | A hex sequence of 1 to 4 hex characters. |


## CORE library

Common words for all base operations.

### Words

| Name     | Imm. | Mode | Description |
|----------|------|------|---|
| !        | no   | IC   | **Store**<br>(x a-addr -- )<br>Store `x` at `addr` (a heap array index). |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing `)` character. |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack. |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack. |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Subtracts n2 from n1 and leaves the difference on the stack. |
| .        | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack. |
| ." str   | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream. |
| 0<       | no   | IC   | **Less than zero**<br>(n -- flag)<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0=       | no   | IC   | **Equal to zero**<br>(n -- flag)<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack. |
| : w      | yes  | I    | **Begin definition**<br>Begins compilation of a word named "w". |
| ;        | yes  | C    | **End definition**<br>Ends compilation of a word. |
| <        | no   | IC   | **Less than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 < n2, 0 otherwise. |
| =        | no   | IC   | **Equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | **Greater than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 > n2, 0 otherwise. |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack. |
| ?DUP     | no   | IC   | **Conditional duplicate**<br>(n -- 0 / n n)<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| ABORT    | no   | IC   | **Abort**<br>Clears the stack and the object stack and performs a QUIT. |
| ABORT" str | yes  | C    | **Abort with message**<br>(flag -- )<br>Prints the string literal that follows in line, then aborts, clearing all execution state to return to the interpreter. |
| ABS      | no   | IC   | **n2 = Abs(n1)**<br>(n1 -- n2)<br>Replaces the top of stack with its absolute value. |
| BASE     | no   | IC   | **Obtain the numeric conversion radix heap index**<br>( -- addr)<br>addr is the index of a value containing the current number-conversion radix {{2...36}}. |
| CR       | no   | IC   | **Carriage return**<br>( -- )<br>The following output will start at the new line. |
| DECIMAL  | no   | IC   | **Set the numeric conversion radix to ten**<br>( -- )<br>Sets the numeric conversion radix to ten (decimal). |
| DEPTH    | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | **Definite loop**<br>(limit index -- ) [ - limit index ]<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit − 1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| DROP     | no   | IC   | **Discard top of stack**<br>(n --)<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | **Duplicate**<br>(n -- n n)<br>Duplicates the value at the top of the stack. |
| ELSE     | yes  | C    | **Else**<br><br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| EMIT     | no   | IC   | **Emit**<br>(n -- )<br>Displays n as a character. |
| EVALUATE | no   | IC   | **Evaluate string**<br>{s -- }<br>Evaluates a string the top of the stack. |
| IF       | yes  | C    | **Conditional statement**<br>(flag --)<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| LOOP     | yes  | C    | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| NEGATE   | no   | IC   | **n2 = -n1**<br>(n1 -- n2)<br>Negates the value the top of the stack. |
| OVER     | no   | IC   | **Duplicate second item**<br>(n1 n2 -- n1 n2 n1)<br>The second item on the stack is copied to the top. |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack. |
| ROT      | no   | IC   | **Rotate 3 items**<br>(n1 n2 n3 -- n2 n3 n1)<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down. |
| S" str   | yes  | IC   | **String literal**<br>( -- s)<br>Consume all source characters till the closing `"` character, creating a string from them and storing the result on the top of the stack. |
| SPACE    | no   | IC   | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES   | no   | IC   | **Print spaces**<br>(n -- )<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| SWAP     | no   | IC   | **Swap top two items**<br>(n1 n2 -- n2 n1)<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | **End if**<br>( -- flag)<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| TYPE     | no   | IC   | **Print string**<br>(s -- )<br>Prints out a value on the top of the stack as a string. |
| [']      | yes  | C    | **Obtain execution token**<br>Places the execution token of the following word to the currently compiled word as a literal. |

### Words (EXT)

| Name     | Imm. | Mode | Description |
|----------|------|------|---|
| \        | yes  | IC   | **Line comment**<br>Skips all source characters till the closing EOLN character. |
| HEX      | no   | IC   | **Set the numeric conversion radix to sixteen**<br>( -- )<br>Sets the numeric conversion radix to sixteen (hexadecimal). |


## EXCEPTION library

Exceptions handling words.

### Words

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| CATCH    | yes  | C    | **Catch an exception**<br>(xt -- n)<br>Pushes the current execution state to the exception stack, executes xt, and returns 0 for no-error execution (dropping the exception frame) and non-zero, if a THROW was executed. |
| THROW    | no   | C    | **Throw an exception**<br>(n -- )<br>If n is zero, does nothing. Otherwise throws an error, terminating the current execution, returning to the CATCH, if a exception frame was found on the exception stack. |

### Words (EXT)

NOTE: Not yet implemented. :-)

| Name       | Imm. | Mode | Description |
| ---        | ---  | ---  | --- |
| ABORT      | no   | IC   | **Abort**<br>Clears the stack and the object stack and performs a QUIT. Extended versions. |
| ABORT" str | yes  | C    | **Abort with message**<br>(flag -- )<br>Prints the string literal that follows in line, then aborts, clearing all execution state to return to the interpreter. Extended version. |


## TOOLS library

Various helper words.

### Words

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| BYE  | no   | IC   | **Terminate execution**<br>( -- )<br>Return control to the host operating system, if any. |


## EXTRA

Various helper words defined by the EFrt Script app.

### Words

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| READ-ALL-TEXT  | no   | IC   | **Read all text**<br>(s -- s)<br>Reads contents of a text file into a string on the stack. |
| TRACE  | no   | IC   | **Tracing mode**<br>(n -- )<br>Turns tracing mode on or off. Tracing lists all words, that will be executed. |
