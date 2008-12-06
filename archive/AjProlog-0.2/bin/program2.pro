grandparent(X,Y) :- parent(X,Z), parent(Z,Y).
parent(john,mary).
parent(mary,jim).
parent(jim,adam).
?- grandparent(X,Y), write(Y), write(' is the grandparent of '),  write(X), nl, fail.
