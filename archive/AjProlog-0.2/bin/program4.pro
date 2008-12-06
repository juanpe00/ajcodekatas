human(socrates).
human(aristotle).
?- human(X), human(Y), write(X), write(','), write(Y), nl, fail.
?- human(X), human(Y), X=Y, write(X), write('='), write(Y), nl, fail.
?- human(X), human(Y), X\=Y, write(X), write('\='), write(Y), nl, fail.
