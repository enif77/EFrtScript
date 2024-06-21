# OF

- Library: CORE-EXT
- Common name: of
- Is immediate: yes
- Mode: compilation
- Stack image: `(n1 n2 -- )`

If the two values on the stack are not equal, discard the top value and continue execution at the location specified by the consumer of of-sys, e.g., following the next ENDOF. Otherwise, discard both values and continue execution in line.

Typical use:

```
: X ...
   CASE
   test1 OF ... ENDOF
   testn OF ... ENDOF
   ... ( default )
   ENDCASE ...
;
```

See the [Forth standard description](https://forth-standard.org/standard/core/OF) for more info.
