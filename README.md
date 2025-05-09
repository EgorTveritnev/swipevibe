# SwipeVibe

![SwipeVibe Logo](assets/images/logo.png)

## Описание

SwipeVibe - это современное веб-приложение для обмена короткими видео, вдохновленное TikTok и Instagram Reels. Приложение создано в стиле неоморфизма с плавными градиентами и анимациями, полностью адаптивно под мобильные и десктопные устройства.

## Особенности

- **Уникальный дизайн**: Неоморфические элементы интерфейса, градиентные акценты и плавные анимации
- **Адаптивность**: Полностью адаптивный дизайн для всех размеров экранов
- **Интуитивная навигация**: Вертикальный свайп для переключения между видео
- **Сообщество**: Возможность подписки на авторов, общение с друзьями, комментирование видео
- **Магазин**: Встроенный раздел для покупки товаров
- **Трансляции**: Возможность просмотра и создания прямых трансляций
- **Расширенный профиль**: Детальные страницы профиля с отображением активности, видео и статистики

## Технологии

- HTML5
- CSS3 (с использованием современных функций, таких как переменные, флексбокс, гридлайоут)
- JavaScript (ES6+)
- Bootstrap 5
- Font Awesome 6

## Структура проекта

```
SwipeVibe/
├── index.html              # Главная страница с рекомендациями
├── watch.html              # Страница просмотра видео
├── subscriptions.html      # Страница подписок
├── friends.html            # Страница друзей
├── activity.html           # Страница активности
├── stream.html             # Страница трансляций
├── shop.html               # Страница магазина
├── upload.html             # Страница загрузки видео
├── about.html              # Страница о нас
├── contacts.html           # Страница контактов
├── policy.html             # Страница политики
├── login.html              # Страница входа
├── register.html           # Страница регистрации
├── admin.html              # Панель администратора
├── 404.html                # Страница ошибки 404
├── css/                    # Стили CSS
│   ├── theme.css           # Основные темы и цвета
│   ├── style.css           # Основные стили
│   ├── animations.css      # Анимации
│   ├── neomorphism.css     # Стили неоморфизма
│   ├── sidebar.css         # Стили боковой панели
│   ├── toast.css           # Стили уведомлений
│   ├── watch.css           # Стили для страницы просмотра
│   ├── subscriptions.css   # Стили для страницы подписок
│   ├── friends.css         # Стили для страницы друзей
│   ├── activity.css        # Стили для страницы активности
│   ├── shop.css            # Стили для страницы магазина
│   ├── upload.css          # Стили для страницы загрузки
│   ├── auth.css            # Стили для страниц авторизации
│   ├── admin.css           # Стили для панели администратора
│   └── error.css           # Стили для страницы ошибки
├── js/                     # JavaScript
│   ├── app.js              # Основной скрипт приложения
│   ├── swiper.js           # Логика свайпов
│   ├── animations.js       # Логика анимаций
│   ├── watch.js            # Скрипты для страницы просмотра
│   ├── subscriptions.js    # Скрипты для страницы подписок
│   ├── friends.js          # Скрипты для страницы друзей
│   ├── activity.js         # Скрипты для страницы активности
│   ├── shop.js             # Скрипты для страницы магазина
│   ├── upload.js           # Скрипты для страницы загрузки
│   ├── auth.js             # Скрипты для страниц авторизации
│   ├── admin.js            # Скрипты для панели администратора
│   └── error.js            # Скрипты для страницы ошибки
├── assets/                 # Ресурсы
│   ├── images/             # Изображения
│   ├── videos/             # Примеры видео
│   └── icons/              # Иконки
└── pages/                  # Дополнительные страницы
    ├── profile.html        # Страница профиля пользователя
    └── category.html       # Страница категории
```

## Установка и использование

1. Клонируйте репозиторий:
```
git clone https://github.com/username/swipevibe.git
```

2. Перейдите в директорию проекта:
```
cd swipevibe
```

3. **Важно**: Для полноценной работы приложения вы можете использовать демо видео и фото из папки `assets` или добавить собственные:
   - Видео-файлы должны быть в формате MP4
   - Изображения рекомендуется подготовить в формате JPG или PNG

4. Откройте `index.html` в вашем веб-браузере.

5. Наслаждайтесь использованием SwipeVibe!

## Навигация по приложению

- **Главная страница**: Здесь отображаются рекомендованные видео, которые можно просматривать, свайпая вверх или вниз.
- **Смотреть**: Исследуйте новые видео из разных категорий.
- **Подписки**: Следите за обновлениями от авторов, на которых вы подписаны.
- **Друзья**: Управляйте списком друзей и просматривайте их контент.
- **Активность**: Отслеживайте активность в приложении, комментарии, лайки и т.д.
- **Магазин**: Просматривайте и покупайте товары, связанные с контентом.
- **Трансляция**: Смотрите и создавайте прямые трансляции.
- **Профиль**: Управляйте своим профилем, просматривайте статистику и настройки.

## Функциональность

- **Просмотр видео**: Плавная навигация между видео, возможность лайкать, комментировать и делиться.
- **Подписки и друзья**: Управление социальными связями внутри приложения.
- **Загрузка контента**: Удобный интерфейс для загрузки собственных видео.
- **Поиск и категории**: Возможность находить контент по категориям и темам.
- **Аутентификация**: Система входа и регистрации пользователей.
- **Администрирование**: Панель управления для администраторов сайта.

## Запланированные улучшения

- Интеграция с реальным бэкендом для сохранения и загрузки видео
- Расширенные возможности редактирования видео
- Система монетизации для авторов контента
- Интеграция с социальными сетями
- Поддержка различных языков

## Функции

- **Уникальный дизайн** — неоморфный дизайн с градиентами и эффектами, создающий современный и стильный интерфейс.
- **Адаптивность** — приложение работает как на мобильных устройствах, так и на десктопных экранах.
- **Интуитивная навигация** — вертикальный свайп для просмотра видео, имитирующий поведение TikTok.
- **Плавные анимации** — анимации для всех интерактивных элементов.
- **Интерактивные элементы** — кнопки лайков, комментариев, закладок и шеринга с визуальной обратной связью.
- **Страница профиля** — отображение информации о пользователе.
- **Страница поиска** — возможность искать контент и просматривать популярные темы.
- **Страница загрузки** — интерфейс для загрузки новых видео.
- **Кнопка подписки** — возможность подписаться на авторов видео.
- **Уведомления** — всплывающие уведомления (тосты) для обратной связи с пользователем.
- **Полноэкранное видео** — видео занимает всю область просмотра для максимального погружения.
- **Шопинг** — возможность приобретать товары, связанные с контентом.
- **Стриминг** — функционал для просмотра и создания прямых трансляций.
- **Система друзей** — возможность добавлять друзей и следить за их активностью. 