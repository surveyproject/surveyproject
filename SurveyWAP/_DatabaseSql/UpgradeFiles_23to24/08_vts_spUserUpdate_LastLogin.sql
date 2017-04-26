USE [YOURDBNAME]
GO
/****** Object:  StoredProcedure [dbo].[vts_spUserUpdate]    Script Date: 3/21/2017 08:36:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[vts_spUserUpdate]
			@UserID int,
			@UserName nvarchar(255),
			@Password nvarchar(255),
		    @PasswordSalt nvarchar(255),
			@LastName nvarchar(255),
			@FirstName nvarchar(255),  
			@Email nvarchar(255),
			@LastLogin datetime
			
AS

UPDATE vts_tbUser SET
	UserName = @UserName,
	FirstName = @FirstName,
	LastName = @LastName,
	Email = @Email
WHERE UserID = @UserID

if @Password is not null or @PasswordSalt is not null
BEGIN
	UPDATE vts_tbUser SET
		Password = @Password,
	    PasswordSalt = @PasswordSalt
	WHERE UserID = @UserID
END

if @LastLogin is not null
BEGIN
	UPDATE vts_tbUser SET
		LastLogin = @LastLogin
	WHERE UserID = @UserID
END

