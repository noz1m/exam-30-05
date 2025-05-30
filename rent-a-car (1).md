# 🏎️ Задание по созданию API для Rent-a-Car

## 🎯 Цель
Создать API для управления данными сервиса аренды автомобилей (пользователи, автомобили, бронирования) с использованием C# и Entity Framework Core. Настроить базу данных PostgreSQL, задать ограничения на модели, реализовать CRUD-операции, использовать DTO, подключить Swagger и добавить 10 дополнительных запросов с JOIN-ами.

---

## 🧱 Структура проекта
1. **Domain** — модели данных.  
2. **Infrastructure** — контекст EF Core, конфигурации, миграции.  
3. **WebAPI** — контроллеры, настройка Swagger.

---

## 1. Модели данных

### 👤 User (Пользователь)
| Поле          | Тип            | Ограничения и свойства                             |
|---------------|----------------|----------------------------------------------------|
| `Id`          | `int`          | Первичный ключ                                     |
| `Username`    | `string`       | **Обязательное**, уникальное, макс. длина 50      |
| `Email`       | `string`       | **Обязательное**, макс. длина 100                 |
| `Phone`       | `string`       | Макс. длина 20                                    |

---

### 🚗 Car (Автомобиль)
| Поле          | Тип            | Ограничения и свойства                             |
|---------------|----------------|----------------------------------------------------|
| `Id`          | `int`          | Первичный ключ                                     |
| `Model`       | `string`       | **Обязательное**, макс. длина 100                 |
| `PricePerDay` | `decimal`      | **Обязательное**, больше 0                        |
| `IsAvailable` | `bool`         | По умолчанию `true`                               |

---

### 📅 Booking (Бронирование)
| Поле          | Тип            | Ограничения и свойства                             |
|---------------|----------------|----------------------------------------------------|
| `Id`          | `int`          | Первичный ключ                                     |
| `UserId`      | `int`          | Внешний ключ (ссылка на `User`)                   |
| `CarId`       | `int`          | Внешний ключ (ссылка на `Car`)                    |
| `StartDate`   | `DateTime`     | **Обязательное**                                  |
| `EndDate`     | `DateTime`     | **Обязательное**, больше `StartDate`              |
| `TotalPrice`  | `decimal`      | Вычисляется как `(EndDate - StartDate) * PricePerDay` |

---

## 2. CRUD-операции 🛠️
Создать контроллеры:  
- `UsersController`  
- `CarsController`  
- `BookingsController`  

Реализовать методы:  
- `GET /users`, `GET /users/{id}` — список пользователей и получение по ID.  
- `POST /users` — создание пользователя.  
- `PUT /users/{id}` — обновление пользователя.  
- `DELETE /users/{id}` — удаление пользователя.  
- Аналогично для `Cars` и `Bookings`.

---

## 3. DTO
Определить DTO для каждой модели:  

### `UserDto`
- `Id` (int)  
- `Username` (string)  
- `Email` (string)  
- `BookingCount` (int) — количество бронирований  

### `CarDto`
- `Id` (int)  
- `Model` (string)  
- `PricePerDay` (decimal)  
- `IsAvailable` (bool)  
- `BookingCount` (int) — количество бронирований  

### `BookingDto`
- `Id` (int)  
- `UserId` (int)  
- `Username` (string) — имя пользователя  
- `CarId` (int)  
- `CarModel` (string) — модель машины  
- `StartDate` (DateTime)  
- `EndDate` (DateTime)  
- `TotalPrice` (decimal)  

Использовать эти DTO в контроллерах для запросов и ответов.

---

## 4. Дополнительные задания (5 запросов) 🎯

1. **GET /cars/available-now**  
   - Описание: Возвращает список доступных машин (без активных бронирований на текущий момент).  
   - DTO: `AvailableCarDto`  
     - `Model` (string)  
     - `PricePerDay` (decimal)  

2. **GET /cars/most-popular**  
   - Описание: Показывает 3 машины с наибольшим числом бронирований.  
   - DTO: `PopularCarDto`  
     - `Model` (string)  
     - `BookingCount` (int)  

3. **GET /users/frequent-renters**  
   - Описание: Возвращает пользователей с более чем 3 бронированиями.  
   - DTO: `FrequentRenterDto`  
     - `Username` (string)  
     - `BookingCount` (int)  

4. **GET /bookings/active**  
   - Описание: Возвращает текущие активные бронирования (где текущая дата между `StartDate` и `EndDate`).  
   - DTO: `ActiveBookingDto`  
     - `CarModel` (string)  
     - `Username` (string)  
     - `EndDate` (DateTime)  

5. **GET /cars/{id}/booking-details**  
   - Описание: Показывает все бронирования конкретной машины по id с данными пользователей.  
   - DTO: `CarBookingDetailsDto`  
     - `Username` (string)  
     - `StartDate` (DateTime)  
     - `EndDate` (DateTime)  
     - `TotalPrice` (decimal)  

---

## Общие требования
- Разделить проект на слои: `Domain`, `Infrastructure`, `WebAPI`.  
- Настроить миграции для базы данных.  
- Проверить ограничения и связи.  
- Тестировать API через Swagger.
