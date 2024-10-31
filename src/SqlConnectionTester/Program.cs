using Spectre.Console.Cli;
using SqlConnectionTester.Commands;

// # ヘルプの表示
// dotnet run -- --help
// # パラメータを指定する場合
// dotnet run --server "localhost\SQLEXPRESS" --database "MyDB" --user "sa" --password "sapassword" --windows-auth false
// dotnet run -S "localhost\SQLEXPRESS" -d "MyDB" -U "sa" -P "sapassword" -E flase

var app = new CommandApp<SqlConnectionCommand>();
return app.Run(args);
