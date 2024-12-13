# Backend для приложения управления грузовыми перевозками

Это серверная часть приложения для управления грузовыми перевозками, реализованная с использованием C# и архитектуры MVC. Бэкенд обрабатывает все запросы, взаимодействует с базой данных и обеспечивает выполнение бизнес-логики приложения.

## Основные особенности

- **MVC архитектура**: Приложение реализует классическую архитектуру Model-View-Controller (MVC), что позволяет организовать код для эффективного взаимодействия между компонентами приложения.
- **SQLite база данных**: Приложение использует SQLite для хранения данных о пользователях, перевозках и других сущностях.
- **Роли пользователей**: Приложение поддерживает несколько ролей пользователей с различными правами доступа, например, администраторы могут управлять перевозками и пользователями, а обычные пользователи могут работать только с их перевозками.
- **REST API**: Сервер предоставляет RESTful API для взаимодействия с фронтендом.
- **Безопасность**: Используются механизмы аутентификации и авторизации для защиты данных и доступа к ресурсам.

## Технологии

- **Язык программирования**: C#
- **Фреймворк**: ASP.NET Core MVC
- **База данных**: SQLite