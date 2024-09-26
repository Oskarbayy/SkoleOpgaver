DROP DATABASE IF EXISTS CBZhotels;

CREATE DATABASE CBZhotels;

USE CBZhotels;

-- Countries Table
CREATE TABLE countries
(
    id   INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    code VARCHAR(3)  NOT NULL
);

-- States Table
CREATE TABLE states
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    name       VARCHAR(50) NOT NULL,
    country_id INT         NOT NULL,
    FOREIGN KEY (country_id) REFERENCES countries (id)
);

-- Cities Table
CREATE TABLE cities
(
    id          INT PRIMARY KEY AUTO_INCREMENT,
    name        VARCHAR(50) NOT NULL,
    postal_code VARCHAR(10) NOT NULL,
    state_id    INT         NOT NULL,
    FOREIGN KEY (state_id) REFERENCES states (id)
);

-- Addresses Table
CREATE TABLE addresses
(
    id      INT PRIMARY KEY AUTO_INCREMENT,
    street  VARCHAR(50) NOT NULL,
    city_id INT         NOT NULL,
    FOREIGN KEY (city_id) REFERENCES cities (id)
);

-- Hotels Table
CREATE TABLE hotels
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    name       VARCHAR(30) NOT NULL,
    address_id INT         NOT NULL,
    FOREIGN KEY (address_id) REFERENCES addresses (id)
);

-- Rooms Table
CREATE TABLE rooms
(
    id          INT PRIMARY KEY AUTO_INCREMENT,
    hotel_id    INT           NOT NULL,
    type        VARCHAR(1)    NOT NULL,
    room_number INT           NOT NULL,
    price       DECIMAL(6, 2) NOT NULL,
    FOREIGN KEY (hotel_id) REFERENCES hotels (id)
);

-- Persons Table
CREATE TABLE persons
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name  VARCHAR(50) NOT NULL,
    phone      VARCHAR(15) NOT NULL,
    email      VARCHAR(50) NOT NULL,
    address_id INT         NOT NULL,
    FOREIGN KEY (address_id) REFERENCES addresses (id)
);

-- Guests Table
CREATE TABLE guests
(
    id            INT PRIMARY KEY AUTO_INCREMENT,
    person_id     INT NOT NULL,
    note          TEXT,
    fdm_member_id INT,
    FOREIGN KEY (person_id) REFERENCES persons (id)
);

-- Booking Sources Table
CREATE TABLE booking_sources
(
    id        INT PRIMARY KEY AUTO_INCREMENT,
    type_name VARCHAR(50) NOT NULL
);

-- Booking States Table
CREATE TABLE booking_states
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    state_name VARCHAR(50) NOT NULL
);

-- Bookings Table
CREATE TABLE bookings
(
    id                INT PRIMARY KEY AUTO_INCREMENT,
    guest_id          INT            NOT NULL,
    room_id           INT            NOT NULL,
    start_date        DATE           NOT NULL,
    end_date          DATE           NOT NULL,
    cancel_date       DATE,
    total_price       DECIMAL(10, 2) NOT NULL,
    creation_date     DATE           NOT NULL,
    booking_source_id INT            NOT NULL,
    state_id          INT            NOT NULL,
    FOREIGN KEY (guest_id) REFERENCES guests (id),
    FOREIGN KEY (room_id) REFERENCES rooms (id),
    FOREIGN KEY (booking_source_id) REFERENCES booking_sources (id),
    FOREIGN KEY (state_id) REFERENCES booking_states (id)
);

-- Staff Positions Table
CREATE TABLE staff_positions
(
    id            INT PRIMARY KEY AUTO_INCREMENT,
    position_name VARCHAR(50) NOT NULL
);

-- Hotel Staff Table
CREATE TABLE hotel_staff
(
    id              INT PRIMARY KEY AUTO_INCREMENT,
    person_id       INT  NOT NULL,
    hotel_id        INT  NOT NULL,
    position_id     INT  NOT NULL,
    hire_date       DATE NOT NULL,
    terminated_date DATE,
    FOREIGN KEY (person_id) REFERENCES persons (id),
    FOREIGN KEY (hotel_id) REFERENCES hotels (id),
    FOREIGN KEY (position_id) REFERENCES staff_positions (id)
);

-- Discounts Table
CREATE TABLE discounts
(
    id           INT PRIMARY KEY AUTO_INCREMENT,
    name         TEXT NOT NULL,
    booking_id   INT  NOT NULL,
    amount_flat  DECIMAL(10, 2),
    amount_multi DECIMAL(5, 2),
    FOREIGN KEY (booking_id) REFERENCES bookings (id)
);

--
CREATE TABLE conference_centers
(
    id        INT PRIMARY KEY AUTO_INCREMENT,
    hotel_id  INT         NOT NULL,
    name      VARCHAR(50) NOT NULL,
    capacity  INT         NOT NULL, -- Maximum number of guests
    equipment TEXT,                 -- Equipment available (e.g., "Projector, Sound system")
    FOREIGN KEY (hotel_id) REFERENCES hotels (id)
);

--
CREATE TABLE catering_options
(
    id          INT PRIMARY KEY AUTO_INCREMENT,
    name        VARCHAR(50) NOT NULL,
    description TEXT,
    price_per_person DECIMAL(10, 2) NOT NULL
);

--
CREATE TABLE conference_bookings
(
    id                    INT PRIMARY KEY AUTO_INCREMENT,
    guest_id              INT NOT NULL,
    conference_center_id   INT NOT NULL,
    start_date            DATE NOT NULL,
    end_date              DATE NOT NULL,
    num_guests            INT NOT NULL,
    equipment_requested   TEXT, -- Equipment requested by the customer
    customer_requests     TEXT, -- Any additional requests from the customer
    catering_option_id    INT,  -- Optional reference to the catering option
    total_price           DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (guest_id) REFERENCES guests(id),
    FOREIGN KEY (conference_center_id) REFERENCES conference_centers(id),
    FOREIGN KEY (catering_option_id) REFERENCES catering_options(id)
);

--
CREATE TABLE bike_types
(
    id   INT PRIMARY KEY AUTO_INCREMENT,
    type_name VARCHAR(50) NOT NULL -- E.g., "Electric bike", "Cargo bike"
);

--
CREATE TABLE bikes
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    type_id    INT NOT NULL,
    lock_code  VARCHAR(10) NOT NULL, -- The lock code for the bike
    available  TINYINT(1) DEFAULT 1, -- Whether the bike is available for rent (1 = yes, 0 = no)
    FOREIGN KEY (type_id) REFERENCES bike_types(id)
);

--
CREATE TABLE bike_rentals
(
    id        INT PRIMARY KEY AUTO_INCREMENT,
    guest_id  INT NOT NULL,
    bike_id   INT NOT NULL,
    start_date DATE NOT NULL,
    end_date   DATE NOT NULL,
    FOREIGN KEY (guest_id) REFERENCES guests(id),
    FOREIGN KEY (bike_id) REFERENCES bikes(id)
);


-- INSERTS
INSERT INTO staff_positions (position_name)
VALUES ('Housekeeping/Cleaning'),
       ('Manager'),
       ('Administration/Reception'),
       ('Service/Kitchen');

-- Insert data into countries table
INSERT INTO countries (name, code)
VALUES ('Country A', 'A1'),
       ('Country B', 'B1');

-- Insert data into states table
INSERT INTO states (name, country_id)
VALUES ('State A1', 1),
       ('State B1', 2);

-- Insert data into cities table
INSERT INTO cities (name, postal_code, state_id)
VALUES ('City A1', '12345', 1),
       ('City B1', '67890', 2);

-- Insert data into addresses table before inserting hotels
INSERT INTO addresses (street, city_id)
VALUES ('123 Main St', 1),
       ('456 Oak St', 2),
       ('789 Pine St', 1),
       ('101 Maple St', 2),
       ('202 Elm St', 1);


INSERT INTO hotels (name, address_id)
VALUES ('CBZ Hotel 1', 1),
       ('CBZ Hotel 2', 2),
       ('CBZ Hotel 3', 3),
       ('CBZ Hotel 4', 4),
       ('CBZ Hotel 5', 5);

DELIMITER //

CREATE PROCEDURE AddHotelStaff(
    IN hotelId INT,
    IN hireDate DATE,
    IN numHousekeeping INT,
    IN numManagers INT,
    IN numAdminReception INT,
    IN numServiceKitchen INT
)
BEGIN
    DECLARE i INT DEFAULT 1;

    -- Insert Housekeeping/Cleaning Staff
    WHILE i <= numHousekeeping
        DO
            -- Insert into persons table first
            INSERT INTO persons (first_name, last_name, phone, email, address_id)
            VALUES (CONCAT('Housekeeper', i), 'Lastname', '0000000000', CONCAT('housekeeper', i, '@hotel.com'),
                    hotelId);

            -- Use the last inserted person_id
            INSERT INTO hotel_staff (person_id, hotel_id, position_id, hire_date)
            VALUES (LAST_INSERT_ID(), hotelId, 1, hireDate); -- Position ID 1 for Housekeeping
            SET i = i + 1;
        END WHILE;

    -- Reset the counter
    SET i = 1;

    -- Insert Manager Staff
    WHILE i <= numManagers
        DO
            -- Insert into persons table first
            INSERT INTO persons (first_name, last_name, phone, email, address_id)
            VALUES (CONCAT('Manager', i), 'Lastname', '0000000000', CONCAT('manager', i, '@hotel.com'), hotelId);

            -- Use the last inserted person_id
            INSERT INTO hotel_staff (person_id, hotel_id, position_id, hire_date)
            VALUES (LAST_INSERT_ID(), hotelId, 2, hireDate); -- Position ID 2 for Managers
            SET i = i + 1;
        END WHILE;

    -- Reset the counter
    SET i = 1;

    -- Insert Administration/Reception Staff
    WHILE i <= numAdminReception
        DO
            -- Insert into persons table first
            INSERT INTO persons (first_name, last_name, phone, email, address_id)
            VALUES (CONCAT('Admin', i), 'Lastname', '0000000000', CONCAT('admin', i, '@hotel.com'), hotelId);

            -- Use the last inserted person_id
            INSERT INTO hotel_staff (person_id, hotel_id, position_id, hire_date)
            VALUES (LAST_INSERT_ID(), hotelId, 3, hireDate); -- Position ID 3 for Admin/Reception
            SET i = i + 1;
        END WHILE;

    -- Reset the counter
    SET i = 1;

    -- Insert Service/Kitchen Staff
    WHILE i <= numServiceKitchen
        DO
            -- Insert into persons table first
            INSERT INTO persons (first_name, last_name, phone, email, address_id)
            VALUES (CONCAT('Service', i), 'Lastname', '0000000000', CONCAT('service', i, '@hotel.com'), hotelId);

            -- Use the last inserted person_id
            INSERT INTO hotel_staff (person_id, hotel_id, position_id, hire_date)
            VALUES (LAST_INSERT_ID(), hotelId, 4, hireDate); -- Position ID 4 for Service/Kitchen
            SET i = i + 1;
        END WHILE;
END //

DELIMITER ;


-- Adding staff for CBZ Hotel 1
CALL AddHotelStaff(1, '2023-01-01', 3, 2, 8, 8);

-- Adding staff for CBZ Hotel 2
CALL AddHotelStaff(2, '2023-01-01', 3, 2, 8, 8);

-- Adding staff for CBZ Hotel 3
CALL AddHotelStaff(3, '2023-01-01', 3, 2, 8, 8);

-- Adding staff for CBZ Hotel 4
CALL AddHotelStaff(4, '2023-01-01', 3, 2, 8, 8);

-- Adding staff for CBZ Hotel 5
CALL AddHotelStaff(5, '2023-01-01', 3, 2, 8, 8);

ALTER TABLE bookings
    ADD COLUMN includes_conference TINYINT(1) DEFAULT 0;

-- Insert conference centers
INSERT INTO conference_centers (hotel_id, name, capacity, equipment)
VALUES (1, 'The Pope', 500, 'Projector, Sound system, Microphones, Stage');

-- Insert catering options
INSERT INTO catering_options (name, description, price_per_person)
VALUES ('Buffet', 'Buffet style catering with a variety of international dishes', 30.00),
       ('Three-course meal', 'A formal three-course meal with vegetarian options', 50.00);

-- Insert a person for Pope Francis
INSERT INTO persons (first_name, last_name, phone, email, address_id)
VALUES ('Pope', 'Francis', '0000000000', 'popefrancis@vatican.va', 1);

-- Insert the guest into the guests table
INSERT INTO guests (person_id, note)
VALUES (LAST_INSERT_ID(), 'Special guest: Pope Francis');

-- Make a conference booking for Pope Francis
INSERT INTO conference_bookings
(guest_id, conference_center_id, start_date, end_date, num_guests, equipment_requested, customer_requests, catering_option_id, total_price)
VALUES
    (LAST_INSERT_ID(), 1, '2024-10-01', '2024-10-03', 300, 'Extra microphones, translation system', 'Private room for the Pope', 2, 15000.00);

-- Insert bike types
INSERT INTO bike_types (type_name)
VALUES ('Electric bike'),
       ('Cargo bike');

-- Insert bikes with lock codes
INSERT INTO bikes (type_id, lock_code)
VALUES (1, '1234'), -- Electric bike with lock code 1234
       (1, '2345'), -- Electric bike with lock code 2345
       (1, '3456'), -- Electric bike with lock code 3456
       (1, '4567'), -- Electric bike with lock code 4567
       (1, '5678'), -- Electric bike with lock code 5678
       (2, '6789'), -- Cargo bike with lock code 6789
       (2, '7890'), -- Cargo bike with lock code 7890
       (2, '8901'), -- Cargo bike with lock code 8901
       (2, '9012'), -- Cargo bike with lock code 9012
       (2, '0123'); -- Cargo bike with lock code 0123

-- Insert bike rental for Pope Francis
INSERT INTO bike_rentals (guest_id, bike_id, start_date, end_date)
VALUES (LAST_INSERT_ID(), 1, '2024-10-01', '2024-10-03'); -- Rent an electric bike with lock code '1234'
