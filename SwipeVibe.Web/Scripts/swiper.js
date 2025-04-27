/**
 * SwipeVibe - обработчик свайпов
 * Обрабатывает жесты свайпа для переключения между видео
 */

// Ждем, когда DOM полностью загрузится
document.addEventListener('DOMContentLoaded', () => {
    console.log('Swiper initialized');
<<<<<<< HEAD:js/swiper.js
    
    // Инициализация свайпера
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
    initSwiper();
});

/**
 * Инициализация обработчика свайпов
 */
function initSwiper() {
    const videoContainer = document.querySelector('.video-container');
    let touchStartY = 0;
    let touchEndY = 0;
    let touchStartX = 0;
    let touchEndX = 0;
<<<<<<< HEAD:js/swiper.js
    const minSwipeDistance = 100; // Минимальное расстояние свайпа в пикселях
    
    // Обработчик начала касания
=======
    const minSwipeDistance = 100; 
    
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
    videoContainer.addEventListener('touchstart', (e) => {
        touchStartY = e.changedTouches[0].screenY;
        touchStartX = e.changedTouches[0].screenX;
    });
    
    // Обработчик окончания касания
    videoContainer.addEventListener('touchend', (e) => {
        touchEndY = e.changedTouches[0].screenY;
        touchEndX = e.changedTouches[0].screenX;
        
<<<<<<< HEAD:js/swiper.js
        // Вычисляем расстояние свайпа
        const swipeDistanceY = touchEndY - touchStartY;
        const swipeDistanceX = touchEndX - touchStartX;
        
        // Определяем направление свайпа (вертикальное или горизонтальное)
=======
        const swipeDistanceY = touchEndY - touchStartY;
        const swipeDistanceX = touchEndX - touchStartX;
        
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
        if (Math.abs(swipeDistanceY) > Math.abs(swipeDistanceX)) {
            // Вертикальный свайп
            handleVerticalSwipe(swipeDistanceY);
        } else {
            // Горизонтальный свайп
            handleHorizontalSwipe(swipeDistanceX);
        }
    });
    
    // Добавляем поддержку мыши для десктопов
    videoContainer.addEventListener('mousedown', (e) => {
        e.preventDefault();
        touchStartY = e.clientY;
        touchStartX = e.clientX;
        
        const handleMouseUp = (e) => {
            touchEndY = e.clientY;
            touchEndX = e.clientX;
<<<<<<< HEAD:js/swiper.js
            
            // Вычисляем расстояние свайпа
            const swipeDistanceY = touchEndY - touchStartY;
            const swipeDistanceX = touchEndX - touchStartX;
            
            // Определяем направление свайпа
=======
           
            const swipeDistanceY = touchEndY - touchStartY;
            const swipeDistanceX = touchEndX - touchStartX;
            
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
            if (Math.abs(swipeDistanceY) > Math.abs(swipeDistanceX)) {
                // Вертикальный свайп
                handleVerticalSwipe(swipeDistanceY);
            } else {
                // Горизонтальный свайп
                handleHorizontalSwipe(swipeDistanceX);
            }
            
<<<<<<< HEAD:js/swiper.js
            // Удаляем временные обработчики
            document.removeEventListener('mouseup', handleMouseUp);
        };
        
        // Добавляем временный обработчик отпускания мыши
=======
            document.removeEventListener('mouseup', handleMouseUp);
        };
        
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
        document.addEventListener('mouseup', handleMouseUp);
    });
    
    // Добавляем обработчик колесика мыши
    videoContainer.addEventListener('wheel', (e) => {
        e.preventDefault();
        
<<<<<<< HEAD:js/swiper.js
        // Если прокрутка вниз - показываем следующее видео
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
        if (e.deltaY > 0) {
            showNextVideo();
        } 
        // Если прокрутка вверх - показываем предыдущее видео
        else if (e.deltaY < 0) {
            showPrevVideo();
        }
    }, { passive: false });
    
<<<<<<< HEAD:js/swiper.js
    // Добавляем обработчики клавиш для навигации
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowUp') {
            showPrevVideo();
        } else if (e.key === 'ArrowDown') {
            showNextVideo();
        }
    });
}
<<<<<<< HEAD:js/swiper.js

/**
 * Обработка вертикального свайпа
 * @param {number} swipeDistance - Расстояние свайпа (положительное - вниз, отрицательное - вверх)
 */
function handleVerticalSwipe(swipeDistance) {
    const minSwipeDistance = 100;
    
    // Проверяем, достаточное ли расстояние для свайпа
=======
function handleVerticalSwipe(swipeDistance) {
    const minSwipeDistance = 100;
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
    if (Math.abs(swipeDistance) < minSwipeDistance) {
        return;
    }
    
    if (swipeDistance < 0) {
        // Свайп вверх - переход к следующему видео
        showNextVideo();
    } else {
        // Свайп вниз - переход к предыдущему видео
        showPrevVideo();
    }
}
<<<<<<< HEAD:js/swiper.js

/**
 * Обработка горизонтального свайпа (для дополнительных функций)
 * @param {number} swipeDistance - Расстояние свайпа (положительное - вправо, отрицательное - влево)
 */
function handleHorizontalSwipe(swipeDistance) {
    const minSwipeDistance = 100;
    
    // Проверяем, достаточное ли расстояние для свайпа
=======
function handleHorizontalSwipe(swipeDistance) {
    const minSwipeDistance = 100;
    
>>>>>>> vadim:SwipeVibe.Web/Scripts/swiper.js
    if (Math.abs(swipeDistance) < minSwipeDistance) {
        return;
    }
    
    if (swipeDistance < 0) {
        // Свайп влево - например, лайк
        const likeButton = document.querySelector('.video-card.active .like-btn');
        if (likeButton) {
            likeButton.click();
        }
    } else {
        // Свайп вправо - например, комментарий
        const commentButton = document.querySelector('.video-card.active .comment-btn');
        if (commentButton) {
            alert('Комментарии будут доступны в следующем обновлении');
        }
    }
} 