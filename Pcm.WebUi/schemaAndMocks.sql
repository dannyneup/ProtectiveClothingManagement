create table category
(
    id          int auto_increment
        primary key,
    description varchar(100) not null
);

create table item
(
    id           int auto_increment
        primary key,
    description  varchar(200) null,
    manufacturer varchar(200) null,
    comment      text         null,
    category_fk  int          not null,
    constraint item_category_id_fk
        foreign key (category_fk) references category (id)
);

create table training_type
(
    type varchar(64) not null
        primary key
);

create table training
(
    id          int auto_increment
        primary key,
    description varchar(100) not null,
    typeFk      varchar(20)  null,
    yearStarted int          not null,
    constraint training_training_type_type_fk
        foreign key (typeFk) references training_type (type)
);

create table apprentice
(
    id          int auto_increment
        primary key,
    firstname   varchar(100) not null,
    lastname    varchar(100) not null,
    mail        varchar(100) not null,
    yearStarted int          not null,
    trainingFk  int          not null,
    constraint apprentice_training_id_fk
        foreign key (trainingFk) references training (id)
);

create table loadout
(
    id         int auto_increment
        primary key,
    categoryFk int null,
    trainingFk int not null,
    count      int not null,
    constraint loadout_category_id_fk
        foreign key (categoryFk) references category (id),
    constraint loadout_training_id_fk
        foreign key (trainingFk) references training (id)
);

INSERT INTO category (description)
VALUES ('Latzhose'),
       ('Gummistiefel'),
       ('Laptop'),
       ('Schraubendreherset'),
       ('Hammer'),
       ('Sicherheitsschuhe');

INSERT INTO item (description, manufacturer, comment, category_fk)
VALUES ('Latzhose 3000 Größe M', 'Engelbert Strauss', 'Robuste Arbeitslatzhose mit Taschen', 1),
       ('Latzhose 5000 Größe L', 'Engelbert Strauss', 'Hochwertige Arbeitslatzhose aus Baumwolle', 1),
       ('Gummistiefel Größe 42', 'Dunlop', 'Wasserdichte Gummistiefel mit Stahlkappe', 2),
       ('Tablet Samsung Galaxy Tab S6', 'Samsung', 'Leistungsstarkes Tablet für die Arbeit im Außendienst', 3),
       ('Schraubendreher-Set billig', 'Wiha', 'Billiges Set mit verschiedenen Schraubendrehern', 4),
       ('Schraubendreher-Set super', 'Wiha', 'Hochwertiges Set mit verschiedenen Schraubendrehern', 4),
       ('Hammer 500g', 'Picard', 'Handlicher Hammer für den Einsatz in der Werkstatt', 5),
       ('Hammer 1000g', 'Picard', 'Handlicher Hammer für den Einsatz in der Werkstatt', 5),
       ('Sicherheitsschuhe Größe 43', 'Puma', 'Atmungsaktive Sicherheitsschuhe mit Stahlkappe', 6),
       ('Sicherheitsschuhe Größe 45', 'Elten', 'Robuste Sicherheitsschuhe mit rutschfester Sohle', 6);

INSERT INTO training_type (type)
VALUES ('dualesstudium'),
       ('ausbildung');

INSERT INTO training (description, typeFk, yearStarted)
VALUES ('Informatik duales Studium', 'dualesstudium', 2021),
       ('Elektriker Ausbildung', 'ausbildung', 2020),
       ('Mechatroniker Ausbildung', 'ausbildung', 2022),
       ('BWL duales Studium', 'dualesstudium', 2019);

INSERT INTO apprentice (firstname, lastname, trainingFk, mail, yearstarted)
VALUES ('Anna', 'Schmidt', 1, 'anna.schmidt@example.com', 2018),
       ('Max', 'Müller', 2, 'max.mueller@example.com', 2018),
       ('Julia', 'Schneider', 3, 'julia.schneider@example.com', 2019),
       ('Lukas', 'Fischer', 1, 'lukas.fischer@example.com', 2019),
       ('Sophie', 'Weber', 4, 'sophie.weber@example.com', 2020),
       ('Benjamin', 'Wagner', 2, 'benjamin.wagner@example.com', 2020),
       ('Laura', 'Becker', 3, 'laura.becker@example.com', 2021),
       ('Tim', 'Hoffmann', 4, 'tim.hoffmann@example.com', 2021),
       ('Johanna', 'Schäfer', 1, 'johanna.schaefer@example.com', 2022),
       ('Jonas', 'Koch', 2, 'jonas.koch@example.com', 2022),
       ('Nina', 'Schwarz', 3, 'nina.schwarz@example.com', 2022),
       ('Sebastian', 'Schmitt', 4, 'sebastian.schmitt@example.com', 2022),
       ('Alexandra', 'Bauer', 1, 'alexandra.bauer@example.com', 2018),
       ('Leon', 'Herrmann', 2, 'leon.herrmann@example.com', 2018),
       ('Marie', 'Kaiser', 3, 'marie.kaiser@example.com', 2019),
       ('Julian', 'Krause', 1, 'julian.krause@example.com', 2019),
       ('Anna-Lena', 'Maier', 4, 'anna-lena.maier@example.com', 2020),
       ('Tobias', 'Mayer', 2, 'tobias.mayer@example.com', 2020),
       ('Melanie', 'Walter', 3, 'melanie.walter@example.com', 2021),
       ('Jan', 'Beck', 4, 'jan.beck@example.com', 2021),
       ('Lisa', 'Bergmann', 1, 'lisa.bergmann@example.com', 2022),
       ('Felix', 'Huber', 2, 'felix.huber@example.com', 2022),
       ('Sarah', 'Kuhn', 3, 'sarah.kuhn@example.com', 2022),
       ('Andreas', 'Schulz', 4, 'andreas.schulz@example.com', 2022),
       ('Katja', 'Graf', 1, 'katja.graf@example.com', 2018),
       ('Paul', 'Zimmermann', 2, 'paul.zimmermann@example.com', 2018),
       ('Laura', 'Hofmann', 3, 'laura.hofmann@example.com', 2019),
       ('Johannes', 'Meyer', 1, 'johannes.meyer@example.com', 2019),
       ('Marlene', 'Frank', 4, 'marlene.frank@example.com', 2020),
       ('Matthias', 'Schneider', 2, 'matthias.schneider@example.com', 2020),
       ('Leonie', 'Jones', 2, 'leoniejones@example.com', 2020),
       ('David', 'Mueller', 3, 'davidmueller@example.com', 2022),
       ('Lucas', 'Zhang', 1, 'lucaszhang@example.com', 2021),
       ('Hannah', 'Wong', 4, 'hannahwong@example.com', 2019),
       ('Anna', 'Singh', 2, 'annasingh@example.com', 2018),
       ('Mohamed', 'Khan', 3, 'mohamedkhan@example.com', 2021),
       ('Lina', 'Smith', 4, 'linasmith@example.com', 2022),
       ('Maximilian', 'Sato', 1, 'maximiliansato@example.com', 2020),
       ('Sophia', 'Kim', 2, 'sophiakim@example.com', 2018),
       ('Emre', 'Garcia', 3, 'emregarcia@example.com', 2019),
       ('Eva', 'Chen', 4, 'evachen@example.com', 2022),
       ('Leo', 'Müller', 1, 'leomuller@example.com', 2021),
       ('Ella', 'Patel', 2, 'ellapatel@example.com', 2019),
       ('Sara', 'Krüger', 3, 'sarakuuger@example.com', 2018),
       ('Darius', 'Choi', 4, 'dariuschoi@example.com', 2022),
       ('Zoe', 'Kim', 1, 'zoekim@example.com', 2020),
       ('Alexander', 'Nguyen', 2, 'alexandernguyen@example.com', 2021),
       ('Nina', 'Lee', 3, 'ninalee@example.com', 2019),
       ('Liam', 'Petersen', 4, 'liampetersen@example.com', 2018),
       ('Isabella', 'Saito', 1, 'isabellasaito@example.com', 2022);
INSERT INTO loadout (categoryFk, trainingFk, count)
VALUES (1, 1, 2),
       (2, 1, 1),
       (4, 2, 3),
       (5, 4, 2),
       (3, 3, 1);

