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
CREATE PROCEDURE [dbo].[SP_ALL_EMAIL] 
	-- Add the parameters for the stored procedure here
	@email_address nvarchar(MAX),
	@start_number INT,
	@page_size INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--IF (@start_number < 1)
	--	SELECT @start_number = 1;
    
	SELECT * from [dbo].[email] as E
		WHERE 
				@email_address is null 
				or E.email_address LIKE N'%' + @email_address + '%'
				and E.is_deleted = '0'
		ORDER BY id
		OFFSET @start_number ROWS
		--OFFSET @page_size * (@start_number - 1) ROWS
		FETCH NEXT @page_size ROWS ONLY;
END
GO
