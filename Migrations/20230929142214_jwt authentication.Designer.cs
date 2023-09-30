﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using chaos.Models;

#nullable disable

namespace chaos.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20230929142214_jwt authentication")]
    partial class jwtauthentication
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("chaos.Models.Apps", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("CLIENT_SECRET")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OrgID")
                        .HasColumnType("text");

                    b.Property<string>("TEST_CLIENT_SECRET")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("OrgID");

                    b.ToTable("APPS");
                });

            modelBuilder.Entity("chaos.Models.Channel", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("AppID")
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

                    b.Property<string>("OrgID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("OrgID");

                    b.ToTable("CHANNEL");
                });

            modelBuilder.Entity("chaos.Models.MediaUploads", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("FileUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("MEDIA_UPLOADS");
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

            modelBuilder.Entity("chaos.Models.MessageMedia", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("MediaID")
                        .HasColumnType("text");

                    b.Property<string>("MessageID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("MediaID");

                    b.HasIndex("MessageID");

                    b.ToTable("MESSAGE_MEDIA");
                });

            modelBuilder.Entity("chaos.Models.Organization", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Banner")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("ORGANIZATION");
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

                    b.Property<string>("AppID")
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("OrgID")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.HasIndex("OrgID");

                    b.ToTable("USER");
                });

            modelBuilder.Entity("chaos.Models.Apps", b =>
                {
                    b.HasOne("chaos.Models.Organization", "Org")
                        .WithMany()
                        .HasForeignKey("OrgID");

                    b.Navigation("Org");
                });

            modelBuilder.Entity("chaos.Models.Channel", b =>
                {
                    b.HasOne("chaos.Models.Apps", "App")
                        .WithMany()
                        .HasForeignKey("AppID");

                    b.HasOne("chaos.Models.User", "User")
                        .WithMany("CreatedChannels")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chaos.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrgID");

                    b.Navigation("App");

                    b.Navigation("Organization");

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

            modelBuilder.Entity("chaos.Models.MessageMedia", b =>
                {
                    b.HasOne("chaos.Models.MediaUploads", "MediaUpload")
                        .WithMany()
                        .HasForeignKey("MediaID");

                    b.HasOne("chaos.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageID");

                    b.Navigation("MediaUpload");

                    b.Navigation("Message");
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
                    b.HasOne("chaos.Models.Apps", "App")
                        .WithMany()
                        .HasForeignKey("AppID");

                    b.HasOne("chaos.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrgID");

                    b.Navigation("App");

                    b.Navigation("Organization");
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
