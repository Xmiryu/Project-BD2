CREATE DATABASE projekt
GO

USE projekt
GO

IF OBJECT_ID('dbo.usp_CreateTablesInprojekt') IS NOT NULL
	DROP PROC dbo.usp_CreateTablesInprojekt;
GO

Drop table pogoda;
GO

CREATE PROC dbo.usp_CreateTablesInprojekt
AS
	CREATE TABLE pogoda(
		data DATETIME,
		temp real,
		opady real,
		snieg bit
	);

GO

EXEC dbo.usp_CreateTablesInprojekt;
GO

BULK INSERT dbo.pogoda FROM 'C:\Users\Administrator\Desktop\Projekt_pogoda.csv' 
  WITH (ROWTERMINATOR='\n', FIELDTERMINATOR=',', FIRSTROW = 1);

select * from dbo.pogoda ;