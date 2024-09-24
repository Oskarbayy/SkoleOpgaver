/*
     Opgave 1 - Opret Databasen og to tabeller
    Du er blevet bedt om at oprette en database til en boghandel. Databasen skal hedde "DB_Boghandel" og der skal
    oprettes to tabeller med navnet "Books" og "Kunder". Tabellerne skal have følgende kolonner:
 */

DROP DATABASE IF EXISTS DB_BogHandler;

CREATE DATABASE DB_BogHandler;

USE DB_BogHandler;

-- Create genres table
CREATE TABLE genres
(
    id    INT AUTO_INCREMENT PRIMARY KEY,
    genre VARCHAR(255) NOT NULL
);

CREATE TABLE persons
(
    id      INT AUTO_INCREMENT PRIMARY KEY,
    name    VARCHAR(255) NOT NULL,
    address VARCHAR(255),
    phone   INT
);

CREATE TABLE books
(
    id       INT AUTO_INCREMENT PRIMARY KEY,
    title    VARCHAR(255)   NOT NULL,
    authorID INT            NOT NULL,
    genreID  INT            NOT NULL,            -- Create genreID column
    price    DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (authorID) REFERENCES persons (id),
    FOREIGN KEY (genreID) REFERENCES genres (id) -- Set genreID as a foreign key
);


CREATE TABLE customers
(
    id            INT AUTO_INCREMENT PRIMARY KEY,
    personID      INT NOT NULL,
    bought_books  INT NOT NULL,
    favorite_book INT,
    FOREIGN KEY (favorite_book) REFERENCES books (id),
    FOREIGN KEY (personID) REFERENCES persons (id)
);

/*
     Opgave 2 – INSERT INTO Syntaks
    - Indsæt mindst tre rækker med bogdata i tabellen Books.
    - Indsæt mindst tre rækker med kundedata i tabellen Kunder.
 */
DELIMITER //

CREATE PROCEDURE AddBook(
    IN book_title VARCHAR(255),
    IN book_genre VARCHAR(255),
    IN book_price DECIMAL(10, 2),
    IN author_name VARCHAR(255)
)
BEGIN
    DECLARE author_id INT;
    DECLARE genre_id INT;

    -- Lookup the author's ID from the persons table
    SELECT id
    INTO author_id
    FROM persons
    WHERE name = author_name
    LIMIT 1;

    -- Lookup the genre's ID from the genres table
    SELECT id
    INTO genre_id
    FROM genres
    WHERE genre = book_genre
    LIMIT 1;

    -- Insert the book into the books table with the correct author_id and genre_id
    INSERT INTO books (title, authorID, genreID, price)
    VALUES (book_title, author_id, genre_id, book_price);
END //

DELIMITER ;

INSERT INTO genres (genre)
VALUES ('Fantasy'),
       ('Fiction'),
       ('Science Fiction'),
       ('Romance'),
       ('Classic');


INSERT INTO persons (name, address, phone)
VALUES ("J.K. Rowling", NULL, NULL),
       ("Harper Lee", NULL, NULL),
       ("George Orwell", NULL, NULL),
       ("Jane Austen", NULL, NULL),
       ("F. Scott Fitzgerald", NULL, NULL),
       ("J.R.R. Tolkien", NULL, NULL),
       ("J.D. Salinger", NULL, NULL);

CALL AddBook('Harry Potter', 'Fantasy', 190.99, 'J.K. Rowling');
CALL AddBook('To Kill a Mockingbird', 'Fiction', 140.99, 'Harper Lee');
CALL AddBook('1984', 'Science Fiction', 120.99, 'George Orwell');
CALL AddBook('Pride and Prejudice', 'Romance', 90.99, 'Jane Austen');
CALL AddBook('The Great Gatsby', 'Classic', 110.99, 'F. Scott Fitzgerald');
CALL AddBook('The Hobbit', 'Fantasy', 150.99, 'J.R.R. Tolkien');
CALL AddBook('The Catcher in the Rye', 'Fiction', 100.99, 'J.D. Salinger');
CALL AddBook('Harry Potter', 'Fantasy', 150.99, 'J.K. Rowling');
CALL AddBook('The Hobbit', 'Fantasy', 150.99, 'J.R.R. Tolkien');
CALL AddBook('Pride and Prejudice', 'Romance', 90.99, 'Jane Austen');


INSERT INTO customers (personID, bought_books, favorite_book)
VALUES (1, 3, NULL), -- J.K. Rowling
       (2, 2, NULL), -- Harper Lee
       (3, 4, NULL), -- George Orwell
       (4, 1, NULL), -- Jane Austen
       (5, 5, NULL), -- F. Scott Fitzgerald
       (6, 6, NULL), -- J.R.R. Tolkien
       (7, 1, NULL);
-- J.D. Salinger

/*
     Opgave 3 – Beskriv betydningen af NULL.
    Beskriv betydningen af NULL i sammenhæng med databaser. Forklar hvad NULL repræsenterer, og hvordan det
    adskiller sig fra andre værdier som f.eks. 0 eller en tom streng.
 */
--  NULL er når en variabel ikke har nogle form for værdi eller type
--  så man kan simpelhen ikke arbejde med den så nemt.

-- Test Normalization
select books.title AS "Titel", persons.name, persons.address, persons.phone
FROM books
         INNER JOIN persons
                    ON books.authorID = persons.id;
