$(document).ready(function() {
    // Инициализация интерфейса стримов
    console.log('Stream page initialized');
    
    // Обработчик нажатия на кнопку "Начать стрим"
    $('.start-stream-section .btn').on('click', function(e) {
        e.preventDefault();
        showStreamModal();
    });
    
    // Функция для отображения модального окна создания стрима
    function showStreamModal() {
        // Здесь будет логика отображения модального окна
        alert('Функция создания стрима будет доступна в ближайшее время!');
    }
    
    // Обновление счетчика зрителей в реальном времени
    function updateViewerCounts() {
        // Имитация обновления счетчиков в реальном времени
        // В реальном приложении здесь будет запрос к API
        setTimeout(function() {
            $('.stream-card.live').each(function() {
                var $viewers = $(this).find('.stream-viewers');
                var currentCount = parseInt($viewers.text());
                var newCount = currentCount + Math.floor(Math.random() * 10) - 5;
                if (newCount < 0) newCount = 0;
                $viewers.text(newCount + ' зрителей');
            });
            
            updateViewerCounts();
        }, 5000);
    }
    
    // Запуск обновления счетчиков
    updateViewerCounts();
}); 