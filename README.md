

https://github.com/user-attachments/assets/537cc95e-dc7a-4917-8630-785f10b9699b



![Swagger](https://github.com/user-attachments/assets/8a515dc9-2689-461b-996c-4425e957469e)
![Строка подключения к БД](https://github.com/user-attachments/assets/ae61b8e5-9981-464e-8050-61a577d58fab)
![applicationUrl](https://github.com/user-attachments/assets/004ed102-0f3a-485c-a242-2bb2302ecbd6)
![использование ApiBaseUrl](https://github.com/user-attachments/assets/f2b664c8-5fd0-4152-9036-514e247b2c59)


БД проекта (PostgreSQL)
------------------------------------------------
```sql
[Uploading Scri-- Создаем схему pipe_Information, если она еще не существует
CREATE SCHEMA IF NOT EXISTS "pipe_Information";


-- Удаляем таблицы в обратном порядке зависимостей, если они существуют
DROP TABLE IF EXISTS "pipe_Information".pipes;
DROP TABLE IF EXISTS "pipe_Information".packages;
DROP TABLE IF EXISTS "pipe_Information".steel_grades;

-- 1. Таблица марок стали (Справочник)
CREATE TABLE "pipe_Information".steel_grades (
    grade_id SERIAL PRIMARY KEY, -- Уникальный ID марки стали (автоинкремент)
    grade_name VARCHAR(100) UNIQUE NOT NULL -- Название марки стали (уникальное, не пустое)
);

-- Комментарий к таблице и столбцам
COMMENT ON TABLE "pipe_Information".steel_grades IS 'Справочник марок стали';
COMMENT ON COLUMN "pipe_Information".steel_grades.grade_id IS 'Первичный ключ таблицы марок стали';
COMMENT ON COLUMN "pipe_Information".steel_grades.grade_name IS 'Уникальное наименование марки стали';

-- 2. Таблица пакетов
CREATE TABLE "pipe_Information".packages (
    package_id SERIAL PRIMARY KEY, -- Уникальный ID пакета (автоинкремент)
    package_number VARCHAR(50) UNIQUE NOT NULL, -- Номер пакета (уникальный, не пустой)
    package_date DATE NOT NULL DEFAULT CURRENT_DATE -- Дата формирования пакета (не пустая, по умолчанию текущая дата)
);

-- Комментарий к таблице и столбцам
COMMENT ON TABLE "pipe_Information".packages IS 'Пакеты, в которые объединяются трубы';
COMMENT ON COLUMN "pipe_Information".packages.package_id IS 'Первичный ключ таблицы пакетов';
COMMENT ON COLUMN "pipe_Information".packages.package_number IS 'Уникальный номер пакета';
COMMENT ON COLUMN "pipe_Information".packages.package_date IS 'Дата формирования пакета';

-- 3. Таблица труб
CREATE TABLE "pipe_Information".pipes (
    pipe_id SERIAL PRIMARY KEY, -- Уникальный ID трубы (автоинкремент)
    pipe_number VARCHAR(50) UNIQUE NOT NULL, -- Номер трубы (уникальный, не пустой)
    is_good_quality BOOLEAN NOT NULL, -- Качество трубы (true - годная, false - брак)
    steel_grade_id INT NOT NULL, -- ID марки стали (внешний ключ)
    length_meters NUMERIC(10, 2) NOT NULL, -- Длина в метрах (точность 2 знака после запятой)
    diameter_mm NUMERIC(10, 2) NOT NULL, -- Диаметр в мм (точность 2 знака после запятой)
    weight_kg NUMERIC(10, 2) NOT NULL, -- Вес в кг (точность 2 знака после запятой)
    package_id INT NULL, -- ID пакета (внешний ключ), может быть NULL

    -- Внешний ключ к таблице марок стали
    CONSTRAINT fk_steel_grade
        FOREIGN KEY(steel_grade_id)
        REFERENCES "pipe_Information".steel_grades(grade_id)
        ON DELETE RESTRICT, -- Запрещаем удалять марку стали, если есть трубы с этой маркой

    -- Внешний ключ к таблице пакетов
    CONSTRAINT fk_package
        FOREIGN KEY(package_id)
        REFERENCES "pipe_Information".packages(package_id)
        ON DELETE SET NULL, -- Если пакет удаляется, у труб в нем package_id станет NULL

    -- Ограничения на ввод данных (контроли)
    CONSTRAINT chk_length_positive CHECK (length_meters > 0), -- Длина должна быть положительной
    CONSTRAINT chk_diameter_positive CHECK (diameter_mm > 0), -- Диаметр должен быть положительным
    CONSTRAINT chk_weight_positive CHECK (weight_kg > 0) -- Вес должен быть положительным
);

-- Комментарий к таблице и столбцам
COMMENT ON TABLE "pipe_Information".pipes IS 'Данные по трубам';
COMMENT ON COLUMN "pipe_Information".pipes.pipe_id IS 'Первичный ключ таблицы труб';
COMMENT ON COLUMN "pipe_Information".pipes.pipe_number IS 'Уникальный номер трубы';
COMMENT ON COLUMN "pipe_Information".pipes.is_good_quality IS 'Качество трубы (true - годная, false - брак)';
COMMENT ON COLUMN "pipe_Information".pipes.steel_grade_id IS 'Внешний ключ к таблице steel_grades';
COMMENT ON COLUMN "pipe_Information".pipes.length_meters IS 'Длина трубы в метрах';
COMMENT ON COLUMN "pipe_Information".pipes.diameter_mm IS 'Диаметр трубы в миллиметрах';
COMMENT ON COLUMN "pipe_Information".pipes.weight_kg IS 'Вес трубы в килограммах';
COMMENT ON COLUMN "pipe_Information".pipes.package_id IS 'Внешний ключ к таблице packages (может быть NULL)';

-- 4. Создание индексов для ускорения запросов
CREATE INDEX idx_pipes_steel_grade_id ON "pipe_Information".pipes(steel_grade_id);
CREATE INDEX idx_pipes_package_id ON "pipe_Information".pipes(package_id);
CREATE INDEX idx_pipes_pipe_number ON "pipe_Information".pipes(pipe_number); -- Индекс для поиска по номеру трубы
CREATE INDEX idx_packages_package_number ON "pipe_Information".packages(package_number); -- Индекс для поиска по номеру пакета

-- 5. Наполнение таблиц тестовыми данными

-- Добавляем марки стали
INSERT INTO "pipe_Information".steel_grades (grade_name) VALUES
('Ст3сп'),
('09Г2С'),
('17Г1С'),
('20'),
('30ХГСА');

-- Добавляем пакеты
INSERT INTO "pipe_Information".packages (package_number, package_date) VALUES
('ПАКЕТ-001', '2025-04-26'),
('ПАКЕТ-002', '2025-04-27');

-- Добавляем трубы
-- Трубы без пакета
INSERT INTO "pipe_Information".pipes (pipe_number, is_good_quality, steel_grade_id, length_meters, diameter_mm, weight_kg, package_id) VALUES
('T-001', TRUE, 1, 12.00, 219.1, 580.50, NULL), -- Ст3сп, годная
('T-002', FALSE, 2, 11.50, 159.0, 350.10, NULL), -- 09Г2С, брак
('T-003', TRUE, 4, 6.00, 89.0, 95.20, NULL);    -- 20, годная

-- Трубы в пакете ПАКЕТ-001
INSERT INTO "pipe_Information".pipes (pipe_number, is_good_quality, steel_grade_id, length_meters, diameter_mm, weight_kg, package_id) VALUES
('T-101', TRUE, 2, 12.00, 159.0, 365.30, 1), -- 09Г2С, годная
('T-102', TRUE, 2, 12.00, 159.0, 366.00, 1), -- 09Г2С, годная
('T-103', FALSE, 2, 11.90, 159.0, 361.15, 1); -- 09Г2С, брак

-- Трубы в пакете ПАКЕТ-002
INSERT INTO "pipe_Information".pipes (pipe_number, is_good_quality, steel_grade_id, length_meters, diameter_mm, weight_kg, package_id) VALUES
('T-201', TRUE, 3, 10.00, 325.0, 850.00, 2), -- 17Г1С, годная
('T-202', TRUE, 3, 10.00, 325.0, 851.50, 2); -- 17Г1С, годная

-- Еще одна труба без пакета
INSERT INTO "pipe_Information".pipes (pipe_number, is_good_quality, steel_grade_id, length_meters, diameter_mm, weight_kg, package_id) VALUES
('T-004', TRUE, 5, 8.50, 114.3, 205.75, NULL); -- 30ХГСА, годная


INSERT INTO "pipe_Information".pipes (pipe_number, is_good_quality, steel_grade_id, length_meters, diameter_mm, weight_kg, package_id) VALUES
('T-005', TRUE, 1, 7.50, 89.0, 120.00, NULL),    -- Ст3сп, годная, без пакета
('T-006', FALSE, 2, 8.00, 159.0, 240.25, NULL),  -- 09Г2С, брак, без пакета
('T-007', TRUE, 3, 9.00, 325.0, 600.00, NULL),   -- 17Г1С, годная, без пакета
('T-008', TRUE, 4, 6.50, 89.0, 102.75, NULL),    -- 20, годная, без пакета
('T-009', FALSE, 5, 10.00, 114.3, 260.50, NULL), -- 30ХГСА, брак, без пакета
('T-104', TRUE, 2, 11.80, 159.0, 359.00, 1),     -- в пакете ПАКЕТ-001
('T-105', TRUE, 2, 12.10, 159.0, 368.45, 1),
('T-106', FALSE, 2, 11.95, 159.0, 360.20, 1),
('T-203', TRUE, 3, 10.50, 325.0, 875.00, 2),     -- в пакете ПАКЕТ-002
('T-204', FALSE, 3, 9.75, 325.0, 830.25, 2),
('T-205', TRUE, 3, 10.25, 325.0, 842.10, 2),
('T-301', TRUE, 1, 8.20, 219.1, 400.00, NULL),  -- новые без пакета
('T-302', TRUE, 4, 5.50, 89.0, 85.30, NULL),
('T-303', FALSE, 5, 8.00, 114.3, 210.00, NULL),
('T-304', TRUE, 1, 9.00, 219.1, 450.75, NULL);


SELECT * FROM "pipe_Information".steel_grades;

SELECT * FROM "pipe_Information".packages;

SELECT
    p.pipe_id,
    p.pipe_number,
    p.is_good_quality,
    sg.grade_name,
    p.length_meters,
    p.diameter_mm,
    p.weight_kg,
    pk.package_number,
    pk.package_date
FROM
    "pipe_Information".pipes p
JOIN
    "pipe_Information".steel_grades sg ON p.steel_grade_id = sg.grade_id
LEFT JOIN
    "pipe_Information".packages pk ON p.package_id = pk.package_id
ORDER BY
    p.pipe_id;
    

select 
  sg.grade_id,
  sg.grade_name
from "pipe_Information".steel_grades sg;
  
select  
  p.package_id,
  p.package_number,
  p.package_date
from "pipe_Information".packages p;
```
