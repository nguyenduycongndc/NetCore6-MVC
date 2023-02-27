USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_OTP]    Script Date: 2023/02/27 8:58:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CHECK_OTP] 
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
	where U.email = @email and U.otp = @otp and U.expiration_date_otp is not null and GETDATE() <= U.expiration_date_otp
	UPDATE users
	SET users.otp = null,
		users.expiration_date_otp = null
	FROM users WHERE users.email = @email and users.otp = @otp
END
