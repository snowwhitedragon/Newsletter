using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newsletter.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Newslett__3214EC07399C52A4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ResponsibilityType = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organiza__3214EC070EC7FF12", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC07336C1F07", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__States__3214EC0785E078BD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcontractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subcontr__3214EC0768BF9C51", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__3214EC0767A70F8D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationNewsletters",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    NewsletterId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organiza__099116EDF2063989", x => new { x.OrganizationId, x.NewsletterId });
                    table.ForeignKey(
                        name: "FK__Organizat__Newsl__671F4F74",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Organizat__Organ__662B2B3B",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    ReadableId = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Salutation = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StateId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    LanguageId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contacts__3214EC07EE55F82D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Contacts__Langua__7A3223E8",
                        column: x => x.LanguageId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Contacts__StateI__7755B73D",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    ReadableId = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC077726D01B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Customers__Conta__65370702",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubcontractorContacts",
                columns: table => new
                {
                    SubcontractorId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subcontr__47CDD96A4176EC5B", x => new { x.SubcontractorId, x.ContactId });
                    table.ForeignKey(
                        name: "FK__Subcontra__Conta__690797E6",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Subcontra__Subco__681373AD",
                        column: x => x.SubcontractorId,
                        principalTable: "Subcontractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    NewsletterId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__9F2C3864C76C0BCE", x => new { x.ContactId, x.NewsletterId });
                    table.ForeignKey(
                        name: "FK__Subscript__Conta__6AEFE058",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Subscript__Newsl__69FBBC1F",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContacts",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__EE2004ED308C8D1D", x => new { x.SupplierId, x.ContactId });
                    table.ForeignKey(
                        name: "FK__SupplierC__Conta__6CD828CA",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__SupplierC__Suppl__6BE40491",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    RegistratedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC07DD65953D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__ContactId__03BB8E22",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Users__Organizat__02C769E9",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "(upper(hex(randomblob(16))))", collation: "NOCASE"),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Picture = table.Column<byte[]>(type: "BLOB", nullable: false),
                    NewsletterId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PublishedById = table.Column<Guid>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC071BAB57FB", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Articles__Create__1F63A897",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Articles__Newsle__1E6F845E",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Articles__Organi__214BF109",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Articles__Publis__1D7B6025",
                        column: x => x.PublishedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Articles__Update__2057CCD0",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__AF2760AD2783AA04", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK__UserRoles__RoleI__6EC0713C",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UserRoles__UserI__01D345B0",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Creator",
                table: "Articles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Newsletter",
                table: "Articles",
                column: "NewsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_OrganizationId",
                table: "Articles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Publisher",
                table: "Articles",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Updated",
                table: "Articles",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_State",
                table: "Contacts",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Language",
                table: "Contacts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Contact",
                table: "Customers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNewsletters_Newsletter",
                table: "OrganizationNewsletters",
                column: "NewsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNewsletters_Organization",
                table: "OrganizationNewsletters",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "UQ__States__2CB664DC2034B2EF",
                table: "States",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__States__8B8C8A34392D791F",
                table: "States",
                column: "LanguageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubcontractorContacts_Contact",
                table: "SubcontractorContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcontractorContacts_Subcontractor",
                table: "SubcontractorContacts",
                column: "SubcontractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Contact",
                table: "Subscriptions",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Newsletter",
                table: "Subscriptions",
                column: "NewsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_Contact",
                table: "SupplierContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_Supplier",
                table: "SupplierContacts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Role",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_User",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Contact",
                table: "Users",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Organization",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "UQ__tmp_ms_x__536C85E4AB504FAA",
                table: "Users",
                column: "Username",
                unique: true);

            // Insert seed data into Roles table
            migrationBuilder.Sql("INSERT INTO Roles (Id, Code, Title) VALUES " +
                "('A8A77FEA-12E5-45AE-8F1E-5D774FA67F37', 'SYS', 'Systemadministrator'), " +
                "('AC40C0A9-072A-4E2B-ABE0-293237BA9965', 'ADM', 'Administrator'), " +
                "('87A21EAA-09DF-48D1-AC70-224C1AF72DBE', 'LEAD', 'Leitung'), " +
                "('B53C2C1C-9D7A-439D-922F-D00AFBA761A3', 'EMP', 'Employee'), " +
                "('13FD0538-0F80-47D3-87D7-C7B19CEB8CF4', 'GUEST', 'Gast');");

            // Insert seed data into Organizations table
            migrationBuilder.Sql("INSERT INTO Organizations (Id, Title, ResponsibilityType, Description) VALUES " +
                "('C47D77B2-A90F-403B-91AF-DAC47AD83372', 'Nentindo', 0, 'Nentindo ist für das Kundengeschäft verantwortlich und repräsentiert auch die Hauptorganisation.'), " +
                "('A1AA1C10-06DB-4384-B8B2-4267BCF695D5', 'N Production', 1, 'N Production ist ein eigenständiger Zweig für Lieferanten zur Planung, Entwicklung und Produktion von Hardware.'), " +
                "('7A137C03-F6FC-4A0B-BBED-F9849FE183D4', 'N Studios', 2, 'N Studios ist die Dachorganisation für Nachunternehmer zur Produktion von Games.');");

            // Insert seed data into Users table
            migrationBuilder.Sql("INSERT INTO Users (Id, Username, DisplayName, PasswordHash, OrganizationId) VALUES " +
                "('EE748299-BF60-4B6E-A8B3-1C9EA329933D', 'nentindo.sensei', 'Sensei Kanri-Sha', 'AQAAAAIAAYagAAAAEOv/hlo8cXnuzWLyO5Jx6zRWek5VDzEgLcf7Jaq2DQnxTaduv4eCAm6HsiP1fKJjDg==', 'C47D77B2-A90F-403B-91AF-DAC47AD83372');");

            // Insert seed data into UserRoles table
            migrationBuilder.Sql("INSERT INTO UserRoles (UserId, RoleId) VALUES " +
                "('EE748299-BF60-4B6E-A8B3-1C9EA329933D', 'AC40C0A9-072A-4E2B-ABE0-293237BA9965');");

            // Insert seed data into Newsletters table
            migrationBuilder.Sql("INSERT INTO Newsletters (Id, Title) VALUES " +
                "('1AF418DC-0E5B-4C90-A9E0-4381FB69ACC6', 'Konzernweite Informationen'), " +
                "('55621593-7A0B-4EB9-91DF-240C330C0544', 'Ankündigung neuer Games'), " +
                "('380D60FA-FF17-4E01-8995-5CF27AE394B5', 'Rabatte und Promos')," +
                "('7AD2022C-F3B0-4468-B6DC-B9483A570D8A', 'Ankündigungen '), " +
                "('3B9C4EEB-2948-4E30-A4E4-FC03AB9B7E83', 'Projektupdates'), " +
                "('08F4FB4D-8845-4BF6-8A32-27B9992BAAAC', 'Partnerinformationen'), " +
                "('0FF6A1F2-FC03-4EB3-8018-CF1A0CCD9AB7', 'Lieferketten Updates');");

            // Insert seed data into OrganizationNewsletters table
            migrationBuilder.Sql("INSERT INTO OrganizationNewsletters (NewsletterId, OrganizationId) VALUES " +
                "('1AF418DC-0E5B-4C90-A9E0-4381FB69ACC6', 'C47D77B2-A90F-403B-91AF-DAC47AD83372'), " +
                "('55621593-7A0B-4EB9-91DF-240C330C0544', 'C47D77B2-A90F-403B-91AF-DAC47AD83372'), " +
                "('380D60FA-FF17-4E01-8995-5CF27AE394B5', 'C47D77B2-A90F-403B-91AF-DAC47AD83372'), " +
                "('7AD2022C-F3B0-4468-B6DC-B9483A570D8A', 'A1AA1C10-06DB-4384-B8B2-4267BCF695D5'), " +
                "('7AD2022C-F3B0-4468-B6DC-B9483A570D8A', '7A137C03-F6FC-4A0B-BBED-F9849FE183D4'), " +
                "('3B9C4EEB-2948-4E30-A4E4-FC03AB9B7E83', 'A1AA1C10-06DB-4384-B8B2-4267BCF695D5'), " +
                "('3B9C4EEB-2948-4E30-A4E4-FC03AB9B7E83', '7A137C03-F6FC-4A0B-BBED-F9849FE183D4'), " +
                "('08F4FB4D-8845-4BF6-8A32-27B9992BAAAC', '7A137C03-F6FC-4A0B-BBED-F9849FE183D4'), " +
                "('0FF6A1F2-FC03-4EB3-8018-CF1A0CCD9AB7', 'A1AA1C10-06DB-4384-B8B2-4267BCF695D5');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrganizationNewsletters");

            migrationBuilder.DropTable(
                name: "SubcontractorContacts");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SupplierContacts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Subcontractors");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
