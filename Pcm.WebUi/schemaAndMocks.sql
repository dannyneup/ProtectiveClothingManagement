

-- Dummy-Daten für Tabelle "category"
INSERT INTO category (name)
VALUES ('Arbeitsbekleidung'), ('Arbeitsgeräte'), ('Elektrowerkzeuge'), ('Handwerkzeuge'), ('Arbeitsschuhe');

-- Dummy-Daten für Tabelle "item"
INSERT INTO item (name, manufacturer, comment, category_fk)
VALUES ('Latzhose 3000 Größe M', 'Engelbert Strauss', 'Robuste Arbeitslatzhose mit Taschen', 1),
       ('Latzhose 5000 Größe L', 'Engelbert Strauss', 'Hochwertige Arbeitslatzhose aus Baumwolle', 1),
       ('Gummistiefel Größe 42', 'Dunlop', 'Wasserdichte Gummistiefel mit Stahlkappe', 1),
       ('Tablet Samsung Galaxy Tab S6', 'Samsung', 'Leistungsstarkes Tablet für die Arbeit im Außendienst', 2),
       ('Laser-Entfernungsmesser', 'Bosch', 'Präziser Entfernungsmesser für die Vermessung von Räumen', 3),
       ('Winkelschleifer GWS 7-125', 'Bosch', 'Leistungsstarker Winkelschleifer für den Einsatz auf der Baustelle', 3),
       ('Schraubendreher-Set', 'Wiha', 'Hochwertiges Set mit verschiedenen Schraubendrehern', 4),
       ('Hammer 500g', 'Picard', 'Handlicher Hammer für den Einsatz in der Werkstatt', 4),
       ('Sicherheitsschuhe Größe 43', 'Puma', 'Atmungsaktive Sicherheitsschuhe mit Stahlkappe', 5),
       ('Sicherheitsschuhe Größe 45', 'Elten', 'Robuste Sicherheitsschuhe mit rutschfester Sohle', 5);

-- Dummy-Daten für Tabelle "training_type"
INSERT INTO training_type (type)
VALUES ('Duales Studium'), ('Ausbildung');

-- Dummy-Daten für Tabelle "training"
INSERT INTO training (name, typeFk, yearStarted)
VALUES ('Informatik duales Studium', 'Duales Studium', 2021),
       ('Elektriker Ausbildung', 'Ausbildung', 2020),
       ('Mechatroniker Ausbildung', 'Ausbildung', 2022),
       ('BWL duales Studium', 'Duales Studium', 2019),

-- Dummy-Daten für Tabelle "apprentice"
INSERT INTO apprentice (firstName, lastName, trainingFk)
VALUES ('Max', 'Mustermann', 1),
    ('Lisa', 'Müller', 2),
    ('Tom', 'Schulze', 3),
    ('Julia', 'Maier', 1),
    ('Tim', 'Schmidt', 4);

-- Dummy-Daten für Tabelle "loadout"
INSERT INTO loadout (categoryFk, trainingFk, count)
VALUES (1, 1, 2),
       (2, 1, 1),
       (4, 2, 3),
       (5, 4, 2),
       (3, 3, 1);
