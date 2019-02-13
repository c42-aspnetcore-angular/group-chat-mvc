﻿// <auto-generated />
using GroupChat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GroupChat.Migrations
{
    [DbContext(typeof(GroupChatContext))]
    [Migration("20190213131732_GroupChatDb")]
    partial class GroupChatDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("GroupChat.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupName");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GroupChat.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddedBy");

                    b.Property<int>("GroupId");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("GroupChat.Models.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UserGroup");
                });
#pragma warning restore 612, 618
        }
    }
}