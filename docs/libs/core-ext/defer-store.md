# DEFER!

- Library: CORE-EXT
- Common name: defer!
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `( xt2 xt1 -- )`

Set the word xt1 to execute xt2. An ambiguous condition exists if xt1 is not for a word defined by DEFER.

See the [Forth standard description](https://forth-standard.org/standard/core/DEFERStore) for more info.
