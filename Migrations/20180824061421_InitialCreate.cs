using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalSplitWise.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(nullable: true),
                    email_id = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    phone_no = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    friendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => x.id);
                    table.ForeignKey(
                        name: "FK_friends_users_friendId",
                        column: x => x.friendId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_friends_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    groupid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    group_name = table.Column<string>(nullable: true),
                    group_created_by = table.Column<int>(nullable: false),
                    is_simplified_depts = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.groupid);
                    table.ForeignKey(
                        name: "FK_groups_users_group_created_by",
                        column: x => x.group_created_by,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    billid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: true),
                    total_amount = table.Column<double>(nullable: false),
                    bill_created_byId = table.Column<int>(nullable: false),
                    bill_updated_byId = table.Column<int>(nullable: false),
                    bill_date = table.Column<DateTime>(nullable: false),
                    bill_created_at = table.Column<DateTime>(nullable: false),
                    bill_updated_at = table.Column<DateTime>(nullable: false),
                    groupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.billid);
                    table.ForeignKey(
                        name: "FK_bills_users_bill_created_byId",
                        column: x => x.bill_created_byId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_bills_users_bill_updated_byId",
                        column: x => x.bill_updated_byId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_bills_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "groupid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "groupMembers",
                columns: table => new
                {
                    memberid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    groupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupMembers", x => x.memberid);
                    table.ForeignKey(
                        name: "FK_groupMembers_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "groupid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupMembers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "settlements",
                columns: table => new
                {
                    settlementid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    payerId = table.Column<int>(nullable: false),
                    payeeId = table.Column<int>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    groupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settlements", x => x.settlementid);
                    table.ForeignKey(
                        name: "FK_settlements_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "groupid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_settlements_users_payeeId",
                        column: x => x.payeeId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_settlements_users_payerId",
                        column: x => x.payerId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transactionid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    payerId = table.Column<int>(nullable: false),
                    payeeId = table.Column<int>(nullable: false),
                    paid_amount = table.Column<double>(nullable: false),
                    paid_on = table.Column<DateTime>(nullable: false),
                    groupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transactionid);
                    table.ForeignKey(
                        name: "FK_transactions_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "groupid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_transactions_users_payeeId",
                        column: x => x.payeeId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_transactions_users_payerId",
                        column: x => x.payerId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "payers",
                columns: table => new
                {
                    payerid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    paid_byId = table.Column<int>(nullable: false),
                    amount_paid = table.Column<double>(nullable: false),
                    billId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payers", x => x.payerid);
                    table.ForeignKey(
                        name: "FK_payers_bills_billId",
                        column: x => x.billId,
                        principalTable: "bills",
                        principalColumn: "billid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payers_users_paid_byId",
                        column: x => x.paid_byId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sharedWiths",
                columns: table => new
                {
                    sharedid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    shared_withId = table.Column<int>(nullable: false),
                    owes_amount = table.Column<double>(nullable: false),
                    billId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sharedWiths", x => x.sharedid);
                    table.ForeignKey(
                        name: "FK_sharedWiths_bills_billId",
                        column: x => x.billId,
                        principalTable: "bills",
                        principalColumn: "billid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sharedWiths_users_shared_withId",
                        column: x => x.shared_withId,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bills_bill_created_byId",
                table: "bills",
                column: "bill_created_byId");

            migrationBuilder.CreateIndex(
                name: "IX_bills_bill_updated_byId",
                table: "bills",
                column: "bill_updated_byId");

            migrationBuilder.CreateIndex(
                name: "IX_bills_groupId",
                table: "bills",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_friendId",
                table: "friends",
                column: "friendId");

            migrationBuilder.CreateIndex(
                name: "IX_friends_userId",
                table: "friends",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_groupMembers_groupId",
                table: "groupMembers",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_groupMembers_userId",
                table: "groupMembers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_groups_group_created_by",
                table: "groups",
                column: "group_created_by");

            migrationBuilder.CreateIndex(
                name: "IX_payers_billId",
                table: "payers",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "IX_payers_paid_byId",
                table: "payers",
                column: "paid_byId");

            migrationBuilder.CreateIndex(
                name: "IX_settlements_groupId",
                table: "settlements",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_settlements_payeeId",
                table: "settlements",
                column: "payeeId");

            migrationBuilder.CreateIndex(
                name: "IX_settlements_payerId",
                table: "settlements",
                column: "payerId");

            migrationBuilder.CreateIndex(
                name: "IX_sharedWiths_billId",
                table: "sharedWiths",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "IX_sharedWiths_shared_withId",
                table: "sharedWiths",
                column: "shared_withId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_groupId",
                table: "transactions",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_payeeId",
                table: "transactions",
                column: "payeeId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_payerId",
                table: "transactions",
                column: "payerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropTable(
                name: "groupMembers");

            migrationBuilder.DropTable(
                name: "payers");

            migrationBuilder.DropTable(
                name: "settlements");

            migrationBuilder.DropTable(
                name: "sharedWiths");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
