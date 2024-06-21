# ENDOF

- Library: CORE-EXT
- Common name: endof
- Is immediate: yes
- Mode: compilation
- Stack image: `( -- )`

Mark the end of the OF...ENDOF part of the CASE structure. Jumps to the coresponding CASE.

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

See the [Forth standard description](https://forth-standard.org/standard/core/ENDOF) for more info.
