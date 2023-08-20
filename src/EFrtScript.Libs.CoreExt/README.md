# CORE-EXT

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes, S = suspended compilation only (not available in I, C or IC).
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack - and description of the word itself.

| Name    | Imm. | Mode | Description                                                                                                              |
|---------|------|------|--------------------------------------------------------------------------------------------------------------------------|
| \       | yes  | IC   | **Line comment**<br>Skips all source characters till the closing EOLN character.                                         |
| AGAIN   | no   | C    | **Indefinite loop**<br>( -- )<br>Marks the end of an indefinite loop opened by the matching BEGIN.                       |
| FLOAT   | no   | IC   | **Convert to floating point value**<br>(x -- n)<br>Converts a value on the top of the stack to a floating point value.   |
| HEX     | no   | IC   | **Set the numeric conversion radix to sixteen**<br>( -- )<br>Sets the numeric conversion radix to sixteen (hexadecimal). |
| INT     | no   | IC   | **Convert to integer value**<br>(x -- n)<br>Converts a value on the top of the stack to an integer value.                |
| ?INT    | no   | IC   | **Checks if x is an integer value**<br>(x -- flag)<br>Sets flag to true, if x is an integer value.                       |
| ?FLOAT  | no   | IC   | **Checks if x is a floating point value**<br>(x -- flag)<br>Sets flag to true, if x is a floating point value.           |
| ?STRING | no   | IC   | **Checks if x is a string value**<br>(x -- flag)<br>Sets flag to true, if x is a string value.                           |
