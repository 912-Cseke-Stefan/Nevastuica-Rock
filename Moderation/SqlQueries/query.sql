drop table Vote
drop table JoinRequestMessage
drop table JoinRequest
drop table PollAward
drop table PostAward
drop table Award
drop table PollOption
drop table PollPost
drop table Post
drop table Report
drop table GroupUser
drop table RolePermission
drop table UserRole

CREATE TABLE UserRole(
	RoleId UNIQUEIDENTIFIER PRIMARY KEY,
	Name nvarchar(255)
)
CREATE TABLE RolePermission(
	RoleId UNIQUEIDENTIFIER references UserRole(RoleId),
	Permission nvarchar(255),
	Primary Key (RoleId,Permission)
)
CREATE TABLE GroupUser(
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(255),
    PostScore INT,
    MarketplaceScore INT,
    StatusRestriction INT, 
	StatusRestrictionDate DATETIME,
    StatusMessage NVARCHAR(MAX)
)
CREATE TABLE Post(
	PostId UNIQUEIDENTIFIER primary key,
	Content nvarchar(MAX),
	UserId UNIQUEIDENTIFIER references GroupUser(Id),
	Score int,
	Status nvarchar(250),
	IsDeleted BIT
)
CREATE TABLE PollPost (
    PollId UNIQUEIDENTIFIER PRIMARY KEY,
    Content NVARCHAR(MAX),
    UserId UNIQUEIDENTIFIER references GroupUser(Id),
    Score INT,
    Status NVARCHAR(50),
    IsDeleted BIT,
)
CREATE TABLE PollOption (
    OptionId INT IDENTITY PRIMARY KEY,
    PollId UNIQUEIDENTIFIER references PollPost(PollId),
    OptionText NVARCHAR(MAX),
)
CREATE TABLE Award(
	AwardId UNIQUEIDENTIFIER primary key,
	Type nvarchar(255),
	PostId UNIQUEIDENTIFIER references Post(PostId)
)
CREATE TABLE PostAward(
	AwardId UNIQUEIDENTIFIER references Award(AwardId),
	PostId UNIQUEIDENTIFIER references Post(PostId),
	primary key(AwardId,PostId)
)
CREATE TABLE PollAward(
	AwardId UNIQUEIDENTIFIER references Award(AwardId),
	PollId UNIQUEIDENTIFIER references PollPost(PollId),
	primary key (AwardId,PollId)
)
CREATE TABLE Report(
	ReportId UNIQUEIDENTIFIER primary key,
	UserId UNIQUEIDENTIFIER references GroupUser(Id),
	Message varchar(250)
)
CREATE TABLE JoinRequest(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER references GroupUser(Id)
)
CREATE TABLE JoinRequestMessage (
    JoinRequestId UNIQUEIDENTIFIER references JoinRequest(Id),
    [Key] NVARCHAR(255),
    [Value] NVARCHAR(MAX),
	primary key(JoinRequestId,[Key])
)
CREATE TABLE Vote(
	VoteId UNIQUEIDENTIFIER primary key,
	UserPost UNIQUEIDENTIFIER references GroupUser(Id),
	PollId UNIQUEIDENTIFIER references PollPost(PollId),
	Options nvarchar(Max)
)