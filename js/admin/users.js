// Инициализация таблицы пользователей
$(document).ready(function() {

    const usersTable = $('#usersTable').DataTable({
        "paging": false,
        "searching": false,
        "info": false,
        "ordering": true,
        "language": {
            "emptyTable": "Нет доступных данных",
            "zeroRecords": "Нет совпадений"
        }
    });
    
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    $('.select-all-checkbox').on('change', function() {
        const isChecked = $(this).prop('checked');
        $('.user-checkbox').prop('checked', isChecked);
    });
    
    $('.add-btn').on('click', function() {
        $('#userModalLabel').text('Добавить пользователя');
        $('#userForm')[0].reset();
        $('#userModal').modal('show');
    });
    
    $('.edit-btn').on('click', function() {
        const row = $(this).closest('tr');
        const userId = row.find('td:eq(1)').text();
        const userName = row.find('.user-name').text();
        const userEmail = row.find('td:eq(3)').text();
        
        $('#userModalLabel').text('Редактировать пользователя');
        $('#userName').val(userName);
        $('#userEmail').val(userEmail);
        
        $('#userPassword').val(''); // Не отображаем пароль
        $('#userBio').val('Био пользователя ' + userName);
        
        $('#userModal').modal('show');
    });
    
    $('.view-btn').on('click', function() {
        const row = $(this).closest('tr');
        const userId = row.find('td:eq(1)').text();
        const userName = row.find('.user-name').text();
        
        alert('Просмотр пользователя: ' + userName + ' (ID: ' + userId + ')');
    });
    
    $('.delete-btn').on('click', function() {
        const row = $(this).closest('tr');
        const userId = row.find('td:eq(1)').text();
        const userName = row.find('.user-name').text();
        
        if (confirm('Вы уверены, что хотите удалить пользователя "' + userName + '"?')) {
            row.fadeOut(300, function() {
                row.remove();
            });
        }
    });
    
    $('.save-user').on('click', function() {
        const form = $('#userForm');
        
        if (form[0].checkValidity() === false) {
            form.addClass('was-validated');
            return;
        }
        
        const userName = $('#userName').val();
        const userEmail = $('#userEmail').val();
        const userRole = $('#userRole').val();
        const userStatus = $('#userStatus').val();
        
        console.log('Сохранение пользователя:', {
            name: userName,
            email: userEmail,
            role: userRole,
            status: userStatus
        });
        
        $('#userModal').modal('hide');
        
        showNotification('Пользователь успешно сохранен');
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
        
        // Обработка клика на опцию экспорта
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
});

function showNotification(message) {
    const notification = $('<div class="notification"></div>').text(message);
    
    notification.css({
        position: 'fixed',
        bottom: '20px',
        right: '20px',
        background: 'var(--accent-primary)',
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