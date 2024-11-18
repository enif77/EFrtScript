# :NONAME

- Library: CORE-EXT
- Common name: :noname
- Is immediate: yes
- Mode: interpretation and compilation
- Stack image: `( -- xt )`

( -- xt )
Like the : word, but creates an anonymous definition. The execution semantics of the new definition are the same
as those of :. It pushes the execution token for the new definition onto the stack.

Typical use:

```
DEFER print
:NONAME ( n -- ) . ; IS print
```

See the [Forth standard description](https://forth-standard.org/standard/core/ColonNONAME) for more info.
