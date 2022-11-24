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
CREATE PROCEDURE [dbo].[SP_DELETE_USER]
	-- Add the parameters for the stored procedure here
	@id int,
	@deleted_by int
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
						users.is_active = 0,
						users.deleted_at = GETDATE(),
						users.deleted_by = @deleted_by
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
GO
