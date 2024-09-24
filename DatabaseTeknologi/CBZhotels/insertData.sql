INSERT INTO hotels (name, address)
VALUES 
    ('The Pope', 'Vatikangade 1, 1111 Bispeborg'),
    ('Lucky Star', 'Bredgade 12, 2222 Hjemby'),
    ('Discount', 'Billigvej 7, 3333 Lilleby'),
    ('deLuxe', 'Kapital Avenue 99, 4444 Borgerslev'),
    ('Discount', 'Billiggade 12, 6666 Roslev');


INSERT INTO rooms (hotel_id, type, pris, room_number)
VALUES 
    (1, 'D', 200, 1),  -- Room 1 in "The Pope"
    (1, 'D', 200, 2),  -- Room 2 in "The Pope"
    (1, 'S', 150, 11), -- Room 11 in "The Pope"
    (1, 'F', 220, 21), -- Room 21 in "The Pope"
    (2, 'D', 230, 1),  -- Room 1 in "Lucky Star"
    (2, 'D', 230, 2),  -- Room 2 in "Lucky Star"
    (2, 'S', 180, 11), -- Room 11 in "Lucky Star"
    (2, 'F', 300, 21), -- Room 21 in "Lucky Star"
    (3, 'D', 175, 1),  -- Room 1 in "Discount" in Lilleby
    (5, 'D', 170, 2);  -- Room 2 in "Discount" in Roslev
