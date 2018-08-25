﻿// <auto-generated />
using System;
using FinalSplitWise.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalSplitWise.Migrations
{
    [DbContext(typeof(SystemDBContext))]
    partial class SystemDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalSplitWise.Models.Bill", b =>
                {
                    b.Property<int>("billid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("bill_created_at");

                    b.Property<int>("bill_created_byId");

                    b.Property<DateTime>("bill_date");

                    b.Property<DateTime>("bill_updated_at");

                    b.Property<int>("bill_updated_byId");

                    b.Property<string>("description");

                    b.Property<int?>("groupId");

                    b.Property<double>("total_amount");

                    b.HasKey("billid");

                    b.HasIndex("bill_created_byId");

                    b.HasIndex("bill_updated_byId");

                    b.HasIndex("groupId");

                    b.ToTable("bills");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Friend", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("friendId");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("friendId");

                    b.HasIndex("userId");

                    b.ToTable("friends");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Group", b =>
                {
                    b.Property<int>("groupid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("group_created_by");

                    b.Property<string>("group_name");

                    b.Property<bool>("is_simplified_depts");

                    b.HasKey("groupid");

                    b.HasIndex("group_created_by");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("FinalSplitWise.Models.GroupMember", b =>
                {
                    b.Property<int>("memberid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("groupId");

                    b.Property<int>("userId");

                    b.HasKey("memberid");

                    b.HasIndex("groupId");

                    b.HasIndex("userId");

                    b.ToTable("groupMembers");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Payer", b =>
                {
                    b.Property<int>("payerid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("amount_paid");

                    b.Property<int>("billId");

                    b.Property<int>("paid_byId");

                    b.HasKey("payerid");

                    b.HasIndex("billId");

                    b.HasIndex("paid_byId");

                    b.ToTable("payers");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Settlements", b =>
                {
                    b.Property<int>("settlementid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("amount");

                    b.Property<int?>("groupId");

                    b.Property<int>("payeeId");

                    b.Property<int>("payerId");

                    b.HasKey("settlementid");

                    b.HasIndex("groupId");

                    b.HasIndex("payeeId");

                    b.HasIndex("payerId");

                    b.ToTable("settlements");
                });

            modelBuilder.Entity("FinalSplitWise.Models.SharedWith", b =>
                {
                    b.Property<int>("sharedid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("billId");

                    b.Property<double>("owes_amount");

                    b.Property<int>("shared_withId");

                    b.HasKey("sharedid");

                    b.HasIndex("billId");

                    b.HasIndex("shared_withId");

                    b.ToTable("sharedWiths");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Transaction", b =>
                {
                    b.Property<int>("transactionid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("groupId");

                    b.Property<double>("paid_amount");

                    b.Property<DateTime>("paid_on");

                    b.Property<int>("payeeId");

                    b.Property<int>("payerId");

                    b.HasKey("transactionid");

                    b.HasIndex("groupId");

                    b.HasIndex("payeeId");

                    b.HasIndex("payerId");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("FinalSplitWise.Models.User", b =>
                {
                    b.Property<int>("userid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email_id");

                    b.Property<string>("password");

                    b.Property<string>("phone_no");

                    b.Property<string>("user_name");

                    b.HasKey("userid");

                    b.ToTable("users");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Bill", b =>
                {
                    b.HasOne("FinalSplitWise.Models.User", "bill_created_by")
                        .WithMany("bill_created_by")
                        .HasForeignKey("bill_created_byId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "bill_updated_by")
                        .WithMany("bill_updated_by")
                        .HasForeignKey("bill_updated_byId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.Group", "group")
                        .WithMany("bill_group_id")
                        .HasForeignKey("groupId");
                });

            modelBuilder.Entity("FinalSplitWise.Models.Friend", b =>
                {
                    b.HasOne("FinalSplitWise.Models.User", "friend")
                        .WithMany("friend_id")
                        .HasForeignKey("friendId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "user")
                        .WithMany("user_id")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.Group", b =>
                {
                    b.HasOne("FinalSplitWise.Models.User", "user")
                        .WithMany("group_created_by")
                        .HasForeignKey("group_created_by")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.GroupMember", b =>
                {
                    b.HasOne("FinalSplitWise.Models.Group", "group")
                        .WithMany("gm_group_id")
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "user")
                        .WithMany("gm_user_id")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.Payer", b =>
                {
                    b.HasOne("FinalSplitWise.Models.Bill", "bill")
                        .WithMany("bill_payer")
                        .HasForeignKey("billId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "paid_by")
                        .WithMany("paid_by_id")
                        .HasForeignKey("paid_byId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.Settlements", b =>
                {
                    b.HasOne("FinalSplitWise.Models.Group", "group")
                        .WithMany("settlement_group_id")
                        .HasForeignKey("groupId");

                    b.HasOne("FinalSplitWise.Models.User", "payee")
                        .WithMany("payee")
                        .HasForeignKey("payeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "payer")
                        .WithMany("payer")
                        .HasForeignKey("payerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.SharedWith", b =>
                {
                    b.HasOne("FinalSplitWise.Models.Bill", "bill")
                        .WithMany("bill_shared_with")
                        .HasForeignKey("billId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "shared_with")
                        .WithMany("shared_with")
                        .HasForeignKey("shared_withId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalSplitWise.Models.Transaction", b =>
                {
                    b.HasOne("FinalSplitWise.Models.Group", "group")
                        .WithMany("transaction_group_id")
                        .HasForeignKey("groupId");

                    b.HasOne("FinalSplitWise.Models.User", "payee")
                        .WithMany("transpayee")
                        .HasForeignKey("payeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalSplitWise.Models.User", "payer")
                        .WithMany("transpayer")
                        .HasForeignKey("payerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
