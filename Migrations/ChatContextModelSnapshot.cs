﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using chaos.Models;

#nullable disable

namespace chaos.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("chaos.Models.Channel", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Banner")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("CreatorID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CreatorID");

                    b.ToTable("CHANNEL");
                });

            modelBuilder.Entity("chaos.Models.Message", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("ChannelID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("SenderID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TextContent")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ChannelID");

                    b.HasIndex("SenderID");

                    b.ToTable("MESSAGE");
                });

            modelBuilder.Entity("chaos.Models.Participant", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("ChannelID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ChannelID");

                    b.HasIndex("UserID");

                    b.ToTable("PARTICIPANT");
                });

            modelBuilder.Entity("chaos.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("USER");
                });

            modelBuilder.Entity("chaos.Models.Channel", b =>
                {
                    b.HasOne("chaos.Models.User", "User")
                        .WithMany("CreatedChannels")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("chaos.Models.Message", b =>
                {
                    b.HasOne("chaos.Models.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chaos.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("chaos.Models.Participant", b =>
                {
                    b.HasOne("chaos.Models.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chaos.Models.User", "User")
                        .WithMany("JoinedChannels")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("chaos.Models.User", b =>
                {
                    b.Navigation("CreatedChannels");

                    b.Navigation("JoinedChannels");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
