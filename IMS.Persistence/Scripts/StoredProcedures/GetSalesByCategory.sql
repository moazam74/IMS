/*
    Stored Procedure: GetSalesByCategory
    Description: Retrieves total sales by category for a specified date range
    Parameters:
        @StartDate - The start date for the sales report
        @EndDate - The end date for the sales report
    Returns:
        CategoryName - Name of the product category
        TotalSales - Total sales amount for the category
    Created By: Muhammad Moazam Ali
    Created Date: 2024-04-14
*/

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSalesByCategory')
    DROP PROCEDURE GetSalesByCategory
GO

CREATE PROCEDURE GetSalesByCategory
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Name AS CategoryName,
        SUM(oi.Price * oi.Quantity) AS TotalSales
    FROM OrderItems oi
    INNER JOIN Orders o ON oi.OrderId = o.Id
    INNER JOIN Products p ON oi.ProductId = p.Id
    INNER JOIN Categories c ON p.CategoryId = c.Id
    WHERE o.Date BETWEEN @StartDate AND @EndDate
    GROUP BY c.Name
    ORDER BY TotalSales DESC;
END
GO

-- Example usage:
-- EXEC GetSalesByCategory @StartDate = '2024-01-01', @EndDate = '2024-12-31' 