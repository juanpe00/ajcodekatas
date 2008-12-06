insert(X,[],[X]) :- !.
insert(X,[Y|Z], [X,Y|Z]) :- X<Y, !.
insert(X,[Y|Z], [Y|W]) :- insert(X,Z,W).
?- insert(1,[],X), write(' X = '), write(X), nl.
?- insert(1,[2,3],X),  write(' X = '), write(X), nl.
?- insert(2,[1,3],X),  write(' X = '), write(X), nl.
?- insert(3,[1,2],X),  write(' X = '), write(X), nl.

