ALTER SEQUENCE IF EXISTS event_id_seq RESTART WITH 1;
ALTER SEQUENCE IF EXISTS account_id_seq RESTART WITH 1;

-- Dodanie danych do tabeli "account"
INSERT INTO account ("name", "password", "email", "phone_number", "profile_image", "active_account", "account_type") 
VALUES 
('John Doe', 'password123', 'john@example.com', '123-456-789', 'john_profile.jpg', true, 0),
('Alice Smith', 'password456', 'alice@example.com', '987-654-321', 'alice_profile.jpg', true, 0),
('Bob Johnson', 'password789', 'bob@example.com', '555-555-555','bo.jpg', true, 0),
('Event Moderator', 'password111', 'moderator@gmail.com', '111-222-333', 'mod.jpg', true, 1),
('Admin', 'admin', 'admin@gmail.com', '456-123-333', 'admin.jpg', true, 2);

INSERT INTO account ("name", "password", "email", "phone_number", "profile_image", "active_account", "account_type") 
VALUES 
('John Doe', 'test', 'test3.john@example.com', '123-456-789', 'john_profile.jpg', true, 0),
('Alice Smith', 'test', 'test3.alice@example.com', '987-654-321', 'alice_profile.jpg', true, 0);

-- Dodanie danych do tabeli "event"

SELECT * FROM event;
SELECT * FROM account a ;
-- Dodanie danych do tabeli "ticket"
INSERT INTO ticket ("name", "date", "type", "description", "price", "event_id") 
VALUES 
('Gwarki', '2024-09-06 10:00:00', 'Standard', 'Bilet standardowy na wydarzenie Gwarki', 0.00, 1),
('Czerwone Gitary VIP', '2024-10-01 18:30:00', 'VIP', 'Bilet VIP na koncert Czerwone Gitary', 200.00, 2),
('Czerwone Gitary Standard', '2024-10-01 19:00:00', 'Standard', 'Bilet standardowy na koncert Czerwone Gitary', 100.00, 2),
('Kortez VIP', '2024-05-20 17:30:00', 'VIP', 'Bilet VIP na koncert Korteza', 180.00, 3),
('Kortez Standard', '2024-05-20 18:00:00', 'Standard', 'Bilet standardowy na koncert Korteza', 90.00, 3),
('Myslovitz VIP', '2024-06-15 18:30:00', 'VIP', 'Bilet VIP na koncert Myslovitz', 170.00, 4),
('Myslovitz Standard', '2024-06-15 19:00:00', 'Standard', 'Bilet standardowy na koncert Myslovitz', 85.00, 4),
('The Legend of Rock Symphonic VIP', '2024-11-05 19:30:00', 'VIP', 'Bilet VIP na koncert The Legend of Rock Symphonic', 210.00, 5),
('The Legend of Rock Symphonic Standard', '2024-11-05 20:00:00', 'Standard', 'Bilet standardowy na koncert The Legend of Rock Symphonic', 105.00, 5),
('Mamma Mia VIP', '2024-07-10 18:30:00', 'VIP', 'Bilet VIP na koncert Mamma Mia - Tribute to Abba', 190.00, 6),
('Mamma Mia Standard', '2024-07-10 19:00:00', 'Standard', 'Bilet standardowy na koncert Mamma Mia - Tribute to Abba', 95.00, 6);
