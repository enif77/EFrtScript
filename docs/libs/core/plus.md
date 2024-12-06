# +

- Library: CORE
- Common name: `+`
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(n1 n2 -- n3)`

Adds `n1` and `n2` and leaves the sum `n3` on the stack.

See the [Forth standard description](https://forth-standard.org/standard/core/Plus) for more info.

## Data Type Handling

- **String Values**: If either `n1` or `n2` is a string, the result is the concatenation of the two strings.
- **Floating Point Values**: If either `n1` or `n2` is a floating point number, the result is the sum of the two floating point numbers.
- **Integer Values**: If both `n1` and `n2` are integers, the result is the sum of the two integers. If an overflow occurs, the values are treated as floating point numbers and their sum is calculated.

## Usage Examples

### Integer Addition

```forth
5 3 +  \ Result: 8
```

### Floating Point Addition

```forth
5.0 3.2 +  \ Result: 8.2
5 3.2 +    \ Result: 8.2
5.2 3 +    \ Result: 8.2
```

### String Concatenation

```forth
S" Hello, " S" world!" +  \ Result: "Hello, world!"
S" str" 123 +             \ Result: "str123"
456 S" str" +             \ Result: "456str"
S" PI = " 3.14 +          \ Result: "PI = 3.14"
```
