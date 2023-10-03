# TOOLS

Various helper words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes, S = suspended compilation only (not available in I, C or IC).
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack - and description of the word itself.

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| BYE  | no   | IC   | **Terminate execution**<br>( -- )<br>Return control to the host operating system, if any. |
