CREATE TABLE [dbo].[UserMapping]
(
	eNegUserID uniqueidentifier primary key, 
	eSourceUserID uniqueidentifier not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
