
\connect userdb

CREATE TABLE users
(
    id serial PRIMARY KEY,
    name  VARCHAR (50)  NOT NULL,
    email  VARCHAR (100)  NOT NULL,
    salary NUMERIC NOT NULL,
    expenses NUMERIC NOT NULL,
);

ALTER TABLE users OWNER TO useradmin;

Insert into users(name,email) values('Title 1','Email1',1000,0);
Insert into users(name,email) values('Title 2','Email2',2000,1000);
Insert into users(name,email) values('Title 3','Email3',3000,1000);
Insert into users(name,email) values('Title 4','Email4',5000,100);
