using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartOffice.Migrations
{
    public partial class AddDataProtectionKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetGroup",
                columns: table => new
                {
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    GroupName = table.Column<string>(maxLength: 100, nullable: false),
                    DivisionCode = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true),
                    OrderNo = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetGroup", x => x.GroupCateg);
                });

            migrationBuilder.CreateTable(
                name: "AspNetMenu",
                columns: table => new
                {
                    MenuIdentity = table.Column<int>(nullable: false),
                    MenuNameE = table.Column<string>(maxLength: 250, nullable: true),
                    MenuNameT = table.Column<string>(maxLength: 250, nullable: true),
                    MenuNameJ = table.Column<string>(maxLength: 250, nullable: true),
                    Action = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Controller = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Param = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    MenuUrl = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Image = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    IconClass = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Badges = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    BadgesName = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Download = table.Column<bool>(nullable: false),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetMenu", x => x.MenuIdentity);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmPassword_Reset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    confirm_id = table.Column<string>(maxLength: 50, nullable: true),
                    active_status = table.Column<bool>(nullable: true),
                    computer_name = table.Column<string>(maxLength: 50, nullable: true),
                    request_date = table.Column<DateTime>(type: "date", nullable: true),
                    confrim_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmPassword_Reset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControlPart",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(maxLength: 100, nullable: true),
                    Action = table.Column<string>(maxLength: 100, nullable: true),
                    Controller = table.Column<string>(maxLength: 100, nullable: true),
                    PathUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPart", x => x.PartID);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    DivisionCode = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    DivisionName = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.DivisionCode);
                });

            migrationBuilder.CreateTable(
                name: "FormMenu",
                columns: table => new
                {
                    MenuIdentity = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(nullable: false),
                    MenuIdentityParent = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    GroupMenuID = table.Column<int>(nullable: false),
                    MenuNameE = table.Column<string>(nullable: true),
                    MenuNameT = table.Column<string>(nullable: true),
                    MenuNameJ = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Badges = table.Column<string>(nullable: true),
                    BadgesName = table.Column<string>(nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    Disable = table.Column<bool>(nullable: false),
                    PartID = table.Column<int>(nullable: false),
                    GroupDisplayOrder = table.Column<int>(nullable: false),
                    GroupMenuName = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormMenu", x => new { x.GroupCateg, x.MenuIdentity });
                });

            migrationBuilder.CreateTable(
                name: "GenerateNumber",
                columns: table => new
                {
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Type_id = table.Column<string>(maxLength: 10, nullable: false),
                    Sequence = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerateNumber", x => new { x.Year, x.Month, x.Type_id });
                });

            migrationBuilder.CreateTable(
                name: "menuCMMFooter",
                columns: table => new
                {
                    DisplayOrder = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(nullable: false),
                    MenuNameE = table.Column<string>(nullable: true),
                    MenuNameT = table.Column<string>(nullable: true),
                    MenuNameJ = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Badges = table.Column<string>(nullable: true),
                    BadgesName = table.Column<string>(nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuCMMFooter", x => new { x.GroupCateg, x.DisplayOrder });
                });

            migrationBuilder.CreateTable(
                name: "MenuCMMHeader",
                columns: table => new
                {
                    DisplayOrder = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(nullable: false),
                    MenuNameE = table.Column<string>(nullable: true),
                    MenuNameT = table.Column<string>(nullable: true),
                    MenuNameJ = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Badges = table.Column<string>(nullable: true),
                    BadgesName = table.Column<string>(nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    MenuIdentity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCMMHeader", x => new { x.GroupCateg, x.DisplayOrder });
                });

            migrationBuilder.CreateTable(
                name: "subSystems",
                columns: table => new
                {
                    DisplayOrder = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(nullable: false),
                    MenuIdentity = table.Column<int>(nullable: false),
                    GroupSub = table.Column<string>(nullable: false),
                    MenuNameE = table.Column<string>(nullable: true),
                    MenuNameT = table.Column<string>(nullable: true),
                    MenuNameJ = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    IconClass = table.Column<string>(nullable: true),
                    Badges = table.Column<string>(nullable: true),
                    BadgesName = table.Column<string>(nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    MenuIdentityParent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subSystems", x => new { x.GroupCateg, x.GroupSub, x.MenuIdentity, x.DisplayOrder });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 200, nullable: false),
                    UserName = table.Column<string>(maxLength: 8, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 8, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OperatorSign = table.Column<string>(unicode: false, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true),
                    Bldg = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetGroup_DocumentGroupCateg",
                        column: x => x.GroupCateg,
                        principalTable: "AspNetGroup",
                        principalColumn: "GroupCateg",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetMenuRoles",
                columns: table => new
                {
                    MenuIdentity = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetMenuRoles", x => new { x.MenuIdentity, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetMenuRoles_AspNetMenu_MenuIdentity",
                        column: x => x.MenuIdentity,
                        principalTable: "AspNetMenu",
                        principalColumn: "MenuIdentity",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetMenuRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(maxLength: 200, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetGroupMenu",
                columns: table => new
                {
                    GroupMenuID = table.Column<int>(nullable: false),
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    GroupDisplayOrder = table.Column<int>(nullable: false),
                    GroupMenuName = table.Column<string>(maxLength: 100, nullable: true),
                    IconClass = table.Column<string>(maxLength: 100, nullable: true),
                    Badges = table.Column<string>(maxLength: 100, nullable: true),
                    BadgesName = table.Column<string>(maxLength: 100, nullable: true),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetGroupMenu_1", x => x.GroupMenuID);
                    table.ForeignKey(
                        name: "FK_AspNetGroupMenu_AspNetGroup_DocumentGroupCateg",
                        column: x => x.GroupCateg,
                        principalTable: "AspNetGroup",
                        principalColumn: "GroupCateg",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetGroupMenu_ControlPart_PartID",
                        column: x => x.PartID,
                        principalTable: "ControlPart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlConfig",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    ConfigOrder = table.Column<int>(nullable: false),
                    TextH = table.Column<string>(maxLength: 100, nullable: true),
                    ColorTextH = table.Column<string>(maxLength: 100, nullable: true),
                    BgH = table.Column<string>(maxLength: 100, nullable: true),
                    CorlorTextD = table.Column<string>(maxLength: 100, nullable: true),
                    BgD = table.Column<string>(maxLength: 100, nullable: true),
                    ColorButton = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlConfig", x => new { x.PartID, x.GroupCateg, x.ConfigOrder });
                    table.ForeignKey(
                        name: "FK_ControlConfig_AspNetGroup_DocumentGroupCateg",
                        column: x => x.GroupCateg,
                        principalTable: "AspNetGroup",
                        principalColumn: "GroupCateg",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlConfig_ControlPart_PartID",
                        column: x => x.PartID,
                        principalTable: "ControlPart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgHeader",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    ImgOrder = table.Column<int>(nullable: false),
                    ImgPath = table.Column<string>(maxLength: 100, nullable: true),
                    ImgActive = table.Column<bool>(nullable: true),
                    ImgType = table.Column<string>(maxLength: 100, nullable: true),
                    Link = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    LinkName = table.Column<string>(maxLength: 100, nullable: true),
                    LinkPath = table.Column<string>(maxLength: 100, nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgHeader", x => new { x.PartID, x.GroupCateg, x.ImgOrder });
                    table.ForeignKey(
                        name: "FK_ImgHeader_AspNetGroup_DocumentGroupCateg",
                        column: x => x.GroupCateg,
                        principalTable: "AspNetGroup",
                        principalColumn: "GroupCateg",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImgHeader_ControlPart_PartID",
                        column: x => x.PartID,
                        principalTable: "ControlPart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NNewsHeader",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    NewHOrder = table.Column<int>(nullable: false),
                    NewsType = table.Column<string>(maxLength: 100, nullable: true),
                    ChildType = table.Column<int>(nullable: true),
                    Title1 = table.Column<string>(maxLength: 400, nullable: true),
                    Title2 = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ImgPath = table.Column<string>(maxLength: 100, nullable: true),
                    LinkPath = table.Column<string>(maxLength: 100, nullable: true),
                    IconClass = table.Column<string>(maxLength: 100, nullable: true),
                    Badges = table.Column<string>(maxLength: 100, nullable: true),
                    BadgesName = table.Column<string>(maxLength: 100, nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    Disable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsHeader", x => new { x.PartID, x.GroupCateg, x.NewHOrder });
                    table.ForeignKey(
                        name: "FK_NewsHeader_AspNetGroup_DocumentGroupCateg",
                        column: x => x.GroupCateg,
                        principalTable: "AspNetGroup",
                        principalColumn: "GroupCateg",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsHeader_ControlPart_PartID",
                        column: x => x.PartID,
                        principalTable: "ControlPart",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    RoleId = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetMenuControl",
                columns: table => new
                {
                    MenuIdentity = table.Column<int>(nullable: false),
                    MenuIdentityParent = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    GroupMenuID = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetMenuControl", x => new { x.MenuIdentity, x.MenuIdentityParent, x.DisplayOrder, x.GroupMenuID });
                    table.ForeignKey(
                        name: "FK_AspNetMenuControl_AspNetGroupMenu_GroupMenuID",
                        column: x => x.GroupMenuID,
                        principalTable: "AspNetGroupMenu",
                        principalColumn: "GroupMenuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetMenuControl_AspNetMenu_MenuIdentity",
                        column: x => x.MenuIdentity,
                        principalTable: "AspNetMenu",
                        principalColumn: "MenuIdentity",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetMenuControl_AspNetMenu_MenuIdentityParent",
                        column: x => x.MenuIdentityParent,
                        principalTable: "AspNetMenu",
                        principalColumn: "MenuIdentity",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgTextHeader",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    ImgOrder = table.Column<int>(nullable: false),
                    TextHOrder = table.Column<int>(nullable: false),
                    TextH = table.Column<string>(maxLength: 300, nullable: false),
                    TextD = table.Column<string>(maxLength: 300, nullable: true),
                    Link = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    LinkPath = table.Column<string>(maxLength: 100, nullable: true),
                    Download = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgTextHeader", x => new { x.PartID, x.GroupCateg, x.ImgOrder, x.TextHOrder });
                    table.ForeignKey(
                        name: "FK_ImgTextHeader_ImgHeader",
                        columns: x => new { x.PartID, x.GroupCateg, x.ImgOrder },
                        principalTable: "ImgHeader",
                        principalColumns: new[] { "PartID", "GroupCateg", "ImgOrder" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NNewsDetail",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false),
                    GroupCateg = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    NewHOrder = table.Column<int>(nullable: false),
                    NewDOrder = table.Column<int>(nullable: false),
                    ItemType = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 400, nullable: true),
                    Value1 = table.Column<string>(maxLength: 400, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(maxLength: 8, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateBy = table.Column<string>(maxLength: 8, nullable: true),
                    ShowPublic = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsDetail", x => new { x.PartID, x.GroupCateg, x.NewHOrder, x.NewDOrder });
                    table.ForeignKey(
                        name: "FK_NewsDetail_NewsHeader",
                        columns: x => new { x.PartID, x.GroupCateg, x.NewHOrder },
                        principalTable: "NNewsHeader",
                        principalColumns: new[] { "PartID", "GroupCateg", "NewHOrder" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetGroupMenu_GroupCateg",
                table: "AspNetGroupMenu",
                column: "GroupCateg");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetGroupMenu_PartID",
                table: "AspNetGroupMenu",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenuControl_GroupMenuID",
                table: "AspNetMenuControl",
                column: "GroupMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenuControl_MenuIdentityParent",
                table: "AspNetMenuControl",
                column: "MenuIdentityParent");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenuRoles_RoleId",
                table: "AspNetMenuRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupCateg",
                table: "AspNetUsers",
                column: "GroupCateg");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_ControlConfig_GroupCateg",
                table: "ControlConfig",
                column: "GroupCateg");

            migrationBuilder.CreateIndex(
                name: "IX_ImgHeader_GroupCateg",
                table: "ImgHeader",
                column: "GroupCateg");

            migrationBuilder.CreateIndex(
                name: "IX_NNewsHeader_GroupCateg",
                table: "NNewsHeader",
                column: "GroupCateg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetMenuControl");

            migrationBuilder.DropTable(
                name: "AspNetMenuRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ConfirmPassword_Reset");

            migrationBuilder.DropTable(
                name: "ControlConfig");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "FormMenu");

            migrationBuilder.DropTable(
                name: "GenerateNumber");

            migrationBuilder.DropTable(
                name: "ImgTextHeader");

            migrationBuilder.DropTable(
                name: "menuCMMFooter");

            migrationBuilder.DropTable(
                name: "MenuCMMHeader");

            migrationBuilder.DropTable(
                name: "NNewsDetail");

            migrationBuilder.DropTable(
                name: "subSystems");

            migrationBuilder.DropTable(
                name: "AspNetGroupMenu");

            migrationBuilder.DropTable(
                name: "AspNetMenu");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ImgHeader");

            migrationBuilder.DropTable(
                name: "NNewsHeader");

            migrationBuilder.DropTable(
                name: "AspNetGroup");

            migrationBuilder.DropTable(
                name: "ControlPart");
        }
    }
}
