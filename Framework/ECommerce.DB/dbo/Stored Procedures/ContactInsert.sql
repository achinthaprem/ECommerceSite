
CREATE PROCEDURE [dbo].[ContactInsert]
	@name NVARCHAR(100),
	@email NVARCHAR(255),
	@contact_no NVARCHAR(30),
	@subject NVARCHAR(50),
	@message NVARCHAR(250),
	@date DATETIME,
	@read_status INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	INSERT INTO [Contact]
	(
		[name],
		[email],
		[contact_no],
		[subject],
		[message],
		[date],
		[read_status]
	)
	VALUES
	(
		@name,
		@email,
		@contact_no,
		@subject,
		@message,
		@date,
		@read_status
	)

	SELECT SCOPE_IDENTITY()