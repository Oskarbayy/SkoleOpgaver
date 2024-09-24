/*
     Opgave 1 - Opret Databasen og to tabeller
    Du er blevet bedt om at oprette en database til en boghandel. Databasen skal hedde "DB_Boghandel" og der skal
    oprettes to tabeller med navnet "Books" og "Kunder". Tabellerne skal have følgende kolonner:
 */

DROP DATABASE IF EXISTS DB_BogHandler;

CREATE DATABASE DB_BogHandler;

USE DB_BogHandler;

CREATE TABLE books
(
    id     int AUTO_INCREMENT PRIMARY KEY,
    title  VARCHAR(255)   NOT NULL,
    author VARCHAR(255),
    genre  VARCHAR(255)   NOT NULL,
    price  DECIMAL(10, 2) NOT NULL
);

CREATE TABLE customers
(
    id            INT AUTO_INCREMENT PRIMARY KEY,
    name          VARCHAR(255) NOT NULL,
    address       VARCHAR(255) NOT NULL,
    phone         int          NOT NULL,
    bought_books  INT          NOT NULL,
    favorite_book INT,
    FOREIGN KEY (favorite_book) REFERENCES books (id)
);

/*
     Opgave 2 – INSERT INTO Syntaks
    - Indsæt mindst tre rækker med bogdata i tabellen Books.
    - Indsæt mindst tre rækker med kundedata i tabellen Kunder.
 */

INSERT INTO books (title, author, genre, price)
VALUES ("The Great Gatsby", "Toby", "Thriller", 1000),
       ("Harry Potter 1", "Toby", "Fantasy", 1000),
       ("Harry Potter 1", "Bob Fischer", "Fantasy", 1000);

-- Make null author to check in opgave 4
INSERT INTO books (title, genre, price)
VALUES ("Harry Potter 1", "Fantasy", 1000);


INSERT INTO customers (name, address, phone, bought_books)
VALUES ("Bob Fischer", "Mælkevejen", 82310562, 1),
       ("Bob 2", "Mælkevejen", 82310562, 1),
       ("Mark", "Mælkevejen", 82310562, 1);

/*
     Opgave 3 – Beskriv betydningen af NULL.
    Beskriv betydningen af NULL i sammenhæng med databaser. Forklar hvad NULL repræsenterer, og hvordan det
    adskiller sig fra andre værdier som f.eks. 0 eller en tom streng.
 */
--  NULL er når en variabel ikke har nogle form for værdi eller type
--  så man kan simpelhen ikke arbejde med den så nemt.


/*
     Opgave 4 – Anvend NULL.
    Skriv en SQL-forespørgsel for at hente titlen på alle bøger sammen med deres forfattere (hvis de har en forfatter) eller
    NULL (hvis de ikke har en forfatter)
 */
SELECT title, author
FROM books;

/*
     Opgave 5 – SQL UPDATE.
    Du har bemærket, at prisen på bogen med titlen "The Great Gatsby" er forkert i databasen. Den korrekte pris er
    30,00kr. Din opgave er at opdatere prisen på denne bog i databasen.
*/
UPDATE books
SET price = 30
WHERE title = "The Great Gatsby";

select *
FROM books;

/*
     Opgave 6 - Select statement (henter alle data)
    Du har en tabel kaldet " Books" med kolonnerne "ID", " Titel ", " Forfatter" og " Pris".
    Brug SELECT-udtrykket til at hente navnene på alle forfattere fra tabellen.
 */
SELECT author
from books;

/*
     Opgave 7 - Select statement (henter specifikke data)
    Du har en tabel kaldet " Books " med kolonnerne ID", " Titel ", " Forfatter" og " Pris".
    Brug SELECT-udtrykket til at hente ”Titel” og ”Forfatter” fra tabellen Books.
 */
SELECT title, author
FROM books;

/*
    Opgave 8 - SQL JOIN
        1. Beskriv med dine egne ord hvad et SQL JOIN er, og hvordan det bliver brugt?
            SQL Join er at sætte to tables sammen i et på forskellige måder
        2. Beskriv forskellen på et LEFT JOIN, RIGHT JOIN, INNER JOIN og FULL JOIN
            Left Join er at sætte to tables sammen med venstre bliver altid vist.
            Right Join er det sammen men højre bliver altid vist.
            Inner Join er den kun viser de table dataer som har begge sider
            Full Join er Right og Left på samme tid og inkludere dem begge i samme table
 */

/*
     Opgave 9 - LEFT JOIN
    Din opgave er at oprette en SQL-forespørgsel, der kombinerer data fra begge tabeller ved hjælp af en
    LEFT JOIN. 
 */
SELECT *
FROM books
         LEFT JOIN customers
                   ON books.author = customers.name;

/*
     Opgave 10 - RIGHT JOIN
    Din opgave er at oprette en SQL-forespørgsel, der kombinerer data fra begge tabeller ved hjælp af en RIGHT JOIN
 */
SELECT *
FROM books
         RIGHT JOIN customers
                    ON books.author = customers.name;

/*
     Opgave 11 - FULL JOIN
    Din opgave er at skabe en SQL-forespørgsel, der forener data fra begge tabeller ved hjælp af en FULL JOIN. Formålet
    er at få vist titel, pris, forfatter og kundens navn for alle bøger og kunder i databasen.
 */
-- FULL OUTER JOIN findes ikke så jeg kombinere left join og right join
SELECT *
FROM books
         LEFT JOIN customers
                   ON books.author = customers.name

UNION

SELECT *
FROM books
         RIGHT JOIN customers
                    ON books.author = customers.name;

/*
     Opgave 12 - FULL OUTER JOIN
    Din opgave er at skabe en SQL-forespørgsel, der kombinerer data fra begge tabeller ved hjælp af en
    FULL OUTER JOIN.
 */
--  Det er det præcis samme som den over bare en anden syntax.
--  FULL OUTER JOIN og FULL JOIN er det samme.

/*
     Opgave 13 - INNER JOIN
   Din opgave er at skabe en SQL-forespørgsel, der kombinerer data fra begge tabeller ved hjælp af en
   INNER JOIN.
 */

SELECT *
FROM books
         INNER JOIN customers
                    ON books.author = customers.name;

/*
     Opgave 14 - SQL GROUP BY
    Din opgave er at oprette en SQL-forespørgsel, der bruger GROUP BY til at gruppere bøger efter deres genre.
    Resultatet skal vise hver genre og det samlede antal bøger i hver genre.
 */
SELECT genre, COUNT(*) AS total_books
FROM books
GROUP BY genre;

/*
     Opgave 15 - SQL STORED PROCEDURE
    Din opgave er at oprette en SQL Stored Procedure, der tager imod en genre som parameter og returnerer alle bøger i
    den specificerede genre.
 */

DELIMITER //

CREATE PROCEDURE GetBooksByGenre(IN genre_param VARCHAR(255))
BEGIN
    SELECT *
    FROM books
    WHERE genre = genre_param;
END //

DELIMITER ;

CALL GetBooksByGenre("Fantasy");

/*
     Opgave 16 - PRIMARY KEY (Primærnøgle)
    Din opgave er at oprette en SQL-forespørgsel, der opretter tabellen 
    "bøger" med en PRIMARY KEY-kolonne.
*/
--  Se create table i opgave 1. 

/*
     Opgave 17 - FOREIGN KEY (Fremmednøglen)
    Din opgave er at oprette en SQL-forespørgsel, der opretter både tabellen "bøger" og tabellen "kunder" med en
    FOREIGN KEY-relation mellem dem
 */
UPDATE customers
SET favorite_book = 1
WHERE id = 1;

SELECT customers.name as "Customer Name",
       books.title    as "Favorite Book"
FROM customers
         INNER JOIN books
                    ON customers.favorite_book = books.id