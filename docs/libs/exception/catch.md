# CATCH

- Library: EXCEPTION
- Common name: catch
- Is immediate: yes
- Mode: compilation only
- Stack image: `(i * x xt -- j * x 0 | i * x n)`

Push an exception frame on the exception stack and then execute the execution token xt (as with [EXECUTE](libs/core/execute.md))
in such a way that control can be transferred to a point just after **CATCH** if [TROW](libs/exception/throw.md) is executed during
the execution of xt.

If the execution of xt completes normally (i.e., the exception frame pushed by this **CATCH** is not popped
by an execution of [TROW](libs/exception/throw.md)) pop the exception frame and return zero on top of the data stack, above whatever
stack items would have been returned by xt [EXECUTE](libs/core/execute.md). Otherwise, the remainder of the execution semantics
are given by [TROW](libs/exception/throw.md).

See the [Forth standard description](https://forth-standard.org/standard/exception/CATCH) for more info.
