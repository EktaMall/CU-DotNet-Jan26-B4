
-- 1. Product Names with Category Names
SELECT p.ProductName, c.CategoryName
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID;

-- 2. Order ID with Customer Company Name
SELECT o.OrderID, c.CompanyName
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID;

-- 3. Product Names with Supplier Company Name
SELECT p.ProductName, s.CompanyName
FROM Products p
JOIN Suppliers s ON p.SupplierID = s.SupplierID;

-- 4. Orders with Employee Name
SELECT o.OrderID, o.OrderDate, e.FirstName, e.LastName
FROM Orders o
JOIN Employees e ON o.EmployeeID = e.EmployeeID;

-- 5. Orders shipped to France with Shipper Name
SELECT o.OrderID, s.CompanyName
FROM Orders o
JOIN Shippers s ON o.ShipVia = s.ShipperID
WHERE o.ShipCountry = 'France';

-- 6. Category Stock
SELECT c.CategoryName, SUM(p.UnitsInStock) AS TotalStock
FROM Categories c
JOIN Products p ON c.CategoryID = p.CategoryID
GROUP BY c.CategoryName;

-- 7. Customer Total Spend
SELECT c.CompanyName,
       SUM(od.UnitPrice * od.Quantity) AS TotalSpent
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.CompanyName;

-- 8. Employee Performance
SELECT e.LastName, COUNT(o.OrderID) AS TotalOrders
FROM Employees e
JOIN Orders o ON e.EmployeeID = o.EmployeeID
GROUP BY e.LastName;

-- 9. Total Freight by Shipper
SELECT s.CompanyName, SUM(o.Freight) AS TotalFreight
FROM Shippers s
JOIN Orders o ON s.ShipperID = o.ShipVia
GROUP BY s.CompanyName;

-- 10. Top 5 Products by Quantity Sold
SELECT TOP 5 p.ProductName, SUM(od.Quantity) AS TotalSold
FROM Products p
JOIN [Order Details] od ON p.ProductID = od.ProductID
GROUP BY p.ProductName
ORDER BY TotalSold DESC;


-- 11. Products Above Average Price
SELECT ProductName
FROM Products
WHERE UnitPrice > (SELECT AVG(UnitPrice) FROM Products);

-- 12. Employees and Their Managers
SELECT e.FirstName + ' ' + e.LastName AS Employee,
       m.FirstName + ' ' + m.LastName AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ReportsTo = m.EmployeeID;

-- 13. Customers with No Orders
SELECT CompanyName
FROM Customers
WHERE CustomerID NOT IN (SELECT CustomerID FROM Orders);

-- 14. High-Value Orders
SELECT o.OrderID
FROM Orders o
JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY o.OrderID
HAVING SUM(od.UnitPrice * od.Quantity) >
(
    SELECT AVG(Total)
    FROM (
        SELECT SUM(UnitPrice * Quantity) AS Total
        FROM [Order Details]
        GROUP BY OrderID
    ) x
);

-- 15. Products Never Ordered After 1997
SELECT ProductName
FROM Products
WHERE ProductID NOT IN
(
    SELECT DISTINCT od.ProductID
    FROM Orders o
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    WHERE YEAR(o.OrderDate) > 1997
);

-- 16. Employee Territory Coverage
SELECT e.FirstName, e.LastName, r.RegionDescription
FROM Employees e
JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
JOIN Territories t ON et.TerritoryID = t.TerritoryID
JOIN Region r ON t.RegionID = r.RegionID;

-- 17. Customers & Suppliers in Same City
SELECT c.CompanyName AS Customer,
       s.CompanyName AS Supplier,
       c.City
FROM Customers c
JOIN Suppliers s ON c.City = s.City;

-- 18. Customers Buying from >3 Categories
SELECT c.CompanyName
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CompanyName
HAVING COUNT(DISTINCT p.CategoryID) > 3;

-- 19. Revenue from Discontinued Products
SELECT SUM(od.UnitPrice * od.Quantity) AS DiscontinuedRevenue
FROM Products p
JOIN [Order Details] od ON p.ProductID = od.ProductID
WHERE p.Discontinued = 1;

-- 20. Most Expensive Product per Category
SELECT c.CategoryName, p.ProductName, p.UnitPrice
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE p.UnitPrice =
(
    SELECT MAX(UnitPrice)
    FROM Products
    WHERE CategoryID = p.CategoryID
);