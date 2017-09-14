/* DROP THE EXISTING PROCEDURE [ContactInsert] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactInsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactInsert]
GO

/* DROP THE EXISTING PROCEDURE [ContactGetByID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactGetByID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactGetByID]
GO

/* DROP THE EXISTING PROCEDURE [ContactList] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactList]
GO

/* DROP THE EXISTING PROCEDURE [ContactUpdate] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactUpdate]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactUpdate]
GO

/* DROP THE EXISTING PROCEDURE [ContactDelete] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactDelete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactDelete]
GO

/* DROP THE EXISTING PROCEDURE [ContactListByReadStatus] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ContactListByReadStatus]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ContactListByReadStatus]
GO

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

GO

CREATE PROCEDURE [dbo].[ContactGetByID]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	FROM [Contact]
	WHERE [ID] = @ID

GO

CREATE PROCEDURE [dbo].[ContactList]
	
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	FROM [Contact]

GO

CREATE PROCEDURE [dbo].[ContactListByReadStatus]
	@read_status INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Contact]
	WHERE [read_status] = @read_status

	SELECT *
	FROM #temp

GO

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

GO

CREATE PROCEDURE [dbo].[ContactDelete]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	DELETE FROM [Contact]
	WHERE [ID] = @ID

GO

