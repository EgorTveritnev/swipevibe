// Инициализация страницы видео
$(document).ready(function() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    $('.upload-btn').on('click', function() {
        $('#videoModalLabel').text('Загрузить видео');
        $('#videoForm')[0].reset();
        $('.upload-preview').addClass('d-none');
        $('.upload-placeholder').removeClass('d-none');
        $('#videoModal').modal('show');
    });
    
    $('.select-video-btn').on('click', function() {
        $('#videoFile').click();
    });
    
    $('#videoFile').on('change', function(e) {
        if (e.target.files.length > 0) {
            const file = e.target.files[0];
            const videoURL = URL.createObjectURL(file);
            
            $('.upload-preview video source').attr('src', videoURL);
            $('.upload-preview video')[0].load();
            
            $('.upload-placeholder').addClass('d-none');
            $('.upload-preview').removeClass('d-none');
        }
    });
    
    $('.remove-video-btn').on('click', function() {
        $('.upload-preview').addClass('d-none');
        $('.upload-placeholder').removeClass('d-none');
        $('#videoFile').val('');
    });
    
    $('#videoStatus').on('change', function() {
        if ($(this).val() === 'scheduled') {
            $('#scheduleContainer').removeClass('d-none');
        } else {
            $('#scheduleContainer').addClass('d-none');
        }
    });
    
    $('.save-video').on('click', function() {
        const form = $('#videoForm');
        
        if (form[0].checkValidity() === false) {
            form.addClass('was-validated');
            return;
        }
        
        const videoTitle = $('#videoTitle').val();
        const videoDescription = $('#videoDescription').val();
        const videoCategory = $('#videoCategory').val();
        const videoTags = $('#videoTags').val();
        
        console.log('Сохранение видео:', {
            title: videoTitle,
            description: videoDescription,
            category: videoCategory,
            tags: videoTags.split(',').map(tag => tag.trim())
        });
        
        $('#videoModal').modal('hide');
        
        showNotification('Видео успешно сохранено');
    });
    
    $('.edit-btn').on('click', function() {
        const card = $(this).closest('.video-card');
        const videoTitle = card.find('.video-title').text();
        const videoAuthor = card.find('.video-author').text();
        
        $('#videoModalLabel').text('Редактировать видео');
        $('#videoTitle').val(videoTitle);
        
        $('#videoDescription').val('Описание видео ' + videoTitle);
        
        const previewSrc = card.find('.video-preview img').attr('src');
        $('.upload-placeholder').addClass('d-none');
        $('.upload-preview').removeClass('d-none');
        
        $('#videoModal').modal('show');
    });
    
    $('.play-btn').on('click', function() {
        const card = $(this).closest('.video-card');
        const videoTitle = card.find('.video-title').text();
        const videoAuthor = card.find('.video-author').text();
        
        $('#modalVideoTitle').text(videoTitle);
        $('#modalVideoAuthor').text(videoAuthor);
        $('#modalVideoDate').text('Опубликовано: 15.05.2023');
        $('#modalVideoDescription').text('Описание видео ' + videoTitle);
        
        const tagsList = card.find('.video-tags').clone();
        $('#modalVideoTags').empty().append(tagsList);
        
        $('#playVideoModal').modal('show');
    });
    
    $('.delete-btn').on('click', function() {
        const card = $(this).closest('.video-card');
        const videoTitle = card.find('.video-title').text();
        
        if (confirm('Вы уверены, что хотите удалить видео "' + videoTitle + '"?')) {
            card.fadeOut(300, function() {
                card.remove();
                showNotification('Видео успешно удалено');
            });
        }
    });
    
    $('.toggle-filters').on('click', function() {
        $('.filters-body').slideToggle(300);
        $(this).find('i').toggleClass('fa-chevron-down fa-chevron-up');
    });
    
    $('.apply-filters').on('click', function() {
        showNotification('Фильтры применены');
    });
    
    $('.clear-filters').on('click', function() {
        $('.filter-group select').val('');
        showNotification('Фильтры сброшены');
    });
    
    $('.export-btn').on('click', function() {
        const exportOptions = ['Excel', 'CSV', 'PDF'];
        
        const exportMenu = $('<div class="export-menu"></div>');
        exportOptions.forEach(option => {
            exportMenu.append(`<div class="export-option" data-format="${option.toLowerCase()}">${option}</div>`);
        });
        
        exportMenu.css({
            position: 'absolute',
            top: $(this).offset().top + $(this).outerHeight(),
            left: $(this).offset().left,
            background: 'var(--bg-secondary)',
            borderRadius: '8px',
            padding: '8px 0',
            boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
            zIndex: 1000
        });
        
        $('body').append(exportMenu);
        
        $('.export-option').on('click', function() {
            const format = $(this).data('format');
            showNotification(`Экспорт в ${format.toUpperCase()} выполнен`);
            exportMenu.remove();
        });
        
        $(document).on('click', function(e) {
            if (!$(e.target).closest('.export-menu').length && !$(e.target).closest('.export-btn').length) {
                exportMenu.remove();
            }
        });
    });
    
    $('.header-search input').on('input', function() {
        const searchTerm = $(this).val().toLowerCase();
        
        if (searchTerm.length >= 3) {
            setTimeout(() => {
                showNotification(`Поиск по запросу "${searchTerm}"`);
            }, 500);
        }
    });
    
    $('.pagination .page-link').on('click', function(e) {
        e.preventDefault();
        
        $('.pagination .page-item').removeClass('active');
        $(this).parent().addClass('active');
        
        const page = $(this).text();
        if (!isNaN(page)) {
            showNotification(`Переход на страницу ${page}`);
        }
    });
    
    $('.items-per-page select').on('change', function() {
        const perPage = $(this).val();
        showNotification(`Показывать по ${perPage} записей`);
    });
    
    const dropArea = $('.upload-placeholder');
    
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropArea[0].addEventListener(eventName, preventDefaults, false);
    });
    
    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }
    
    ['dragenter', 'dragover'].forEach(eventName => {
        dropArea[0].addEventListener(eventName, highlight, false);
    });
    
    ['dragleave', 'drop'].forEach(eventName => {
        dropArea[0].addEventListener(eventName, unhighlight, false);
    });
    
    function highlight() {
        dropArea.addClass('drag-active');
    }
    
    function unhighlight() {
        dropArea.removeClass('drag-active');
    }
    
    dropArea[0].addEventListener('drop', handleDrop, false);
    
    function handleDrop(e) {
        const dt = e.dataTransfer;
        const files = dt.files;
        
        if (files.length > 0 && files[0].type.startsWith('video/')) {
            $('#videoFile')[0].files = files;
            $('#videoFile').trigger('change');
        } else {
            showNotification('Пожалуйста, загрузите видеофайл', 'error');
        }
    }
});

function showNotification(message, type = 'success') {
    const notification = $('<div class="notification"></div>').text(message);
    
    let bgColor = 'var(--accent-primary)';
    if (type === 'error') {
        bgColor = '#e74c3c';
    } else if (type === 'warning') {
        bgColor = '#f39c12';
    }
    
    notification.css({
        position: 'fixed',
        bottom: '20px',
        right: '20px',
        background: bgColor,
        color: 'white',
        padding: '10px 20px',
        borderRadius: '8px',
        boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
        zIndex: 9999,
        opacity: 0
    });
    
    $('body').append(notification);
    
    notification.animate({ opacity: 1, bottom: '30px' }, 300);
    
    setTimeout(() => {
        notification.animate({ opacity: 0, bottom: '20px' }, 300, function() {
            notification.remove();
        });
    }, 3000);
} 