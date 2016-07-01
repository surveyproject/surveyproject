USE [DBNAME]

if not exists(select * from sys.columns where Name = N'CssClass' and Object_ID = Object_ID(N'vts_tbAnswer'))
begin
    ALTER TABLE dbo.vts_tbAnswer ADD CssClass nvarchar(50) NULL
end