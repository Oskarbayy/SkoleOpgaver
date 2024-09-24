DROP DATABASE IF EXISTS CBZhotels;

CREATE DATABASE CBZhotels;

USE CBZhotels;

CREATE TABLE hotels (
    id INT AUTO_INCREMENT PRIMARY KEY,
	name varchar(30) NOT NULL,
    address varchar(50) NOT NULL
);

CREATE TABLE rooms (
    id INT AUTO_INCREMENT PRIMARY KEY,
	hotel_id INT NOT NULL,
    name VARCHAR(5) NOT NULL,
    pris DECIMAL(4,0) NOT NULL,
    FOREIGN KEY (hotel_id) REFERENCES hotels(ID)
);