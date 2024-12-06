# /

- Library: CORE
- Common name: `/`
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(n1 n2 -- n3)`

Divides `n1` by `n2` and leaves the quotient `n3` on the stack.

See the [Forth standard description](https://forth-standard.org/standard/core/Div) for more info.

## Data Type Handling

- **Floating Point Values**: If either `n1` or `n2` is a floating point number, the result is the quotient of the two floating point numbers. If `n2` is zero, a division by zero error is thrown.
- **Integer Values**: If both `n1` and `n2` are integers, the result is the quotient of the two integers. If `n2` is zero, a division by zero error is thrown.
