# Pico Forth

Miniature, toy grade, Forth language implementation.

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
| !        | no   | IC   | **Store**<br>(x a-addr -- )<br>Store `x` at `a-addr` (a cell aligned heap array index).                                                                                  |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing ) character.                                                                                                 |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack.                                                                           |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack.                                                                                     |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Subtracts n2 from n1 and leaves the difference on the stack.                                                                        |
| .        | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack.                                                                                  |
| ." str   | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream.                                                                                        |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack.                                                                              |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack.                                                        |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack.                                                     |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack.                 |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack.                                                     |
| CR       | no   | IC   | **Carriage return**<br>( -- )<br>The following output will start at the new line.                                                                                        |
| S" str   | yes  | IC   | **String literal**<br>( -- s)<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the stack. |


# STRING library

String manipulation words.

## Words

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| S.   | no   | IC   | **Print string**<br>(s -- )<br>A string on the top of the stack is printed. |
