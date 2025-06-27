using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveArchivedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildPieces_Builds_BuildId",
                table: "BuildPieces");

            migrationBuilder.DropTable(
                name: "BuildPiecesLink");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "UserTrainers");

            migrationBuilder.DropTable(
                name: "PieceLinks");

            migrationBuilder.DropTable(
                name: "LearningTasks");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropTable(
                name: "BuildPieces");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Preview",
                table: "Lessons",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsTest",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTest",
                table: "Lessons");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Preview",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.CreateTable(
                name: "LearningTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccuracyThreshold = table.Column<float>(type: "real", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrefabId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pieces_Prefabs_PrefabId",
                        column: x => x.PrefabId,
                        principalTable: "Prefabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainers",
                columns: table => new
                {
                    TraineeId = table.Column<long>(type: "bigint", nullable: false),
                    TrainerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrainers", x => new { x.TraineeId, x.TrainerId });
                    table.ForeignKey(
                        name: "FK_UserTrainers_AspNetUsers_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrainers_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PieceLinks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PieceId = table.Column<long>(type: "bigint", nullable: false),
                    JsonDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PieceLinks_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuildPieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildId = table.Column<long>(type: "bigint", nullable: false),
                    PieceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildPieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildPieces_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildPiecesLink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoldingBuildPieceId = table.Column<long>(type: "bigint", nullable: false),
                    OnBuildPieceId = table.Column<long>(type: "bigint", nullable: false),
                    SocketTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildPiecesLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildPiecesLink_BuildPieces_HoldingBuildPieceId",
                        column: x => x.HoldingBuildPieceId,
                        principalTable: "BuildPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuildPiecesLink_BuildPieces_OnBuildPieceId",
                        column: x => x.OnBuildPieceId,
                        principalTable: "BuildPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildPiecesLink_PieceLinks_SocketTypeId",
                        column: x => x.SocketTypeId,
                        principalTable: "PieceLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPieceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builds_BuildPieces_FirstPieceId",
                        column: x => x.FirstPieceId,
                        principalTable: "BuildPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    ExaminationTaskId = table.Column<long>(type: "bigint", nullable: false),
                    ModelBuildId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modules_Builds_ModelBuildId",
                        column: x => x.ModelBuildId,
                        principalTable: "Builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modules_LearningTasks_ExaminationTaskId",
                        column: x => x.ExaminationTaskId,
                        principalTable: "LearningTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildPieces_BuildId",
                table: "BuildPieces",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildPieces_PieceId",
                table: "BuildPieces",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildPiecesLink_HoldingBuildPieceId",
                table: "BuildPiecesLink",
                column: "HoldingBuildPieceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildPiecesLink_OnBuildPieceId",
                table: "BuildPiecesLink",
                column: "OnBuildPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildPiecesLink_SocketTypeId",
                table: "BuildPiecesLink",
                column: "SocketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_FirstPieceId",
                table: "Builds",
                column: "FirstPieceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CreatedById",
                table: "Modules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ExaminationTaskId",
                table: "Modules",
                column: "ExaminationTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ModelBuildId",
                table: "Modules",
                column: "ModelBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceLinks_PieceId",
                table: "PieceLinks",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_PrefabId",
                table: "Pieces",
                column: "PrefabId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainers_TrainerId",
                table: "UserTrainers",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildPieces_Builds_BuildId",
                table: "BuildPieces",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
