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
CREATE PROCEDURE [dbo].[SP_REGISTER] 
	-- Add the parameters for the stored procedure here
	@user_name nvarchar(MAX),
	@password nvarchar(MAX),
	@salt nvarchar(MAX),
	@email nvarchar(MAX)
AS
BEGIN
	declare @Result bit;
	SET NOCOUNT ON;
	insert into [dbo].[users]( [user_name], [full_name], [date_of_joining], [role_id], [is_active],[email], [password], [salt], [created_by], [created_at]) 
		VALUES (@user_name, LOWER(@user_name), GETDATE(), 2, 1, @email, @password, @salt, Null,  GETDATE());
		DECLARE @u_id int = (select Top 1 id from [dbo].[users] as U where U.user_name = @user_name and U.is_active = 1 ORDER BY created_at)
		IF (ISNULL(@u_id, '') <> '')
		BEGIN
			insert into [dbo].[users_roles]([users_id], [roles_id]) 
			VALUES (@u_id, 2);
		END
	BEGIN
		set @Result = 1;
		select @Result as Rs
	END
END
GO
