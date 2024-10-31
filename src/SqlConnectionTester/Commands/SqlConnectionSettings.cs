// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlConnectionSettings.cs" company="MareMare">
// Copyright © 2024 MareMare.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Spectre.Console.Cli;
using System.ComponentModel;

namespace SqlConnectionTester.Commands;

/// <summary>
/// SQL Server 接続設定を表します。
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class SqlConnectionSettings : CommandSettings
{
    /// <summary>
    /// アプリケーション名を取得または設定します。
    /// </summary>
    /// <value>
    /// アプリケーション名を表す <see cref="string" /> 型。
    /// <para>SQL Serverのアクティビティモニターでの識別に使用されます。既定値は "Dummy" です。</para>
    /// </value>
    [CommandOption("-K|--app-name")]
    [Description("アプリケーション名を指定します。SQL Serverのアクティビティモニターでの識別に使用されます。")]
    [DefaultValue("Dummy")]
    public string ApplicationName { get; set; } = default!;

    /// <summary>
    /// SQL Server のホスト名または IP アドレスを取得または設定します。
    /// </summary>
    /// <value>
    /// ホスト名または IP アドレスを表す <see cref="string" /> 型。
    /// <para>インスタンス名を含める場合は「サーバー名\\インスタンス名」の形式で指定します。既定値は "localhost\\MSSQLSERVER" です。</para>
    /// </value>
    [CommandOption("-S|--server")]
    [Description("SQL Serverのホスト名またはIPアドレスを指定します。インスタンス名を含める場合は「サーバー名\\インスタンス名」の形式で指定します。")]
    [DefaultValue("localhost\\MSSQLSERVER")]
    public string DataSource { get; set; } = default!;

    /// <summary>
    /// 接続するデータベース名を取得または設定します。
    /// </summary>
    /// <value>
    /// データベース名を表す <see cref="string" /> 型。
    /// <para>既定値は "DatabaseName" です。</para>
    /// </value>
    [CommandOption("-d|--database")]
    [Description("接続するデータベース名を指定します。")]
    [DefaultValue("DatabaseName")]
    public string InitialCatalog { get; set; } = default!;

    /// <summary>
    /// Windows 認証を使用するかどうかを取得または設定します。
    /// </summary>
    /// <value>
    /// Windows 認証を使用するかどうかを表す <see cref="bool" /> 型。
    /// <para>true の場合は Windows 認証を使用します。既定値は false です。</para>
    /// </value>
    [CommandOption("-E|--windows-auth")]
    [Description("Windows認証を使用する場合はtrueを指定します。falseの場合はSQL Server認証を使用します。")]
    [DefaultValue(false)]
    public bool UseWindowsAuth { get; set; }

    /// <summary>
    /// SQL Server 認証で使用するユーザー ID を取得または設定します。
    /// </summary>
    /// <value>
    /// ユーザー ID を表す <see cref="string" /> 型。
    /// <para>Windows 認証の場合は無視されます。既定値は "ユーザ" です。</para>
    /// </value>
    [CommandOption("-U|--user")]
    [Description("SQL Server認証で使用するユーザーIDを指定します。Windows認証の場合は無視されます。")]
    [DefaultValue("ユーザ")]
    public string UserID { get; set; } = default!;

    /// <summary>
    /// SQL Server 認証で使用するパスワードを取得または設定します。
    /// </summary>
    /// <value>
    /// パスワードを表す <see cref="string" /> 型。
    /// <para>Windows 認証の場合は無視されます。既定値は "パスワード" です。</para>
    /// </value>
    [CommandOption("-P|--password")]
    [Description("SQL Server認証で使用するパスワードを指定します。Windows認証の場合は無視されます。")]
    [DefaultValue("パスワード")]
    public string Password { get; set; } = default!;

    /// <summary>
    /// データベース接続のタイムアウト時間を取得または設定します。
    /// </summary>
    /// <value>
    /// タイムアウト時間を秒単位で表す <see cref="int" /> 型。
    /// <para>既定値は 90 秒です。</para>
    /// </value>
    [CommandOption("-l|--timeout")]
    [Description("データベース接続のタイムアウト時間を秒単位で指定します。")]
    [DefaultValue(90)]
    public int ConnectTimeout { get; set; }

    /// <summary>
    /// 接続文字列内にセキュリティ情報を保持するかどうかを取得または設定します。
    /// </summary>
    /// <value>
    /// セキュリティ情報を保持するかどうかを表す <see cref="bool" /> 型。
    /// <para>既定値は true です。</para>
    /// </value>
    [CommandOption("-p|--persist-security")]
    [Description("接続文字列内にセキュリティ情報（パスワードなど）を保持するかどうかを指定します。")]
    [DefaultValue(true)]
    public bool PersistSecurityInfo { get; set; }

    /// <summary>
    /// 接続プーリングを使用するかどうかを取得または設定します。
    /// </summary>
    /// <value>
    /// 接続プーリングを使用するかどうかを表す <see cref="bool" /> 型。
    /// <para>既定値は true です。</para>
    /// </value>
    [CommandOption("-A|--pooling")]
    [Description("接続プーリングを使用するかどうかを指定します。パフォーマンス向上のため、通常はtrueのままにします。")]
    [DefaultValue(true)]
    public bool Pooling { get; set; }

    /// <summary>
    /// データベース接続の暗号化を有効にするかどうかを取得または設定します。
    /// </summary>
    /// <value>
    /// 暗号化を有効にするかどうかを表す <see cref="bool" /> 型。
    /// <para>既定値は false です。</para>
    /// </value>
    [CommandOption("-N|--encrypt")]
    [Description("データベース接続の暗号化を有効にするかどうかを指定します。本番環境ではセキュリティのためtrueを推奨します。")]
    [DefaultValue(false)]
    public bool Encrypt { get; set; }

    /// <summary>
    /// 接続テスト時のタイムアウト時間を取得または設定します。
    /// </summary>
    /// <value>
    /// タイムアウト時間を秒単位で表す <see cref="int" /> 型。
    /// <para>通常の接続タイムアウトより短い値を設定します。既定値は 5 秒です。</para>
    /// </value>
    [CommandOption("-o|--open-timeout")]
    [Description("接続テスト時のタイムアウト時間を秒単位で指定します。通常の接続タイムアウトより短い値を設定します。")]
    [DefaultValue(5)]
    public int TimeoutToOpen { get; set; }
}
