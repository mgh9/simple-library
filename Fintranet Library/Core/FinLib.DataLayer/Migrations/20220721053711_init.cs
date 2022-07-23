using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinLib.DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CNT");

            migrationBuilder.EnsureSchema(
                name: "SEC");

            migrationBuilder.CreateTable(
                name: "BookBorrowings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CustomerUserRoleId = table.Column<int>(type: "int", nullable: false),
                    LibrarianUserRoleId = table.Column<int>(type: "int", nullable: false),
                    BorrowingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuLinkOwners",
                schema: "CNT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuLinkId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuLinkOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuLinks",
                schema: "CNT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuLinks_MenuLinks_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "CNT",
                        principalTable: "MenuLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "SEC",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEC",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserRoles_CreatedByUserRoleId",
                        column: x => x.CreatedByUserRoleId,
                        principalSchema: "SEC",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_UserRoles_UpdatedByUserRoleId",
                        column: x => x.UpdatedByUserRoleId,
                        principalSchema: "SEC",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Entity PrimaryKey")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLoggedInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedByUserRoleId = table.Column<int>(type: "int", nullable: true, comment: "شناسه ی کاربر ویرایش کننده"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ویرایش"),
                    CreatedByUserRoleId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_CreatedByUserRoleId",
                        column: x => x.CreatedByUserRoleId,
                        principalSchema: "SEC",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UpdatedByUserRoleId",
                        column: x => x.UpdatedByUserRoleId,
                        principalSchema: "SEC",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "SEC",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_BookId",
                table: "BookBorrowings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_CreatedByUserRoleId",
                table: "BookBorrowings",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_CustomerUserRoleId",
                table: "BookBorrowings",
                column: "CustomerUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_LibrarianUserRoleId",
                table: "BookBorrowings",
                column: "LibrarianUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_UpdatedByUserRoleId",
                table: "BookBorrowings",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByUserRoleId",
                table: "Books",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UpdatedByUserRoleId",
                table: "Books",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedByUserRoleId",
                table: "Categories",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedByUserRoleId",
                table: "Categories",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinkOwners_CreatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinkOwners_MenuLinkId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "MenuLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinkOwners_RoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinkOwners_UpdatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinks_CreatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinks",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinks_ParentId",
                schema: "CNT",
                table: "MenuLinks",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinks_Title",
                schema: "CNT",
                table: "MenuLinks",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLinks_UpdatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinks",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "SEC",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedByUserRoleId",
                schema: "SEC",
                table: "Roles",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "SEC",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "SEC",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "SEC",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_CreatedByUserRoleId",
                schema: "SEC",
                table: "UserRoles",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "SEC",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UpdatedByUserRoleId",
                schema: "SEC",
                table: "UserRoles",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "SEC",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "SEC",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedByUserRoleId",
                schema: "SEC",
                table: "Users",
                column: "CreatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedByUserRoleId",
                schema: "SEC",
                table: "Users",
                column: "UpdatedByUserRoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "SEC",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowings_Books_BookId",
                table: "BookBorrowings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowings_UserRoles_CreatedByUserRoleId",
                table: "BookBorrowings",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowings_UserRoles_CustomerUserRoleId",
                table: "BookBorrowings",
                column: "CustomerUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowings_UserRoles_LibrarianUserRoleId",
                table: "BookBorrowings",
                column: "LibrarianUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowings_UserRoles_UpdatedByUserRoleId",
                table: "BookBorrowings",
                column: "UpdatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserRoles_CreatedByUserRoleId",
                table: "Books",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserRoles_UpdatedByUserRoleId",
                table: "Books",
                column: "UpdatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_UserRoles_CreatedByUserRoleId",
                table: "Categories",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_UserRoles_UpdatedByUserRoleId",
                table: "Categories",
                column: "UpdatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinkOwners_MenuLinks_MenuLinkId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "MenuLinkId",
                principalSchema: "CNT",
                principalTable: "MenuLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinkOwners_Roles_RoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "RoleId",
                principalSchema: "SEC",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinkOwners_UserRoles_CreatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinkOwners_UserRoles_UpdatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinkOwners",
                column: "UpdatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinks_UserRoles_CreatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinks",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLinks_UserRoles_UpdatedByUserRoleId",
                schema: "CNT",
                table: "MenuLinks",
                column: "UpdatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                schema: "SEC",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "SEC",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserRoles_CreatedByUserRoleId",
                schema: "SEC",
                table: "Roles",
                column: "CreatedByUserRoleId",
                principalSchema: "SEC",
                principalTable: "UserRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "SEC",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "SEC",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "SEC",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "SEC",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "SEC",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "SEC",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserRoles_CreatedByUserRoleId",
                schema: "SEC",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_CreatedByUserRoleId",
                schema: "SEC",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UpdatedByUserRoleId",
                schema: "SEC",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BookBorrowings");

            migrationBuilder.DropTable(
                name: "MenuLinkOwners",
                schema: "CNT");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "MenuLinks",
                schema: "CNT");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "SEC");
        }
    }
}
