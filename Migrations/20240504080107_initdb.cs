using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using razorweb.models;

#nullable disable

namespace Razorweb.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "posts",
                columns: new [] { "Tittle", "Created", "Content" },
                values: new object[] {
                    "Bai viet 1",
                    new DateTime(2024, 1, 1),
                    "Noi dung 1"
                }
            );

            Randomizer.Seed = new Random();
            var fakerArticles = new Faker<Article>();
            fakerArticles.RuleFor(a => a.Tittle, f => f.Lorem.Sentence(5, 5));
            fakerArticles.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2024, 1, 1), new DateTime(2024, 5, 5)));
            fakerArticles.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));

            Article article = fakerArticles.Generate();
            for (int i=0; i<150; i++) {
                migrationBuilder.InsertData(
                    table: "posts",
                    columns: new [] { "Tittle", "Created", "Content" },
                    values: new object[] {
                        article.Tittle,
                        article.Created,
                        article.Content
                    }
                );
            }

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");
        }
    }
}
