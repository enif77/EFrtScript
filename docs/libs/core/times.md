# *

- Library: CORE
- Common name: `*`
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(n1 n2 -- n3)`

Multiplies `n1` and `n2` and leaves the product `n3` on the stack.

See the [Forth standard description](https://forth-standard.org/standard/core/Times) for more info.

## Data Type Handling

- **Floating Point Values**: If either `n1` or `n2` is a floating point number, the result is the product of the two floating point numbers.
- **Integer Values**: If both `n1` and `n2` are integers, the result is the product of the two integers. If an overflow occurs, the values are treated as floating point numbers and their product is calculated.

## Usage Examples

### Integer Multiplication

```forth
5 3 *  \ Result: 15
```

### Floating Point Multiplication

```forth
5.0 3.2 *  \ Result: 16.0
5 3.2 *    \ Result: 16.0
5.2 3 *    \ Result: 15.6
```
