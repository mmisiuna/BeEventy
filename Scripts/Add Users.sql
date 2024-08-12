TRUNCATE TABLE account RESTART IDENTITY CASCADE;

ALTER SEQUENCE IF EXISTS event_id_seq RESTART WITH 1;
ALTER SEQUENCE IF EXISTS account_id_seq RESTART WITH 1;

INSERT INTO account ("name", "password", "email", "phone_number", "profile_image", "active_account", "account_type") 
VALUES 
('John Doe', 'password123', 'john@example.com', '123-456-789', 'john_profile.jpg', true, 0),
('Alice Smith', 'password456', 'alice@example.com', '987-654-321', 'alice_profile.jpg', true, 0),
('Bob Johnson', 'password789', 'bob@example.com', '555-555-555','bo.jpg', true, 0),
('Event Moderator', 'password111', 'moderator@gmail.com', '111-222-333', 'mod.jpg', true, 1),
('Admin', 'admin', 'admin@gmail.com', '456-123-333', 'admin.jpg', true, 2),
('John Third', 'test', 'test3.Third@example.com', '123-456-789', 'third.jpg', true, 0),
('Alice Third', 'test', 'test3.alice@example.com', '987-654-321', 'alice_profile.jpg', true, 0),
('Filip Kostic', 'qwerty', 'kostic@cmr.com', '441-267-435', 'kostic.jpg', true, 0),
('Maxwell Crena', 'crena', 'maxwell.crena@gmail.com', '863-901-471', 'crena.png', true, 0),
('EventEnjoyer', 'qwerty', 'ee@example.com', '311-413-900','ee.jpg', true, 0),
('TrueFan123', '123', 'aaa@wp.pl', '802-789-008','aaa.PNG', true, 0),
('Zira', 'zira', 'zira.nuivenn@mitamza.pl', '054-808-339','zira.PNG', true, 1),
('renvo', 'renvo', 'renvo@onet.pl', '566-455-356', 'renvo.jpg', true, 1);

select profile_image from account a;
SELECT * FROM account a ;