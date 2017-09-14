
CREATE PROCEDURE [dbo].[ContactUpdate]
	@ID INT,
	@name NVARCHAR(100),
	@email NVARCHAR(255),
	@contact_no NVARCHAR(30),
	@subject NVARCHAR(50),
	@message NVARCHAR(250),
	@date DATETIME,
	@read_status INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	UPDATE [Contact] SET
		[name] = @name,
		[email] = @email,
		[contact_no] = @contact_no,
		[subject] = @subject,
		[message] = @message,
		[read_status] = @read_status
	WHERE [ID] = @ID