# TYPE

- Library: CORE
- Common name: type
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(x -- )`

Displays `x`. `x` may be a number or a string. Integer numbers are displayed using the current numeric conversion radix.
Floating point numbers are displayed using free field format.

See the [Forth standard description](https://forth-standard.org/standard/core/TYPE) for more info.

## Usage Examples

### Print a String

```forth
S" Hello, world!" TYPE  \ Output: Hello, world!
```

### Print an Integer

```forth
123 TYPE  \ Output: 123
```

### Print a Floating Point Number

```forth
3.14 TYPE  \ Output: 3.14
```
