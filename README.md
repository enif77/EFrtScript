# Pico Forth 

A miniature, toy grade, Forth language implementation.


## Forth differences

- Selected words are implemented only.
- Words cannot be redefined.
- Stack, return stack and the heap contains value objects (integers, strings etc.).
- No "direct" memory access.
- No low level stuff.


Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes, S = suspended compilation only (not available in I, C or IC).
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack - and description of the word itself.


## CORE library

Common words for all base operations.

### Words

| Name     | Imm. | Mode | Description                                                                                                                                                              |
|----------|------|------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| !        | no   | IC   | **Store**<br>(x a-addr -- )<br>Store `x` at `addr` (a heap array index).  |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing `)` character. |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack. |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack. |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Subtracts n2 from n1 and leaves the difference on the stack. |
| .        | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack. |
| ." str   | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream. |
| 0<       | no   | IC   | **Less than zero**<br>(n -- flag)<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0=       | no   | IC   | **Equal to zero**<br>(n -- flag)<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack. |
| <        | no   | IC   | **Less than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 < n2, 0 otherwise. |
| =        | no   | IC   | **Equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | **Greater than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 > n2, 0 otherwise. |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack. |
| ?DUP     | no   | IC   | **Conditional duplicate**<br>(n -- 0 / n n)<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| CR       | no   | IC   | **Carriage return**<br>( -- )<br>The following output will start at the new line. |
| DEPTH    | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | **Definite loop**<br>(limit index -- ) [ - limit index ]<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit − 1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| DROP     | no   | IC   | **Discard top of stack**<br>(n --)<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | **Duplicate**<br>(n -- n n)<br>Duplicates the value at the top of the stack. |
| ELSE     | yes  | C    | **Else**<br><br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| IF       | yes  | C    | **Conditional statement**<br>(flag --)<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| LOOP     | yes  | C    | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| OVER     | no   | IC   | **Duplicate second item**<br>(n1 n2 -- n1 n2 n1)<br>The second item on the stack is copied to the top. |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack. |
| ROT      | no   | IC   | **Rotate 3 items**<br>(n1 n2 n3 -- n2 n3 n1)<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down.  |
| S" str   | yes  | IC   | **String literal**<br>( -- s)<br>Consume all source characters till the closing `"` character, creating a string from them and storing the result on the top of the stack. |
| SPACE    | no   | IC   | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES   | no   | IC   | **Print spaces**<br>(n -- )<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| SWAP     | no   | IC   | **Swap top two items**<br>(n1 n2 -- n2 n1)<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | **End if**<br>( -- flag)<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| TYPE     | no   | IC   | **Print string**<br>(s -- )<br>Prints out a value on the top of the stack as a string. |


# STRING library

String manipulation words.

## Words

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| S.   | no   | IC   | **Print string**<br>(s -- )<br>A string on the top of the stack is printed. |
