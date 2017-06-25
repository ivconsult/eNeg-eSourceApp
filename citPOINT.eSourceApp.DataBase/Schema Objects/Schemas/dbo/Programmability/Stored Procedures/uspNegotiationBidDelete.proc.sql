CREATE PROCEDURE [dbo].[uspNegotiationBidDelete]
	@NegotiationBidID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationBid]
	SET Deleted = 0, DeletedOn = GETDATE() 
	WHERE  [NegotiationBidID] = @NegotiationBidID

	COMMIT