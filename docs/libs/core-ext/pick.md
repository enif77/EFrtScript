# PICK

- Library: CORE-EXT
- Common name: pick
- Is immediate: no
- Mode: interpretation and compilation
- Stack image: `(xu ... x1 x0 u -- xu ... x1 x0 xu)`

Removes u. Copy the xu to the top of the stack. 0 PICK is equivalent to DUP. 1 PICK is equivalent to OVER.
