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
