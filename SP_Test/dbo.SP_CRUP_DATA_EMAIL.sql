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
CREATE PROCEDURE [dbo].[SP_CRUP_DATA_EMAIL]
	-- Add the parameters for the stored procedure here
	@subject nvarchar(Max), 
	@body nvarchar(Max), 
	@created_by int, 
	@checkauto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @Result bit;
	DECLARE @data_email_count INT;
    -- Insert statements for procedure here
	SET @data_email_count = (Select COUNT(data_email.id) FROM data_email)
	IF (@data_email_count > 0)
		BEGIN
			UPDATE TOP (1) data_email
					set 
						data_email.[subject] = @subject,
						data_email.body = @body,
						data_email.created_by = @created_by,
						data_email.created_at = GETDATE(),
						data_email.check_auto = @checkauto
					from data_email
			BEGIN
				set @Result = 1;
				select @Result as Rs
			END
		END
	ELSE IF(@data_email_count = 0)
			BEGIN
				insert into [dbo].[data_email]( [subject], body, created_by, created_at, check_auto) 
				VALUES (@subject, @body, @created_by, GETDATE(), @checkauto);
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
