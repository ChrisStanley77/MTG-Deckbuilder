Create Database mtg_deckbuilder
GO

use mtg_deckbuilder
CREATE TABLE Accounts (
	Id int IDENTITY(1,1) PRIMARY KEY,
	Email varchar(50) NOT NULL,
	Username varchar(50) NOT NULL,
	Password varchar(50) NOT NULL
);
GO
