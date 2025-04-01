USE BaseReservation
GO

BEGIN TRANSACTION ClearDatabase
    WITH MARK N'Cleaning database';

BEGIN TRY
  -- Invoices
    DELETE FROM InvoiceDetailProduct;
    DELETE FROM InvoiceDetail;
    DELETE FROM Invoice;

    -- Order
    DELETE FROM OrderDetailProduct;
    DELETE FROM OrderDetail;
    DELETE FROM [Order];

    -- Reservation
    DELETE FROM ReservationQuestion;
    DELETE FROM ReservationService;
    DELETE FROM Reservation;

    -- Inventory
    DELETE FROM InventoryProductTransaction;
    DELETE FROM InventoryProduct;
    DELETE FROM Inventory;

    -- Supplier
    DELETE FROM Contact;
    DELETE FROM Supplier;

    -- Product
    DELETE FROM Product;
    DELETE FROM Category;
    DELETE FROM UnitOfMeasure;

    -- Service
    DELETE FROM Service;
    DELETE FROM ServiceType;

    -- Client
    DELETE FROM Customer;

    -- User/Security
    DELETE FROM TokenMaster;
    DELETE FROM UserBranch;
    DELETE FROM [User];
    DELETE FROM Role;

    -- Branch
    DELETE FROM BranchScheduleBlock;
    DELETE FROM BranchSchedule;
    DELETE FROM BranchHoliday;
    DELETE FROM Schedule;
    DELETE FROM Holiday;
    DELETE FROM Branch;

    -- General
    DELETE FROM PaymentType;
    DELETE FROM Gender;
    DELETE FROM Tax;
    DELETE FROM UnitMeasure;

    -- Addresses
    DELETE FROM District;
    DELETE FROM Canton;
    DELETE FROM Province;

    EXEC sp_MSForEachTable '
    IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1
    DBCC CHECKIDENT (''?'', RESEED, 1)'
    
    COMMIT TRANSACTION ClearDatabase;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION ClearDatabase;

    SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH