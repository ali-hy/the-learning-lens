using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_TrainerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TimeLimit",
                table: "Modules",
                newName: "ModelBuildId");

            migrationBuilder.RenameColumn(
                name: "AccuracyThreshold",
                table: "Modules",
                newName: "ExaminationTaskId");

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    AccuracyThreshold = table.Column<float>(type: "real", nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningTasks_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prefabs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainers",
                columns: table => new
                {
                    TrainerId = table.Column<long>(type: "bigint", nullable: false),
                    TraineeId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    PrefabId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "BuildPieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PieceId = table.Column<long>(type: "bigint", nullable: false),
                    BuildId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildPieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildPieces_Builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildPieces_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PieceLinks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InPieceId = table.Column<long>(type: "bigint", nullable: true),
                    OutPieceId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PieceLinks_Pieces_InPieceId",
                        column: x => x.InPieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PieceLinks_Pieces_OutPieceId",
                        column: x => x.OutPieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuildPiecesLink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutPieceId = table.Column<long>(type: "bigint", nullable: false),
                    OutLinkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InPieceId = table.Column<long>(type: "bigint", nullable: false),
                    InLinkName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildPiecesLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildPiecesLink_BuildPieces_InPieceId",
                        column: x => x.InPieceId,
                        principalTable: "BuildPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuildPiecesLink_BuildPieces_OutPieceId",
                        column: x => x.OutPieceId,
                        principalTable: "BuildPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_BuildPieces_BuildId",
                table: "BuildPieces",
                column: "BuildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildPieces_PieceId",
                table: "BuildPieces",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildPiecesLink_InPieceId",
                table: "BuildPiecesLink",
                column: "InPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildPiecesLink_OutPieceId",
                table: "BuildPiecesLink",
                column: "OutPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningTasks_ModuleId",
                table: "LearningTasks",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceLinks_InPieceId",
                table: "PieceLinks",
                column: "InPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceLinks_OutPieceId",
                table: "PieceLinks",
                column: "OutPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_PrefabId",
                table: "Pieces",
                column: "PrefabId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefabs_Url",
                table: "Prefabs",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainers_TrainerId",
                table: "UserTrainers",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Builds_ModelBuildId",
                table: "Modules",
                column: "ModelBuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_LearningTasks_ExaminationTaskId",
                table: "Modules",
                column: "ExaminationTaskId",
                principalTable: "LearningTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Builds_ModelBuildId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_LearningTasks_ExaminationTaskId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "BuildPiecesLink");

            migrationBuilder.DropTable(
                name: "LearningTasks");

            migrationBuilder.DropTable(
                name: "PieceLinks");

            migrationBuilder.DropTable(
                name: "UserTrainers");

            migrationBuilder.DropTable(
                name: "BuildPieces");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "Prefabs");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ExaminationTaskId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ModelBuildId",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "ModelBuildId",
                table: "Modules",
                newName: "TimeLimit");

            migrationBuilder.RenameColumn(
                name: "ExaminationTaskId",
                table: "Modules",
                newName: "AccuracyThreshold");

            migrationBuilder.AddColumn<long>(
                name: "TrainerId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainerId",
                table: "AspNetUsers",
                column: "TrainerId",
                unique: true,
                filter: "[TrainerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_TrainerId",
                table: "AspNetUsers",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
