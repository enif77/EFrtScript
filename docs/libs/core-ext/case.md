# CASE

- Library: CORE-EXT
- Common name: case
- Is immediate: yes
- Mode: compilation
- Stack image: `(n -- n)`

Mark the start of the CASE...OF...ENDOF...ENDCASE structure. Append the run-time semantics given below to the current definition.

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

See the [Forth standard description](https://forth-standard.org/standard/core/CASE) for more info.
