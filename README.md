# SqlConnectionTester
⚠️for personal use. SQL Server 接続テストと接続文字列表示を行うツールです。

## Usage
```ps1
.\SqlConnectionTester.exe [OPTIONS]
```

## Options

| オプション           | デフォルト                  | 説明                                                                                         |
|----------------------|-----------------------------|----------------------------------------------------------------------------------------------|
| `-h`, `--help`       |                             | ヘルプ情報を表示します。                                                                    |
| `-K`, `--app-name`   | Dummy                       | アプリケーション名を指定します。SQL Serverのアクティビティモニターでの識別に使用されます。 |
| `-S`, `--server`     | localhost\MSSQLSERVER       | SQL Serverのホスト名またはIPアドレスを指定します。インスタンス名を含める場合は「サーバー名\インスタンス名」の形式で指定します。 |
| `-d`, `--database`   | DatabaseName                | 接続するデータベース名を指定します。                                                        |
| `-E`, `--windows-auth` |                          | Windows認証を使用する場合はtrueを指定します。falseの場合はSQL Server認証を使用します。       |
| `-U`, `--user`       | ユーザ                       | SQL Server認証で使用するユーザーIDを指定します。Windows認証の場合は無視されます。           |
| `-P`, `--password`   | パスワード                   | SQL Server認証で使用するパスワードを指定します。Windows認証の場合は無視されます。           |
| `-l`, `--timeout`    | 90                          | データベース接続のタイムアウト時間を秒単位で指定します。                                    |
| `-p`, `--persist-security` | True                 | 接続文字列内にセキュリティ情報（パスワードなど）を保持するかどうかを指定します。            |
| `-A`, `--pooling`    | True                        | 接続プーリングを使用するかどうかを指定します。パフォーマンス向上のため、通常はtrueのままにします。 |
| `-N`, `--encrypt`    |                             | データベース接続の暗号化を有効にするかどうかを指定します。本番環境ではセキュリティのためtrueを推奨します。 |
| `-o`, `--open-timeout` | 5                        | 接続テスト時のタイムアウト時間を秒単位で指定します。通常の接続タイムアウトより短い値を設定します。 |

## Examples

### Windows 認証
```ps1
.\SqlConnectionTester.exe --server "localhost\SQLEXPRESS" --database "HogeDB" --windows-auth true
```

```ps1
.\SqlConnectionTester.exe -S "localhost\SQLEXPRESS" -d "HogeDB" -E true
```

### SQL Server 認証
```ps1
.\SqlConnectionTester.exe --server "localhost\SQLEXPRESS" --database "HogeDB" --user ユーザ --password パスワード
```

```ps1
.\SqlConnectionTester.exe -S "localhost\SQLEXPRESS" -d "HogeDB" -U ユーザ -P パスワード
```
