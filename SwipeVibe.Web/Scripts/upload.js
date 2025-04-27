/**
 * Скрипт для обработки загрузки видео 
 */

document.addEventListener('DOMContentLoaded', function () {
    // Получаем все необходимые элементы
    const dropzone = document.getElementById('dropzone');
    const videoUpload = document.getElementById('video-upload');
    const videoPreview = document.getElementById('video-preview');
    const videoPreviewContainer = document.querySelector('.video-preview-container');
    const submitBtn = document.querySelector('.submit-btn');
    const removeVideoBtn = document.getElementById('remove-video');
    const trimVideoBtn = document.getElementById('trim-video');
    const uploadProgress = document.querySelector('.upload-progress');
    const progressBar = document.querySelector('.progress-bar');
    const progressPercentage = document.querySelector('.progress-percentage');
    const videoTitle = document.getElementById('video-title');
    const videoDescription = document.getElementById('video-description');
    const videoTags = document.getElementById('video-tags');
    const categoryBtns = document.querySelectorAll('.category-btn');
    const cancelBtn = document.querySelector('.cancel-btn');

    // Максимальный размер файла (в байтах) - 50MB
    const MAX_FILE_SIZE = 52428800;

    // Инициализация перетаскивания файлов
    initDragAndDrop();

    // Инициализация обработчиков событий
    initEventListeners();

    // Функция для инициализации перетаскивания файлов
    function initDragAndDrop() {
        dropzone.addEventListener('dragover', function (e) {
            e.preventDefault();
            dropzone.classList.add('drag-over');
        });

        dropzone.addEventListener('dragleave', function () {
            dropzone.classList.remove('drag-over');
        });

        dropzone.addEventListener('drop', function (e) {
            e.preventDefault();
            dropzone.classList.remove('drag-over');

            if (e.dataTransfer.files.length) {
                handleVideoFile(e.dataTransfer.files[0]);
            }
        });
    }

    // Функция для инициализации обработчиков событий
    function initEventListeners() {
        // Обработка выбора файла через инпут
        videoUpload.addEventListener('change', function () {
            if (this.files.length) {
                handleVideoFile(this.files[0]);
            }
        });

        // Обработка удаления видео
        removeVideoBtn.addEventListener('click', function () {
            resetUpload();
        });

        // Обработка обрезки видео (пока просто уведомление)
        trimVideoBtn.addEventListener('click', function () {
            showToast('Функция обрезки видео в разработке', 'info');
        });

        // Проверка заполнения формы для активации кнопки отправки
        videoTitle.addEventListener('input', validateForm);

        // Обработка нажатия кнопки публикации
        submitBtn.addEventListener('click', function (e) {
            if (!submitBtn.disabled) {
                simulateUpload();
            }
        });

        // Обработка нажатия кнопки отмены
        cancelBtn.addEventListener('click', function () {
            if (confirm('Вы уверены, что хотите отменить загрузку? Все внесенные данные будут потеряны.')) {
                resetUpload();
                videoTitle.value = '';
                videoDescription.value = '';
                videoTags.value = '';

                // Сбрасываем активную категорию на первую
                categoryBtns.forEach((btn, index) => {
                    btn.classList.toggle('active', index === 0);
                });

                // Сбрасываем выбор доступа на "Публичное"
                document.querySelector('input[name="access"][value="public"]').checked = true;
            }
        });

        // Обработка выбора категорий
        categoryBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                categoryBtns.forEach(b => b.classList.remove('active'));
                this.classList.add('active');
            });
        });
    }

    // Функция для обработки видео-файла
    function handleVideoFile(file) {
        // Проверяем тип файла
        if (!file.type.startsWith('video/')) {
            showToast('Пожалуйста, выберите видеофайл', 'error');
            return;
        }

        // Проверяем размер файла
        if (file.size > MAX_FILE_SIZE) {
            showToast(`Размер файла превышает максимально допустимый (${MAX_FILE_SIZE / 1048576}МБ)`, 'error');
            return;
        }

        // Создаем URL для предпросмотра видео
        const videoURL = URL.createObjectURL(file);
        videoPreview.src = videoURL;

        // Показываем предпросмотр и скрываем зону перетаскивания
        dropzone.style.display = 'none';
        videoPreviewContainer.style.display = 'flex';

        // Активируем кнопку отправки если есть название
        validateForm();

        // Обработчик события загрузки видео для проверки длительности
        videoPreview.onloadedmetadata = function () {
            // Проверяем длительность видео (максимум 3 минуты = 180 секунд)
            if (videoPreview.duration > 180) {
                showToast('Длительность видео не должна превышать 3 минуты', 'warning');
            }
        };
    }

    // Функция для сброса загрузки
    function resetUpload() {
        // Сбрасываем видео и показываем зону перетаскивания
        videoPreview.src = '';
        videoPreviewContainer.style.display = 'none';
        dropzone.style.display = 'flex';
        videoUpload.value = '';

        // Скрываем прогресс загрузки
        uploadProgress.style.display = 'none';
        progressBar.style.width = '0%';

        // Деактивируем кнопку отправки
        submitBtn.disabled = true;
    }

    // Функция для валидации формы
    function validateForm() {
        // Активируем кнопку отправки только если есть видео и название
        if (videoPreview.src && videoTitle.value.trim() !== '') {
            submitBtn.disabled = false;
        } else {
            submitBtn.disabled = true;
        }
    }

    // Функция для симуляции загрузки видео (для демонстрации)
    function simulateUpload() {
        // Показываем прогресс загрузки
        uploadProgress.style.display = 'block';

        // Блокируем кнопку отправки во время загрузки
        submitBtn.disabled = true;

        let progress = 0;
        const interval = setInterval(() => {
            progress += Math.random() * 10;
            if (progress >= 100) {
                progress = 100;
                clearInterval(interval);

                // После завершения загрузки показываем сообщение и перенаправляем на главную
                setTimeout(() => {
                    showToast('Видео успешно загружено!', 'success');
                    setTimeout(() => {
                        window.location.href = 'index.html';
                    }, 2000);
                }, 500);
            }

            // Обновляем прогресс
            progressBar.style.width = `${progress}%`;
            progressPercentage.textContent = `${Math.round(progress)}%`;
        }, 500);
    }

    // Функция для отображения всплывающего уведомления
    function showToast(message, type = 'info') {
        // Создаем элемент toast
        const toast = document.createElement('div');
        toast.className = `toast-notification ${type}`;

        // Добавляем иконку в зависимости от типа
        let icon = '';
        switch (type) {
            case 'success':
                icon = '<i class="fas fa-check-circle"></i>';
                break;
            case 'error':
                icon = '<i class="fas fa-exclamation-circle"></i>';
                break;
            case 'warning':
                icon = '<i class="fas fa-exclamation-triangle"></i>';
                break;
            default:
                icon = '<i class="fas fa-info-circle"></i>';
        }

        // Добавляем содержимое toast
        toast.innerHTML = `
            ${icon}
            <span>${message}</span>
        `;

        // Добавляем toast на страницу
        document.body.appendChild(toast);

        // Добавляем класс для анимации появления
        setTimeout(() => {
            toast.classList.add('show');
        }, 10);

        // Удаляем toast через 3 секунды
        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => {
                document.body.removeChild(toast);
            }, 300);
        }, 3000);
    }
}); 