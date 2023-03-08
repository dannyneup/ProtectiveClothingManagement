create table latzhose."Manufacturer"
(
    "Id"   serial
        constraint "Manufacturer_pk"
            primary key,
    "Name" varchar(255) not null
);

alter table latzhose."Manufacturer"
    owner to postgres;

create table latzhose."WorkItemCategory"
(
    "Id"   serial
        constraint "WorkItemCategory_pk"
            primary key,
    "Name" varchar(255) not null
);

alter table latzhose."WorkItemCategory"
    owner to postgres;

create table latzhose."WorkItem"
(
    "Id"                  serial
        constraint "WorkItem_pk"
            primary key,
    "WorkItemCategory_fk" integer not null
        constraint "WorkItem_WorkItemCategory_Id_fk"
            references latzhose."WorkItemCategory",
    "Manufacturer_fk"     serial
        constraint "WorkItem_Manufacturer_Id_fk"
            references latzhose."Manufacturer"
);

alter table latzhose."WorkItem"
    owner to postgres;

create table latzhose."Inventory"
(
    "Id"                  serial
        constraint "Inventory_pk"
            primary key,
    "WorkItem_fk"         integer not null
        constraint "Inventory_WorkItem_Id_fk"
            references latzhose."WorkItem",
    "Total"               integer not null,
    "Circulating"         integer not null,
    "PropertyDescription" text
);

alter table latzhose."Inventory"
    owner to postgres;

create table latzhose."OrderableWorkItem"
(
    "Id"          integer default nextval('latzhose."WorkItemPolicy_Id_seq"'::regclass) not null
        constraint "OrderableWorkItem_pk"
            primary key,
    "WorkItem_fk" integer                                                               not null
        constraint "OrderableWorkItem_WorkItem_Id_fk"
            references latzhose."WorkItem",
    "Model"       varchar(255)                                                          not null
);

alter table latzhose."OrderableWorkItem"
    owner to postgres;

create table latzhose."Person"
(
    "Id"           serial
        constraint "Person_pk"
            primary key,
    "FirstName"    varchar(255),
    column_name    varchar(255) not null,
    "EmailAddress" varchar(255) not null
);

comment on column latzhose."Person".column_name is 'LastName';

alter table latzhose."Person"
    owner to postgres;

create table latzhose."InventoryInPerson"
(
    "Person_fk"    integer not null
        constraint "InventoryInPerson_Person_Id_fk"
            references latzhose."Person",
    "Inventory_fk" integer not null
        constraint "InventoryInPerson_Inventory_Id_fk"
            references latzhose."Inventory",
    "Count"        integer not null,
    constraint "InventoryInPerson_pk"
        primary key ("Person_fk", "Inventory_fk")
);

alter table latzhose."InventoryInPerson"
    owner to postgres;

create table latzhose."Apprenticeship"
(
    "Id"   integer      not null
        constraint "Apprenticeship_pk"
            primary key,
    "Name" varchar(255) not null
);

alter table latzhose."Apprenticeship"
    owner to postgres;

create table latzhose."PersonInApprenticeship"
(
    "Person_fk"         integer not null
        constraint "PersonInApprenticeship_Person_Id_fk"
            references latzhose."Person",
    "Apprenticeship_fk" integer not null
        constraint "PersonInApprenticeship_Apprenticeship_Id_fk"
            references latzhose."Apprenticeship",
    constraint "PersonInApprenticeship_pk"
        primary key ("Person_fk", "Apprenticeship_fk")
);

alter table latzhose."PersonInApprenticeship"
    owner to postgres;

create table latzhose."WorkItemCategoryInApprenticeship"
(
    "WorkitemCategory_fk" integer not null
        constraint "WorkItemCategoryInApprenticeship_WorkItemCategory_Id_fk"
            references latzhose."WorkItemCategory",
    "Apprenticeship_fk"   integer not null
        constraint "WorkItemCategoryInApprenticeship_Apprenticeship_Id_fk"
            references latzhose."Apprenticeship",
    "Count"               integer not null,
    constraint "WorkItemCategoryInApprenticeship_pk"
        primary key ("WorkitemCategory_fk", "Apprenticeship_fk")
);

alter table latzhose."WorkItemCategoryInApprenticeship"
    owner to postgres;

create table latzhose."Order"
(
    "Id"     serial
        constraint "Order_pk"
            primary key,
    "Vendor" varchar(255),
    "Sum"    numeric,
    "Status" char not null
);

alter table latzhose."Order"
    owner to postgres;

create table latzhose."OrderableWorkItemInOrder"
(
    "OrderableWorkItem_fk" serial
        constraint "OrderableWorkItemInOrder_OrderableWorkItem_Id_fk"
            references latzhose."OrderableWorkItem",
    "Order_fk"             serial
        constraint "OrderableWorkItemInOrder_Order_Id_fk"
            references latzhose."Order",
    "OrderedCount"         integer not null,
    "Status"               char    not null,
    constraint "OrderableWorkItemInOrder_pk"
        primary key ("OrderableWorkItem_fk", "Order_fk")
);

alter table latzhose."OrderableWorkItemInOrder"
    owner to postgres;

INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (1, 'Dell');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (2, 'HP');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (3, 'Veleda');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (4, 'Weisser Riese');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (5, 'Nike');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (6, 'Adidas');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (7, 'Rohrzangenmonopolist');
INSERT INTO latzhose."Manufacturer" ("Id", "Name") VALUES (8, 'Primark');

INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (1, 'Sicherheitslatzhose');
INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (2, 'Windows Laptop mit .net Framework 4.2');
INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (3, 'Stahlkappenschuhe');
INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (4, 'Rohrzange');
INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (5, 'Wischmopp zum Schleim entfernen');
INSERT INTO latzhose."WorkItemCategory" ("Id", "Name") VALUES (6, 'Bequeme Latzhose');


INSERT INTO latzhose."Person" ("Id", "FirstName", column_name, "EmailAddress") VALUES (1, 'Scrum', 'Master', 'sm@rostock.de');
INSERT INTO latzhose."Person" ("Id", "FirstName", column_name, "EmailAddress") VALUES (2, 'Technik', 'Nerd', 'tn@rostock.de');
INSERT INTO latzhose."Person" ("Id", "FirstName", column_name, "EmailAddress") VALUES (3, 'Karriere', 'Geil', 'kg@rostock.de');
INSERT INTO latzhose."Person" ("Id", "FirstName", column_name, "EmailAddress") VALUES (4, 'Machen', 'statt Labern', 'ml@rostock.de');

INSERT INTO latzhose."Apprenticeship" ("Id", "Name") VALUES (1, 'Wirtschaftsinformatik');
INSERT INTO latzhose."Apprenticeship" ("Id", "Name") VALUES (2, 'Gas Wasser Scheisse');
INSERT INTO latzhose."Apprenticeship" ("Id", "Name") VALUES (3, 'Prozessmanager');

INSERT INTO latzhose."Order" ("Id", "Vendor", "Sum", "Status") VALUES (1, 'Amazon', 100000.12, 'f');
INSERT INTO latzhose."Order" ("Id", "Vendor", "Sum", "Status") VALUES (2, 'Amazon', 42000, 'c');
INSERT INTO latzhose."Order" ("Id", "Vendor", "Sum", "Status") VALUES (3, 'Vendor A', 12000, 'f');
INSERT INTO latzhose."Order" ("Id", "Vendor", "Sum", "Status") VALUES (4, 'Vendor B', 800, 'f');
INSERT INTO latzhose."Order" ("Id", "Vendor", "Sum", "Status") VALUES (5, 'Vendor C', 120, 'p');

INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (1, 1, 5);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (2, 1, 6);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (3, 2, 1);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (4, 2, 2);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (5, 3, 5);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (6, 4, 7);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (7, 5, 3);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (8, 5, 4);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (9, 6, 5);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (10, 6, 6);
INSERT INTO latzhose."WorkItem" ("Id", "WorkItemCategory_fk", "Manufacturer_fk") VALUES (11, 6, 8);


INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (1, 1, 'Latzhose 3000');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (2, 1, 'Latzhose 5000');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (3, 2, 'Latzhose A43');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (4, 3, 'Model 1');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (5, 3, 'Model 2');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (6, 4, 'Model X');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (7, 4, 'Model Y');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (8, 5, 'Schuh 3000');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (9, 5, 'Schuh 5000');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (10, 5, 'Schuh 7000');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (11, 6, 'Rohrzange X3');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (12, 6, 'Rohrzange X4');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (13, 7, 'Mopp 300');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (14, 7, 'Mopp 400');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (15, 7, 'Auto Mopp');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (16, 8, 'Mopp XJ Ultra');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (17, 8, 'Supermopp 2.0');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (18, 9, 'Flausch ');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (19, 9, 'Bausch');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (20, 10, 'Plagiat 2');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (21, 11, 'Plastikmodell 1');
INSERT INTO latzhose."OrderableWorkItem" ("Id", "WorkItem_fk", "Model") VALUES (22, 11, 'Plastikmodell 2');


INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (1, 1, 200, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (2, 1, 200, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (16, 1, 1000, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (1, 2, 300, 'c');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (2, 2, 300, 'c');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (6, 2, 200, 'c');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (12, 2, 1, 'c');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (2, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (3, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (4, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (7, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (8, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (14, 3, 42, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (22, 4, 10, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (12, 4, 3, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (11, 4, 4, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (1, 4, 12, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (5, 5, 3, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (8, 5, 4, 'f');
INSERT INTO latzhose."OrderableWorkItemInOrder" ("OrderableWorkItem_fk", "Order_fk", "OrderedCount", "Status") VALUES (16, 5, 7, 'p');

INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (5, 2, 5, 5, 'Size 54');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (7, 3, 4, 2, 'Slow');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (2, 1, 5, 3, 'Size 50');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (13, 6, 2, 1, 'Länge 1');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (3, 1, 5, 3, 'Size 56');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (1, 1, 10, 10, 'Size 48');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (15, 7, 10, 5, 'mit Schleimeimer');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (14, 6, 1, 1, 'Länge 2');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (6, 2, 5, 5, 'Size 56');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (22, 11, 2, 2, 'Size 48');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (21, 11, 2, 2, 'Size 42');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (20, 10, 2, 2, 'Size 46');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (19, 10, 2, 2, 'Size 44');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (18, 9, 2, 2, 'Size 44');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (4, 2, 10, 8, 'Size 52');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (12, 5, 5, 5, 'Size 47');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (17, 9, 2, 2, 'Size 42');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (9, 5, 5, 2, 'Size 43');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (16, 8, 10, 5, 'mit zweitem Mopp');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (11, 5, 5, 5, 'Size 46');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (10, 5, 5, 2, 'Size 45');
INSERT INTO latzhose."Inventory" ("Id", "WorkItem_fk", "Total", "Circulating", "PropertyDescription") VALUES (8, 4, 2, 2, 'Fast');

INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (1, 7, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (1, 22, 2);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (1, 15, 3);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (1, 16, 3);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (2, 17, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (2, 21, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (2, 8, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (3, 18, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (3, 19, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (3, 8, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (4, 7, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (4, 13, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (4, 14, 1);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (4, 1, 2);
INSERT INTO latzhose."InventoryInPerson" ("Person_fk", "Inventory_fk", "Count") VALUES (4, 12, 2);

INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (2, 1, 1);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (6, 1, 2);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (1, 2, 3);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (3, 2, 2);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (4, 2, 1);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (5, 3, 7);
INSERT INTO latzhose."WorkItemCategoryInApprenticeship" ("WorkitemCategory_fk", "Apprenticeship_fk", "Count") VALUES (6, 3, 1);

INSERT INTO latzhose."PersonInApprenticeship" ("Person_fk", "Apprenticeship_fk") VALUES (1, 3);
INSERT INTO latzhose."PersonInApprenticeship" ("Person_fk", "Apprenticeship_fk") VALUES (2, 1);
INSERT INTO latzhose."PersonInApprenticeship" ("Person_fk", "Apprenticeship_fk") VALUES (3, 1);
INSERT INTO latzhose."PersonInApprenticeship" ("Person_fk", "Apprenticeship_fk") VALUES (4, 2);
