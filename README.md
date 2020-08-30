<h3>Серверная часть для торговой площадки<br></h3>Данное приложение предназначено для работы с <br><h2 dir="auto">
Как запустить</h2>
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

<p dir="auto"><em>Приложение опубликовано по адресу (Swagger): <br></em></p>
<h3 dir="auto">
IDE</h3>
<p dir="auto">Приложение разрабатывалось с использованием Visual Studio 2019 Enterprise и Rider.<br></p>


<h3 dir="auto">
База данных</h3>
<p dir="auto">Используется СУБД 
MSSQL, для работы с базой данных используется ADO NET:</p><p dir="auto">КАК ИСПОЛЬЗОВАТЬ ПРИЛОЖЕНИЕ</p><p dir="auto">1)<br></p>
Todo дописать
