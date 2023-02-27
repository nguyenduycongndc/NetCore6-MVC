USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_FORGOT_PASSWORD_USER]    Script Date: 2023/02/27 9:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_FORGOT_PASSWORD_USER] 
	-- Add the parameters for the stored procedure here
	@email nvarchar(MAX),
	@password nvarchar(MAX),
	@salt nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @Result bit;
	SET NOCOUNT ON;
	If exists (SELECT * FROM users WHERE email = @email)
    -- Insert statements for procedure here
	BEGIN
			UPDATE users
					set 
						users.email = @email,
						users.modified_at = GETDATE(),
						users.otp = null,
						users.expiration_date_otp = null,
						users.password = @password,
						users.salt = @salt
					from users 
					WHERE users.email = @email
			BEGIN
				set @Result = 1;
				select @Result as Rs
			END
		END
	ELSE
		BEGIN
			set @Result = 0;
			select @Result as Rs
		END
END
