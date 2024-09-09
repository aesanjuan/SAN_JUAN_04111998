USE shortclipsdb

INSERT INTO [dbo].[Videos]
	([Title], [Description], [CategoryId], [IsDeleted], [UploadDateTime], [LastUpdatedDateTime], [ThumbnailFilePath], [VideoFilePath], [VideoContentType])
VALUES
	('Road', 'This is a road.', 5, 0, GETDATE(), GETDATE(), 'ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Thumbnails\773129ac-4def-44d5-a2d7-c4de0dbecd09.png', 'ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Videos\773129ac-4def-44d5-a2d7-c4de0dbecd09.mp4', 'video/mp4'),
	('Ocean Waves', 'Enjoy the ocean waves.', 5, 0, GETDATE(), GETDATE(), 'ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Thumbnails\8c337405-f588-40fc-a693-1669859a9e94.png', 'ShortClips\ShortClipsWeb\short-clips-web-api\short-clips-web-api\UserUploads\Videos\8c337405-f588-40fc-a693-1669859a9e94.mp4', 'video/mp4')

	SELECT * from [dbo].[Videos]