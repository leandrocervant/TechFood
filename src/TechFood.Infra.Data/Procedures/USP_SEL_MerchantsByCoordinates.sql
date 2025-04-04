IF(EXISTS(SELECT * FROM sys.procedures WHERE name = 'USP_SEL_MerchantsByCoordinates'))
DROP PROCEDURE [dbo].[USP_SEL_MerchantsByCoordinates]

GO
CREATE PROCEDURE [dbo].[USP_SEL_MerchantsByCoordinates]
	@Latitude decimal = NULL,
	@Longitude decimal = NULL,
	@Distance int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	    SELECT *
	FROM TblMerchant
	WHERE
		(
			6371 * /*km=6371 miles=3959*/
			acos(cos(radians(37)) * 
			cos(radians(AddressLatitude)) * 
			cos(radians(AddressLongitude) - 
			radians(-122)) + 
			sin(radians(37)) * 
			sin(radians(AddressLatitude)))
		) <= @Distance
END