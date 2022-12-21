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
CREATE PROCEDURE [dbo].[SP_UPDATE_EMAIL] 
	-- Add the parameters for the stored procedure here
	@id int, 
	@email_address nvarchar(Max), 
	@cc nvarchar(Max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @Result bit;
	SET NOCOUNT ON;
	If exists (SELECT * FROM users WHERE id = @id and (is_deleted <> 1 or is_deleted is null))
    -- Insert statements for procedure here
	BEGIN
			UPDATE email
					set 
						email.email_address = @email_address,
						email.cc = @cc
					from email 
					WHERE email.id = @id and (is_deleted <> 1 or is_deleted is null)
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
