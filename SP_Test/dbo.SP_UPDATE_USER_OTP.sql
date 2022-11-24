-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UPDATE_USER_OTP] 
	-- Add the parameters for the stored procedure here
	@email nvarchar(MAX), 
	@otp int, 
	@expdate datetime
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
						users.otp = @otp,
						users.expiration_date_otp = @expdate
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
GO
