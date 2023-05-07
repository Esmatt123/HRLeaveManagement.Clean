using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Identity.Migrations
{
    /// <inheritdoc />
    public partial class updatingeverything : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1188e72a-a583-4b1c-bc29-4dd606e614fe", "AQAAAAIAAYagAAAAELPyBURS7JhGUuupPYOILDKPe3nLIKTLNvuwbgJXkA2QqQOPvFwdnPkEci04H0tQqQ==", "e7d6f9eb-511c-4450-b074-0bc93f156528" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "624e3106-23cb-4e60-834b-4a9f1dd21166", "AQAAAAIAAYagAAAAEOYv7KLvQAkQUt4q4yOqHnP2VWLjuPxGmWWAClSTMrVttB8GkcArFVqOCegwUKGYrw==", "b9caa8d1-b3f3-473e-b687-4541fa65a00e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "158e7a6b-8bf6-4b9a-be57-2b732741b5ac", "AQAAAAIAAYagAAAAEN6iiSy1Vm029s93orwcVoclClTzBrhhc46gUQVbUXeysZfE9G4/FEWoFn3LDP+bLQ==", "84513364-daa0-44ba-81d7-ff7a52db78de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2946968a-1863-4e4f-a1d7-7b5c126badb1", "AQAAAAIAAYagAAAAEI2Zr3C1cogKY9cek3KEYTA8uK8hrSEKHG+k3IjDBHZR5c5dDEpDYVfmahejs1FPGw==", "41165c25-0014-49c7-a273-70c7912fe5dd" });
        }
    }
}
