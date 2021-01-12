# Учебный проект "Помощник по обучению"
## Общее описание проекта
Приложение предназначено для пользователей, которые хотят освоить какую-либо область знаний. Приложение подойдет как для индивидуального использования, так и для группового. 
В обоих случаях пользователь может добавлять / удалять предметы, создавать занятия, оценивать свои результаты, следить за своими успехами с помощью статистики. 
## Использованные технологии
* Backend: C#, ASP .NET Core 3.1, Entity Framework Core, Autofac, MS SQL, Asp Net Core Authentication JWT
* Frontend: JS, HTML, CSS, Vue.js, Vuex, Axios, BootstrapVue
## Настройки и запуск проекта
1. Склонируйте репозиторий с помощью ``git clone https://github.com/VladislavTrifonov/EducationalHelp.git``
2. Откройте файл проекта ``EducationalHelp.Web/appsettings.json`` и настройте строку подключения к базе данных (``{"ConnectionStrings": { "default": <строка_подключения> }}``
3. Соберите проект C#: перейдите в корневую папку с проектом и выполните команду ``dotnet build``
4. Установите зависимости npm: перейдите в папку ``EducationalHelp/EducationalHelp.Client`` и выполните ``npm install``. 
5. Запустите проект ASP .NET: перейдите в папку ``EducationalHelp/EducationalHelp.Web/bin/Debug/netcoreapp3.1`` (для запуска в режиме отладки или в папку ``EducationalHelp/EducationalHelp.Web/bin/Release/netcoreapp3.1``
для запуска в режиме "production") и запустите файл EducationalHelp.Web.Exe (в ОС Windows). 
6. Запустите проект Vue: перейдите в папку ``EducationalHelp/EducationalHelp.Client`` и введите команду ``npm run serve``. 
## Скриншоты проекта
Скриншоты находятся в папке 
[screens](//github.com/VladislavTrifonov/EducationalHelp/raw/master/screens)
