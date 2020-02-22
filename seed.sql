
\connect userdb

CREATE TABLE users
(
    id serial PRIMARY KEY,
    name  VARCHAR (50)  NOT NULL,
    email  VARCHAR (100)  NOT NULL,
    salary NUMERIC NOT NULL,
    expenses NUMERIC NOT NULL
);

ALTER TABLE users OWNER TO useradmin;