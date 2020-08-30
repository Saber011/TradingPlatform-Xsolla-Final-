<h3>Серверная часть для торговой площадки<br></h3>Данное приложение предназначено для работы с пользователями и продуктами торговой площадки<br><h2 dir="auto">
Как запустить</h2>

<p class="MsoNormal"><span style="font-size:14.0pt;line-height:107%;font-family:
&quot;Times New Roman&quot;,serif">Перед запуском необходимо установить </span><span style="font-size:14.0pt;line-height:107%;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-US" lang="EN-US">redis</span><span style="font-size:14.0pt;line-height:
107%;font-family:&quot;Times New Roman&quot;,serif"> (</span><span style="font-size:14.0pt;line-height:107%;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-US" lang="EN-US">https</span><span style="font-size:14.0pt;line-height:
107%;font-family:&quot;Times New Roman&quot;,serif">://</span><span style="font-size:14.0pt;line-height:107%;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-US" lang="EN-US">redis</span><span style="font-size:14.0pt;line-height:
107%;font-family:&quot;Times New Roman&quot;,serif">.</span><span style="font-size:14.0pt;line-height:107%;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-US" lang="EN-US">io</span><span style="font-size:14.0pt;line-height:
107%;font-family:&quot;Times New Roman&quot;,serif">/</span><span style="font-size:14.0pt;line-height:107%;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-US" lang="EN-US">download</span><span style="font-size:14.0pt;
line-height:107%;font-family:&quot;Times New Roman&quot;,serif">) сервер локально,
запустить его. Развернуть бекап базы данных, находиться в фале bac.bacpac (<a href="https://docs.microsoft.com/ru-ru/sql/relational-databases/data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database?view=sql-server-ver15">https://docs.microsoft.com/ru-ru/sql/relational-databases/data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database?view=sql-server-ver15</a>)</span></p> После установить свое подключение в файле appsettings.json




<h3 dir="auto">
Убедитесь, что у Вас установлен версии .NET Core 3.1</h3>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">dotnet --version</span></code></pre>
<p dir="auto">В случае ошибки выполнения команды скачайте все файлы для Вашей системы.<br></p>
<h3 dir="auto">
Запустите проект через dotnet builder</h3>
<p dir="auto">Восстановите все зависимости решения:</p>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">dotnet restore</span></code></pre>
<p dir="auto">Перейдите в папку <em>TradingPlatform\TradingPlatform</em></p>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">cd </span></code><em>TradingPlatform\TradingPlatform</em></pre>
<p dir="auto">И запустите проект в dev режиме</p>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">dotnet watch run</span></code></pre>
<p dir="auto">Собрать приложение (необязательно) можно с помощью команды</p>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">dotnet build --configuration Release</span></code></pre>
<p dir="auto">Опубликовать</p>
<pre lang="plaintext"><code><span id="user-content-lc1" lang="plaintext">dotnet publish -c Release</span></code></pre>

<p dir="auto"><em>Приложение опубликовано по адресу (Swagger): <br></em><a>https://tradingplatformapi.azurewebsites.net/</a></p>
<h3 dir="auto">
IDE</h3>
<p dir="auto">Приложение разрабатывалось с использованием Visual Studio 2019 Enterprise и Rider.<br></p>

<h3 dir="auto">
База данных</h3>
<p dir="auto">Используется СУБД 
MSSQL, для работы с базой данных используется ADO NET:</p>


<h2>Как использовать приложение<br></h2>
<ol><li style="margin-left: 18pt;">Создать пользователя с помощью <span style="mso-ansi-language:EN-US" lang="EN-US">post</span><span lang="EN-US"> </span>запроса
на ендпоит <span style="mso-ansi-language:EN-US" lang="EN-US">User</span></li><li style="margin-left: 18pt;">Получить токен авторизации через
метод <span style="mso-ansi-language:EN-US" lang="EN-US">Login</span></li><li style="margin-left: 18pt;">Авторизоватся</li><li style="margin-left: 18pt;">После чего доступны действия с
продуктами</li><li style="margin-left: 18pt;">При создании продукта
пользователю присваеватся роль <span style="mso-ansi-language:EN-US" lang="EN-US">Vendor</span>,
изначальная роль <span style="mso-ansi-language:EN-US" lang="EN-US">Buyer</span></li><li style="margin-left: 18pt;">Часть операций доступна с учетной
записью Админа</li><li style="margin-left: 18pt;">Метод <span style="mso-ansi-language:EN-US" lang="EN-US">Buy</span><span lang="EN-US"> </span>предназначен
для симуляции пополнения</li></ol>



<p class="MsoListParagraphCxSpMiddle">Учетные данные админа <span style="mso-ansi-language:EN-US" lang="EN-US"><a href="mailto:user@example89.com">user<span style="mso-ansi-language:RU" lang="RU">@</span>example<span style="mso-ansi-language:RU" lang="RU">89.</span>com</a></span><span lang="EN-US"> </span><span style="mso-ansi-language:EN-US" lang="EN-US">string</span></p>

<p class="MsoListParagraphCxSpLast">&nbsp;</p>
