TRUNCATE TABLE distributor RESTART IDENTITY CASCADE;

INSERT INTO distributor ("name", "search_adress") 
VALUES 
('eBilet', 'https://www.ebilet.pl/wydarzenia?text='),
('Going.', 'https://goingapp.pl/szukaj?query='),
('KupBilecik', 'https://www.kupbilecik.pl/szukaj/?q='),
('eventim', 'https://www.eventim.pl/search/?affiliate=PLE&searchterm='),
('Empik Bilety', 'https://empikbilety.pl/szukaj?query='),
('Bilety24', 'https://www.bilety24.pl/szukaj?search=');
