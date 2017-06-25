CREATE TABLE [dbo].[NegotiationBid]
(
	NegotiationBidID uniqueidentifier primary key,
	NegotiationID uniqueidentifier not null,
	BidID uniqueidentifier not null, 
	IsClosed bit,
	eNegUserID uniqueidentifier references UserMapping(eNegUserID) not null, 
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
