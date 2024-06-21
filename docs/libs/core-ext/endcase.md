# ENDCASE

- Library: CORE-EXT
- Common name: endcase
- Is immediate: yes
- Mode: compilation
- Stack image: `(n -- )`

Mark the end of the CASE...OF...ENDOF...ENDCASE structure. Use case-sys to resolve the entire structure.
Discard the case selector x and continue execution.

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

See the [Forth standard description](https://forth-standard.org/standard/core/ENDCASE) for more info.
