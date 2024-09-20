INSERT INTO Roles(Id, Code, Title)
VALUES ('A8A77FEA-12E5-45AE-8F1E-5D774FA67F37', 'SYS', 'Systemadministrator'),
('AC40C0A9-072A-4E2B-ABE0-293237BA9965', 'ADM', 'Administrator'),
('87A21EAA-09DF-48D1-AC70-224C1AF72DBE', 'LEAD', 'Leitung'),
('B53C2C1C-9D7A-439D-922F-D00AFBA761A3', 'EMP', 'Employee'),
('13FD0538-0F80-47D3-87D7-C7B19CEB8CF4', 'GUEST', 'Gast');

GO

INSERT INTO Organizations(Id, Title, ResponsibilityType, Description)
VALUES ('c47d77b2-a90f-403b-91af-dac47ad83372', 'Nentindo', 0, 'Nentindo ist für das Kundengeschäft verantwortlich und repräsentiert auch die Hauptorganisation.'),
('a1aa1c10-06db-4384-b8b2-4267bcf695d5', 'N Production', 1, 'N Production ist ein eigenständiger Zweig für Lieferanten zur Planung, Entwicklung und Produktion von Hardware.'),
('7a137c03-f6fc-4a0b-bbed-f9849fe183d4', 'N Studios', 2, 'N Studios ist die Dachorganisation für Nachunternehmer zur Produktion von Games.');

GO

-- Ask Jenny for super secret PW
INSERT INTO Users(Id, Username, DisplayName, PasswordHash, OrganizationId)
VALUES ('EE748299-BF60-4B6E-A8B3-1C9EA329933D', 'nentindo.sensei', 'Sensei Kanri-Sha', 'AQAAAAIAAYagAAAAEOv/hlo8cXnuzWLyO5Jx6zRWek5VDzEgLcf7Jaq2DQnxTaduv4eCAm6HsiP1fKJjDg==', 'c47d77b2-a90f-403b-91af-dac47ad83372');

GO

INSERT INTO UserRoles(UserId, RoleId)
VALUES ('EE748299-BF60-4B6E-A8B3-1C9EA329933D', 'AC40C0A9-072A-4E2B-ABE0-293237BA9965');

GO

INSERT INTO Newsletters(Id, Title)
VALUES ('1af418dc-0e5b-4c90-a9e0-4381fb69acc6', 'Konzernweite Informationen'),
('55621593-7a0b-4eb9-91df-240c330c0544', 'Ankündigung neuer Games'),
('380d60fa-ff17-4e01-8995-5cf27ae394b5', 'Rabatte und Promos'),
('7ad2022c-f3b0-4468-b6dc-b9483a570d8a', 'Ankündigungen '),
('3b9c4eeb-2948-4e30-a4e4-fc03ab9b7e83', 'Projektupdates'),
('08f4fb4d-8845-4bf6-8a32-27b9992baaac', 'Partnerinformationen'),
('0ff6a1f2-fc03-4eb3-8018-cf1a0ccd9ab7', 'Lieferketten Updates');

GO

INSERT INTO OrganizationNewsletters (NewsletterId, OrganizationId)
VALUES ('1af418dc-0e5b-4c90-a9e0-4381fb69acc6', 'c47d77b2-a90f-403b-91af-dac47ad83372'),
('55621593-7a0b-4eb9-91df-240c330c0544', 'c47d77b2-a90f-403b-91af-dac47ad83372'),
('380d60fa-ff17-4e01-8995-5cf27ae394b5', 'c47d77b2-a90f-403b-91af-dac47ad83372'),
('7ad2022c-f3b0-4468-b6dc-b9483a570d8a', 'a1aa1c10-06db-4384-b8b2-4267bcf695d5'),
('7ad2022c-f3b0-4468-b6dc-b9483a570d8a', '7a137c03-f6fc-4a0b-bbed-f9849fe183d4'),
('3b9c4eeb-2948-4e30-a4e4-fc03ab9b7e83', 'a1aa1c10-06db-4384-b8b2-4267bcf695d5'),
('3b9c4eeb-2948-4e30-a4e4-fc03ab9b7e83', '7a137c03-f6fc-4a0b-bbed-f9849fe183d4'),
('08f4fb4d-8845-4bf6-8a32-27b9992baaac', '7a137c03-f6fc-4a0b-bbed-f9849fe183d4'),
('0ff6a1f2-fc03-4eb3-8018-cf1a0ccd9ab7', 'a1aa1c10-06db-4384-b8b2-4267bcf695d5');