CREATE PROCEDURE [dbo].[uspNegotiationBidSelect]
	@NegotiationBidID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationBidID], [NegotiationID], [BidID], [IsClosed], [eNegUserID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegotiationBid] 
	WHERE  ([NegotiationBidID] = @NegotiationBidID OR @NegotiationBidID IS NULL) 

	COMMIT