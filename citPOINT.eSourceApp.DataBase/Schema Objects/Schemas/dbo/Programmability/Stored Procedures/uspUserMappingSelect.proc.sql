CREATE PROCEDURE [dbo].[uspUserMappingSelect]
    @eNegUserID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [eNegUserID], [eSourceUserID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[UserMapping] 
	WHERE  ([eNegUserID] = @eNegUserID OR @eNegUserID IS NULL) 

	COMMIT