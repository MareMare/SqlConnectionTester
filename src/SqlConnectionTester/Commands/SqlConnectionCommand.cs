// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlConnectionCommand.cs" company="MareMare">
// Copyright © 2024 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SqlConnectionTester.Commands;

/// <summary>
/// 指定された設定を使用して SQL Server 接続をテストするコマンドを表します。
/// </summary>
public class SqlConnectionCommand : AsyncCommand<SqlConnectionSettings>
{
    /// <inheritdoc />
    public override async Task<int> ExecuteAsync(CommandContext context, SqlConnectionSettings settings)
    {
        var builder = new SqlConnectionStringBuilder
        {
            ApplicationName = settings.ApplicationName,
            DataSource = settings.DataSource,
            InitialCatalog = settings.InitialCatalog,
            ConnectTimeout = settings.ConnectTimeout,
            PersistSecurityInfo = settings.PersistSecurityInfo,
            Pooling = settings.Pooling,
            Encrypt = settings.Encrypt,
        };

        // 認証方式の設定
        if (settings.UseWindowsAuth)
        {
            builder.IntegratedSecurity = true;
        }
        else
        {
            builder.IntegratedSecurity = false;
            builder.UserID = settings.UserID;
            builder.Password = settings.Password;
        }
        var connectionString = builder.ConnectionString;

        // 接続設定の表示
        AnsiConsole.Write(new Rule("[yellow]接続設定[/]").LeftJustified());
        var table = new Table()
            .AddColumn("項目")
            .AddColumn("値");

        table.AddRow("サーバー", $"{settings.DataSource}");
        table.AddRow("データベース", $"{settings.InitialCatalog}");
        table.AddRow("認証方式", settings.UseWindowsAuth ? "Windows認証" : "SQL Server認証");
        if (!settings.UseWindowsAuth)
        {
            table.AddRow("ユーザーID", $"{settings.UserID}");
        }
        table.AddRow("接続タイムアウト", $"{settings.ConnectTimeout}");
        table.AddRow("接続確認タイムアウト", $"{settings.TimeoutToOpen}");
        table.AddRow("プーリング", $"{settings.Pooling}");
        table.AddRow("暗号化", $"{settings.Encrypt}");

        AnsiConsole.Write(table);

        try
        {
            await AnsiConsole.Status()
                .StartAsync(
                    "データベースに接続しています...",
                     async ctx =>
                     {
                         ctx.Spinner(Spinner.Known.Dots);
                         ctx.SpinnerStyle(Style.Parse("green"));

                         // 接続確認するときだけ Timeout を短くします。
                         builder.ConnectTimeout = settings.TimeoutToOpen;
                         var connection = new SqlConnection(builder.ConnectionString);
                         await using (connection.ConfigureAwait(false))
                         {
                             await connection.OpenAsync().ConfigureAwait(false);
                         }
                     });

            AnsiConsole.MarkupLine("[green]接続に成功しました！[/]");

            // Windows認証の場合は現在のユーザー情報を表示
            if (settings.UseWindowsAuth)
            {
                var connection = new SqlConnection(builder.ConnectionString);
                await using (connection.ConfigureAwait(false))
                {
                    await connection.OpenAsync();
                    var currentUser = await GetCurrentUserAsync(connection);
                    AnsiConsole.MarkupLine($"[blue]現在の接続ユーザー:[/] {currentUser}");
                }
            }

            // 接続文字列の表示
            AnsiConsole.Write(new Rule("[yellow]接続文字列[/]").LeftJustified());
            var panel = new Panel(connectionString)
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1)
            };
            AnsiConsole.Write(panel);

            return 0;
        }
        catch (SqlException ex)
        {
            AnsiConsole.MarkupLine("[red]接続できません。[/]");
            var errorPanel = new Panel(ex.Message)
            {
                Border = BoxBorder.Heavy,
                BorderStyle = new Style(foreground: Color.Red),
                Padding = new Padding(1)
            };
            AnsiConsole.Write(errorPanel);

            // スタックトレースの表示（オプション）
            if (AnsiConsole.Confirm("詳細なエラー情報を表示しますか？"))
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths | ExceptionFormats.ShowLinks);
            }

            return -1;
        }

        static async Task<string> GetCurrentUserAsync(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            await using (command.ConfigureAwait(false))
            {
                command.CommandText = "SELECT SYSTEM_USER";
                var systemUser = await command.ExecuteScalarAsync().ConfigureAwait(false);
                return systemUser?.ToString() ?? "Unknown";
            }
        }
    }
}
