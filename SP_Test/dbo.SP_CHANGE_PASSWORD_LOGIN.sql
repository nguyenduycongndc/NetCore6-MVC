USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CHANGE_PASSWORD_LOGIN]    Script Date: 2023/02/27 8:55:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CHANGE_PASSWORD_LOGIN] 
	-- Add the parameters for the stored procedure here
	@id nvarchar(MAX),
	@passwordnew nvarchar(MAX),
	@salt nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @Result bit;
	SET NOCOUNT ON;
	If exists (SELECT * FROM users WHERE id = @id)
    -- Insert statements for procedure here
	BEGIN
			UPDATE users
					set 
						users.modified_at = GETDATE(),
						users.otp = null,
						users.expiration_date_otp = null,
						users.password = @passwordnew,
						users.salt = @salt
					from users 
					WHERE users.id = @id
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
