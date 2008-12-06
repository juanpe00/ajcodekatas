member(X,[X|Y]).
member(X,[Y|Z]):-member(X,Z).
?- member(X,[a,b,c,d]), write(X), nl, fail.

