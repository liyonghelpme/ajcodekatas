; True
T = \xy.x
; False
F = \xy.y
; And
AND = \ab.abF
; If Then Else
IFTHENELSE = \x.x
; Natural numbers
0 = \fx.x
1 = \fx.fx
2 = \fx.f(fx)
3 = \fx.f(f(fx))
; Succesor
SUCC = \nfx.f(nfx)
; Add
ADD = \nmfx.nf(mfx)
; Multiply
MULT = \nmf.n(mf)
; Is Zero
ISZERO = \nxy.n(\z.y)x
; Predecessor
PRED = \nfx.n(\gh.h(gf))(\u.x)(\u.u)
; Pair Functions
PAIR = \xyf.fxy
FIRST = \p.pT
SECOND = \p.pF
NIL = \x.T
NULL = \p.p(\xy.F)
; Fixed Point
A = \xy.y(xxy)
Y = A A
; Factorial
FACT = Y (\f.\n.IFTHENELSE (ISZERO n) (1) (MULT n (f (PRED n)))))
