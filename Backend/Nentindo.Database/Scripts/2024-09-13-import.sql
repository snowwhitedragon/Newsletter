INSERT INTO Roles(Code, Title)
VALUES ('SYS', 'Systemadministrator'),
('ADM', 'Administrator'),
('LEAD', 'Leitung'),
('EMP', 'Employee'),
('GUEST', 'Gast');

GO

INSERT INTO Organizations(Id, Title, ResponsibilityType, Description)
VALUES (1, 'Nentindo', 0, 'Nentindo ist für das Kundengeschäft verantwortlich und repräsentiert auch die Hauptorganisation.'),
(2, 'N Production', 1, 'N Production ist ein eigenständiger Zweig für Lieferanten zur Planung, Entwicklung und Produktion von Hardware.'),
(3, 'N Studios', 2, 'N Studios ist die Dachorganisation für Nachunternehmer zur Produktion von Games.');

GO

INSERT INTO Newsletters(Id, Title)
VALUES (1, 'Konzernweite Informationen'),
(2, 'Ankündigung neuer Games'),
(3, 'Rabatte und Promos'),
(4, 'Ankündigungen '),
(5, 'Projektupdates'),
(6, 'Partnerinformationen'),
(7, 'Lieferketten Updates');

GO

INSERT INTO OrganizationNewsletters (NewsletterId, OrganizationId)
VALUES (1, 1),
(2, 1),
(3, 1),
(4, 2),
(4, 3),
(5, 2),
(5, 3),
(6, 3),
(7, 2);