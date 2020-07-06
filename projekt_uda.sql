USE projekt;
GO

CREATE ASSEMBLY [Projekt.lib]
FROM 'C:\Users\Administrator\Documents\Visual Studio 2008\Projects\Projekt\Projekt\bin\Debug\Projekt.dll';
GO

CREATE AGGREGATE [dbo].[Temperature.uda_CountOfNegatives](@value INT)
RETURNS INT
EXTERNAL NAME [Projekt.lib].[Temperature.uda_CountOfNegatives];
GO

--SELECT [dbo].[Temperature.uda_CountOfNegatives](temp) as "Iloœæ dni z ujemn¹ temperatura" FROM pogoda

CREATE AGGREGATE [dbo].[Temperature.uda_CountOfVegetation](@value INT)
RETURNS INT
EXTERNAL NAME [Projekt.lib].[Temperature.uda_CountOfVegetation];
GO

CREATE AGGREGATE [dbo].[Temperature.uda_CountOfRange](@value1 INT, @value2 INT, @value3 INT)
RETURNS INT
EXTERNAL NAME [Projekt.lib].[Temperature.uda_CountOfRange];
GO

CREATE AGGREGATE [dbo].[Temperature.Median](@value DOUBLE PRECISION)
RETURNS DOUBLE PRECISION
EXTERNAL NAME [Projekt.lib].[Temperature.Median];
GO

CREATE AGGREGATE [dbo].[Dominanta.Dominanta](@value INT)
RETURNS INT
EXTERNAL NAME [Projekt.lib].[Dominanta.Dominanta];
GO
select * from pogoda;


--opady--

CREATE AGGREGATE [dbo].[Rainfall.uda_GeoAvg](@value DOUBLE PRECISION)
RETURNS DOUBLE PRECISION
EXTERNAL NAME [Projekt.lib].[Rainfall.uda_GeoAvg];
GO

CREATE AGGREGATE [dbo].[Rainfall.uda_SnowPercent](@value DOUBLE PRECISION, @value2 INT)
RETURNS DOUBLE PRECISION
EXTERNAL NAME [Projekt.lib].[Rainfall.uda_SnowPercent];
GO

CREATE AGGREGATE [dbo].[Rainfall.uda_SumOfRange](@value DOUBLE PRECISION, @data DateTime, @data1 DateTime, @data2 DateTime)
RETURNS DOUBLE PRECISION
EXTERNAL NAME [Projekt.lib].[Rainfall.uda_SumOfRange];
GO