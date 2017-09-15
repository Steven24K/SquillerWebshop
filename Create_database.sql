CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Customer" (
    "Id" serial NOT NULL,
    "Adress" text NULL,
    "City" text NULL,
    "Email" text NULL,
    "Gender" int4 NOT NULL,
    "Name" text NULL,
    "Password" text NULL,
    "PostalCode" text NULL,
    "RegistrationDate" timestamp NOT NULL,
    "Surname" text NULL,
    CONSTRAINT "PK_Customer" PRIMARY KEY ("Id")
);

CREATE TABLE "Product" (
    "Id" serial NOT NULL,
    "Amount" int4 NOT NULL,
    "Brand" text NULL,
    "Category" text NULL,
    "DateAdded" timestamp NOT NULL,
    "Description" text NULL,
    "Extra" int4 NOT NULL,
    "Gender" int4 NOT NULL,
    "Name" text NULL,
    "Price" float8 NOT NULL,
    CONSTRAINT "PK_Product" PRIMARY KEY ("Id")
);

CREATE TABLE "shoppingCard" (
    "CustomerId" int4 NOT NULL,
    "ProductId" int4 NOT NULL,
    CONSTRAINT "PK_shoppingCard" PRIMARY KEY ("CustomerId", "ProductId"),
    CONSTRAINT "FK_shoppingCard_Customer_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customer" ("Id") ON DELETE C
ASCADE,
    CONSTRAINT "FK_shoppingCard_Product_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Product" ("Id") ON DELETE CASCA
DE
);

CREATE INDEX "IX_shoppingCard_ProductId" ON "shoppingCard" ("ProductId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20170914124311_InitialCreateWebshopDB', '2.0.0-rtm-26452');