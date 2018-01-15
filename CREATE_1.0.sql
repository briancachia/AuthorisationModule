CREATE SCHEMA usr

--main user details table to store info about registered users
CREATE TABLE usr.UserDetails(
	ID INT NOT NULL IDENTITY,
	Username VARCHAR(100) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Surname VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Password VARCHAR(MAX) NOT NULL,
	Status VARCHAR(1) NOT NULL DEFAULT 'A',
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_UserDetails PRIMARY KEY (ID)
);

--Roles list for the system, including a level integer to specify the role level
CREATE TABLE usr.SysRoles(
	ID INT NOT NULL IDENTITY,
	Name VARCHAR(50) NOT NULL,
	Level INT NOT NULL,
	Status VARCHAR(1) NOT NULL DEFAULT 'A',
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_SysRoles PRIMARY KEY (ID)
);

--Role allocation table
CREATE TABLE usr.UserRoles(
	ID INT NOT NULL IDENTITY,
	RoleId INT NOT NULL,
	UserId INT NOT NULL,
	Status VARCHAR(1) NOT NULL DEFAULT 'A',
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_UserRoles PRIMARY KEY (ID),
	CONSTRAINT FK_UserRoles_SysRoles FOREIGN KEY (RoleId) REFERENCES usr.SysRoles(ID),
	CONSTRAINT FK_UserRoles_User FOREIGN KEY (UserId) REFERENCES usr.UserDetails(ID)
);

--Some dummy data
INSERT INTO usr.SysRoles (Name, Level) VALUES ('Administrator', 0);
INSERT INTO usr.SysRoles (Name, Level) VALUES ('Manager', 1);
INSERT INTO usr.SysRoles (Name, Level) VALUES ('User', 2);