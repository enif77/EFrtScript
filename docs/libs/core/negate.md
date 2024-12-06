# NEGATE

- Library: CORE
- Common name: negate
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(n1 -- n2)`

Negates `n1` and leaves the result `n2` on the stack.

See the [Forth standard description](https://forth-standard.org/standard/core/NEGATE) for more info.

## Data Type Handling

- **Floating Point Values**: If `n1` is a floating point number, the result is the negation of the floating point number.
- **Integer Values**: If `n1` is an integer, the result is the negation of the integer. If an overflow occurs during negation, the value is treated as a floating point number and its negation is calculated.

## Usage Examples

### Integer Negation

```forth
5 negate   \ Result: -5
-5 negate  \ Result: 5
```

### Floating Point Negation

```forth
3.2 negate   \ Result: -3.2
-3.2 negate  \ Result: 3.2
```
