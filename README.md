# TorchMaster

## Цели проекта
Основная цель проекта - протестировать комплексный архитектурных подход основанный на паттернах.

Вторичные цели:
- Реализовать механику подбора предметов
- Реализовать механику инвентаря
- Реализовать механику боя, в том числе Enchants оружия
- Реализовать механику процедурной генерации подземелья

## Описание

Почувствуйте себя рыцарем оказавшимся в мрачном подземелье, полном опасностей. 
Единственный способ выбраться 
— исследовать лабиринт, избегать призраков и поддерживать рассудок при помощи света.

Факелы — единственный источник света, но они быстро сгорают. 
Можно носить несколько факелов и зажигать костры, чтобы защитить свой рассудок.

Безумие — постепенно растёт в темноте. 
Если достигнет предела, игра окончена.

Призраки — бродят по лабиринту и атакуют героя. 
Если у вас есть факел — вы его роняете. Если нет — рассудок падает.

Флаконы — редкие предметы, восстанавливающие рассудок.

Головоломки — нажимные плиты открывают новые области, а двери заперты на ключи, которые нужно найти.

Используйте свет, сохраняйте рассудок, избегайте призраков и найдите путь к свободе!

## Архитектура проекта

В проекте исплюзуется ряд паттернов для создания сущностей с параметрами и поведением обеспечивая обеспечивающие гибкость и расширяемость системы.
Основные компоненты системы:

#### I...Builder (Интерфейс строителя)
Определяет методы для установки свойств сущности в том числе систему команд, FSM и другие уникальные для каждой сущности параметры

#### A... || I... (Модель сущнсоти)
Содержит характеристики сущности

#### ...SO (Менеджер настроек сущностей)
Хранит параметры конфигурации для различных типов сущностей.

#### ...Director (Директор)
Отвечает за создание предметов, используя I...Builder. Вызывает методы установки параметров и собирает сущность.

#### ...Builder (Глобальный менеджер сущностей)
Singleton, управляющий созданием объектов в игре через ...Director.

#### ...CommandManager (Менеджер команд)
Реализует паттерн "Стратегия" (Strategy), управляя командами сущностей. 
Позволяет добавлять, подписываться и отписываться от команд через ...CommandType.

#### ...CollisionObserver && ...CollisionHandler (Обозреватель коллизий)
Использует паттерн "Наблюдатель" (Observer) для обработки столкновений. 
Позволяет гибко добавлять реакции на взаимодействия между сущностями.

## Выводы

Данная архитектура упрощает добавление новых типов объектов, настройку их поведения через FSM и систему команд.
Гибкая архитектура проекта, основанная на проверенных паттернах проектирования, 
позволяет легко расширять игру, добавлять новые механики и улучшать игровой процесс.