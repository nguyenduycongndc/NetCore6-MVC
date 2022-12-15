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
CREATE PROCEDURE [dbo].[SP_CHECK_OTP] 
	-- Add the parameters for the stored procedure here
	@email nvarchar(MAX),
	@otp int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from [dbo].[users] as U 
	where U.email = @email and U.otp = @otp and U.expiration_date_otp = GETDATE()
	UPDATE users
	SET users.otp = null,
		users.expiration_date_otp = null
	FROM users WHERE users.email = @email and expiration_date_otp <> GETDATE()
END
GO
