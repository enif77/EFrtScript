# -

- Library: CORE
- Common name: `-`
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(n1 n2 -- n3)`

Subtracts `n2` from `n1` and leaves the difference `n3` on the stack.

See the [Forth standard description](https://forth-standard.org/standard/core/Minus) for more info.

## Data Type Handling

- **Floating Point Values**: If either `n1` or `n2` is a floating point number, the result is the difference of the two floating point numbers.
- **Integer Values**: If both `n1` and `n2` are integers, the result is the difference of the two integers. If an underflow occurs, the values are treated as floating point numbers and their difference is calculated.

## Usage Examples

### Integer Subtraction

```forth
10 3 -  \ Result: 7
```

### Floating Point Subtraction

```forth
10.5 3.2 -  \ Result: 7.3
10 3.2 -    \ Result: 6.8
10.5 3 -    \ Result: 7.5
```
