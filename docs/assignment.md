# Исходное задание

> Задание курсовой работы, по которому написано это приложение (VKI NSU, 2024).
> Текст сохранён как есть — включая формулировки и опечатки оригинала; это
> документ этапа проектирования, а не описание того, что в итоге получилось.
>
> Схема приложения ушла от задания в одном месте: появилась таблица `Roles`, а у
> `Employees` — `role_id` и `password`, потому что в приложении есть вход по
> логину и разграничение по ролям, а в задании авторизации не было. Остальные
> девять таблиц те же.
>
> Скрипт, снятый с рабочей базы SQL Server (ещё без ролей), лежит в
> [`docs/sql/schema-and-seed.sql`](sql/schema-and-seed.sql); действующая схема
> описана конфигурациями EF Core в
> `Marketplace.EntityFramework/EntitiesBuilders/`.
>
> Оригиналы в `.docx`: [dbscript](dbscript.docx), [inserts](inserts.docx),
> [queries](queries.docx).

## Часть 1. Схема базы данных

1. Создать БД онлайн-магазина (по типу Вайлдберриз или Озона). Обязательно включить: товары, продавцы, сотрудники, клиенты, заказы, пункты выдачи. Дополнительные таблицы БУДУТ, так как БД должна быть в 3НФ, то есть:

```sql
CREATE TABLE Categories (
category_id INT PRIMARY KEY,
[name] NVARCHAR(100)
);
GO

CREATE TABLE Products_Instances (
product_instance_id INT PRIMARY KEY,
category_id INT,
[name] NVARCHAR(100),
[description] NVARCHAR(MAX),
[availability] BIT
);
GO

CREATE TABLE Markets (
market_id INT PRIMARY KEY,
[name] NVARCHAR(100),
city NVARCHAR(100),
[address] NVARCHAR(100)
);
GO

CREATE TABLE Products (
product_id INT PRIMARY KEY,
product_instance_id INT,
market_id INT,
rating DECIMAL(3,2) CHECK (rating >= 0.00 AND rating <= 5.00),
price DECIMAL(10, 2)
);
GO

CREATE TABLE Employees (
employee_id INT PRIMARY KEY,
delivery_point_id INT,
[name] NVARCHAR(100),
email NVARCHAR(100),
phone NVARCHAR(100),
salary DECIMAL(10,2)
);
GO

CREATE TABLE Clients (
client_id INT PRIMARY KEY,
[name] NVARCHAR(100),
email NVARCHAR(100),
phone NVARCHAR(100)
);
GO

CREATE TABLE Orders_Items (
orders_item_id INT PRIMARY KEY,
order_id INT,
product_id INT
);
GO

CREATE TABLE Delivery_Points (
delivery_point_id INT PRIMARY KEY,
[address] NVARCHAR (100),
city NVARCHAR(100),
rating DECIMAL(3,2) CHECK (rating >= 0.00 AND rating <= 5.00),
zipcode NVARCHAR(20)
);
GO

CREATE TABLE Orders (
order_id INT PRIMARY KEY,
client_id INT,
delivery_point_id INT,
order_date DATETIME,
[status] NVARCHAR(50),
total_amount DECIMAL(10, 2)
);
GO
ALTER TABLE Employees
ADD FOREIGN KEY (delivery_point_id) REFERENCES Delivery_Points(delivery_point_id);
GO

ALTER TABLE Products_Instances
ADD FOREIGN KEY (category_id) REFERENCES Categories (category_id);
GO

ALTER TABLE Products
ADD FOREIGN KEY (product_instance_id) REFERENCES Products_Instances (product_instance_id),
FOREIGN KEY (market_id) REFERENCES Markets (market_id);
GO

ALTER TABLE Orders_Items
ADD FOREIGN KEY (product_id) REFERENCES Products (product_id),
FOREIGN KEY (order_id) REFERENCES Orders (order_id);
GO

ALTER TABLE Orders
ADD FOREIGN KEY (client_id) REFERENCES Clients (client_id),
FOREIGN KEY (delivery_point_id) REFERENCES Delivery_Points (delivery_point_id);
GO
```

## Часть 2. Тестовые данные

2. Заполнить все таблицы минимум по 5-10 строк, в товары лучше больше (около 20).

```sql
INSERT INTO Delivery_Points (delivery_point_id, [address], city, zipcode, rating) VALUES
(1, '123 Main Street', 'Anytown', '12345', 0.00),
(2, '456 Elm Street', 'Othertown', '67890', 2.73),
(3, '789 Oak Street', 'Anycity', '13579', 4.51),
(4, '321 Pine Avenue', 'Sometown', '24680', 5.00),
(5, '555 Maple Drive', 'Anyville', '97531', 3.56);

INSERT INTO Employees (employee_id, delivery_point_id, [name], email, phone, salary) VALUES
(1, 1, 'John Doe', 'john.doe@example.com', '123-456-7890', 35000.00),
(2, 1, 'Jane Smith', 'jane.smith@example.com', '456-789-0123', 35000.00),
(3, 2, 'Michael Johnson', 'michael.johnson@example.com', '789-012-3456', 35000.00),
(4, 2, 'Emily Davis', 'emily.davis@example.com', '012-345-6789', 35000.00),
(5, 3, 'William Wilson', 'william.wilson@example.com', '345-678-9012', 35000.00),
(6, 3, 'Olivia Brown', 'olivia.brown@example.com', '678-901-2345', 35000.00),
(7, 4, 'Daniel Lee', 'daniel.lee@example.com', '901-234-5678', 35000.00),
(8, 4, 'Alexis Martinez', 'alexis.martinez@example.com', '234-567-8901', 35000.00),
(9, 5, 'Grace Anderson', 'grace.anderson@example.com', '567-890-1234', 35000.00),
(10, 5, 'Kevin Hernandez', 'kevin.hernandez@example.com', '890-123-4567', 35000.00);

INSERT INTO Clients (client_id, [name], email, phone) VALUES
(1, 'Alice Johnson', 'alice.johnson@example.com', '123-456-7890'),
(2, 'Bob Smith', 'bob.smith@example.com', '456-789-0123'),
(3, 'Eva Williams', 'eva.williams@example.com', '789-012-3456'),
(4, 'Daniel Brown', 'daniel.brown@example.com', '012-345-6789'),
(5, 'Grace Miller', 'grace.miller@example.com', '345-678-9012'),
(6, 'Peter Davis', 'peter.davis@example.com', '678-901-2345'),
(7, 'Sophia Garcia', 'sophia.garcia@example.com', '901-234-5678'),
(8, 'Aiden Martinez', 'aiden.martinez@example.com', '234-567-8901'),
(9, 'Nora Wilson', 'nora.wilson@example.com', '567-890-1234'),
(10, 'Olivia Taylor', 'olivia.taylor@example.com', '890-123-4567');
INSERT INTO Categories (category_id, [name]) VALUES
(1, 'Electronics'),
(2, 'Clothing'),
(3, 'Home & Kitchen'),
(4, 'Books'),
(5, 'Toys & Games');

INSERT INTO Products_Instances (product_instance_id, category_id, [name], [description], [availability]) VALUES
(1, 1, 'Smart TV', 'Description for Smart TV', 1),
(2, 1, 'Smartphone', 'Description for Smartphone', 1),
(3, 2, 'T-shirts', 'Description for T-shirts', 0),
(4, 2, 'Jeans', 'Description for Jeans', 1),
(5, 3, 'Stainless Steel Cookware Set', 'Description for Stainless Steel Cookware Set', 1),
(6, 3, 'Kitchen Knife Set', 'Description for Kitchen Knife Set', 0),
(7, 4, '"To Kill a Mockingbird" by Harper Lee', 'Description for "To Kill a Mockingbird" by Harper Lee', 1),
(8, 4, '"1984" by George Orwell', 'Description for "1984" by George Orwell', 1),
(9, 5, 'LEGO Classic Creative Bricks Set', 'Description for LEGO Classic Creative Bricks Set', 0),
(10, 5, 'Monopoly Board Game', 'Description for Monopoly Board Game', 1),
(11, 1, 'Laptop', 'Description for Laptop', 1),
(12, 1, 'Wireless Headphones', 'Description for Wireless Headphones', 1),
(13, 2, 'Jackets', 'Description for Jackets', 0),
(14, 2, 'Shoes', 'Description for Shoes', 1),
(15, 3, 'Glass Food Storage Containers', 'Description for Glass Food Storage Containers', 1),
(16, 3, 'Coffee Maker', 'Description for Coffee Maker', 0),
(17, 4, '"The Alchemist" by Paulo Coelho', 'Description for "The Alchemist" by Paulo Coelho', 1),
(18, 4, '"The Catcher in the Rye" by J.D. Salinger', 'Description for "The Catcher in the Rye" by J.D. Salinger', 1),
(19, 5, 'Nerf N-Strike Elite Retaliator Blaster', 'Description for Nerf N-Strike Elite Retaliator Blaster', 0),
(20, 5, 'Rubiks Cube', 'Description for Rubiks Cube', 1);

INSERT INTO Markets (market_id, [name], city, [address]) VALUES
(1, 'City Central Market', 'New York', '123 Main Street'),
(2, 'Fresh Fare Marketplace', 'Los Angeles', '456 Elm Street'),
(3, 'Urban Gourmet Market', 'Chicago', '789 Oak Street'),
(4, 'Pacific Coast Marketplace', 'Houston', '101 Pine Street'),
(5, 'Sunrise Valley Market', 'Miami', '202 Maple Street');

INSERT INTO Products (product_id, product_instance_id, market_id, price) VALUES
(1, 1, 1, 101.50),
(2, 2, 2, 15.75),
(3, 3, 3, 20.25),
(4, 4, 4, 182.99),
(5, 5, 5, 220.50),
(6, 6, 1, 30.00),
(7, 7, 2, 122.25),
(8, 8, 3, 170.99),
(9, 9, 4, 25.50),
(10, 10, 5, 198.75),
(11, 11, 1, 28.99),
(12, 12, 2, 32.25),
(13, 13, 3, 16.50),
(14, 14, 4, 23.75),
(15, 15, 5, 21.25),
(16, 16, 1, 147.99),
(17, 17, 2, 39.50),
(18, 18, 3, 27.25),
(19, 19, 4, 31.99),
(20, 20, 5, 183.75);

INSERT INTO Orders (order_id, client_id, order_date, [status], total_amount)
VALUES
(1, 1, '2024-01-15 08:30:00', 'Pending', 704.49),
(2, 3, '2024-01-16 12:45:00', 'Shipped', 325.23),
(3, 5, '2024-01-17 16:20:00', 'Delivered', 226.00),
(4, 10, '2024-01-18 09:10:00', 'Pending', 28.99),
(5, 8, '2024-01-19 11:55:00', 'Shipped', 116.75);

INSERT INTO Orders_Items (orders_item_id, order_id, product_id)
VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 20),
(4, 1, 4),
(5, 1, 5),
(6, 2, 19),
(7, 2, 7),
(8, 2, 8),
(9, 3, 18),
(10, 3, 10),
(11, 4, 11),
(12, 5, 12),
(13, 5, 17),
(14, 5, 14),
(15, 5, 15);
```

## Часть 3. Запросы и процедуры

3. Сделать запросы (+ балл, если это будут процедуры) на:

Получение списка товаров одного продавца;

```sql
CREATE PROCEDURE GetMarketProducts
    @marketId INT = 3
AS
BEGIN
    SELECT m.market_id, m.name, pri.name
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId;
END;
```

Поиск определенного товара в списке (например, «носки» или «пюре»). Запрос должен вернуть все записи, содержащие искомую строку в любом месте названия;

```sql
CREATE PROCEDURE GetProductInstancesByName
    @partialName NVARCHAR(100) = ‘by’
AS
BEGIN
    SELECT *
    FROM Products_Instances
    WHERE [name] LIKE '%' + @partialName + '%';
END;
```

Вывод всех товаров с сортировкой по возрастанию рейтинга (от худшего к лучшему);

```sql
CREATE PROCEDURE GetProductInstancesWithRating
AS
BEGIN
    SELECT pi.[name], p.rating
    FROM Products p
    JOIN Products_Instances pi ON p.product_instance_id = pi.product_instance_id
    ORDER BY p.rating ASC;
END;
```

Запрос, объединяющий все предыдущие;

```sql
CREATE PROCEDURE GetProductInstancesByMarketAndName
    @marketId INT = 3,
    @partialName NVARCHAR(100) = ‘by’

AS
BEGIN
    SELECT m.market_id, m.name, pri.name, p.rating
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId AND pri.name LIKE '%' + @partialName + '%';
END;
```

То же самое, что и 4, но только 2 записи, начиная с 3-ей;

```sql
CREATE PROCEDURE GetFilteredProductInstances
    @marketId INT = 3,
    @partialName NVARCHAR(100) = ‘’,
    @offsetRows INT = 2,
    @fetchRows INT = 3
AS
BEGIN
    SELECT m.market_id, m.name, pri.name, p.rating
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId AND pri.name LIKE '%' + @partialName + '%'
    ORDER BY p.rating ASC
    OFFSET @offsetRows ROWS
    FETCH NEXT @fetchRows ROWS ONLY;
END;
```

Вывод списка товаров самого дорогого заказа, включая их количество, стоимость и поставщика (название, не ID);

```sql
CREATE PROCEDURE GetExpensiveOrderDetails
AS
BEGIN
DECLARE @expensiveOrderId INT;
SELECT TOP 1 @expensiveOrderId = order_id
FROM Orders
ORDER BY total_amount DESC;
SELECT OI.order_id, P.price, PI.name AS product_name, M.name AS market_name, COUNT(*) [count]
FROM Orders_Items AS OI
JOIN Products AS P ON OI.product_id = P.product_id
JOIN Products_Instances AS PI ON P.product_instance_id = PI.product_instance_id
JOIN Markets AS M ON P.market_id = M.market_id
WHERE OI.order_id = @expensiveOrderId
GROUP BY OI.order_id, P.price, PI.name, M.name
END
```

Вывод информации о 4 клиентах, которые делают самые маленькие заказы (по количеству товаров в заказе вне зависимости от цены);

```sql
CREATE PROCEDURE GetSmallestOrderClients
AS
BEGIN
    WITH OrderQuantities AS (
        SELECT order_id, COUNT(*) AS num_items
        FROM Orders_Items
        GROUP BY order_id
    ), SmallestOrders AS (
        SELECT TOP 4 order_id, num_items
        FROM OrderQuantities
        ORDER BY num_items
    )
    SELECT C.name, C.email, C.phone, SO.num_items
    FROM Clients C
    JOIN Orders AS O ON C.client_id = O.client_id
    JOIN SmallestOrders AS SO ON O.order_id = SO.order_id
    ORDER BY SO.num_items;
END;
```

Информация о работе ПВЗ: какой сотрудник обслужил какого клиента и когда (имена, а не ID);

```sql
CREATE PROCEDURE GetEmployeeClientOrders
AS
BEGIN
    SELECT E.name AS employee_name, C.name AS client_name, O.order_date
    FROM Employees AS E
    JOIN Delivery_Points AS DP ON E.delivery_point_id = DP.delivery_point_id
    JOIN Orders AS O ON DP.delivery_point_id = O.delivery_point_id
    JOIN Clients AS C ON O.client_id = C.client_id;
END;
```

Понижение зарплаты на 20% всем сотрудникам ПВЗ с рейтингом меньше 3.5;

```sql
CREATE PROCEDURE UpdateEmployeeSalaryBasedOnDeliveryPointRating
AS
BEGIN
    UPDATE Employees
    SET salary = salary * 0.8
    WHERE delivery_point_id IN (
        SELECT delivery_point_id
        FROM Delivery_Points
        WHERE rating < 3.5
    );
END;
```

Удаление всех ПВЗ с оборотом заказов меньше 1 в месяц со всеми их сотрудниками.

```sql
CREATE PROCEDURE DeleteDeliveryPointsWithEmployees
AS
BEGIN
    WITH PointsToDelete AS (
        SELECT dp.delivery_point_id
        FROM Delivery_Points dp
        LEFT JOIN (
            SELECT delivery_point_id, COUNT(*) as monthly_order_count
            FROM Orders
            WHERE order_date >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
            AND order_date < DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE()), 0)
            GROUP BY delivery_point_id
        ) order_counts ON dp.delivery_point_id = order_counts.delivery_point_id
        WHERE order_counts.monthly_order_count < 1
    )

    DELETE FROM Employees
    WHERE delivery_point_id IN (SELECT delivery_point_id FROM PointsToDelete);

    DELETE FROM Delivery_Points
    WHERE delivery_point_id IN (SELECT delivery_point_id FROM PointsToDelete);
END;
```
