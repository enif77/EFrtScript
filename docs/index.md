# EFrt Script

A Forth language implementation with scripting languages twist.

## Forth differences

- Selected words are implemented only.
- Stack, return stack and the heap contains value objects (integers, strings etc.).
- No "direct" memory access.
- No low level stuff.

## Stacks

- Data stack: Main stack for user data. Holds all values.
- Return stack: Stack for (mostly) interpreter internal use. Holds values. Mostly integers.
- Exception stack: Not accessible for users. Its used internally by THROW and CATCH words.
- Input source stack: Stack for keeping the inputs. Used by the EVALUATE word.

## Numbers

Unknown words are parsed as numbers, following C/C# conventions. 
Any problem with parsing a number makes the parser to return a word, which is unknown, and it leads to an error.

```
unsigned-single-cell-integer :: digit-sequence .
unsigned-double-cell-integer :: digit-sequence ( 'L' | 'l' ) .
unsigned-floating-point-number :: digit-sequence ( 'D' | 'd' ) .
unsigned-number :: unsigned-single-cell-integer | unsigned-double-cell-integer | unsigned-floating-point-number .
unsigned-floating-point-number :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
scale-factor :: [ sign ] digit-sequence .
fractional-part :: digit-sequence .
sign :: '+' | '-' .
```

Note: Forth style numbers parsing using the BASE variable is implemented also.

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

NOTE: The `\U` escape sequence will be added later.

## Libraries

 * [CORE](lib-core.md)
 * [CORE-EXT](lib-core-ext.md)
 * [EXCEPTION](lib-exception.md)
 * [STRING](lib-string.md)
 * [TOOLS](lib-tools.md)
 
## EXTRA Words

Various helper words defined by the EFrt Script app.

### Words

| Name | Imm. | Mode | Description                                                                 |
|------|------|------|-----------------------------------------------------------------------------|
| READ-ALL-TEXT  | no   | IC   | **Read all text**<br>(s -- s)<br>Reads contents of a text file into a string on the stack. |
| TRACE  | no   | IC   | **Tracing mode**<br>(n -- )<br>Turns tracing mode on or off. Tracing lists all words, that will be executed. |

## Debugging

Simple debugging is possible - word `TRACE` can turn listing of executed words on/off.

```
1 TRACE  \ Turns executed words listing on.
0 TRACE  \ Turns executed words listing on.

: a 123 . CR ;
1 TRACE
a

Trace: A
Trace: LITERAL
Trace: .
123 Trace: CR

Trace: ExitControlWord
```

Interpreter has two public events - `ExecutingWord` and `WordExecuted` - to notify a caller that a word is going to be executed and
that a word was executed. The `ExecutingWord` event is fired before a word is executed and the `WordExecuted` event is fired after a word is executed.
