using Microsoft.EntityFrameworkCore.Migrations;

namespace Thesis.Infrastructure.Migrations
{
    public partial class Achievement_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Achievement");
            migrationBuilder.Sql(
                @"
                CREATE OR alter TRIGGER OnScoreInsertedTrigger
                ON Score
                AFTER INSERT 
                AS
                DECLARE @userId INT
                DECLARE @newAmount INT
                DECLARE @sum INT
                BEGIN
	                SELECT @userId = UserId, @newAmount = Amount FROM inserted;

	                SELECT @sum = SUM(Amount) FROM score
	                WHERE UserId = @userId;

	                IF @sum >= 30 AND @sum - @newAmount < 30
		                INSERT INTO Achievement(UserId, Type, Date, Description)
		                Values(@userId, 20, GETDATE(), 'Brązowy medal energii');

	                IF @sum >= 100 AND @sum - @newAmount < 100
		                INSERT INTO Achievement(UserId, Type, Date, Description)
		                Values(@userId, 21, GETDATE(), 'Srebrny medal energii');

	                IF @sum >= 300 AND @sum - @newAmount < 300
		                INSERT INTO Achievement(UserId, Type, Date, Description)
		                Values(@userId, 22, GETDATE(), 'Złoty medal energii');

	                IF @sum >= 1000 AND @sum - @newAmount < 1000
		                INSERT INTO Achievement(UserId, Type, Date, Description)
		                Values(@userId, 23, GETDATE(), 'Mistrz energii');
                END;"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Achievement",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.Sql("DROP TRIGGER OnScoreInsertedTrigger ON Score");
        }
    }
}
