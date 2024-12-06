# CONSTANT

- Library: CORE
- Common name: constant
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: Compilation `( x "<spaces>name" -- )`, interpretation `( -- x)`

Skip leading space delimiters. Parse name delimited by a space. Create a definition for name with the execution semantics defined below.

name is referred to as a "constant".

## name Execution

   `( -- x )`

   Place x on the stack.

## Rationale

   Typical use: `... DECIMAL 10 CONSTANT TEN ...`.

## Testing

```forth
   T{ 123 CONSTANT X123 -> }T
   T{ X123 -> 123 }T
   T{ : EQU CONSTANT ; -> }T
   T{ X123 EQU Y123 -> }T
   T{ Y123 -> 123 }T
```

See the [Forth standard description](https://forth-standard.org/standard/core/CONSTANT) for more info.