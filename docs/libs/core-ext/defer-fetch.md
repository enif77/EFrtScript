# DEFER@

- Library: CORE-EXT
- Common name: defer@
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `( xt1 -- xt2 )`

xt2 is the execution token xt1 is set to execute. An ambiguous condition exists if xt1 is not the execution token
of a word defined by DEFER, or if xt1 has not been set to execute a xt.

See the [Forth standard description](https://forth-standard.org/standard/core/DEFERFetch) for more info.
