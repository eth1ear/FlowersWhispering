using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowersWhisperingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UseUserIdSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Users",
                type: "NUMBER(10)",
                nullable: false,
                defaultValueSql: "user_id_seq.NEXTVAL",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Users",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldDefaultValueSql: "user_id_seq.NEXTVAL")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");
        }
    }
}
