human(socrates).
human(aristotle).
mortal(X) :- human(X).
?- mortal(X), write(X), nl, fail.
