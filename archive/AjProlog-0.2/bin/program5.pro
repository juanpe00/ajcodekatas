fibo(1,1) :- !.
fibo(2,2) :- !.
fibo(N,X) :- 1 < N, N1 is N-1, N2 is N-2, fibo(N1,X1), fibo(N2,X2), X is X1+X2.
?- fibo(1,X), write(X), nl.
?- fibo(2,X), write(X), nl.
?- fibo(3,X), write(X), nl.
?- fibo(4,X), write(X), nl.
?- fibo(10,X), write(X), nl.
