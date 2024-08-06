using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowersWhisperingAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    common_name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    scientific_name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    portrayal = table.Column<string>(type: "CLOB", nullable: false),
                    growth_environment = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    care_conditions = table.Column<string>(type: "CLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.plant_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    registration_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    user_role = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    user_status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    language_preference = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "MedicinalHerbs",
                columns: table => new
                {
                    herb_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    medicinal_properties = table.Column<string>(type: "CLOB", nullable: false),
                    usage_instructions = table.Column<string>(type: "CLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinalHerbs", x => x.herb_id);
                    table.ForeignKey(
                        name: "FK_MedicinalHerbs_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantRegionSuitability",
                columns: table => new
                {
                    suitability_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    region_name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    suitability = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantRegionSuitability", x => x.suitability_id);
                    table.ForeignKey(
                        name: "FK_PlantRegionSuitability_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    article_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    article_title = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    article_content = table.Column<string>(type: "CLOB", nullable: false),
                    publisher_id = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    published_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ArticleType = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.article_id);
                    table.ForeignKey(
                        name: "FK_Articles_Users_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "CareLogs",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    care_activity = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    log_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareLogs", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_CareLogs_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareLogs_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareReminders",
                columns: table => new
                {
                    reminder_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ReminderText = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    reminder_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<string>(type: "NVARCHAR2(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareReminders", x => x.reminder_id);
                    table.ForeignKey(
                        name: "FK_CareReminders_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareReminders_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    comment_content = table.Column<string>(type: "CLOB", nullable: false),
                    created_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comments_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    favorite_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.favorite_id);
                    table.ForeignKey(
                        name: "FK_Favorites_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    feedback_content = table.Column<string>(type: "CLOB", nullable: false),
                    submitted_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    sender_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    receiver_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    message_content = table.Column<string>(type: "CLOB", nullable: false),
                    sent_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.message_id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantImages",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    image_url = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    recognition_result = table.Column<string>(type: "CLOB", nullable: true),
                    diagnosis_result = table.Column<string>(type: "CLOB", nullable: true),
                    upload_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantImages", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_PlantImages_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantImages_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    modified_content = table.Column<string>(type: "CLOB", nullable: false),
                    submitted_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    submitter_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    plant_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    review_status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    review_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Reviews_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_submitter_id",
                        column: x => x.submitter_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLogs",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ActivityType = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    activity_description = table.Column<string>(type: "CLOB", nullable: false),
                    activity_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivityLogs", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_UserActivityLogs_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    group_name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    creator_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPoints",
                columns: table => new
                {
                    points_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    user_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    points = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    last_updated = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPoints", x => x.points_id);
                    table.ForeignKey(
                        name: "FK_UserPoints_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_publisher_id",
                table: "Articles",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_CareLogs_plant_id",
                table: "CareLogs",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_CareLogs_user_id",
                table: "CareLogs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CareReminders_plant_id",
                table: "CareReminders",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_CareReminders_user_id",
                table: "CareReminders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_plant_id",
                table: "Comments",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_plant_id",
                table: "Favorites",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_user_id",
                table: "Favorites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_user_id",
                table: "Feedbacks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinalHerbs_plant_id",
                table: "MedicinalHerbs",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_receiver_id",
                table: "Messages",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_sender_id",
                table: "Messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantImages_plant_id",
                table: "PlantImages",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantImages_user_id",
                table: "PlantImages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantRegionSuitability_plant_id",
                table: "PlantRegionSuitability",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_plant_id",
                table: "Reviews",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_submitter_id",
                table: "Reviews",
                column: "submitter_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_user_id",
                table: "UserActivityLogs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_creator_id",
                table: "UserGroups",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPoints_user_id",
                table: "UserPoints",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "CareLogs");

            migrationBuilder.DropTable(
                name: "CareReminders");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "MedicinalHerbs");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PlantImages");

            migrationBuilder.DropTable(
                name: "PlantRegionSuitability");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserActivityLogs");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserPoints");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
